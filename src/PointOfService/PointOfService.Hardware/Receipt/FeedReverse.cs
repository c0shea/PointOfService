using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedReverse : ICommand
    {
        public string Name => nameof(FeedReverse);
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.FeedReverse(Lines));
        }
    }
}
