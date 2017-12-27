using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CameraSlider.Frontend.Shared.Abstractions;
using CameraSlider.Frontend.Shared.Models;
using MvvmHelpers;

namespace CameraSlider.Frontend.Shared.Services
{
    public interface IBluetoothLeService
    {
        /// <summary>
        /// Gets or sets the connected device.
        /// </summary>
        /// <value>The connected device.</value>
        IBluetoothDevice ConnectedDevice { get; set; }

        /// <summary>
        /// Scans for devices async.
        /// </summary>
        /// <returns>The for devices async.</returns>
        //Task<List<IBluetoothDevice>> ScanForDevicesAsync();
        Task ScanForDevicesAsync(ObservableCollection<IBluetoothDevice> bluetoothDevices);

        /// <summary>
        /// Writes to service characteristic async.
        /// </summary>
        /// <returns>If write was successful.</returns>
        /// <param name="message">Message.</param>
        /// <param name="serviceUuid">Service UUID.</param>
        /// <param name="characteristicUuid">Characteristic UUID.</param>
        Task<bool> WriteToServiceCharacteristicAsync(string message, string serviceUuid, string characteristicUuid);

        /// <summary>
        /// Connects to device async.
        /// </summary>
        /// <returns>The to device async.</returns>
        /// <param name="device">Device.</param>
        Task ConnectToDeviceAsync(IBluetoothDevice device);

        /// <summary>
        /// Checks if Bluetooth LE is activated and available on this device
        /// </summary>
        /// <value><c>true</c> if is bluetooth le available; otherwise, <c>false</c>.</value>
        bool GetConnectionStatus();

    }
}
