using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedUnits : ICommand
    {
        public int? Units { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedUnits(Units));
        }
    }
}
