using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.PointOfService;
using Newtonsoft.Json;
using PointOfService.Hardware.Receipt;
using PointOfService.Hardware.Receipt.Template;

namespace PointOfService.Hardware.Sample
{
    public static class PrinterTest
    {
        public static void Run()
        {
            using (var printer = new Printer("PosPrinter"))
            {
                printer.Open();

                var option = 0;

                while (option >= 0)
                {
                    Program.WriteMenuHeader("PRINTER");

                    Console.WriteLine("Select an Option:");
                    Console.WriteLine(" 1  Slip");
                    Console.WriteLine(" 2  Open Cash Drawer");
                    Console.WriteLine(" 3  Receipt");
                    Console.WriteLine(" 4  Receipt from JSON");
                    Console.WriteLine(" 5  Receipt with OPOS Properties");
                    Console.WriteLine(" 6  Template Renderer");
                    Console.WriteLine("-1  Exit");
                    Program.WritePrompt();

                    var input = Console.ReadLine();

                    if (input == null || !int.TryParse(input, out option))
                    {
                        Console.WriteLine("No valid option was selected");
                        continue;
                    }

                    switch (option)
                    {
                        case 1:
                            printer.PrintSlip(BuildSlip(), new TimeSpan(0, 0, 30));
                            break;

                        case 2:
                            printer.CashDrawerOpenCodes = new byte[] { 27, 112, 0, 100, 250 };
                            printer.OpenCashDrawer();
                            break;

                        case 3:
                            printer.PrintReceipt(BuildReceipt());
                            break;

                        case 4:
                            var document = JsonConvert.DeserializeObject<Document>(File.ReadAllText("Receipt.json"));
                            printer.PrintReceipt(document);
                            break;

                        case 5:
                            PrintProperties(printer);
                            break;

                        case 6:
                            var se = new StringEnumerator("${Date} abc123");
                            var renderers = RendererParser.CompileRenderers(se, false, out var text);
                            var sb = new StringBuilder();

                            foreach (var renderer in renderers)
                            {
                                renderer.Append(sb);
                            }

                            var str = sb.ToString();

                            ;
                            break;
                    }
                }
            }
        }
        
        private static Document BuildReceipt()
        {
            var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var logoPath = Path.Combine(binDirectory, "Logo.bmp");

            var document = new Document
            {
                Commands = new List<ICommand>
                {
                    new Bitmap
                    {
                        Alignment = Alignment.Center,
                        FileName = logoPath
                    },
                    new FeedUnits
                    {
                        Units = 250
                    },
                    new Line
                    {
                        Alignment = Alignment.Center,
                        Text = "123 MAIN ST, BOSTON, MA 01841"
                    },
                    new FeedUnits
                    {
                        Units = 250
                    },
                    new Line
                    {
                        IsBold = true,
                        IsUnderline = true,
                        Text = "Item Description              Price       "
                    },
                    new Line
                    {
                        IsBold = true,
                        Text = "DAIRY"
                    },
                    new Line
                    {
                        CharactersPerLine = 42,
                        Text = "2% GAL MILK                      $3.99"
                    },
                    new FeedUnits
                    {
                        Units = 250
                    },
                    new Barcode
                    {
                        Alignment = Alignment.Center,
                        Data = "09231700130015342",
                        Height = 400,
                        Width = 1000,
                        Symbology = BarCodeSymbology.Code128,
                        TextPosition = BarCodeTextPosition.Below
                    },
                    new FeedAndPaperCut()
                }
            };

            return document;
        }

        private static Document BuildSlip()
        {
            var document = new Document
            {
                Commands = new List<ICommand>
                {
                    new Line
                    {
                        Text = "TEST FROM SLIP PRINTER"
                    },
                    new Line
                    {
                        Text = DateTime.Now.ToString("MMMM d, yyyy")
                    }
                }
            };

            return document;
        }

        private static void PrintProperties(Printer printer)
        {
            string FormatPropertyValue(object obj)
            {
                var sb = new StringBuilder();

                if (obj != null)
                {
                    sb.Append(obj);
                }

                if (obj is Array array)
                {
                    sb.AppendLine();

                    for (var i = 0; i < array.Length; i++)
                    {
                        sb.Append("  [");
                        sb.Append(i);
                        sb.Append("]: ");
                        sb.Append(array.GetValue(i));

                        if (i < array.Length - 1)
                        {
                            sb.AppendLine();
                        }
                    }
                }

                return sb.ToString();
            }

            var document = new Document
            {
                Commands = new List<ICommand>()
            };

            foreach (var property in printer.Device.GetType().GetProperties())
            {
                document.Commands.Add(new Line
                {
                    Text = $"{EscapeSequence.Bold()}{property.Name}: {EscapeSequence.Normal}{FormatPropertyValue(property.GetValue(printer.Device))}"
                });
            }

            document.Commands.Add(new FeedAndPaperCut());

            printer.PrintReceipt(document);
        }
    }
}
