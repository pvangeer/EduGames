using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Games.MandalaGamePlugin.Annotations;
using Games.MandalaGamePlugin.Model;

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
                mandala.PropertyChanged += MandalaPropertyChanged;
            }
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.CircularGridResolution):
                    OnPropertyChanged(nameof(CircularGridResolution));
                    break;
                case nameof(Mandala.BackgroundColor):
                    OnPropertyChanged(nameof(BackgroundColor));
                    break;
                case nameof(Mandala.GridBrushStrokeColor):
                    OnPropertyChanged(nameof(GridBrushStrokeColor));
                    break;
                case nameof(Mandala.GridBrushStrokeThickness):
                    OnPropertyChanged(nameof(GridBrushStrokeThickness));
                    break;
            }
        }

        public Color BackgroundColor => mandala.BackgroundColor;

        public int CircularGridResolution => mandala.CircularGridResolution;

        public Color GridBrushStrokeColor => mandala.GridBrushStrokeColor;

        public double GridBrushStrokeThickness => mandala.GridBrushStrokeThickness;

        public ObservableCollection<GridCircleViewModel> TestItems => new ObservableCollection<GridCircleViewModel>()
        {
            new GridCircleViewModel(1.0, Colors.Black, 2),
            new GridCircleViewModel(0.8, Colors.Black, 2),
            new GridCircleViewModel(0.5, Colors.Black, 2)
        };

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
