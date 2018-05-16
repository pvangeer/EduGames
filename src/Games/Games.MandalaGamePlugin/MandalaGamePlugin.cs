using System;
using System.Windows.Controls;
using Core.Data;
using Fluent;
using Games.MandalaGamePlugin.Model;
using Games.MandalaGamePlugin.ModelView;
using Games.MandalaGamePlugin.Sandbox;
using Games.MandalaGamePlugin.View;

namespace Games.MandalaGamePlugin
{
    public class MandalaGamePlugin : IGamePlugin
    {
        private readonly View.MandalaRibbonView ribbon;
        private readonly MandalaGameControl mandalaGameControl;
        private CanvasListBoxControl secondGameControl;

        public MandalaGamePlugin()
        {
            ribbon = new View.MandalaRibbonView();
            mandalaGameControl = new MandalaGameControl();
            secondGameControl = new CanvasListBoxControl();

            InitiateNewMandala();
        }

        private void InitiateNewMandala()
        {
            var mandala = new Mandala();
            var mandalaRibbonViewModel = new MandalaRibbonViewModel(mandala);
            mandalaRibbonViewModel.NewMandalaRequested += NewMandalaClicked;
            mandalaRibbonViewModel.SaveMandalaRequested += SaveMandalaClicked;
            ribbon.ViewModel = mandalaRibbonViewModel;
            mandalaGameControl.Mandala = mandala;
            //secondGameControl.DataContext = new MandalaViewModel(mandala);
            secondGameControl.DataContext = new MandalaGridViewModel(mandala);
        }

        private void SaveMandalaClicked(object sender, EventArgs e)
        {
            MandalaSaveHelper.SaveMandala(mandalaGameControl.PaintCanvas);
        }

        private void NewMandalaClicked(object sender, EventArgs e)
        {
            InitiateNewMandala();
        }

        public string Name => "Mandala maker";

        public Control GameControl => secondGameControl;

        public RibbonTabItem GameRibbon => ribbon.RibbonTabItem;

        public Control SettingsControl => null;

        public string Icon => @"/Games.MandalaGamePlugin;component\Resources\Mandala.png";

        public GameType GameType => GameType.Math;
    }
}
