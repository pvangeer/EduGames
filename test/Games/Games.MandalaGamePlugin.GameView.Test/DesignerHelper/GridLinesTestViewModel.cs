using System.Windows.Media;
using Games.MandalaGamePlugin.GameView.ViewModels;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.GameView.Test
{
    public class GridLinesTestViewModel : GridLinesViewModel
    {
        public static Mandala TestMandala = new Mandala();

        public GridLinesTestViewModel() : base(TestMandala)
        {
            TestMandala.GridBrushStrokeColor = Colors.Red;
            TestMandala.GridBrushStrokeThickness = 14;
            TestMandala.MandalaGridResolution = 20;
        }
    }
}