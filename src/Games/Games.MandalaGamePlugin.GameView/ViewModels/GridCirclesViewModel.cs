using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Games.MandalaGamePlugin.GameView.Properties;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class GridCirclesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CircleViewModel> circleList;

        private readonly Mandala mandala;
        public GridCirclesViewModel() : this(new Mandala()) { }

        public GridCirclesViewModel(Mandala mandala)
        {
            this.mandala = mandala;
            if (mandala != null)
            {
                CircleList = CreateCircleList();
                mandala.PropertyChanged += MandalaPropertyChanged;
            }
        }

        public ObservableCollection<CircleViewModel> CircleList
        {
            get => circleList;
            set
            {
                circleList = value;
                OnPropertyChanged(nameof(CircleList));
            }
        }

        public bool GridVisible => mandala.ShowGrid;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.CircularGridResolution):
                    circleList = CreateCircleList();
                    OnPropertyChanged(nameof(CircleList));
                    break;
                case nameof(Mandala.GridBrushStrokeColor):
                    circleList = CreateCircleList();
                    OnPropertyChanged(nameof(CircleList));
                    break;
                case nameof(Mandala.GridBrushStrokeThickness):
                    circleList = CreateCircleList();
                    OnPropertyChanged(nameof(CircleList));
                    break;
                case nameof(Mandala.ShowGrid):
                    OnPropertyChanged(nameof(GridVisible));
                    break;
            }
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
    }
}
