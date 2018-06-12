using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Test
{
    public class GridCirclesTestViewModel : GridCirclesViewModel
    {
        static Mandala TestMandala = new Mandala();

        public GridCirclesTestViewModel() : base(TestMandala)
        {
            TestMandala.GridBrushStrokeColor = Colors.Red;
            TestMandala.CircularGridResolution = 10;
        }
    }
}