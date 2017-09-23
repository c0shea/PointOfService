using System.Text;
using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class Line : ICommand
    {
        [XmlAttribute]
        public Alignment Alignment { get; set; }

        [XmlAttribute]
        public bool IsBold { get; set; }

        [XmlAttribute]
        public bool IsUnderline { get; set; }

        [XmlAttribute]
        public bool IsItalic { get; set; }

        [XmlAttribute]
        public short? CharactersPerLine { get; set; }

        [XmlAttribute]
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

        public void Execute(PosPrinter printer)
        {
            if (CharactersPerLine.HasValue)
            {
                printer.RecLineChars = CharactersPerLine.Value;
            }

            printer.Print(ToString());
        }
    }
}
