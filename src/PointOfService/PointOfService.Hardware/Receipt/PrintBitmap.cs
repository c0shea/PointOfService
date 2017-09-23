using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class PrintBitmap : ICommand
    {
        public short BitmapNumber { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.PrintBitmap(BitmapNumber));
        }
    }
}
