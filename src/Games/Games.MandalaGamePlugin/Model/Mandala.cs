using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Games.MandalaGamePlugin.Model
{
    public class Mandala : INotifyPropertyChanged
    {
        private int circularGridResolution;
        private int mandalaGridResolution;
        private bool showGrid;
        private int currentElementStrokeThickness;
        private Color currentElementColor;
        private double paintBrushStrokeThickness;
        private Color paintBrushStrokeColor;
        private double gridBrushStrokeThickness;
        private Color gridBrushStrokeColor;
        private Color backgroundColor;
    
        public Mandala()
        {
            Elements = new ObservableCollection<IMandalaElement>();
            BackgroundColor = Colors.LightYellow;
            CircularGridResolution = 10;
            MandalaGridResolution = 6;
            PaintBrushStrokeColor = Colors.Purple;
            PaintBrushStrokeThickness = 1;
            GridBrushStrokeColor = Colors.Gray;
            GridBrushStrokeThickness = 1;
            CurrentElementColor = Colors.Green;
            CurrentElementStrokeThickness = 3;
            ShowGrid = true;
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
