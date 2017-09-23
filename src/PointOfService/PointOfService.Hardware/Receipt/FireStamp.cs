using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FireStamp : ICommand
    {
        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.FireStamp);
        }
    }
}
