using System.ComponentModel;
using System.Windows.Media;
using EduGames.Annotations;

namespace EduGames.Games.MandalaGame.ModelView
{
    public class LineShapeObject : IShapeObject, INotifyPropertyChanged
    {
        private Color color;
        private double strokeThickness;
        private Transform renderTransform;

        public LineShapeObject(double x1, double y1, double x2, double y2, Color color, double strokeThickness, Transform renderTransform, DoubleCollection strokeDashArray = null)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Color = color;
            StrokeThickness = strokeThickness;
            RenderTransform = renderTransform;
            StrokeDashArray = strokeDashArray;
        }

        public double X1 { get; }

        public double X2 { get; }

        public double Y1 { get; }

        public double Y2 { get; }

        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public double StrokeThickness
        {
            get { return strokeThickness; }
            set
            {
                strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
            }
        }

        public DoubleCollection StrokeDashArray { get; }

        public Transform RenderTransform
        {
            get { return renderTransform; }
            set
            {
                renderTransform = value;
                OnPropertyChanged(nameof(RenderTransform));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}