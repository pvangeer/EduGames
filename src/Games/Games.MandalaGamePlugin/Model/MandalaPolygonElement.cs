using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.Model
{
    public class MandalaPolygonElement : IMandalaElement
    {
        public MandalaPolygonElement(string name)
        {
            Type = name;
        }

        public string DisplayName => $"{Type} ({Points?.Count() ?? 0} punten)";

        public string Type { get; private set; }

        public IEnumerable<Point> Points;

        public Color StrokeColor { get; set; }

        public double StrokeThickness { get; set; }

        public int NumberOfDubplications { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}