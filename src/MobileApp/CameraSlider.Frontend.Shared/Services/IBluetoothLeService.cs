using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CameraSlider.Frontend.Shared.Models;

namespace CameraSlider.Frontend.Shared.Services
{
    public interface IBluetoothLeService
    {
        IBluetoothDevice ConnectedDevice { get; set; }
        Task<List<IBluetoothDevice>> ScanForDevicesAsync();
        Task WriteToServiceCharacteristicAsync(string message, string serviceUuid, string characteristicUuid);
        Task ConnectToDeviceAsync(IBluetoothDevice device);
    }
}
