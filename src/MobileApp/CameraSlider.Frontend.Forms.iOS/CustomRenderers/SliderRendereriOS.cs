using CameraSlider.Frontend.Forms.iOS.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Slider), typeof(SliderRendereriOS))]
namespace CameraSlider.Frontend.Forms.iOS.CustomRenderers
{
    public class SliderRendereriOS : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            Control.TintColor = UIColor.FromRGB(39, 147, 150);
        }
    }
}