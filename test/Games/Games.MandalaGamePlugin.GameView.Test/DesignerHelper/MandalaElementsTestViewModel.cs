using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Test.DesignerHelper
{
    public class MandalaElementsTestViewModel : MandalaElementsViewModel
    {
        public static readonly Mandala TestMandala = new Mandala();

        public MandalaElementsTestViewModel() : base(TestMandala)
        {
            TestMandala.Elements.Add(new MandalaPolygonElement
            {
                NumberOfDubplications = 6,
                Points = new List<Point>
                {
                    new Point(0.1,0.2),
                    new Point(0.4,0.3)
                },
                StrokeThickness = 2,
                StrokeColor = Colors.Red
            });
        }
    }
}