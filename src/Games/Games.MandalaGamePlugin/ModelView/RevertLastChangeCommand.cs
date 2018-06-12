using System;
using System.Collections.Specialized;
using System.Windows.Input;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.ModelView
{
    public class RevertLastChangeCommand : ICommand
    {
        private readonly Mandala mandala;

        public RevertLastChangeCommand(Mandala mandala)
        {
            this.mandala = mandala;
            mandala.Elements.CollectionChanged += RaiseCanExecuteChanged;
        }

        public void Execute(object parameter)
        {
            mandala?.Elements.RemoveAt(mandala.Elements.Count-1);
        }

        public bool CanExecute(object parameter)
        {
            return mandala != null && mandala.Elements.Count > 0;
        }

        private void RaiseCanExecuteChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CanExecuteChanged?.Invoke(this, null);
        }

        public event EventHandler CanExecuteChanged;
    }
}