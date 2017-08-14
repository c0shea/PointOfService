using System;
using Microsoft.PointOfService;

namespace PointOfService.Hardware
{
    public class Printer : IDisposable
    {
        public PosPrinter Device { get; }

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

        public void Dispose()
        {
            Device.DeviceEnabled = false;
            Device.Release();
            Device.Close();
        }
    }
}
