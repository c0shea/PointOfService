using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedAndPaperCut : ICommand
    {
        public string Name => nameof(FeedAndPaperCut);
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            if (!printer.CapRecPaperCut && station == PrinterStation.Receipt)
            {
                return;
            }

            printer.PrintNormal(station, EscapeSequence.FeedAndPaperCut(PercentCut));
        }
    }
}
