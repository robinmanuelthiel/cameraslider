using System;
namespace CameraSlider.Frontend.Shared.Models
{
    public interface IBluetoothDevice
    {
        string Name { get; set; }
        string Uuid { get; set; }
    }
}
