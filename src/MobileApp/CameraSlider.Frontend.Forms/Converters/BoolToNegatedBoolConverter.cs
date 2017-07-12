using System;
using System.Globalization;
using Xamarin.Forms;

namespace CameraSlider.Frontend.Forms.Converters
{
    public class BoolToNegatedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
