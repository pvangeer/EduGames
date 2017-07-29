using System.Collections.ObjectModel;
using EduGames.Games.FlickerGame;
using EduGames.Games.MandalaGame;
using EduGames.Games.WordGame;

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