using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using CameraSlider.Frontend.Forms.Droid.Effects;
using CameraSlider.Frontend.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.ComponentModel;
using Xamarin.Forms;

using Android.Graphics;
using Android.Graphics.Drawables;
using CameraSlider.Frontend.Forms.Extensions;
using CameraSlider.Frontend.Forms.Droid.CustomRenderers;


[assembly: ResolutionGroupName("CameraSlider")]
[assembly: ExportEffect(typeof(ColorOverlayEffectDrod), nameof(ColorOverlayEffect))]
namespace CameraSlider.Frontend.Forms.Droid.Effects
{
    public class ColorOverlayEffectDrod : PlatformEffect
    {
        private Drawable originalImage;

        protected override void OnAttached()
        {
            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            if (!(Control is ImageView))
                return;

            originalImage = ((ImageView)Control).Drawable;

            SetOverlay(effect.Color);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (!(Control is ImageView))
                return;

            Debug.WriteLine(args.PropertyName);
            //if (!args.PropertyName.Equals("Source") || !args.PropertyName.Equals("Renderer"))
            //    return;

            var effect = (ColorOverlayEffect)Element.Effects.FirstOrDefault(e => e is ColorOverlayEffect);
            if (effect == null)
                return;

            SetOverlay(effect.Color);
        }

        private void SetOverlay(Xamarin.Forms.Color color)
        {
            var formsImage = (Xamarin.Forms.Image)Element;
            if (formsImage?.Source == null)
                return;

            var drawable = ((ImageView)Control).Drawable.Mutate();
            //var drawable = Control.Context.Resources.GetDrawable("left.png");

            drawable.SetColorFilter(color.ToAndroid(), PorterDuff.Mode.SrcAtop);
            drawable.Alpha = color.ToAndroid().A;

            ((ImageView)Control).SetImageDrawable(drawable);
            ((IVisualElementController)Element).NativeSizeChanged();
        }

        protected override void OnDetached()
        {
            //if (!(Control is ImageView) || ((ImageView)Control).Drawable == null || originalImage == null)
            //    return;

            //((ImageView)Control).SetImageDrawable(originalImage);
        }
    }
}