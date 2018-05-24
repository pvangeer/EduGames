using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class MandalaGridViewModel : INotifyPropertyChanged
    {
        public MandalaGridViewModel() : this(new Mandala()) { }

        public MandalaGridViewModel(Mandala mandala)
        {
            this.mandala = mandala;

            if (mandala != null)
            {
                CircleList = CreateCircleList();
                GridLinesList = CreateGridLinesList();
                mandala.PropertyChanged += MandalaPropertyChanged;
            }
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.CircularGridResolution):
                    circleList = CreateCircleList();
                    RaisePropertyChanged(nameof(CircleList));
                    break;
                case nameof(Mandala.GridBrushStrokeThickness):
                case nameof(Mandala.GridBrushStrokeColor):
                    circleList = CreateCircleList();
                    gridLinesList = CreateGridLinesList();
                    RaisePropertyChanged(nameof(CircleList));
                    RaisePropertyChanged(nameof(GridLinesList));
                    break;
                case nameof(Mandala.BackgroundColor):
                    RaisePropertyChanged(nameof(BackgroundColor));
                    break;
                case nameof(Mandala.ShowGrid):
                    RaisePropertyChanged(nameof(GridVisible));
                    break;
            }

        }

        private ObservableCollection<GridLineViewModel> CreateGridLinesList()
        {
            var list = new ObservableCollection<GridLineViewModel>();
            var dRotation = 360.0 / mandala.MandalaGridResolution;
            for (int i = 0; i < mandala.MandalaGridResolution; i++)
            {
                list.Add(new GridLineViewModel(i*dRotation,mandala.GridBrushStrokeThickness, mandala.GridBrushStrokeColor));
            }

            return list;
        }

        private ObservableCollection<CircleViewModel> CreateCircleList()
        {
            var list = new ObservableCollection<CircleViewModel>();
            var d = 1.0 / mandala.CircularGridResolution;
            for (int i = 0; i < mandala.CircularGridResolution; i++)
            {
                list.Add(new CircleViewModel((i+1) * d,mandala.GridBrushStrokeThickness, mandala.GridBrushStrokeColor));
            }

            return list;
        }

        private ObservableCollection<CircleViewModel> circleList;
        private ObservableCollection<GridLineViewModel> gridLinesList;
        private readonly Mandala mandala;

        public ObservableCollection<CircleViewModel> CircleList
        {
            get => circleList;
            set
            {
                circleList = value;
                RaisePropertyChanged("CircleList");
            }
        }

        public SolidColorBrush BackgroundColor => new SolidColorBrush(mandala.BackgroundColor);

        public ObservableCollection<GridLineViewModel> GridLinesList
        {
            get => gridLinesList;
            set
            {
                gridLinesList = value;
                RaisePropertyChanged("GridLinesList");
            }
        }

        public bool GridVisible => mandala.ShowGrid;

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}