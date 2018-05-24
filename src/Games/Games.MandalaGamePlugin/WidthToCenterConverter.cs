using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Games.MandalaGamePlugin
{
    public class WidthToCenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return width / 2.0;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}