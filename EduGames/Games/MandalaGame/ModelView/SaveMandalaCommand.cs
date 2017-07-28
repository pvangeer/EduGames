using System;
using System.Windows.Input;

namespace EduGames.Games.MandalaGame.ModelView
{
    public class SaveMandalaCommand : ICommand
    {
        private readonly MandalaRibbonViewModel viewModel;

        public SaveMandalaCommand(MandalaRibbonViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            viewModel.RaisSaveMandalaRequest();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}