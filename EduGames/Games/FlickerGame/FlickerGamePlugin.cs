using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Fluent;

namespace EduGames.Games.FlickerGame
{
    public class FlickerGamePlugin : IGamePlugin
    {
        private readonly FlickerGameControl flickerGameControl;
        private FlickerRibbonControl ribbon;

        public FlickerGamePlugin()
        {
            flickerGameControl = new FlickerGameControl();
            ribbon = new FlickerRibbonControl();
            ribbon.NewWordClicked += NewWordClickedInRibbon;
            ribbon.RepeatWordClicked += RepeatWordClickedInRibbon;
            ribbon.SpeedSliderValueChanged += SpeedSliderValueChangedInRibbon;
        }

        public string Name => "Woordflits";

        public Control GameControl => flickerGameControl;

        public RibbonTabItem GameRibbon => ribbon.Ribbon.Tabs.FirstOrDefault();

        public Control SettingsControl => null;

        public string Icon => @"\Resources\WoordFlits.png";

        public GameType GameType => GameType.Reading;

        private void NewWordClickedInRibbon(object sender, EventArgs e)
        {
            flickerGameControl.ShowNewWord();
        }

        private void RepeatWordClickedInRibbon(object sender, EventArgs e)
        {
            flickerGameControl.RepeatWord();
        }

        private void SpeedSliderValueChangedInRibbon(object sender, EventArgs e)
        {
            flickerGameControl.ShowWordTime = (int) ((double)sender);
        }
    }
}
