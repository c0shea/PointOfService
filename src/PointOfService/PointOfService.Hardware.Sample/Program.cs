using System;
using System.Threading;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Select an Option:");
            Console.WriteLine(" 1. Scanner");
            Console.WriteLine(" 2. Printer");
            Console.Write("\r\nSelection: ");
            var option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    RunScanner();
                    break;
                case 2:
                    RunPrinter();
                    break;
                default:
                    Console.WriteLine("No valid option was selected");
                    break;
            }

            Console.WriteLine("Exiting...");
            Thread.Sleep(1000);
        }

        private static void RunScanner()
        {
            var scanner = new Scanner("STI_USBSCANNER");

            scanner.Start((barcode, symbology) =>
            {
                Console.WriteLine($"Symbology: {symbology}");
                Console.WriteLine($"Barcode:   {barcode}\r\n");
            });

            Console.WriteLine("Ready");
            Console.ReadLine();
            scanner.Dispose();
        }

        private static void RunPrinter()
        {
            var printer = new Printer("PosPrinter");
            printer.Open();

            Console.WriteLine("Printing...");

            printer.Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);
            printer.Device.PrintNormal(PrinterStation.Receipt, "Test Receipt");
            printer.Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);

            printer.Dispose();
        }
    }
}
