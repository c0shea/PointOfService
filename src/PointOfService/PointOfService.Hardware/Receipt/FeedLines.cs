using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedLines : Command
    {
        [XmlAttribute]
        public short? Lines { get; set; }

        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedLines(Lines));
        }
    }
}
