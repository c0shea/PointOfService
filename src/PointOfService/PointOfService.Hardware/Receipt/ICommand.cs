using Microsoft.PointOfService;
using Newtonsoft.Json;
using PointOfService.Hardware.Receipt.Converter;

namespace PointOfService.Hardware.Receipt
{
    [JsonConverter(typeof(CommandConverter))]
    public interface ICommand
    {
        string Name { get; }

        void Execute(PosPrinter printer, PrinterStation station);
    }
}
