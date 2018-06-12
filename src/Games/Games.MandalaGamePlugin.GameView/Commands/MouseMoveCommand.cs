using System;
using System.Windows;
using System.Windows.Input;
using Games.MandalaGamePlugin.GameView.MousePositionTracker;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public class MouseMoveCommand : MouseCommandBase
    {
        public MouseMoveCommand(DrawCanvasViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            if (!(parameter is MouseEventArgs eventArgs) || !(eventArgs.Source is FrameworkElement frameworkElement))
            {
                return;
            }

            MandalaViewModel.PositionsList.Add(GetRelativeMousePosition(eventArgs, frameworkElement));
            MandalaViewModel.OnPropertyChanged(nameof(MandalaViewModel.PositionsList));
        }

        public override bool CanExecute(object parameter)
        {
            return MandalaViewModel.IsDrawing;
        }
    }
}