using System;
using System.Windows.Controls;
using Core.Data;
using Fluent;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView;
using Games.MandalaGamePlugin.GameView.ViewModels;
using Games.MandalaGamePlugin.ModelView;

namespace Games.MandalaGamePlugin
{
    public class MandalaGamePlugin : IGamePlugin
    {
        private readonly View.MandalaRibbonView ribbon;
        private readonly MandalaGameView mandalaGameView;

        public MandalaGamePlugin()
        {
            ribbon = new View.MandalaRibbonView();
            mandalaGameView = new MandalaGameView();

            InitiateNewMandala();
        }

        private void InitiateNewMandala()
        {
            var mandala = new Mandala();
            var mandalaRibbonViewModel = new MandalaRibbonViewModel(mandala);
            mandalaRibbonViewModel.NewMandalaRequested += NewMandalaClicked;
            mandalaRibbonViewModel.SaveMandalaRequested += SaveMandalaClicked;
            ribbon.ViewModel = mandalaRibbonViewModel;
            mandalaGameView.DataContext = new MandalaGameViewModel(mandala);
        }

        private void SaveMandalaClicked(object sender, EventArgs e)
        {
            MandalaSaveHelper.SaveMandala(mandalaGameView.MandalaGrid);
        }

        private void NewMandalaClicked(object sender, EventArgs e)
        {
            InitiateNewMandala();
        }

        public string Name => "Mandala maker";

        public Control GameControl => mandalaGameView;

        public RibbonTabItem GameRibbon => ribbon.RibbonTabItem;

        public Control SettingsControl => null;

        public string Icon => @"/Games.MandalaGamePlugin;component\Resources\Mandala.png";

        public GameType GameType => GameType.Math;
    }
}
