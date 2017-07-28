using System.Windows.Media;

namespace EduGames.Games.MandalaGame
{
    public interface IMandalaElement
    {
        string DisplayName { get; }

        string Type { get; }

        Color StrokeColor { get; }

        double StrokeThickness { get; }

        string ToString();
    }
}