using System.IO;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class Bitmap : ICommand
    {
        public string Name => nameof(Bitmap);
        public Alignment Alignment { get; set; }
        public string FileName { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            if (!printer.CapRecBitmap && station == PrinterStation.Receipt || !printer.CapSlpBitmap && station == PrinterStation.Slip)
            {
                return;
            }

            if (!File.Exists(FileName))
            {
                return;
            }

            var alignment = 0;

            switch (Alignment)
            {
                case Alignment.Center:
                    alignment = PosPrinter.PrinterBitmapCenter;
                    break;
                case Alignment.Left:
                    alignment = PosPrinter.PrinterBitmapLeft;
                    break;
                case Alignment.Right:
                    alignment = PosPrinter.PrinterBitmapRight;
                    break;
            }

            printer.PrintBitmap(PrinterStation.Receipt, FileName, PosPrinter.PrinterBitmapAsIs, alignment);
        }
    }
}
