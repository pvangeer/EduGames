using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class MandalaElementsViewModel : INotifyPropertyChanged
    {
        private readonly Mandala mandala;
        private ObservableCollection<ElementViewModel> mandalaElementsList = new ObservableCollection<ElementViewModel>();

        public MandalaElementsViewModel(Mandala mandala)
        {
            this.mandala = mandala;
            if (mandala != null)
            {
                mandala.Elements.CollectionChanged += MandalaElementsCollectionChanged;
                MandalaElementsList = new ObservableCollection<ElementViewModel>(mandala.Elements.Select(CreateMandalaElementViewModel));
            }
        }

        private void MandalaElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var mandalaElement in e.NewItems.OfType<IMandalaElement>())
                {
                    MandalaElementsList.Add(CreateMandalaElementViewModel(mandalaElement));
                }
                OnPropertyChanged(nameof(MandalaElementsList));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var mandalaElement in e.OldItems)
                {
                    MandalaElementsList.Remove(MandalaElementsList.First(vm => vm.MandalaElement == mandalaElement));
                }
                OnPropertyChanged(nameof(MandalaElementsList));
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                MandalaElementsList.Clear();
                OnPropertyChanged(nameof(MandalaElementsList));
            }
        }

        public ObservableCollection<ElementViewModel> MandalaElementsList
        {
            get => mandalaElementsList;
            private set
            {
                mandalaElementsList = value;
                OnPropertyChanged(nameof(MandalaElementsList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ElementViewModel CreateMandalaElementViewModel(IMandalaElement element)
        {
            if (element is MandalaPolygonElement polygonElement)
            {
                return new PolygonElementViewModel(polygonElement);
            }

            throw new NotImplementedException();
        }
    }
}
