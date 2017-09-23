using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedUnits : ICommand
    {
        [XmlAttribute]
        public int? Units { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedUnits(Units));
        }
    }
}
