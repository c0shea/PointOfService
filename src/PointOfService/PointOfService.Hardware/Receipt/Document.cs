using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class Document
    {
        public List<ICommand> Commands { get; set; }

        public void Print(Printer printer)
        {
            if (printer.Device.CapTransaction)
            {
                printer.Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);
            }

            foreach (var command in Commands)
            {
                command.Execute(printer.Device);
            }

            if (printer.Device.CapTransaction)
            {
                printer.Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
            }
        }
    }

    public static class CommandExtensions
    {
        public static void Print(this PosPrinter printer, string command)
        {
            printer.PrintNormal(PrinterStation.Receipt, command);
        }
    }
}
