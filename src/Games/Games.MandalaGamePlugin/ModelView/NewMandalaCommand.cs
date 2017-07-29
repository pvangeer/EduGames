using System;
using System.Windows.Input;

namespace Games.MandalaGamePlugin.ModelView
{
    public class NewMandalaCommand : ICommand
    {
        private readonly MandalaRibbonViewModel mandalaRibbonViewModel;

        public NewMandalaCommand(MandalaRibbonViewModel mandalaRibbonViewModel)
        {
            this.mandalaRibbonViewModel = mandalaRibbonViewModel;
        }

        public void Execute(object parameter)
        {
            mandalaRibbonViewModel.RaiseNewMandalaRequested();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}