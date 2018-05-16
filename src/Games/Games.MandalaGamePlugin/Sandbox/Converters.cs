using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Games.MandalaGamePlugin.Sandbox
{
    public static class Constants
    {
        public const double LatTop = 50.0;
        public const double LatBottom = 24.0;

        public const double LongLeft = 125.0;
        public const double LongRight = 66.0;
    }

    public class DiameterToCanvasTopValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double diameter = (double)values[0];
            double width = (double)values[1];
            double height = (double)values[2];

            var minWidthHeight = Math.Min(width, height);
            double d = 0.0;
            if (width < height)
            {
                d = (height - width) / 2;
            }
            return d + minWidthHeight * (1-diameter) / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DiameterToCanvasLeftValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double diameter = (double)values[0];
            double width = (double)values[1];
            double height = (double)values[2];

            var minWidthHeight = Math.Min(width, height);
            double d = 0.0;
            if (height < width)
            {
                d = (width - height) / 2;
            }
            return d + minWidthHeight * (1 - diameter) / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
