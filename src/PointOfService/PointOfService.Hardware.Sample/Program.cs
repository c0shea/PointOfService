using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.PointOfService;
using PointOfService.Hardware.Receipt;

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

        public static string Format(object obj)
        {
            var sb = new StringBuilder();

            if (obj != null)
            {
                sb.AppendLine(obj.ToString());
            }

            var array = obj as Array;

            if (array != null)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    sb.Append("  [");
                    sb.Append(i);
                    sb.Append("]: ");
                    sb.AppendLine(array.GetValue(i).ToString());
                }
            }

            return sb.ToString();
        }

        private static void RunPrinter()
        {
            var printer = new Printer("PosPrinter");
            printer.Open();

            //Console.WriteLine("Opening Drawer...");
            //printer.CashDrawerOpenCodes = new byte[] {27, 112, 0, 100, 250};
            //printer.OpenCashDrawer();

            //Console.WriteLine("Printing...");
            //printer.Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);
            //printer.Device.PrintNormal(PrinterStation.Receipt, "Test Receipt");
            //printer.Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);

            //var b = new Bitmap
            //{
            //    Alignment = Alignment.Center,
            //    FileName = @"C:\Users\hinta\Desktop\Test.bmp"
            //};

            //printer.Execute(b);

            //printer.Execute(new Barcode
            //{
            //    Alignment = Alignment.Center,
            //    Symbology = BarCodeSymbology.Upca,
            //    Data = "609032551339",
            //    Height = 750,
            //    Width = 1500,
            //    TextPosition = BarCodeTextPosition.Below
            //});

            var lines = new List<Line>();

            var t = printer.Device.GetType();
            var p = t.GetProperties();

            foreach (var propertyInfo in p)
            {
                lines.Add(new Line
                {
                    Alignment = Alignment.Left,
                    Text = $"{EscapeSequence.Bold()}{propertyInfo.Name}: {EscapeSequence.Normal}{Format(propertyInfo.GetValue(printer.Device))}",
                    CharactersPerLine = 56
                });
            }


            printer.ExecuteAll(lines);

            // CPL Lg = 28, M = 42, Sm = 56
            //printer.ExecuteAll(new List<Line>
            //{
            //    new Line
            //    {
            //        IsBold = true,
            //        Text = "This is bold text"
            //    },
            //    new Line
            //    {
            //        IsItalic = true,
            //        Text = "This is italic text",
            //        Alignment = Alignment.Center,
            //        CharactersPerLine = 56
            //    },
            //    new Line
            //    {
            //        IsUnderline = true,
            //        Text = "This is underline text",
            //        Alignment = Alignment.Right
            //    },
            //    new Line
            //    {
            //        IsBold = true,
            //        IsItalic = true,
            //        IsUnderline = true,
            //        Text = "This is all three",
            //        CharactersPerLine = 28
            //    }
            //});

            //printer.Execute(new FeedLines{Lines = 4});

            printer.Execute(new FeedAndPaperCut());
            
            printer.Dispose();

            Console.ReadLine();
        }
    }
}
