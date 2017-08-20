using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PointOfService;

namespace PointOfService.Hardware
{
    public enum Alignment
    {
        Center,
        Left,
        Right
    }

    public class Bitmap
    {
        public Alignment Alignment { get; set; }
        public string FileName { get; set; }
    }

    public class Barcode
    {
        public Alignment Alignment { get; set; }
        public BarCodeSymbology Symbology { get; set; }
        public string Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public BarCodeTextPosition TextPosition { get; set; }
    }

    //public class Document
    //{
    //    public List<IPrintControl> PrintControls { get; set; }
    //}

    //public interface IPrintControl
    //{
        
    //}

    //public class Text : IPrintControl
    //{
    //    public string Value { get; set; }
    //}
}
