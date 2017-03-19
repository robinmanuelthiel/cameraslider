using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CameraSlider.Frontend.Forms.Extensions;
using CameraSlider.Frontend.Forms.iOS.CustomRenderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(GestureFrameRenderer))]
namespace CameraSlider.Frontend.Forms.iOS.CustomRenderers
{
    public class GestureFrameRenderer : FrameRenderer
    {
        UILongPressGestureRecognizer pressGestureRecognizer;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;


                //Control.UserInteractionEnabled = true;

                pressGestureRecognizer = new UILongPressGestureRecognizer(() =>
                {
                    if (pressGestureRecognizer.State == UIGestureRecognizerState.Began)
                    {
                        foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                        {
                            var touchGestureRecognizer = recognizer as TouchGestureRecognizer;
                            if (touchGestureRecognizer != null)
                            {
                                if (touchGestureRecognizer.TouchDownCommand != null)
                                    touchGestureRecognizer.TouchDownCommand.Execute(touchGestureRecognizer.TouchDownCommandParameter);

                                if (touchGestureRecognizer.TouchDown != null)
                                    touchGestureRecognizer.TouchDown();
                            }
                        }
                    }
                    else if (pressGestureRecognizer.State == UIGestureRecognizerState.Ended)
                    {
                        foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                        {
                            var touchGestureRecognizer = recognizer as TouchGestureRecognizer;
                            if (touchGestureRecognizer != null)
                            {
                                if (touchGestureRecognizer.TouchUpCommand != null)
                                    touchGestureRecognizer.TouchUpCommand.Execute(touchGestureRecognizer.TouchUpCommandParameter);

                                if (touchGestureRecognizer.TouchUp != null)
                                    touchGestureRecognizer.TouchUp();
                            }
                        }
                    }
                });

                pressGestureRecognizer.MinimumPressDuration = 0.0;
                //pressGestureRecognizer.Delegate = gestureDelegate;

                AddGestureRecognizer(pressGestureRecognizer);
            }
        }
    }
}