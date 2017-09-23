using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedAndPaperCut : Command
    {
        [XmlElement(IsNullable = true)]
        public byte? PercentCut { get; set; }

        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedAndPaperCut(PercentCut));
        }
    }
}
