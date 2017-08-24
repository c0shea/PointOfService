using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PointOfService;

namespace PointOfService.Hardware
{
    public enum Alignment
    {
        Center,
        Left,
        Right
    }

    public class Bitmap
    {
        public Alignment Alignment { get; set; }
        public string FileName { get; set; }
    }

    public class Barcode
    {
        public Alignment Alignment { get; set; }
        public BarCodeSymbology Symbology { get; set; }
        public string Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public BarCodeTextPosition TextPosition { get; set; }
    }

    public class Line
    {
        public Alignment Alignment { get; set; }
        public bool IsBold { get; set; }
        public bool IsUnderline { get; set; }
        public bool IsItalic { get; set; }
        public short CharactersPerLine { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder(Text?.Length ?? 10);

            switch (Alignment)
            {
                case Alignment.Center:
                    sb.Append(EscapeSequence.Center);
                    break;
                case Alignment.Left:
                    sb.Append(EscapeSequence.Left);
                    break;
                case Alignment.Right:
                    sb.Append(EscapeSequence.Right);
                    break;
            }

            if (IsBold)
            {
                sb.Append(EscapeSequence.Bold());
            }

            if (IsUnderline)
            {
                sb.Append(EscapeSequence.Underline());
            }

            if (IsItalic)
            {
                sb.Append(EscapeSequence.Italic());
            }

            sb.AppendLine(Text);

            return sb.ToString();
        }
    }
    
    public static class EscapeSequence
    {
        private const char EscapeCharacter = (char)27;

        public static string Esc => $"{EscapeCharacter}|";
        public static string Normal => $"{Esc}N";

        public static string Bold(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}bC";
        public static string Underline(bool isEnabled = true, double? thickness = null) => $"{Esc}{(isEnabled ? "" : "!")}{thickness?.ToString() ?? ""}uC";
        public static string Italic(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}iC";

        public static string AlternateColor(double? color = null) => $"{Esc}{color?.ToString() ?? ""}rC";
        public static string ReverseVideo(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}rvC";
        public static string Shading(double? percentShade = null) => $"{Esc}{percentShade?.ToString() ?? ""}sC";

        public static string SingleHighAndWide => $"{Esc}1C";
        public static string DoubleWide => $"{Esc}2C";
        public static string DoubleHigh => $"{Esc}3C";
        public static string DoubleHighAndWide => $"{Esc}4C";

        public static string ScaleHorizontally(byte multiple) => $"{Esc}{multiple}hC";
        public static string ScaleVertically(byte multiple) => $"{Esc}{multiple}vC";

        public static string RgbColor(byte red, byte green, byte blue) => $"{Esc}{red:000}{green:000}{blue:000}fC";
        public static string Subscript(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}tbC";
        public static string Superscript(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}tpC";
        public static string Strikethrough(bool isEnabled = true, double? thickness = null) => $"{Esc}{(isEnabled ? "" : "!")}{thickness?.ToString() ?? ""}stC";

        public static string Center => $"{Esc}cA";
        public static string Right => $"{Esc}rA";
        public static string Left => $"{Esc}lA";
    }
}
