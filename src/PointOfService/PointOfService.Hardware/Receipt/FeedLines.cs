using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedLines : ICommand
    {
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedLines(Lines));
        }
    }
}
