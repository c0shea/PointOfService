// Portions of the parsing code is taken from NLog and modified to fit the needs of this project
// https://github.com/NLog/NLog/blob/master/src/NLog/Layouts/LayoutParser.cs
// 
// Copyright (c) 2004-2017 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PointOfService.Hardware.Receipt.Template
{
    public static class RendererParser
    {
        public static List<Renderer> CompileRenderers(StringEnumerator se, bool isNested, out string text)
        {
            var renderers = new List<Renderer>();
            var literal = new StringBuilder();

            int character;
            int startPosition = se.Position;

            while ((character = se.Peek()) != -1)
            {
                if (isNested)
                {
                    // Possible escape char `\` 
                    if (character == '\\')
                    {
                        se.Pop();
                        var nextChar = se.Peek();

                        // Escape chars
                        if (IsEndOfRenderer(nextChar))
                        {
                            // Read next char and append
                            se.Pop();
                            literal.Append((char)nextChar);
                        }
                        else
                        {
                            // Don't treat `\` as escape char
                            literal.Append('\\');
                        }

                        continue;
                    }

                    if (IsEndOfRenderer(character))
                    {
                        // End of inner renderer. 
                        // `}` is when double nested inner renderer. 
                        // `:` when single nested renderer
                        break;
                    }
                }

                se.Pop();

                // Detect `${` (indicates a new renderer)
                if (character == '$' && se.Peek() == '{')
                {
                    AddLiteral(literal, renderers);

                    var newRenderer = ParseRenderer(se);

                    if (CanBeConvertedToLiteral(newRenderer))
                    {
                        newRenderer = ConvertToLiteral(newRenderer);
                    }

                    renderers.Add(newRenderer);
                }
                else
                {
                    literal.Append((char) character);
                }
            }

            AddLiteral(literal, renderers);

            var endPosition = se.Position;

            MergeLiterals(renderers);

            text = se.Substring(startPosition, endPosition);

            return renderers;
        }
        
        private static Renderer CreateRendererInstance(string name)
        {
            var type = GetRenderer(name);
            var instance = Activator.CreateInstance(type) as Renderer;

            return instance;
        }

        private static Type GetRenderer(string name)
        {
            var types = from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where t.IsDefined(typeof(RendererAttribute), false)
                        select t;

            var matchingAttributes = from t in types
                                     from a in t.GetCustomAttributes(typeof(RendererAttribute), false)
                                     where ((RendererAttribute)a).Name == name
                                     select t;

            return matchingAttributes.FirstOrDefault();
        }

        private static bool IsEndOfRenderer(int character) => character == '}' || character == ':';

        private static Renderer ParseRenderer(StringEnumerator se)
        {
            var character = se.Pop();

            var name = ParseRendererName(se);
            var renderer = CreateRendererInstance(name);

            character = se.Pop();

            while (character != -1 && character != '}')
            {
                var parameterName = ParseParameterName(se).Trim();

                if (se.Peek() == '=')
                {
                    // Skip the `=`
                    se.Pop();

                    var parameterValue = ParseParameterValue(se);
                    SetPropertyValue(renderer, parameterName, parameterValue);
                }

                character = se.Pop();
            }

            return renderer;
        }

        private static string ParseRendererName(StringEnumerator se)
        {
            var name = new StringBuilder();
            int character;

            while ((character = se.Peek()) != -1)
            {
                if (character == ':' || character == '}')
                {
                    break;
                }

                name.Append((char)character);
                se.Pop();
            }

            return name.ToString();
        }

        private static string ParseParameterName(StringEnumerator se)
        {
            var parameterName = new StringBuilder();
            int character;
            int nestLevel = 0;

            while ((character = se.Peek()) != -1)
            {
                if ((character == '=' || character == '}' || character == ':') && nestLevel == 0)
                {
                    break;
                }

                switch (character)
                {
                    case '$':
                        se.Pop();
                        parameterName.Append('$');

                        if (se.Peek() == '{')
                        {
                            parameterName.Append('{');
                            nestLevel++;
                            se.Pop();
                        }

                        continue;

                    case '}':
                        nestLevel--;
                        break;

                    case '\\':
                        // Skip the backslash
                        se.Pop();

                        // Append next character
                        parameterName.Append((char)se.Pop());
                        continue;
                }

                parameterName.Append((char)character);
                se.Pop();
            }

            return parameterName.ToString();
        }

        private static string ParseParameterValue(StringEnumerator se)
        {
            var value = new StringBuilder();
            int character;

            while ((character = se.Peek()) != -1)
            {
                if (character == ':' || character == '}')
                {
                    break;
                }

                // Code in this condition was replaced
                // to support escape codes e.g. '\r' '\n' '\u003a',
                // which can not be used directly as they are used as tokens by the parser
                // All escape codes listed in the following link were included
                // in addition to "\{", "\}", "\:" which are NLog specific:
                // http://blogs.msdn.com/b/csharpfaq/archive/2004/03/12/what-character-escape-sequences-are-available.aspx
                if (character == '\\')
                {
                    // Skip the backslash
                    se.Pop();

                    var nextChar = (char)se.Peek();

                    switch (nextChar)
                    {
                        case ':':
                        case '{':
                        case '}':
                        case '\'':
                        case '"':
                        case '\\':
                            se.Pop();
                            value.Append(nextChar);
                            break;

                        case '0':
                            se.Pop();
                            value.Append('\0');
                            break;

                        case 'a':
                            se.Pop();
                            value.Append('\a');
                            break;

                        case 'b':
                            se.Pop();
                            value.Append('\b');
                            break;

                        case 'f':
                            se.Pop();
                            value.Append('\f');
                            break;

                        case 'n':
                            se.Pop();
                            value.Append('\n');
                            break;

                        case 'r':
                            se.Pop();
                            value.Append('\r');
                            break;

                        case 't':
                            se.Pop();
                            value.Append('\t');
                            break;

                        case 'u':
                            se.Pop();
                            value.Append(GetUnicode(se, 4)); // 4 digits
                            break;

                        case 'U':
                            se.Pop();
                            value.Append(GetUnicode(se, 8)); // 8 digits
                            break;

                        case 'x':
                            se.Pop();
                            value.Append(GetUnicode(se, 4)); // 1-4 digits
                            break;

                        case 'v':
                            se.Pop();
                            value.Append('\v');
                            break;
                    }

                    continue;
                }

                value.Append((char)character);
                se.Pop();
            }

            return value.ToString();
        }

        private static char GetUnicode(StringEnumerator se, int maxDigits)
        {
            var code = 0;

            for (var count = 0; count < maxDigits; count++)
            {
                var digitCode = se.Peek();

                if (digitCode >= '0' && digitCode <= '9')
                {
                    digitCode = digitCode - '0';
                }
                else if (digitCode >= 'a' && digitCode <= 'f')
                {
                    digitCode = digitCode - 'a' + 10;
                }
                else if (digitCode >= 'A' && digitCode <= 'F')
                {
                    digitCode = digitCode - 'A' + 10;
                }
                else
                {
                    break;
                }

                se.Pop();
                code = code * 16 + digitCode;
            }

            return (char)code;
        }

        private static bool CanBeConvertedToLiteral(Renderer renderer)
        {
            return false;
        }

        private static Renderer ConvertToLiteral(Renderer renderer)
        {
            var sb = new StringBuilder();
            renderer.Append(sb);

            return new LiteralRenderer(sb.ToString());
        }

        private static void AddLiteral(StringBuilder sb, List<Renderer> renderers)
        {
            if (sb.Length > 0)
            {
                renderers.Add(new LiteralRenderer(sb.ToString()));
                sb.Length = 0;
            }
        }

        private static void MergeLiterals(List<Renderer> renderers)
        {
            for (var i = 0; i + 1 < renderers.Count;)
            {
                if (renderers[i] is LiteralRenderer left && renderers[i + 1] is LiteralRenderer right)
                {
                    left.Text += right.Text;
                    renderers.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }
        }

        private static void SetPropertyValue(object obj, string propertyName, string value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
                return;

            Type t = propertyInfo.PropertyType;
            object d;

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                d = string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, t.GetGenericArguments()[0]);
            }
            else if (t == typeof(Guid))
            {
                d = new Guid(value);
            }
            else
            {
                d = Convert.ChangeType(value, t);
            }

            propertyInfo.SetValue(obj, d, null);
        }
    }
}
