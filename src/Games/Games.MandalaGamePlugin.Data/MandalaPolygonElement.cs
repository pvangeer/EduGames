using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.Data
{
    public class MandalaPolygonElement : IMandalaElement
    {
        public string DisplayName => $"Getrokken lijn ({Points?.Count() ?? 0} punten)";

        public IEnumerable<Point> Points;

        public Color StrokeColor { get; set; }

        public double StrokeThickness { get; set; }

        public int NumberOfDubplications { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}