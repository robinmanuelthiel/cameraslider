using System;
using CameraSlider.Frontend.Shared.Services;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Exceptions;
using System.Text;
using CameraSlider.Frontend.Shared.Models;
using CameraSlider.Frontend.Forms.Models;

namespace CameraSlider.Frontend.Forms.Services
{
    public class BluetoothLeService : IBluetoothLeService
    {
        private IBluetoothLE bluetoothLe;
        private IAdapter adapter;

        public IBluetoothDevice ConnectedDevice { get; set; }

        public BluetoothLeService()
        {
            bluetoothLe = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<List<IBluetoothDevice>> ScanForDevicesAsync()
        {
            var devices = new List<IBluetoothDevice>();
            adapter.DeviceDiscovered += (s, a) => devices.Add(new BluetoothDevice(a.Device));
            await adapter.StartScanningForDevicesAsync();
            return devices;
        }

        public async Task WriteToServiceCharacteristicAsync(string message, string serviceUuid, string characteristicUuid)
        {
            if (ConnectedDevice != null && ConnectedDevice is BluetoothDevice)
            {
                var service = await ((BluetoothDevice)ConnectedDevice).Device.GetServiceAsync(Guid.Parse(serviceUuid));
                var characteristic = await service.GetCharacteristicAsync(Guid.Parse(characteristicUuid));
                await characteristic.WriteAsync(Encoding.UTF8.GetBytes(message));
            }
        }

        public async Task ConnectToDeviceAsync(IBluetoothDevice device)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(((BluetoothDevice)device).Device);
                ConnectedDevice = device;
            }
            catch (DeviceConnectionException e)
            {
                // ... could not connect to device
                ConnectedDevice = null;
            }
        }
    }
}
