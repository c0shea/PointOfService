using System.Xml.Serialization;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    [XmlRoot]
    [XmlInclude(typeof(Barcode))]
    [XmlInclude(typeof(Bitmap))]
    [XmlInclude(typeof(FeedAndPaperCut))]
    [XmlInclude(typeof(FeedCutAndStamp))]
    [XmlInclude(typeof(FeedLines))]
    [XmlInclude(typeof(FeedReverse))]
    [XmlInclude(typeof(FeedUnits))]
    [XmlInclude(typeof(FireStamp))]
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(PaperCut))]
    [XmlInclude(typeof(PrintBitmap))]
    [XmlInclude(typeof(PrintBottomLogo))]
    [XmlInclude(typeof(PrintTopLogo))]
    public abstract class Command
    {
        public abstract void Execute(PosPrinter printer);
    }
}