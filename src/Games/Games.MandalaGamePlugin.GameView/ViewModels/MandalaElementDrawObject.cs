using System.Windows;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class MandalaElementDrawObject
    {
        public MandalaElementDrawObject(Point[] points, double strokeThickness, Brush strokeBrush, double rotation)
        {
            Points = points;
            StrokeThickness = strokeThickness;
            StrokeColor = strokeBrush;
            Rotation = rotation;
        }

        public Point[] Points { get; }

        public double StrokeThickness { get; }

        public Brush StrokeColor { get; }

        public double Rotation { get; }
    }
}