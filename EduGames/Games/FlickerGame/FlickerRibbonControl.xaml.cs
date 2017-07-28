using System;
using System.Windows;
using System.Windows.Controls;

namespace EduGames.Games.FlickerGame
{
    /// <summary>
    /// Interaction logic for FlickerRibbonControl.xaml
    /// </summary>
    public partial class FlickerRibbonControl : UserControl
    {
        public FlickerRibbonControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> NewWordClicked;
        private void NewWordClick(object sender, RoutedEventArgs e)
        {
            if (NewWordClicked != null)
            {
                NewWordClicked(this, null);
            }
        }

        public event EventHandler<EventArgs> RepeatWordClicked;
        private void RepeatWordClick(object sender, RoutedEventArgs e)
        {
            if (RepeatWordClicked != null)
            {
                RepeatWordClicked(this, null);
            }
        }

        public event EventHandler<EventArgs> SpeedSliderValueChanged;
        private void SpeedSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedSliderValueChanged != null)
            {
                SpeedSliderValueChanged(SpeedSlider.Value, null);
            }
        }
    }
}
