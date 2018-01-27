using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedUnits : ICommand
    {
        public string Name => nameof(FeedUnits);
        public int? Units { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.FeedUnits(Units));
        }
    }
}
