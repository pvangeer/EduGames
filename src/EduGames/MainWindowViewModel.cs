using System.Collections.ObjectModel;
using Core.Data;
using EduGames.Games.MandalaGame;
using EduGames.Games.WordGame;
using Games.FLickerGamePlugin;

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