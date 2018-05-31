using System;
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
    }
}