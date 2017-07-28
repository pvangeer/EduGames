using System.ComponentModel;
using System.Windows.Media;
using EduGames.Annotations;

namespace EduGames.Games.MandalaGame.ModelView
{
    internal class EllipseShapeObject : IShapeObject, INotifyPropertyChanged
    {
        private Transform renderTransform;
        private double strokeThickness;
        private Color color;
        private double radius;
        private DoubleCollection strokeDashArray;

        public EllipseShapeObject(double radius)
        {
            this.Radius = radius;
        }

        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                OnPropertyChanged(nameof(Radius));
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

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

        public Transform RenderTransform
        {
            get { return renderTransform; }
            set
            {
                renderTransform = value;
                OnPropertyChanged(nameof(RenderTransform));
            }
        }

        public DoubleCollection StrokeDashArray
        {
            get { return strokeDashArray; }
            set
            {
                strokeDashArray = value; 
                OnPropertyChanged(nameof(StrokeDashArray));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}