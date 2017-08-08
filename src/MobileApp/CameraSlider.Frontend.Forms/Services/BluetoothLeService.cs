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

        public async Task<bool> WriteToServiceCharacteristicAsync(string message, string serviceUuid, string characteristicUuid)
        {
            if (ConnectedDevice == null || !(ConnectedDevice is BluetoothDevice))
                return false;

            try
            {
                var service = await ((BluetoothDevice)ConnectedDevice).Device.GetServiceAsync(Guid.Parse(serviceUuid));
                var characteristic = await service.GetCharacteristicAsync(Guid.Parse(characteristicUuid));
                await characteristic.WriteAsync(Encoding.UTF8.GetBytes(message));

            }
            catch (NullReferenceException)
            {
                // Service or Characteristics UUID might not have been found
                return false;
            }
            catch (CharacteristicReadException)
            {
                // TODO: Find out when and why this happens
                return false;
            }

            return true;
        }

        public async Task ConnectToDeviceAsync(IBluetoothDevice device)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(((BluetoothDevice)device).Device);
                ConnectedDevice = device;
            }
            catch (DeviceConnectionException)
            {
                // ... could not connect to device
                ConnectedDevice = null;
            }
        }

        public Task<bool> TestDeviceConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public bool GetConnectionStatus()
        {
            return
                (bluetoothLe.State == BluetoothState.On || bluetoothLe.State == BluetoothState.TurningOn) &&
                bluetoothLe.IsAvailable;
        }
    }
}
