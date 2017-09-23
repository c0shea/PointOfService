using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class PaperCut : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.PaperCut(PercentCut));
        }
    }
}
