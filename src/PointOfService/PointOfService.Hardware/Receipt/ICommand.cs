using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public interface ICommand
    {
        void Execute(PosPrinter printer, PrinterStation station);
    }
}