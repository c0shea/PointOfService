using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedCutAndStamp : ICommand
    {
        public string Name => nameof(FeedCutAndStamp);
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            if (!printer.CapRecPaperCut && station == PrinterStation.Receipt)
            {
                return;
            }

            printer.PrintNormal(station, EscapeSequence.FeedCutAndStamp(PercentCut));
        }
    }
}
