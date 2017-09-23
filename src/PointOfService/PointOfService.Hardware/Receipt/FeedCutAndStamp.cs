using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedCutAndStamp : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.FeedCutAndStamp(PercentCut));
        }
    }
}
