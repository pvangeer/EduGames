using System;
using System.Windows.Input;
using Games.MandalaGamePlugin.ModelView;

namespace Games.MandalaGamePlugin.Sandbox
{
    public abstract class MouseCommandBase : ICommand
    {
        protected readonly MandalaViewModel MandalaViewModel;

        protected MouseCommandBase(MandalaViewModel mandalaViewModel)
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