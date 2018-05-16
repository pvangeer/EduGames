using System;
using System.Globalization;
using System.Windows.Data;

namespace Games.MandalaGamePlugin
{
    public class CanvasMinWidthHeightMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3 || !(values[0] is double width) || !(values[1] is double height) || !(values[2] is double fraction))
            {
                return 100;
            }

            return fraction * Math.Min(width, height);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}