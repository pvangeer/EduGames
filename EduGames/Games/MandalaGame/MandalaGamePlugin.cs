﻿using System;
using System.Windows.Controls;
using EduGames.Games.MandalaGame.ModelView;
using Fluent;

namespace EduGames.Games.MandalaGame
{
    public class MandalaGamePlugin : IGamePlugin
    {
        private readonly View.MandalaRibbonView ribbon;
        private readonly MandalaGameControl mandalaGameControl;

        public MandalaGamePlugin()
        {
            ribbon = new View.MandalaRibbonView();
            mandalaGameControl = new MandalaGameControl();

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

        public Control GameControl => mandalaGameControl;

        public RibbonTabItem GameRibbon => ribbon.RibbonTabItem;

        public Control SettingsControl => null;

        public string Icon => @"\Resources\Mandala.png";

        public GameType GameType => GameType.Math;
    }
}
