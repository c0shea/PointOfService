using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedLines : ICommand
    {
        [XmlAttribute]
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedLines(Lines));
        }
    }
}
