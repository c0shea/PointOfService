using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class PrintBottomLogo : ICommand
    {
        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.PrintBottomLogo);
        }
    }
}
