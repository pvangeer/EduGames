using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class GridDataToPathDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3 || !(values[0] is int resolution) || !(values[1] is Color strokeColor) || !(values[2] is double strokeThickness))
            {
                throw new NotImplementedException();
            }

            var viewModels = new List<GridCircleViewModel>();
            var dFraction = 1.0 / resolution;
            for (int i = 0; i < resolution; i++)
            {
                viewModels.Add(new GridCircleViewModel(1.0-i*dFraction, strokeColor, strokeThickness));
            }

            return viewModels;

            /*var width = canvas.ActualWidth;
            if (width < 1e-8)
            {
                return null;
            }

            var elements = new List<FrameworkElement>();
            var dFraction = 1.0 / resolution;
            for (int i = 0; i < resolution; i++)
            {
                var ellipse = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Transparent),
                    Stroke = new SolidColorBrush(strokeColor),
                    StrokeDashArray = new DoubleCollection {2.0, 2.0},
                    StrokeThickness = strokeThickness
                };

                MultiBinding multiBindingWidth = new MultiBinding
                {
                    //Converter = new CanvasMinWidthHeightMultiValueConverter(1.0 - i*dFraction),
                    NotifyOnSourceUpdated = true
                };
                multiBindingWidth.Bindings.Add(new Binding("ActualWidth") { Source = canvas });
                multiBindingWidth.Bindings.Add(new Binding("ActualHeight") { Source = canvas });
                BindingOperations.SetBinding(ellipse, FrameworkElement.WidthProperty, multiBindingWidth);

                MultiBinding multiBindingHeight = new MultiBinding
                    {
                        //Converter = new CanvasMinWidthHeightMultiValueConverter(1.0 - i * dFraction),
                        NotifyOnSourceUpdated = true
                };
                multiBindingHeight.Bindings.Add(new Binding("ActualWidth") { Source = canvas });
                multiBindingHeight.Bindings.Add(new Binding("ActualHeight") { Source = canvas }); 
                BindingOperations.SetBinding(ellipse, FrameworkElement.HeightProperty, multiBindingHeight);

                elements.Add(ellipse);
            }
*/
            //          canvas.SizeChanged += (sender, e) => CanvasSizeChanged(canvas, elements);
            /*
            <Ellipse
                     Fill="LightGray"
                     x:Name="ellipse">
                    <Ellipse.Width>
                        <MultiBinding Converter="{StaticResource CanvasMinWidthHeightMultiValueConverter}">
                            <Binding ElementName="Canvas" Path="ActualWidth"/>
                            <Binding ElementName="Canvas" Path="ActualHeight"/>
                        </MultiBinding>
                    </Ellipse.Width>
                    <Ellipse.Height>
                        <MultiBinding Converter="{StaticResource CanvasMinWidthHeightMultiValueConverter}">
                            <Binding ElementName="Canvas" Path="ActualWidth"/>
                            <Binding ElementName="Canvas" Path="ActualHeight"/>
                        </MultiBinding>
                    </Ellipse.Height>
                    <Canvas.Left>
                        <MultiBinding Converter="{StaticResource HalfValueConverter}">
                            <Binding ElementName="Canvas" Path="ActualWidth" />
                            <Binding ElementName="ellipse" Path="ActualWidth" />
                        </MultiBinding>
                    </Canvas.Left>
                    <Canvas.Top>
                        <MultiBinding Converter="{StaticResource HalfValueConverter}">
                            <Binding ElementName="Canvas" Path="ActualHeight" />
                            <Binding ElementName="ellipse" Path="ActualHeight" />
                        </MultiBinding>
                    </Canvas.Top>
                </Ellipse>*/

            //        return elements;
        }

            private void CanvasSizeChanged(Canvas canvas, List<FrameworkElement> elements)
        {
            double totalWidth = canvas.ActualWidth;
            double totalHeight = canvas.ActualHeight;
            foreach (var element in elements)
            {
                double width = element.ActualWidth;
                double height = element.ActualHeight;
                Canvas.SetLeft(element, (totalWidth - width) / 2);
                Canvas.SetLeft(element, (totalHeight - height) / 2);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}