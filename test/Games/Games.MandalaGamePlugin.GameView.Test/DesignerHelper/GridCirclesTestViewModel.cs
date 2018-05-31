using System.Windows.Media;
using Games.MandalaGamePlugin.GameView.ViewModels;
using Games.MandalaGamePlugin.Model;

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