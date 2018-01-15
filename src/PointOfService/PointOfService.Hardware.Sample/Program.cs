using System;
using System.Threading;

namespace PointOfService.Hardware.Sample
{
    public static class Program
    {
        public static void Main()
        {
            var option = 0;

            while (option >= 0)
            {
                Console.WriteLine("Select an Option:");
                Console.WriteLine(" 1  Scanner");
                Console.WriteLine(" 2  Printer");
                Console.WriteLine("-1  Exit");
                Console.Write("\r\nSelection: ");

                var input = Console.ReadLine();

                if (input == null || !int.TryParse(input, out option))
                {
                    Console.WriteLine("No valid option was selected");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        ScannerTest.Run();
                        break;
                    case 2:
                        PrinterTest.Run();
                        break;
                }
            }

            Console.WriteLine("Exiting...");
            Thread.Sleep(750);
        }
    }
}
