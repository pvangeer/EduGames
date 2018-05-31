using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Games.MandalaGamePlugin.Annotations;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class MandalaInteractiveDrawObjectViewModel : INotifyPropertyChanged
    {
        public MandalaInteractiveDrawObjectViewModel() : this(new Mandala()) { }

        public MandalaInteractiveDrawObjectViewModel(Mandala mandala)
        {
            PositionsList = new ObservableCollection<Point>();
            Mandala = mandala;
            if (mandala != null)
            {
                mandala.PropertyChanged += MandalaPropertyChanged;
            }
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.MandalaGridResolution):
                    OnPropertyChanged(nameof(MandalaGridResolution));
                    break;
                case nameof(Mandala.CurrentElementColor):
                    OnPropertyChanged(nameof(CurrentElementColor));
                    break;
                case nameof(Mandala.CurrentElementStrokeThickness):
                    OnPropertyChanged(nameof(CurrentElementStrokeThickness));
                    break;
            }
        }

        public bool IsDrawing { get; set; }

        public ObservableCollection<Point> PositionsList { get; }

        public int MandalaGridResolution => Mandala.MandalaGridResolution;

        private Mandala Mandala { get; }

        public SolidColorBrush CurrentElementColor => new SolidColorBrush(Mandala.CurrentElementColor);

        public double CurrentElementStrokeThickness => Mandala.CurrentElementStrokeThickness;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}