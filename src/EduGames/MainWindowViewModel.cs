using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Core.Data;
using Fluent;
using Games.FLickerGamePlugin;
using Games.MandalaGamePlugin;
using Games.WordGamePlugin;

namespace EduGames
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Control gamesContentPanelContent;
        private RibbonTabItem ribbonSelectedTabItem;

        public MainWindowViewModel()
        {
            GamesPluginList = new ObservableCollection<IGamePlugin>
            {
                new WordGamePlugin(),
                new FlickerGamePlugin(),
                new MandalaGamePlugin()
            };

            ActivateGame(GamesPluginList.Last());
        }

        public readonly ObservableCollection<IGamePlugin> GamesPluginList;

        public IEnumerable<IGamePlugin> CalculaGames => new ObservableCollection<IGamePlugin>(
            GamesPluginList.Where(gp => gp.GameType == GameType.Math));

        public IEnumerable<IGamePlugin> ReadingGames => new ObservableCollection<IGamePlugin>(
            GamesPluginList.Where(gp => gp.GameType == GameType.Reading));

        public void ActivateGame(IGamePlugin gamePlugin)
        {
            GamesContentPanelContent = gamePlugin.GameControl;
            foreach (var plugin in GamesPluginList)
            {
                if (plugin.GameRibbon != null)
                {
                    var isActivePlugin = plugin == gamePlugin;
                    plugin.GameRibbon.Visibility = isActivePlugin ? Visibility.Visible : Visibility.Collapsed;
                    if (isActivePlugin)
                    {
                        RibbonSelectedTabItem = gamePlugin.GameRibbon;
                    }
                }
            }
        }

        public Control GamesContentPanelContent
        {
            get { return gamesContentPanelContent; }
            set
            {
                gamesContentPanelContent = value;
                OnPropertyChanged(nameof(GamesContentPanelContent));
            }
        }

        public RibbonTabItem RibbonSelectedTabItem
        {
            get { return ribbonSelectedTabItem; }
            set
            {
                ribbonSelectedTabItem = value;
                OnPropertyChanged(nameof(RibbonSelectedTabItem));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}