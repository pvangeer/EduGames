using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Games.MandalaGamePlugin.Annotations;
using Games.MandalaGamePlugin.Model;
using Games.MandalaGamePlugin.Sandbox;

namespace Games.MandalaGamePlugin.ModelView
{
    public class MandalaViewModel : INotifyPropertyChanged
    {
        private readonly Mandala mandala;
        public MandalaViewModel() : this(new Mandala()) { }

        public MandalaViewModel(Mandala mandala)
        {
            this.mandala = mandala;
            if (mandala != null)
            {
                CircleList = CreateCircleList();
                GridLinesList = CreateGridLinesList();
                mandala.PropertyChanged += MandalaPropertyChanged;
                mandala.Elements.CollectionChanged += MandalaElementsCollectionChanged;
            }

            Elements = new ObservableCollection<ElementViewModel>();
            DrawObjectViewModel = new MandalaInteractiveDrawObjectViewModel();
        }

        public ObservableCollection<ElementViewModel> Elements { get; }

        private void MandalaElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.CircularGridResolution):
                    circleList = CreateCircleList();
                    OnPropertyChanged(nameof(CircleList));
                    OnPropertyChanged(nameof(CircularGridResolution));
                    break;
                case nameof(Mandala.BackgroundColor):
                    OnPropertyChanged(nameof(BackgroundColor));
                    break;
                case nameof(Mandala.GridBrushStrokeColor):
                    circleList = CreateCircleList();
                    gridLinesList = CreateGridLinesList();
                    OnPropertyChanged(nameof(CircleList));
                    OnPropertyChanged(nameof(GridLinesList));
                    OnPropertyChanged(nameof(GridBrushStrokeColor));
                    break;
                case nameof(Mandala.GridBrushStrokeThickness):
                    circleList = CreateCircleList();
                    gridLinesList = CreateGridLinesList();
                    OnPropertyChanged(nameof(CircleList));
                    OnPropertyChanged(nameof(GridLinesList));
                    OnPropertyChanged(nameof(GridBrushStrokeThickness));
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

        private ObservableCollection<GridLineViewModel> CreateGridLinesList()
        {
            var list = new ObservableCollection<GridLineViewModel>();
            var dRotation = 360.0 / mandala.MandalaGridResolution;
            for (int i = 0; i < mandala.MandalaGridResolution; i++)
            {
                list.Add(new GridLineViewModel(i * dRotation, mandala.GridBrushStrokeThickness, mandala.GridBrushStrokeColor));
            }

            return list;
        }

        private ObservableCollection<CircleViewModel> CreateCircleList()
        {
            var list = new ObservableCollection<CircleViewModel>();
            var d = 1.0 / mandala.CircularGridResolution;
            for (int i = 0; i < mandala.CircularGridResolution; i++)
            {
                list.Add(new CircleViewModel((i + 1) * d, mandala.GridBrushStrokeThickness, mandala.GridBrushStrokeColor));
            }

            return list;
        }

        private ObservableCollection<CircleViewModel> circleList;
        private ObservableCollection<GridLineViewModel> gridLinesList;

        public ObservableCollection<CircleViewModel> CircleList
        {
            get => circleList;
            set
            {
                circleList = value;
                OnPropertyChanged(nameof(CircleList));
            }
        }

        public ObservableCollection<GridLineViewModel> GridLinesList
        {
            get => gridLinesList;
            set
            {
                gridLinesList = value;
                OnPropertyChanged(nameof(GridLinesList));
            }
        }

        public bool GridVisible => mandala.ShowGrid;

        public Color BackgroundColor => mandala.BackgroundColor;

        public int CircularGridResolution => mandala.CircularGridResolution;

        public Color GridBrushStrokeColor => mandala.GridBrushStrokeColor;

        public double GridBrushStrokeThickness => mandala.GridBrushStrokeThickness;

        public MandalaInteractiveDrawObjectViewModel DrawObjectViewModel { get; }

        public ICommand MouseDownHandler => new MouseDownCommand(this);

        public ICommand MouseMoveHandler => new MouseMoveCommand(this);

        public ICommand MouseUpHandler => new MouseUpCommand(this);

        public Color PaintBrushStrokeColor => mandala.PaintBrushStrokeColor;

        public double PaintBrushStrokeThickness => mandala.PaintBrushStrokeThickness;

        public int MandalaGridResolution => mandala.MandalaGridResolution;

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

    public class PolygonElementViewModel : ElementViewModel
    {
        public PolygonElementViewModel(MandalaPolygonElement polygonElement)
        {
            PolygonElement = polygonElement;
        }

        public MandalaPolygonElement PolygonElement { get; set; }

        public double StrokeThickness => PolygonElement.StrokeThickness;

        public Brush StrokeColor => new SolidColorBrush(PolygonElement.StrokeColor);

        // TODO: Make separate viewmodel for rotated mandala elements and user property in converter
        public double Rotation { get; }
    }

    public class ElementViewModel
    {
    }
}
