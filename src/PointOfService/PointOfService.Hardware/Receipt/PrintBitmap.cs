using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class PrintBitmap : ICommand
    {
        public short BitmapNumber { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            if (!printer.CapRecBitmap && station == PrinterStation.Receipt || !printer.CapSlpBitmap && station == PrinterStation.Slip)
            {
                return;
            }

            printer.PrintNormal(station, EscapeSequence.PrintBitmap(BitmapNumber));
        }
    }
}
