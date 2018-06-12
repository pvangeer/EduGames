using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class PolygonElementViewModel : ElementViewModel
    {
        private MandalaPolygonElement polygonElement;

        public PolygonElementViewModel(MandalaPolygonElement polygonElement) : base(polygonElement)
        {
            this.polygonElement = polygonElement;
        }

        public IEnumerable<Point> ShapeData => polygonElement.Points;

        public SolidColorBrush StrokeColor => new SolidColorBrush(polygonElement.StrokeColor);

        public double StrokeThickness => polygonElement.StrokeThickness;

        public int NumberOfDuplications => polygonElement.NumberOfDubplications;
    }
}