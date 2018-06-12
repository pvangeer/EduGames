using System;
using System.Windows;
using System.Windows.Input;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public abstract class MouseCommandBase : ICommand
    {
        protected readonly DrawCanvasViewModel MandalaViewModel;

        protected MouseCommandBase(DrawCanvasViewModel mandalaViewModel)
        {
            MandalaViewModel = mandalaViewModel;
        }

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        protected static Point GetRelativeMousePosition(MouseEventArgs mouseEventArgs, FrameworkElement frameworkElement)
        {
            var mousePosition = mouseEventArgs.GetPosition((IInputElement)mouseEventArgs.Source);
            var width = frameworkElement.ActualWidth;
            var height = frameworkElement.ActualHeight;

            var minWidthHeightRadius = Math.Min(width / 2.0, height / 2.0);
            var relativeX = (mousePosition.X - width / 2.0) / minWidthHeightRadius;
            var relativeY = (height / 2.0 - mousePosition.Y) / minWidthHeightRadius;
            var point = new Point(relativeX, relativeY);
            return point;
        }
    }
}