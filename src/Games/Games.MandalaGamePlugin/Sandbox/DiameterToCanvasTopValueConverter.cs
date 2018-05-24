using System;
using System.Windows.Data;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class DiameterToCanvasTopValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double diameter;
            double width;
            double height;
            if (values.Length == 2)
            {
                diameter = 1;
                width = (double)values[0];
                height = (double)values[1];
            }
            else
            {
                diameter = (double)values[0];
                width = (double)values[1];
                height = (double)values[2];
            }

            var minWidthHeight = Math.Min(width, height);
            double d = 0.0;
            if (width < height)
            {
                d = (height - width) / 2.0;
            }
            return d + minWidthHeight * (1-diameter) / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}