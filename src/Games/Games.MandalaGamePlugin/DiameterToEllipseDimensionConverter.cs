using System;
using System.Globalization;
using System.Windows.Data;

namespace Games.MandalaGamePlugin
{
    public class DiameterToEllipseDimensionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var diameter = (double) values[0];
            var width = (double)values[1];
            var height = (double)values[2];

            var minWidthHeight = Math.Min(width, height);
            return diameter * minWidthHeight;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}