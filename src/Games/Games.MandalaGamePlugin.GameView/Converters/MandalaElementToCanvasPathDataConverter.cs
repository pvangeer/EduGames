using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class MandalaElementToCanvasPathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 4 || !(values[0] is double width) || !(values[1] is double height) ||
                !(values[2] is IEnumerable<Point> relativePoints) || !(values[3] is int numberOfDuplications))
            {
                throw new NotImplementedException();
            }

            var relativePointsArray = relativePoints as Point[] ?? relativePoints.ToArray();
            if (!relativePointsArray.Any())
            {
                return null;
            }

            var group = new GeometryGroup();

            var canvasCenter = new Point(width / 2.0, height / 2.0);
            var canvasRadius = Math.Min(width, height) / 2.0;

            var dRotation = 360.0 / numberOfDuplications;
            for (int i = 0; i < numberOfDuplications; i++)
            {
                var rotation = i * dRotation;
                var pointsToAddToGeometry = relativePointsArray.Select(p => RotatePoint(p, rotation)).ToArray();

                Point start = RelativeToAbsolutePoint(pointsToAddToGeometry[0], canvasCenter, canvasRadius);

                List<LineSegment> segments = new List<LineSegment>();
                for (int ip = 1; ip < pointsToAddToGeometry.Length; ip++)
                {
                    segments.Add(new LineSegment(RelativeToAbsolutePoint(pointsToAddToGeometry[ip], canvasCenter, canvasRadius), true));
                }
                PathFigure figure = new PathFigure(start, segments, false); //true if closed
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                group.Children.Add(geometry);
            }

            return group;
        }

        private Point RotatePoint(Point point, double rotation)
        {
            var rotationRadians = Math.PI / 180 * rotation;
            var positionedX = point.X * Math.Cos(rotationRadians) - point.Y * Math.Sin(rotationRadians);
            var positionedY = point.X * Math.Sin(rotationRadians) + point.Y * Math.Cos(rotationRadians);
            return new Point(positionedX, positionedY);
        }

        private static Point RelativeToAbsolutePoint(Point point, Point canvasCenter, double canvasRadius)
        {
            return new Point(canvasCenter.X + point.X * canvasRadius, canvasCenter.Y - point.Y * canvasRadius);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}