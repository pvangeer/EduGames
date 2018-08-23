using System.Windows;
using System.Windows.Input;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public class MouseDownCommand : MouseCommandBase
    {
        public MouseDownCommand(DrawCanvasViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            if (!(parameter is MouseEventArgs eventArgs))
            {
                return;
            }

            if (!(eventArgs.Source is FrameworkElement frameworkElement) || eventArgs.LeftButton == MouseButtonState.Released)
            {
                eventArgs.Handled = false;
                return;
            }
            
            frameworkElement.CaptureMouse();

            MandalaViewModel.PositionsList.Clear();
            MandalaViewModel.IsDrawing = true;
            MandalaViewModel.OnPropertyChanged(nameof(MandalaViewModel.IsDrawing));

            MandalaViewModel.PositionsList.Add(GetRelativeMousePosition(eventArgs, frameworkElement));
        }
    }
}