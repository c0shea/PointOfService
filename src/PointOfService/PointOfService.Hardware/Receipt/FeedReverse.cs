using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedReverse : ICommand
    {
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedReverse(Lines));
        }
    }
}
