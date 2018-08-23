using System;
using System.Windows.Input;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public class MandalaElementMouseDownCommand : ICommand
    {
        private readonly PolygonElementViewModel viewModel;

        public MandalaElementMouseDownCommand(PolygonElementViewModel polygonElementViewModel)
        {
            this.viewModel = polygonElementViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.EditElementProperties = true;
            viewModel.OnPropertyChanged(nameof(viewModel.EditElementProperties));
        }

        public event EventHandler CanExecuteChanged;
    }
}