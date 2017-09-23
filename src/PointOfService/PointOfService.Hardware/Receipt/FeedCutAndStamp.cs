using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedCutAndStamp : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedCutAndStamp(PercentCut));
        }
    }
}
