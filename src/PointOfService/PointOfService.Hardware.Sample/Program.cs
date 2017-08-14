using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfService.Hardware.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var scanner = new Scanner("STI_USBSCANNER");

            scanner.Start((barcode, symbology) =>
            {
                Console.WriteLine(symbology.ToString() + "    " + barcode);
            });

            Console.WriteLine("Ready...");
            Console.ReadLine();
            scanner.Dispose();
        }
    }
}
