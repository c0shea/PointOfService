using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class Printer : IDisposable
    {
        public PosPrinter Device { get; }
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
        
        //public void Execute(List<Line> lines)
        //{
        //    Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);

        //    foreach (var line in lines)
        //    {
        //        Device.PrintNormal(PrinterStation.Receipt, line.ToString());
        //    }

        //    Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
        //}

        //public void ExecuteAll(IEnumerable<ICommand> commands)
        //{
        //    Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);

        //    foreach (var command in commands)
        //    {
        //        command.Execute(Device);
        //    }

        //    Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
        //}

        //public void Execute(ICommand command)
        //{
        //    command.Execute(Device);
        //}
    }
}
