using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FireStamp : ICommand
    {
        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FireStamp);
        }
    }
}
