using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Games.MandalaGamePlugin.Annotations;
using Games.MandalaGamePlugin.Data;

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

        public bool GridVisible => mandala.ShowGrid;

        public Color BackgroundColor => mandala.BackgroundColor;

        public int CircularGridResolution => mandala.CircularGridResolution;

        public Color GridBrushStrokeColor => mandala.GridBrushStrokeColor;

        public double GridBrushStrokeThickness => mandala.GridBrushStrokeThickness;

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

    public class MandalaElementDrawObject
    {
        public MandalaElementDrawObject(Point[] points, double strokeThickness, Brush strokeBrush, double rotation)
        {
            Points = points;
            StrokeThickness = strokeThickness;
            StrokeColor = strokeBrush;
            Rotation = rotation;
        }

        public Point[] Points { get; }

        public double StrokeThickness { get; }

        public Brush StrokeColor { get; }

        // TODO: Make separate viewmodel for rotated mandala elements and user property in converter
        public double Rotation { get; }
    }

    public class PolygonElementViewModel : ElementViewModel
    {
        public PolygonElementViewModel(MandalaPolygonElement polygonElement)
        {
            throw new NotImplementedException();
        }
    }

    public class ElementViewModel
    {
    }
}
