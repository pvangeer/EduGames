using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.Properties;
using Games.MandalaGamePlugin.GameView.Commands;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class PolygonElementViewModel : ElementViewModel, INotifyPropertyChanged
    {
        private readonly MandalaPolygonElement polygonElement;

        public PolygonElementViewModel(MandalaPolygonElement polygonElement) : base(polygonElement)
        {
            this.polygonElement = polygonElement;
        }

        public IEnumerable<Point> ShapeData => polygonElement.Points;

        public SolidColorBrush StrokeColor => new SolidColorBrush(polygonElement.StrokeColor);

        public double StrokeThickness => polygonElement.StrokeThickness;

        public int NumberOfDuplications => polygonElement.NumberOfDubplications;

        public bool EditElementProperties { get; set; }

        public ICommand MouseDownHandler => new MandalaElementMouseDownCommand(this);
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}