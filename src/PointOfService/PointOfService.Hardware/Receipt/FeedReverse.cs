using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedReverse : Command
    {
        [XmlAttribute]
        public short? Lines { get; set; }

        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedReverse(Lines));
        }
    }
}
