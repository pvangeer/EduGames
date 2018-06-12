using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin
{
    /// <summary>
    /// Interaction logic for MandalaGameControl.xaml
    /// </summary>
    public partial class MandalaGameControl : UserControl
    {
        private bool paintingNewElement;
        private List<Point> newElementPoints;
        private readonly string[] dependentPropertyNames =
        {
            "MandalaGridResolution",
            "ShowGrid",
            "GridBrushStrokeThickness",
            "GridBrushStrokeColor"
        };

        public MandalaGameControl()
        {
            InitializeComponent();
            PaintCanvas.SizeChanged += CanvasSizeChanged;
            PreviewMouseDown += HandleMouseDown;
        }

        private Mandala mandala;
        public Mandala Mandala
        {
            get { return mandala; }
            set
            {
                if (mandala != null)
                {
                    ((INotifyPropertyChanged)mandala).PropertyChanged -= SettingsPropertyChanged;
                    ((INotifyCollectionChanged) mandala.Elements).CollectionChanged -= ElementsCollectionChanged;
                }
                CancelPaintingElement(this);
                mandala = value;
                if (mandala != null)
                {
                    ((INotifyPropertyChanged)mandala).PropertyChanged += SettingsPropertyChanged;
                    ((INotifyCollectionChanged)mandala.Elements).CollectionChanged += ElementsCollectionChanged;
                    PaintCanvas.Background = new SolidColorBrush(mandala.BackgroundColor);
                    RepaintCanvas();
                }
            }
        }

        private void ElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                RepaintCanvas();
            }
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "BackgroundColor")
            {
                PaintCanvas.Background = new SolidColorBrush(mandala.BackgroundColor);
            }

            if (dependentPropertyNames.Contains(e.PropertyName))
            {
                RepaintCanvas();
            }
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            var points = CancelPaintingElement(sender);
            if (points != null)
            {
                mandala.Elements.Add(new MandalaPolygonElement("Getrokken lijn")
                {
                    Points = CreateRelativePointsArray(points),
                    StrokeColor = Mandala.CurrentElementColor,
                    StrokeThickness = Mandala.CurrentElementStrokeThickness,
                    NumberOfDubplications = Mandala.MandalaGridResolution
                });
            }
            RepaintCanvas();
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (!paintingNewElement)
            {
                return;
            }

            var point = e.GetPosition(PaintCanvas);
            var relativePoint = CreateRelativePointsArray(new[] { point }).FirstOrDefault();
            if (Math.Sqrt(Math.Pow(relativePoint.X, 2) + Math.Pow(relativePoint.Y, 2)) > 1.0)
            {
                var points = CancelPaintingElement(sender);
                if (points != null)
                {
                    mandala.Elements.Add(new MandalaPolygonElement("Getrokken lijn")
                    {
                        Points = CreateRelativePointsArray(points),
                        StrokeColor = Mandala.CurrentElementColor,
                        StrokeThickness = Mandala.CurrentElementStrokeThickness,
                        NumberOfDubplications = Mandala.MandalaGridResolution
                    });
                }
                RepaintCanvas();
                return;
            }
            newElementPoints.Add(point);

            RepaintCanvas();
        }

        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            CancelPaintingElement(null);
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            paintingNewElement = true;
            PreviewMouseMove += HandleMouseMove;
            PreviewMouseUp += HandleMouseUp;
            newElementPoints = new List<Point> { e.GetPosition(PaintCanvas) };
            ((UIElement)sender).CaptureMouse();
        }

        private double CanvasRadius => Math.Min(PaintCanvas.ActualWidth, PaintCanvas.ActualHeight) / 2.0;

        private Point CanvasCenter => new Point(PaintCanvas.ActualWidth / 2.0, PaintCanvas.ActualHeight / 2.0);

        private IEnumerable<Point> CreateRelativePointsArray(IEnumerable<Point> points)
        {
            var list = new List<Point>();
            foreach (var point in points)
            {
                list.Add(new Point((point.X - CanvasCenter.X)/CanvasRadius, (point.Y - CanvasCenter.Y) / CanvasRadius));
            }
            return list;
        }
        
        private IList<Point> CancelPaintingElement(object sender)
        {
            if (sender != null)
            {
                PreviewMouseMove -= HandleMouseMove;
                PreviewMouseUp -= HandleMouseUp;
                ((UIElement) sender).ReleaseMouseCapture();
            }
            if (paintingNewElement && newElementPoints != null && newElementPoints.Count > 1)
            {
                var points = newElementPoints.ToList();
                paintingNewElement = false;
                newElementPoints = null;
                return points;
            }
            paintingNewElement = false;
            newElementPoints = null;
            return null;
        }

        public void RepaintCanvas()
        {
            PaintCanvas.Children.Clear();
            if (Mandala.ShowGrid)
            {
                AddGridElements();
            }
            AddPaintedElements();
            AddPaintedElement();
        }

        private void AddPaintedElements()
        {
            foreach (var element in mandala.Elements.OfType<MandalaPolygonElement>())
            {
                var polyLine = new Polyline
                {
                    Points = new PointCollection(CreateRelatedPointsArray(element.Points)),
                    Stroke = new SolidColorBrush(element.StrokeColor),
                    StrokeThickness = element.StrokeThickness
                };
                PaintMandalaObject(polyLine, 0, 0, PaintCanvas.ActualWidth / 2.0, PaintCanvas.ActualHeight / 2.0, element.NumberOfDubplications);
            }
        }

        private IEnumerable<Point> CreateRelatedPointsArray(IEnumerable<Point> elementPoints)
        {
            var list = new List<Point>();
            foreach (var point in elementPoints)
            {
                list.Add(new Point(CanvasCenter.X + point.X*CanvasRadius, CanvasCenter.Y + point.Y * CanvasRadius));
            }
            return list;
        }

        private void AddPaintedElement()
        {
            if (paintingNewElement && newElementPoints != null && newElementPoints.Count > 1)
            {
                var polyLine = new Polyline
                {
                    Points = new PointCollection(newElementPoints),
                    Stroke = new SolidColorBrush(Mandala.PaintBrushStrokeColor),
                    StrokeThickness = Mandala.PaintBrushStrokeThickness,
                    StrokeDashArray = new DoubleCollection(new[] { 2.0, 2 }),
                };
                PaintMandalaObject(polyLine, 0, 0, PaintCanvas.ActualWidth / 2.0, PaintCanvas.ActualHeight / 2.0);
            }
        }

        private void AddGridElements()
        {
            if (CanvasRadius < 1)
            {
                return;
            }

            var topCanvasMargin = (PaintCanvas.ActualHeight - CanvasRadius * 2.0) / 2.0;

            var line = new Line
            {
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = CanvasRadius,
                Stroke = new SolidColorBrush(Mandala.GridBrushStrokeColor),
                StrokeThickness = Mandala.GridBrushStrokeThickness,
                StrokeDashArray = new DoubleCollection(new[] { 2.0, 2 }),
            };
            PaintMandalaObject(line,CanvasCenter.X, topCanvasMargin, 0, CanvasCenter.Y);

            for (int i = 0; i < Mandala.CircularGridResolution + 1; i++)
            {
                var gridLineRadius = CanvasRadius / Mandala.CircularGridResolution * i;
                var gridLine = new Ellipse
                {
                    StrokeThickness = Mandala.GridBrushStrokeThickness,
                    StrokeDashArray = new DoubleCollection(new[] { 2.0, 2 }),
                    Stroke = new SolidColorBrush(Mandala.GridBrushStrokeColor),
                    Width = gridLineRadius * 2,
                    Height = gridLineRadius * 2,
                };

                var leftCanvasMargin = (PaintCanvas.ActualWidth - gridLineRadius * 2.0) / 2.0;
                topCanvasMargin = (PaintCanvas.ActualHeight - gridLineRadius * 2.0) / 2.0;
                
                AlignAndAddElementToCanvas(gridLine, leftCanvasMargin, topCanvasMargin);
            }
        }


        private void PaintMandalaObject(UIElement uiElement, double left, double top, double rotationCenterLeft, double rotationCenterTop, int numberOfDuplications = -1)
        {
            if (numberOfDuplications == -1)
            {
                numberOfDuplications = Mandala.MandalaGridResolution;
            }

            var dAngle = 360.0 / numberOfDuplications;

            for (int i = 0; i < numberOfDuplications; i++)
            {
                var newElement = CloneElement(uiElement);
                newElement.RenderTransform = new RotateTransform(i * dAngle, rotationCenterLeft, rotationCenterTop);
                
                AlignAndAddElementToCanvas(newElement, left, top);
            }
        }

        private static UIElement CloneElement(UIElement orig)
        {

            if (orig == null)
            {
                return null;
            }

            string s = XamlWriter.Save(orig);

            StringReader stringReader = new StringReader(s);

            XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings());

            return (UIElement)XamlReader.Load(xmlReader);
        }

        private void AlignAndAddElementToCanvas(UIElement uiElement, double left, double top)
        {
            Canvas.SetTop(uiElement, top);
            Canvas.SetLeft(uiElement, left);
            PaintCanvas.Children.Add(uiElement);
        }

        private void CanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RepaintCanvas();
        }
    }
}
