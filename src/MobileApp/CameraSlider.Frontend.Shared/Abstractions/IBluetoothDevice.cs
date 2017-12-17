using System;
namespace CameraSlider.Frontend.Shared.Abstractions
{
    public interface IBluetoothDevice
    {
        string Name { get; set; }
        string Uuid { get; set; }
    }
}
