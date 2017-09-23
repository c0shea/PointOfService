using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class Bitmap : Command
    {
        [XmlAttribute]
        public Alignment Alignment { get; set; }

        [XmlAttribute]
        public string FileName { get; set; }

        public override void Execute(PosPrinter printer)
        {
            var alignment = 0;

            switch (Alignment)
            {
                case Alignment.Center:
                    alignment = PosPrinter.PrinterBitmapCenter;
                    break;
                case Alignment.Left:
                    alignment = PosPrinter.PrinterBitmapLeft;
                    break;
                case Alignment.Right:
                    alignment = PosPrinter.PrinterBitmapRight;
                    break;
            }

            printer.PrintBitmap(PrinterStation.Receipt, FileName, PosPrinter.PrinterBitmapAsIs, alignment);
        }
    }
}
