using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FireStamp : Command
    {
        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FireStamp);
        }
    }
}
