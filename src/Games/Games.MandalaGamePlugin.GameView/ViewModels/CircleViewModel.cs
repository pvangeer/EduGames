using System.Windows.Media;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class CircleViewModel
    {
        public CircleViewModel(double diameter, double strokeThickness, Color strokeColor)
        {
            Diameter = diameter;
            StrokeColor = new SolidColorBrush(strokeColor);
            StrokeThickness = strokeThickness;
        }

        public double Diameter { get; }

        public double StrokeThickness { get; }

        public SolidColorBrush StrokeColor { get; }
    }
}