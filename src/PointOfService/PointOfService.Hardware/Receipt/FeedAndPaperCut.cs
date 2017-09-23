using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedAndPaperCut : ICommand
    {
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedAndPaperCut(PercentCut));
        }
    }
}
