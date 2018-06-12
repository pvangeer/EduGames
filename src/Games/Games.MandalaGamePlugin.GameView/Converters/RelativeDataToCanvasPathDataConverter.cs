using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class RelativeDataToCanvasPathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3 || values.Length > 4 || !(values[0] is double width) || !(values[1] is double height) ||
                !(values[2] is Point[] relativePoints))
            {
                throw new NotImplementedException();
            }

            if (values.Length == 4 && (values[3] is double rotation))
            {
                relativePoints = relativePoints.Select(p => RotatePoint(p, rotation)).ToArray();
            }

            var canvasCenter = new Point(width/2.0, height/2.0);
            var canvasRadius = Math.Min(width, height) / 2.0;

            if (relativePoints.Length <= 0)
            {
                return null;
            }

            Point start = RelativeToAbsolutePoint(relativePoints[0], canvasCenter, canvasRadius);

            List<LineSegment> segments = new List<LineSegment>();
            for (int i = 1; i < relativePoints.Length; i++)
            {
                segments.Add(new LineSegment(RelativeToAbsolutePoint(relativePoints[i], canvasCenter, canvasRadius), true));
            }

            PathFigure figure = new PathFigure(start, segments, false); //true if closed
            PathGeometry geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            return geometry;
        }

        private Point RotatePoint(Point point, double rotation)
        {
            var rotationInRadians = Math.PI / 180.0 * rotation;
            var positionedX = point.X * Math.Cos(rotationInRadians) - point.Y * Math.Sin(rotationInRadians);
            var positionedY = point.X * Math.Sin(rotationInRadians) + point.Y * Math.Cos(rotationInRadians);
            return new Point(positionedX, positionedY);
        }

        private static Point RelativeToAbsolutePoint(Point point, Point canvasCenter, double canvasRadius)
        {
            return new Point(canvasCenter.X + point.X*canvasRadius, canvasCenter.Y - point.Y * canvasRadius);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}