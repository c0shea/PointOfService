using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FireStamp : ICommand
    {
        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FireStamp);
        }
    }
}
