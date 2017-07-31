using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Core.Data;
using Core.Utils;

namespace EduGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.PropertyChanged += ViewModelPropertyChanged;
                foreach (var pluginRibbon in viewModel.GamesPluginList.Select(gp => gp.GameRibbon))
                {
                    if (pluginRibbon != null)
                    {
                        Ribbon.Tabs.Add(pluginRibbon);
                    }
                }
            }
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // TODO: This should not be necessary. Binding should work.
            if (e.PropertyName == "RibbonSelectedTabItem")
            {
                Ribbon.SelectedTabItem = viewModel.RibbonSelectedTabItem;
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

        private void GameSelectionButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Fluent.Button;
            var gamePlugin = button?.DataContext as IGamePlugin;
            if (gamePlugin != null)
            {
                viewModel.ActivateGame(gamePlugin);
            }
        }
    }
}
