using System.Linq;
using System.Windows;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public class MouseUpCommand : MouseCommandBase
    {
        public MouseUpCommand(DrawCanvasViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            if (!(parameter is FrameworkElement frameworkElement) || !MandalaViewModel.IsDrawing)
            {
                return;
            }

            MandalaViewModel.AddNewMandalaElement(new MandalaPolygonElement
            {
                Points = MandalaViewModel.PositionsList.ToList()
            });

            MandalaViewModel.PositionsList.Clear();
            MandalaViewModel.IsDrawing = false;
            MandalaViewModel.OnPropertyChanged(nameof(MandalaViewModel.IsDrawing));

            frameworkElement.ReleaseMouseCapture();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}