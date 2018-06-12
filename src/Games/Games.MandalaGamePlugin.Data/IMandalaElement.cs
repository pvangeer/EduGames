using System.Windows.Media;

namespace Games.MandalaGamePlugin.Data
{
    public interface IMandalaElement
    {
        string DisplayName { get; }

        string Type { get; }

        int NumberOfDubplications { get; set; }

        Color StrokeColor { get; set; }

        double StrokeThickness { get; set; }

        string ToString();
    }
}