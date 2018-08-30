using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class GridLinesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GridLineViewModel> gridLinesList;
        private Mandala mandala;

        public GridLinesViewModel(Mandala mandala)
        {
            this.mandala = mandala;
            if (mandala != null)
            {
                GridLinesList = CreateGridLinesList();
                mandala.PropertyChanged += MandalaPropertyChanged;
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

        public bool MandalaGridVisible => mandala.ShowMandalaGrid;

        public double Margin => mandala.Margin;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.GridBrushStrokeColor):
                    gridLinesList = CreateGridLinesList();
                    OnPropertyChanged(nameof(GridLinesList));
                    break;
                case nameof(Mandala.GridBrushStrokeThickness):
                    gridLinesList = CreateGridLinesList();
                    OnPropertyChanged(nameof(GridLinesList));
                    break;
                case nameof(Mandala.MandalaGridResolution):
                    gridLinesList = CreateGridLinesList();
                    OnPropertyChanged(nameof(GridLinesList));
                    break;
                case nameof(Mandala.ShowMandalaGrid):
                    OnPropertyChanged(nameof(MandalaGridVisible));
                    break;
                case nameof(Mandala.Margin):
                    OnPropertyChanged(nameof(Margin));
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
    }
}
