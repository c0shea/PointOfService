using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class PrintTopLogo : ICommand
    {
        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintTopLogo);
        }
    }
}
