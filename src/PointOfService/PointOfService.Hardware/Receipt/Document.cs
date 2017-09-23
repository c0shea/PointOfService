using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class Document
    {
        public List<ICommand> Commands { get; set; }
    }

    public static class CommandExtensions
    {
        public static void Print(this PosPrinter printer, string command)
        {
            printer.PrintNormal(PrinterStation.Receipt, command);
        }
    }
}
