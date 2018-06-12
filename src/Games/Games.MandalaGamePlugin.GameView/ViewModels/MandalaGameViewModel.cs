using System.ComponentModel;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.Properties;

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
            }

            GridCirclesViewModel = new GridCirclesViewModel(mandala);
            GridLinesViewModel = new GridLinesViewModel(mandala);
            DrawCanvasViewModel = new DrawCanvasViewModel(mandala);
            MandalaElementsViewModel = new MandalaElementsViewModel(mandala);
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.BackgroundColor):
                    OnPropertyChanged(nameof(BackgroundColor));
                    break;
            }
        }

        public SolidColorBrush BackgroundColor => new SolidColorBrush(mandala.BackgroundColor);

        public GridCirclesViewModel GridCirclesViewModel { get; }

        public GridLinesViewModel GridLinesViewModel { get; }

        public DrawCanvasViewModel DrawCanvasViewModel { get; }

        public MandalaElementsViewModel MandalaElementsViewModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
