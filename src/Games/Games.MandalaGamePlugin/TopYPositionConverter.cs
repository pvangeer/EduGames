using System;
using System.Globalization;
using System.Windows.Data;

namespace Games.MandalaGamePlugin
{
    public class TopYPositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double) values[0];
            var height = (double) values[1];
            return height <= width ? height/2.0 : width / 2.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}