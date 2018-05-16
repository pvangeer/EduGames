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
                mandala.PropertyChanged += MandalaPropertyChanged;
            }
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.CircularGridResolution):
                case nameof(Mandala.GridBrushStrokeThickness):
                case nameof(Mandala.GridBrushStrokeColor):
                    circleList = CreateCircleList();
                    RaisePropertyChanged(nameof(CircleList));
                    break;
                case nameof(Mandala.BackgroundColor):
                    RaisePropertyChanged(nameof(BackgroundColor));
                    break;
            }

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
        private Mandala mandala;

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

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}