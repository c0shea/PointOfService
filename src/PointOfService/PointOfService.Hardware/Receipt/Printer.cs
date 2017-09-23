using System;
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

        public void PrintReceipt(Document document)
        {
            if (!Device.CapRecPresent)
            {
                throw new InvalidOperationException("The device doesn't have the receipt print station capability.");
            }

            if (Device.CapTransaction)
            {
                Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);
            }

            foreach (var command in document.Commands)
            {
                command.Execute(Device, PrinterStation.Receipt);
            }

            if (Device.CapTransaction)
            {
                Device.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
            }
        }

        public void PrintSlip(Document document, TimeSpan insertionTimeout)
        {
            if (!Device.CapSlpPresent)
            {
                throw new InvalidOperationException("The device doesn't have the slip print station capability.");
            }

            Device.BeginInsertion((int)insertionTimeout.TotalMilliseconds);

            //if (Device.CapTransaction)
            //{
            //    Device.TransactionPrint(PrinterStation.Slip, PrinterTransactionControl.Transaction);
            //}

            foreach (var command in document.Commands)
            {
                command.Execute(Device, PrinterStation.Slip);
            }

            //if (Device.CapTransaction)
            //{
            //    Device.TransactionPrint(PrinterStation.Slip, PrinterTransactionControl.Normal);
            //}

            Device.EndInsertion();
        }

        public void Dispose()
        {
            Device.DeviceEnabled = false;
            Device.Release();
            Device.Close();
        }
    }
}
