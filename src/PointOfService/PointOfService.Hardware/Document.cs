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
        Left,
        Center,
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

        public void Execute(PosPrinter printer)
        {
            if (CharactersPerLine.HasValue)
            {
                printer.RecLineChars = CharactersPerLine.Value;
            }

            printer.Print(ToString());
        }
    }

    public interface ICommand
    {
        void Execute(PosPrinter printer);
    }

    public static class CommandExtensions
    {
        public static void Print(this PosPrinter printer, string command)
        {
            printer.PrintNormal(PrinterStation.Receipt, command);
        }
    }

    public class PaperCut : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PaperCut(PercentCut));
        }
    }

    public class FeedAndPaperCut : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedAndPaperCut(PercentCut));
        }
    }

    public class FeedCutAndStamp : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedCutAndStamp(PercentCut));
        }
    }

    public class FireStamp : ICommand
    {
        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FireStamp);
        }
    }

    public class PrintBitmap : ICommand
    {
        public short BitmapNumber { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintBitmap(BitmapNumber));
        }
    }

    public class PrintTopLogo : ICommand
    {
        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintTopLogo);
        }
    }

    public class PrintBottomLogo : ICommand
    {
        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintBottomLogo);
        }
    }

    public class FeedLines : ICommand
    {
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedLines(Lines));
        }
    }

    public class FeedUnits : ICommand
    {
        public int? Units { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedUnits(Units));
        }
    }

    public class FeedReverse : ICommand
    {
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedReverse(Lines));
        }
    }
}
