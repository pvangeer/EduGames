using System;
using System.Windows.Media;

namespace EduGames.Games.MandalaGame.ModelView
{
    public interface IShapeObject : ICloneable
    {
        Color Color { get; set; }

        double StrokeThickness { get; set; }

        Transform RenderTransform { get; set; }
    }
}