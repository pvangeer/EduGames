using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using EduGames.Annotations;
using EduGames.Games.MandalaGame.Model;
using Brush = System.Windows.Media.Brush;

namespace EduGames.Games.MandalaGame.ModelView
{
    public class MandalaGameViewModel : INotifyPropertyChanged
    {
        private Mandala mandala;
        private readonly ObservableCollection<IShapeObject> gridShapesCollection = new ObservableCollection<IShapeObject>();
        private readonly ObservableCollection<IShapeObject> elementShapesCollection = new ObservableCollection<IShapeObject>();

        public MandalaGameViewModel()
        {
            this.mandala = new Mandala();
        }

        public MandalaGameViewModel(Mandala mandala)
        {
            Mandala = mandala;
        }

        public Mandala Mandala
        {
            get { return mandala; }
            private set
            {
                if (mandala != null)
                {
                    mandala.PropertyChanged -= MandalaPropertyChanged;
                }
                mandala = value;
                if (mandala != null)
                {
                    mandala.PropertyChanged += MandalaPropertyChanged;
                    UpdateGridShapes("");
                }
            }
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mandala.BackgroundColor))
            {
                OnPropertyChanged(nameof(BackgroundBrush));
            }

            if (e.PropertyName == nameof(Mandala.ShowGrid) || e.PropertyName == nameof(Mandala.GridBrushStrokeColor) || e.PropertyName == nameof(Mandala.GridBrushStrokeThickness) || e.PropertyName == nameof(Mandala.MandalaGridResolution))
            {
                UpdateGridShapes(e.PropertyName);
            }

            if (e.PropertyName == nameof(Mandala.CurrentElementStrokeThickness) ||
                e.PropertyName == nameof(Mandala.CurrentElementColor) ||
                e.PropertyName == nameof(Mandala.MandalaGridResolution))
            {
                // TODO: Expose propertyChanged of these properties, such that the view can draw the correct color.
            }
        }

        private void UpdateGridShapes(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Mandala.ShowGrid):
                    if (!Mandala.ShowGrid)
                    {
                        gridShapesCollection.Clear();
                        OnPropertyChanged(nameof(GridShapes));
                    }
                    else
                    {
                        GenerateAllGridLines();
                        OnPropertyChanged(nameof(GridShapes));
                    }
                    break;
                case "":
                    GenerateAllGridLines();
                    OnPropertyChanged(nameof(GridShapes));
                    break;
                case nameof(Mandala.MandalaGridResolution):
                    ChangeMandalaGridResolution();
                    OnPropertyChanged(nameof(GridShapes));
                    break;
                case nameof(Mandala.GridBrushStrokeColor):
                    foreach (var shapeObject in gridShapesCollection)
                    {
                        shapeObject.Color = Mandala.GridBrushStrokeColor;
                    }
                    OnPropertyChanged(nameof(GridShapes));
                    break;
                case nameof(Mandala.GridBrushStrokeThickness):
                    foreach (var shapeObject in gridShapesCollection)
                    {
                        shapeObject.StrokeThickness = Mandala.GridBrushStrokeThickness;
                    }
                    OnPropertyChanged(nameof(GridShapes));
                    break;
            }
        }

        private void ChangeMandalaGridResolution()
        {
            var objectsToRemove = gridShapesCollection.OfType<LineShapeObject>().ToList();
            foreach (var lineShapeObject in objectsToRemove)
            {
                gridShapesCollection.Remove(lineShapeObject);
            }

            CreateMandalaGridLines();
        }

        private void GenerateAllGridLines()
        {
            CreateMandalaGridLines();
            CreateCircularGridLines();
        }

        private void CreateCircularGridLines()
        {
            for (int i = 0; i < Mandala.CircularGridResolution + 1; i++)
            {
                var gridLineRadius = 1.0 / Mandala.CircularGridResolution * i;
                var gridLine = new EllipseShapeObject(gridLineRadius)
                {
                    StrokeThickness = Mandala.GridBrushStrokeThickness,
                    StrokeDashArray = new DoubleCollection(new[] { 2.0, 2 }),
                    Color = Mandala.GridBrushStrokeColor,
                };
                gridShapesCollection.Add(gridLine);
            }
        }

        private void CreateMandalaGridLines()
        {
            var line = new LineShapeObject(0, 0, 0, 1, Mandala.GridBrushStrokeColor, Mandala.GridBrushStrokeThickness,
                null, new DoubleCollection(new[] {2.0, 2}));
            CreateMandalaObject(line);
        }

        private void CreateMandalaObject(IShapeObject shapeObject, int numberOfDuplications = -1)
        {
            if (numberOfDuplications == -1)
            {
                numberOfDuplications = Mandala.MandalaGridResolution;
            }

            var dAngle = 360.0 / numberOfDuplications;

            for (int i = 0; i < numberOfDuplications; i++)
            {
                IShapeObject newObject = (IShapeObject)shapeObject.Clone();
                newObject.RenderTransform = new RotateTransform(i * dAngle, 0, 0);
                gridShapesCollection.Add(newObject);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Brush BackgroundBrush => new SolidColorBrush(mandala.BackgroundColor);

        public ObservableCollection<IShapeObject> ElementShapes => elementShapesCollection;

        public ObservableCollection<IShapeObject> GridShapes => gridShapesCollection;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
