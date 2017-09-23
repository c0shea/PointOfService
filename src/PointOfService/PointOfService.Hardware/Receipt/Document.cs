using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public static class CommandExtensions
    {
        public static void Print(this PosPrinter printer, string command)
        {
            printer.PrintNormal(PrinterStation.Receipt, command);
        }
    }
}
