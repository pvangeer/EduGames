using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Converters
{
    public class PositionsToMandalaElementDrawObjectsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 4 ||
                !(values[0] is SolidColorBrush brush) ||
                !(values[1] is double strokeThickness) ||
                !(values[2] is ObservableCollection<Point> positions) ||
                !(values[3] is int gridResolution))
            {
                throw new NotImplementedException($"Length={values.Length}, brush = {values[0]}, strokeThickness = {values[1]}, positionslist = {values[2]}, gridResolution = {values[3]}");
            }

            var elements = new List<MandalaElementDrawObject>();
            var dAngle = 360.0 / gridResolution;
            for (int i = 0; i < gridResolution; i++)
            {
                elements.Add(new MandalaElementDrawObject(positions.ToArray(),strokeThickness, brush,dAngle * i));
            }

            return elements;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}