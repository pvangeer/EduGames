using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EduGames.Helpers;
using FluentButton = Fluent.Button;

namespace EduGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Dictionary<FluentButton, IGamePlugin> gamesDictionary;

        public MainWindow()
        {
            ViewModel = new MainWindowViewModel();
            InitializeComponent();

            InitializeGames();
            ActivateGame(ViewModel.GamesPluginList.First());
        }

        public MainWindowViewModel ViewModel { get; }

        private void InitializeGames()
        {
            gamesDictionary = new Dictionary<FluentButton, IGamePlugin>();
            foreach (var gamePlugin in ViewModel.GamesPluginList)
            {
                //Create button in main ribbon
                var button = new FluentButton {Header = gamePlugin.Name, LargeIcon = gamePlugin.Icon};
                button.Click += OnGamesButtonClick;

                // Add button to tab in ribbon
                switch (gamePlugin.GameType)
                {
                    case GameType.Reading:
                        ReadingGroupBox.Items.Add(button);
                        break;
                    case GameType.Math:
                        CalculaGroupBox.Items.Add(button);
                        break;
                }

                if (gamePlugin.GameRibbon != null)
                {
                    Ribbon.Tabs.Add(gamePlugin.GameRibbon);
                    gamePlugin.GameRibbon.Visibility = Visibility.Hidden;
                }

                // Add button to dictionary
                gamesDictionary[button] = gamePlugin;
            }
        }

        private void OnGamesButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ActivateGame(gamesDictionary[sender as FluentButton]);
        }

        private void ActivateGame(IGamePlugin gamePlugin)
        {
            GamesContentPanel.Content = gamePlugin.GameControl;
            foreach (var plugin in ViewModel.GamesPluginList)
            {
                if (plugin.GameRibbon != null)
                {
                    var isActivePlugin = plugin == gamePlugin;
                    plugin.GameRibbon.Visibility = isActivePlugin ? Visibility.Visible : Visibility.Collapsed;
                    if (isActivePlugin)
                    {
                        Ribbon.SelectedTabItem = plugin.GameRibbon;
                    }
                }
            }
        }

        private void SyntaxHighlightingToggleButonClicked(object sender, RoutedEventArgs e)
        {
            RichTextBoxExtenstions.UseColorHighlighting = SyntaxHighlightingToggleButon.IsChecked ?? false;
            foreach (var tb in FindVisualChildren<RichTextBox>(this))
            {
                tb.ApplyStyleToText();
            }    
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        } 
    }
}
