using System;
using CameraSlider.Frontend.Shared.Models;
using Plugin.BLE.Abstractions.Contracts;
namespace CameraSlider.Frontend.Forms.Models
{
    public class BluetoothDevice : IBluetoothDevice
    {
        public string Name { get; set; }
        public string Uuid { get; set; }

        public IDevice Device;

        public BluetoothDevice(IDevice device)
        {
            this.Name = device.Name;
            this.Uuid = device.Id.ToString();
            this.Device = device;

            if (Name == null)
                Name = "Unknown device";
        }
    }
}
