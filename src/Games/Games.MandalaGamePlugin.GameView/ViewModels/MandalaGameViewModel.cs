using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.Properties;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class MandalaGameViewModel : INotifyPropertyChanged
    {
        private readonly Mandala mandala;
        public MandalaGameViewModel() : this(new Mandala()) { }

        public MandalaGameViewModel(Mandala mandala)
        {
            this.mandala = mandala;
            if (mandala != null)
            {
                mandala.PropertyChanged += MandalaPropertyChanged;
                /*mandala.Elements.CollectionChanged += MandalaElementsCollectionChanged;*/
            }

            GridCirclesViewModel = new GridCirclesViewModel(mandala);
            GridLinesViewModel = new GridLinesViewModel(mandala);

            /*Elements = new ObservableCollection<ElementViewModel>();*/
            DrawObjectViewModel = new MandalaInteractiveDrawObjectViewModel(mandala);
        }

        /*public ObservableCollection<ElementViewModel> Elements { get; }*/

        /* private void MandalaElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
         {
             if (e.Action == NotifyCollectionChangedAction.Add)
             {
                 var mandalaElement = e.NewItems.OfType<IMandalaElement>().First();
                 Elements.Insert(mandala.Elements.IndexOf(mandalaElement),CreateElementViewModel(mandalaElement));
             }

             if (e.Action == NotifyCollectionChangedAction.Remove)
             {
                 var mandalaElement = e.OldItems.OfType<IMandalaElement>().First();
                 Elements.RemoveAt(mandala.Elements.IndexOf(mandalaElement));
             }
         }

         private ElementViewModel CreateElementViewModel(IMandalaElement mandalaElement)
         {
             if (mandalaElement is MandalaPolygonElement polygonElement)
             {
                 return new PolygonElementViewModel(polygonElement);
             }
             throw new NotImplementedException();
         }
 */

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.BackgroundColor):
                    OnPropertyChanged(nameof(BackgroundColor));
                    break;
                case nameof(Mandala.PaintBrushStrokeColor):
                    OnPropertyChanged(nameof(PaintBrushStrokeColor));
                    break;
                case nameof(Mandala.PaintBrushStrokeThickness):
                    OnPropertyChanged(nameof(PaintBrushStrokeThickness));
                    break;
                case nameof(Mandala.MandalaGridResolution):
                    OnPropertyChanged(nameof(MandalaGridResolution));
                    break;
            }
        }

        public Color BackgroundColor => mandala.BackgroundColor;

        public MandalaInteractiveDrawObjectViewModel DrawObjectViewModel { get; }

        public Color PaintBrushStrokeColor => mandala.PaintBrushStrokeColor;

        public double PaintBrushStrokeThickness => mandala.PaintBrushStrokeThickness;

        public int MandalaGridResolution => mandala.MandalaGridResolution;

        public GridCirclesViewModel GridCirclesViewModel { get; }

        public GridLinesViewModel GridLinesViewModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddNewMandalaElement(IMandalaElement item)
        {
            mandala.Elements.Add(item);
        }
    }
}
