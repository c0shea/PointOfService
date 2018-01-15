using System;

namespace PointOfService.Hardware.Sample
{
    public static class ScannerTest
    {
        public static void Run()
        {
            var scanner = new Scanner("STI_USBSCANNER");

            scanner.Start((barcode, symbology) =>
            {
                Console.WriteLine($"Symbology: {symbology}");
                Console.WriteLine($"Barcode:   {barcode}\r\n");
            });

            Console.WriteLine("Ready");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
            scanner.Dispose();
        }
    }
}
