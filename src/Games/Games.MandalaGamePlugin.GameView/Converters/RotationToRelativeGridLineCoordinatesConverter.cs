using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class RotationToRelativeGridLineCoordinatesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double rotation))
            {
                throw new NotImplementedException();
            }

            return new[]
            {
                new Point(0.0,0.0),
                new Point(Math.Sin(Math.PI * rotation / 180.0), Math.Cos(Math.PI * rotation / 180.0))
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}