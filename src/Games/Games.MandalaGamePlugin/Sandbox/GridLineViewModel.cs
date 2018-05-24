using System.Windows.Media;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class GridLineViewModel
    {
        public GridLineViewModel(double rotation, double strokeThickness, Color strokeColor)
        {
            Rotation = rotation;
            StrokeThickness = strokeThickness;
            StrokeColor = new SolidColorBrush(strokeColor);
        }

        public double StrokeThickness { get; }

        public Brush StrokeColor { get; }

        public double Rotation { get; }
    }
}