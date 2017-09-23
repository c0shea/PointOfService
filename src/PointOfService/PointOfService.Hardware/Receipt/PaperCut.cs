using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class PaperCut : Command
    {
        [XmlAttribute]
        public byte? PercentCut { get; set; }

        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PaperCut(PercentCut));
        }
    }
}
