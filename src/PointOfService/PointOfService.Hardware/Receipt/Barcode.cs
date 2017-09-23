using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    public class Barcode : Command
    {
        [XmlAttribute]
        public Alignment Alignment { get; set; }

        [XmlAttribute]
        public BarCodeSymbology Symbology { get; set; }

        [XmlAttribute]
        public string Data { get; set; }

        [XmlAttribute]
        public int Height { get; set; }

        [XmlAttribute]
        public int Width { get; set; }

        [XmlAttribute]
        public BarCodeTextPosition TextPosition { get; set; }

        public override void Execute(PosPrinter printer)
        {
            var alignment = 0;

            switch (Alignment)
            {
                case Alignment.Center:
                    alignment = PosPrinter.PrinterBarCodeCenter;
                    break;
                case Alignment.Left:
                    alignment = PosPrinter.PrinterBarCodeLeft;
                    break;
                case Alignment.Right:
                    alignment = PosPrinter.PrinterBarCodeRight;
                    break;
            }

            printer.PrintBarCode(PrinterStation.Receipt, Data, Symbology, Height, Width, alignment, TextPosition);
        }
    }
}
