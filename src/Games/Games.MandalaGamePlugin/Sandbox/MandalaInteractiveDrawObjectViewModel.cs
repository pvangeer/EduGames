using System.Collections.ObjectModel;
using System.Windows;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class MandalaInteractiveDrawObjectViewModel
    {
        public MandalaInteractiveDrawObjectViewModel()
        {
            PositionsList = new ObservableCollection<Point>();
        }

        public bool IsDrawing { get; set; }

        public ObservableCollection<Point> PositionsList { get; }
    }
}