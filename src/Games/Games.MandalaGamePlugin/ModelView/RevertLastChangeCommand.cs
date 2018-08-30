using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.ModelView
{
    public class RevertLastChangeCommand : ICommand
    {
        private readonly Mandala mandala;
        private MandalaRibbonViewModel viewModel;

        public RevertLastChangeCommand(Mandala mandala, MandalaRibbonViewModel mandalaRibbonViewModel)
        {
            this.mandala = mandala;
            this.viewModel = mandalaRibbonViewModel;
            viewModel.PropertyChanged += ViewModelPropertyChanged;
            mandala.Elements.CollectionChanged += RaiseCanExecuteChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MandalaRibbonViewModel.SelectedElement))
            {
                RaiseCanExecuteChanged(this,null);
            }
        }

        public void Execute(object parameter)
        {
            mandala?.Elements.RemoveAt(mandala.Elements.IndexOf(viewModel.SelectedElement));
        }

        public bool CanExecute(object parameter)
        {
            return mandala != null && mandala.Elements.Count > 0 && viewModel.SelectedElement != null;
        }

        private void RaiseCanExecuteChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CanExecuteChanged?.Invoke(this, null);
        }

        public event EventHandler CanExecuteChanged;
    }
}