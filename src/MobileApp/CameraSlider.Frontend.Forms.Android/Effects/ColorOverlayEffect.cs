using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CameraSlider.Frontend.Forms.Droid.Effects;
using CameraSlider.Frontend.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("CameraSlider")]
[assembly: ExportEffect(typeof(ColorOverlayEffectDrod), nameof(ColorOverlayEffect)]
namespace CameraSlider.Frontend.Forms.Droid.Effects
{
    public class ColorOverlayEffectDrod : PlatformEffect
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.CreateAttached("Color", typeof(Color), typeof(ColorOverlayEffectDrod), Color.Default);


        protected override void OnAttached()
        {
            if (!(Control is ImageView))
                return;

            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            var drawable = ((ImageView)Control).Drawable.Mutate();
            //drawable.SetColorFilter(new LightingColorFilter(effect.Color.ToAndroid(), Element.Foreground.ToAndroid()));
            drawable.Alpha = effect.Color.ToAndroid().A;

            ((ImageView)Control).SetImageDrawable(drawable);
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}