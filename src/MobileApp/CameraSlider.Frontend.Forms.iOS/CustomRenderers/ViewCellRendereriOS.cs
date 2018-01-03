using System;
using CameraSlider.Frontend.Forms.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ViewCell), typeof(ViewCellRendereriOS))]
namespace CameraSlider.Frontend.Forms.iOS.CustomRenderers
{
    public class ViewCellRendereriOS : ViewCellRenderer
    {
        private UIView selectedBackground;

        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            if (selectedBackground == null)
            {
                selectedBackground = new UIView(cell.SelectedBackgroundView.Bounds);
                selectedBackground.Layer.BackgroundColor = Color.FromHex("#444444").ToCGColor();
            }

            cell.SelectedBackgroundView = selectedBackground;

            return cell;
        }
    }
}
