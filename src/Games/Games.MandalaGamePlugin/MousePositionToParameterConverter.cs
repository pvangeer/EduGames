using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Games.MandalaGamePlugin
{
    public class MousePositionToParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3 || !(values[0] is Point mousePosition) || !(values[1] is double elementWidth) ||
                !(values[2] is double elementHeight))
            {
                throw new NotImplementedException();
            }

            return new MouseTrackingObject
            {
                MousePosition = mousePosition,
                ElementHeight = elementHeight,
                ElementWidth = elementWidth
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}