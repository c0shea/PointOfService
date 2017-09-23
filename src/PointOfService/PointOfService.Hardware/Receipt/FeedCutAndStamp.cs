using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class FeedCutAndStamp : ICommand
    {
        [XmlAttribute]
        public byte? PercentCut { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.FeedCutAndStamp(PercentCut));
        }
    }
}
