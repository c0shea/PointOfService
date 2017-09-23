using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class PrintBitmap : Command
    {
        [XmlAttribute]
        public short BitmapNumber { get; set; }

        public override void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintBitmap(BitmapNumber));
        }
    }
}
