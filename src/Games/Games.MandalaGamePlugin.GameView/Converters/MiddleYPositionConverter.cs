using System;
using System.Globalization;
using System.Windows.Data;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class MiddleYPositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var height = (double) values[1];
            return height / 2.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}