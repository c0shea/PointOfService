using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedUnits : Command
    {
        [XmlAttribute]
        public int? Units { get; set; }

        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedUnits(Units));
        }
    }
}
