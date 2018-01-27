using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FireStamp : ICommand
    {
        public string Name => nameof(FireStamp);

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            if (!printer.CapRecStamp && station == PrinterStation.Receipt)
            {
                return;
            }

            printer.PrintNormal(station, EscapeSequence.FireStamp);
        }
    }
}
