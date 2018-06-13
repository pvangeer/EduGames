using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.Data
{
    public class Mandala : INotifyPropertyChanged
    {
        private int circularGridResolution = 10;
        private int mandalaGridResolution = 6;
        private bool showGrid = true;
        private int currentElementStrokeThickness = 3;
        private Color currentElementColor = Colors.Green;
        private double paintBrushStrokeThickness = 1;
        private Color paintBrushStrokeColor = Colors.Purple;
        private double gridBrushStrokeThickness = 1;
        private Color gridBrushStrokeColor = Colors.Gray;
        private Color backgroundColor = Colors.LightYellow;
    
        public Mandala()
        {
            Elements = new ObservableCollection<IMandalaElement>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IMandalaElement> Elements { get; }

        #region Settings
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public int CircularGridResolution
        {
            get => circularGridResolution;
            set
            {
                circularGridResolution = value;
                OnPropertyChanged(nameof(CircularGridResolution));
            }
        }

        public int MandalaGridResolution
        {
            get => mandalaGridResolution;
            set
            {
                mandalaGridResolution = value;
                OnPropertyChanged(nameof(MandalaGridResolution));
            }
        }

        public bool ShowGrid
        {
            get => showGrid;
            set
            {
                showGrid = value;
                OnPropertyChanged(nameof(ShowGrid));
            }
        }

        public int CurrentElementStrokeThickness
        {
            get => currentElementStrokeThickness;
            set
            {
                currentElementStrokeThickness = value;
                OnPropertyChanged(nameof(CurrentElementStrokeThickness));
            }
        }

        public Color CurrentElementColor
        {
            get => currentElementColor;
            set
            {
                currentElementColor = value;
                OnPropertyChanged(nameof(CurrentElementColor));
            }
        }

        public double PaintBrushStrokeThickness
        {
            get => paintBrushStrokeThickness;
            set
            {
                paintBrushStrokeThickness = value;
                OnPropertyChanged(nameof(PaintBrushStrokeThickness));
            }
        }

        public Color PaintBrushStrokeColor
        {
            get => paintBrushStrokeColor;
            set
            {
                paintBrushStrokeColor = value;
                OnPropertyChanged(nameof(PaintBrushStrokeColor));
            }
        }

        public double GridBrushStrokeThickness
        {
            get => gridBrushStrokeThickness;
            set
            {
                gridBrushStrokeThickness = value;
                OnPropertyChanged(nameof(GridBrushStrokeThickness));
            }
        }

        public Color GridBrushStrokeColor
        {
            get => gridBrushStrokeColor;
            set
            {
                gridBrushStrokeColor = value;
                OnPropertyChanged(nameof(GridBrushStrokeColor));
            }
        }

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
