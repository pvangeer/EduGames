using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private readonly MandalaGameControl mandalaGameControl;
        private readonly MandalaGameView secondGameControl;

        public MandalaGamePlugin()
        {
            ribbon = new View.MandalaRibbonView();
            mandalaGameControl = new MandalaGameControl();
            secondGameControl = new MandalaGameView();

            InitiateNewMandala();
        }

        private void InitiateNewMandala()
        {
            var mandala = new Mandala();
            var mandalaPolygonElement = new MandalaPolygonElement("test")
            {
                StrokeThickness = 2,
                StrokeColor = Colors.CornflowerBlue,
                Points = new List<Point> { new Point(0.1,0.1),new Point(0.1,0.9)},
                NumberOfDubplications = 8
            };
            mandala.Elements.Add(mandalaPolygonElement);
            var mandalaRibbonViewModel = new MandalaRibbonViewModel(mandala);
            mandalaRibbonViewModel.NewMandalaRequested += NewMandalaClicked;
            mandalaRibbonViewModel.SaveMandalaRequested += SaveMandalaClicked;
            ribbon.ViewModel = mandalaRibbonViewModel;
            mandalaGameControl.Mandala = mandala;
            secondGameControl.DataContext = new MandalaGameViewModel(mandala);
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
