using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class PrintBitmap : ICommand
    {
        [XmlAttribute]
        public short BitmapNumber { get; set; }

        public void Execute(PosPrinter printer)
        {
            printer.Print(EscapeSequence.PrintBitmap(BitmapNumber));
        }
    }
}
