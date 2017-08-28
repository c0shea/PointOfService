using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PointOfService;

namespace PointOfService.Hardware
{
    public class Printer : IDisposable
    {
        public const char Escape = (char)27;

        public PosPrinter Device { get; }
        public string Esc => Escape + "|";
        public byte[] CashDrawerOpenCodes { get; set; }

        public Printer(string logicalName)
        {
            var explorer = new PosExplorer();
            var device = explorer.GetDevice(DeviceType.PosPrinter, logicalName);

            Device = explorer.CreateInstance(device) as PosPrinter;
        }

        public void Open()
        {
            Device.Open();
            Device.Claim(1000);
            Device.DeviceEnabled = true;
            Device.RecLetterQuality = true;
            Device.MapMode = MapMode.Metric;
        }

        public void OpenCashDrawer()
        {
            Device.PrintNormal(PrinterStation.Receipt, new string(CashDrawerOpenCodes.Select(c => (char)c).ToArray()));
        }

        public void Dispose()
        {
            Device.DeviceEnabled = false;
            Device.Release();
            Device.Close();
        }

        public void Execute(Bitmap bitmap)
        {
            var alignment = 0;

            switch (bitmap.Alignment)
            {
                case Alignment.Center:
                    alignment = PosPrinter.PrinterBitmapCenter;
                    break;
                case Alignment.Left:
                    alignment = PosPrinter.PrinterBitmapLeft;
                    break;
                case Alignment.Right:
                    alignment = PosPrinter.PrinterBitmapRight;
                    break;
            }
            
            Device.PrintBitmap(PrinterStation.Receipt, bitmap.FileName, PosPrinter.PrinterBitmapAsIs, alignment);
        }

        public void Execute(Barcode barcode)
        {
            var alignment = 0;

            switch (barcode.Alignment)
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

            Device.PrintBarCode(PrinterStation.Receipt, barcode.Data, barcode.Symbology, barcode.Height, barcode.Width, alignment, barcode.TextPosition);
        }

        public void Execute(List<Line> lines)
        {
            Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);

            foreach (var line in lines)
            {
                Device.PrintNormal(PrinterStation.Receipt, line.ToString());
            }

            Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
        }

        public void ExecuteAll(IEnumerable<ICommand> commands)
        {
            Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);

            foreach (var command in commands)
            {
                command.Execute(Device);
            }

            Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
        }

        public void Execute(ICommand command)
        {
            command.Execute(Device);
        }
    }
}
