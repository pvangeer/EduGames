using System.Windows;
using System.Windows.Media;
using Games.MandalaGamePlugin.GameView.ViewModels;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.GameView.Test
{
    public class DrawCanvasTestViewModel : DrawCanvasViewModel
    {
        public static Mandala TestMandala = new Mandala();

        public DrawCanvasTestViewModel() : base(TestMandala)
        {
            PositionsList.Add(new Point(0.23, 0.489));
            PositionsList.Add(new Point(0.69, 0.12));
            PositionsList.Add(new Point(0, 0.12));
            PositionsList.Add(new Point(-0.19, 0.1952));
            PositionsList.Add(new Point(-0.264, -0.993));
            IsDrawing = true;

            TestMandala.PaintBrushStrokeColor = Colors.Aqua;
            TestMandala.MandalaGridResolution = 6;
        }
    }
}