using System.Windows.Controls;
using Fluent;

namespace Core.Data
{
    public interface IGamePlugin
    {
        string Name { get; }

        Control GameControl { get; }

        RibbonTabItem GameRibbon { get; }

        // TODO: Implement
        Control SettingsControl { get; }

        string Icon { get; }

        GameType GameType { get; }
    }
}
