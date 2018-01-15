using System.Text;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class Line : ICommand
    {
        public Alignment Alignment { get; set; }
        public bool IsBold { get; set; }
        public bool IsUnderline { get; set; }
        public bool IsItalic { get; set; }
        public short? CharactersPerLine { get; set; }
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

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            if (CharactersPerLine.HasValue)
            {
                if (station == PrinterStation.Receipt)
                {
                    printer.RecLineChars = CharactersPerLine.Value;
                }
                else if (station == PrinterStation.Slip)
                {
                    printer.SlpLineChars = CharactersPerLine.Value;
                }
            }
            
            if (station == PrinterStation.Slip)
            {
                printer.PrintNormal(station, Text);
            }
            else
            {
                printer.PrintNormal(station, ToString());
            }
        }
    }
}
