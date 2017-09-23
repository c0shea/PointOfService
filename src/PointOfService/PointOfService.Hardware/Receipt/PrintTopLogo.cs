using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class PrintTopLogo : ICommand
    {
        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.PrintTopLogo);
        }
    }
}
