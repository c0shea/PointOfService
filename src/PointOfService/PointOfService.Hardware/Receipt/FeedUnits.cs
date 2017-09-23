using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedUnits : ICommand
    {
        public int? Units { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.FeedUnits(Units));
        }
    }
}
