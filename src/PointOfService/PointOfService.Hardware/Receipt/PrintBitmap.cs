using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class PrintBitmap : ICommand
    {
        public short BitmapNumber { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintBitmap(BitmapNumber));
        }
    }
}
