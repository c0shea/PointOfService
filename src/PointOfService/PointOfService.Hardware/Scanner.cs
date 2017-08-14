using System;
using System.Text;
using Microsoft.PointOfService;

namespace PointOfService.Hardware
{
    public class Scanner : IDisposable
    {
        public Microsoft.PointOfService.Scanner Device { get; }

        public Scanner(string logicalName)
        {
            var explorer = new PosExplorer();
            var device = explorer.GetDevice(DeviceType.Scanner, logicalName);

            Device = explorer.CreateInstance(device) as Microsoft.PointOfService.Scanner;
        }

        public void Start(Action<string, BarcodeSymbology> action)
        {
            Device.DataEvent += (sender, args) =>
            {
                var decodedBarcode = Encoding.UTF8.GetString(Device.ScanDataLabel);
                var symbology = (BarcodeSymbology)Device.ScanDataType;

                action(decodedBarcode, symbology);

                Device.DataEventEnabled = true;
            };

            Device.Open();
            Device.Claim(1000);
            Device.DeviceEnabled = true;
            Device.DataEventEnabled = true;
            Device.DecodeData = true;
        }

        public void Dispose()
        {
            Device.DeviceEnabled = false;
            Device.Release();
            Device.Close();
        }
    }
}
