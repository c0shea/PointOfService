using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class Barcode : ICommand
    {
        public Alignment Alignment { get; set; }
        public BarCodeSymbology Symbology { get; set; }
        public string Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public BarCodeTextPosition TextPosition { get; set; }

        public void Execute(PosPrinter printer)
        {
            var alignment = 0;

            switch (Alignment)
            {
                case Alignment.Center:
                    alignment = PosPrinter.PrinterBarCodeCenter;
                    break;
                case Alignment.Left:
                    alignment = PosPrinter.PrinterBarCodeLeft;
                    break;
                case Alignment.Right:
                    alignment = PosPrinter.PrinterBarCodeRight;
                    break;
            }

            printer.PrintBarCode(PrinterStation.Receipt, Data, Symbology, Height, Width, alignment, TextPosition);
        }
    }
}
