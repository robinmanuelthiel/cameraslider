using System;
using System.ComponentModel;
using System.Diagnostics;

using CameraSlider.Frontend.Forms.Effects;
using CameraSlider.Frontend.Forms.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("CameraSlider")]
[assembly: ExportEffect(typeof(ColorOverlayEffectiOS), nameof(ColorOverlayEffect))]
namespace CameraSlider.Frontend.Forms.iOS.Effects
{
    public class ColorOverlayEffectiOS : PlatformEffect
    {
        protected override void OnAttached()
        {
            //throw new NotImplementedException();
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}