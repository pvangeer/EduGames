using System.Windows.Media;

namespace Games.MandalaGamePlugin.Model
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