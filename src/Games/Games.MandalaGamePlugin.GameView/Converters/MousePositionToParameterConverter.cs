using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Games.MandalaGamePlugin.GameView.MousePositionTracker;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class MousePositionToParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3 || !(values[0] is Canvas canvas) || !(values[1] is double elementWidth) ||
                !(values[2] is double elementHeight))
            {
                throw new NotImplementedException();
            }

            var mousePosition = Mouse.GetPosition(canvas);

            return new RelativeMousePositionInformation
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