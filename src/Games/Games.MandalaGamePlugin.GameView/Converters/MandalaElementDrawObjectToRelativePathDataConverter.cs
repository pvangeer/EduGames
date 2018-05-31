using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class MandalaElementDrawObjectToRelativePathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is Point[] points) || !(values[1] is double rotation))
            {
                throw new NotImplementedException();
            }

            var positionedPoints = new List<Point>();
            foreach (var point in points)
            {
                var positionedX = point.X * Math.Cos(rotation) - point.Y * Math.Sin(rotation);
                var positionedY = point.X * Math.Sin(rotation) + point.Y * Math.Cos(rotation);
                positionedPoints.Add(new Point(positionedX,positionedY));
            }

            return positionedPoints.ToArray();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}