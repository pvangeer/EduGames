using System.Windows.Media;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin
{
    public class GridCircleViewModel
    {
        public GridCircleViewModel(double fraction, Color strokeColor, double strokeThickness)
        {
            Fraction = fraction;
            GridBrushStrokeColor = strokeColor;
            GridStrokeThickness = strokeThickness;
        }

        public double Fraction { get; }

        public double GridStrokeThickness { get; }

        public Color GridBrushStrokeColor { get; }
    }
}