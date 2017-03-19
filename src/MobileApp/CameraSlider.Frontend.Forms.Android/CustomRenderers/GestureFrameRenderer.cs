using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Views;
using CameraSlider.Frontend.Forms.Droid.CustomRenderers;
using CameraSlider.Frontend.Forms.Extensions;


using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Frame), typeof(GestureFrameRenderer))]
namespace CameraSlider.Frontend.Forms.Droid.CustomRenderers
{
    public class GestureFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;

                ViewGroup.Touch += (object sender, TouchEventArgs te) =>
                {
                    switch (te.Event.Action)
                    {
                        case MotionEventActions.Down:
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
                            break;
                        case MotionEventActions.Up:
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
                            break;
                    }
                };
            }
        }
    }
}