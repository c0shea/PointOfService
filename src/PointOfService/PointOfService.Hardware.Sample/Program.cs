using System;

namespace PointOfService.Hardware.Sample
{
    public static class Program
    {
        public static void Main()
        {
            var option = 0;

            while (option >= 0)
            {
                WriteMenuHeader("MAIN");

                Console.WriteLine("Select an Option:");
                Console.WriteLine(" 1  Scanner");
                Console.WriteLine(" 2  Printer");
                Console.WriteLine("-1  Exit");
                WritePrompt();

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
        }

        public static void WriteMenuHeader(string menuName)
        {
            const string menuSuffix = " MENU";
            const int paddingPerSide = 2;
            var headerLength = menuName.Length + menuSuffix.Length + paddingPerSide * 2;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(new string('*', headerLength));
            Console.WriteLine($"* {menuName}{menuSuffix} *");
            Console.WriteLine(new string('*', headerLength));
            Console.WriteLine(Environment.NewLine);

            Console.ResetColor();
        }

        public static void WritePrompt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{Environment.NewLine}Selection: ");
            Console.ResetColor();
        }
    }
}
