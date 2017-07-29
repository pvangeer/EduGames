using System.Collections.ObjectModel;
using Core.Data;
using Games.FLickerGamePlugin;
using Games.MandalaGamePlugin;
using Games.WordGamePlugin;

namespace EduGames
{
    public class MainWindowViewModel
    {
        public readonly ObservableCollection<IGamePlugin> GamesPluginList;

        public MainWindowViewModel()
        {
            GamesPluginList = new ObservableCollection<IGamePlugin>
            {
                new WordGamePlugin(),
                new FlickerGamePlugin(),
                new MandalaGamePlugin(),
            };
        }
    }
}