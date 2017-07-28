using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Fluent;

namespace EduGames.Games.WordGame
{
    public class WordGamePlugin : IGamePlugin
    {
        private Dictionary<string, string> allImageFiles;

        public WordGamePlugin()
        {
            GatherImages();
        }

        public string Name => "Woorden schrijven";

        public Control GameControl => new WordGameControl();

        public RibbonTabItem GameRibbon => null;

        public Control SettingsControl => null;

        public string Icon => @"\Resources\WoordSchrijven.png";

        public GameType GameType => GameType.Reading;

        private void GatherImages()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            var ext = new List<string> { ".jpeg", ".jpg", ".gif", ".png", ".bmp" };
            allImageFiles = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(s => ext.Any(s.EndsWith))
                .ToDictionary(Path.GetFileNameWithoutExtension, f => f);
        }
    }
}
