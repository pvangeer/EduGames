using System;
using System.Windows;
using Games.MandalaGamePlugin.GameView.MousePositionTracker;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public class MouseMoveCommand : MouseCommandBase
    {
        public MouseMoveCommand(DrawCanvasViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            if (!(parameter is MouseTrackingObject trackingInformation))
            {
                return;
            }

            var minWidthHeightRadius = Math.Min(trackingInformation.ElementWidth / 2.0, trackingInformation.ElementHeight / 2.0);
            var relativeX = (trackingInformation.MousePosition.X - trackingInformation.ElementWidth / 2.0) / minWidthHeightRadius;
            var relativeY = (trackingInformation.ElementHeight / 2.0 - trackingInformation.MousePosition.Y) / minWidthHeightRadius;
            MandalaViewModel.PositionsList.Add(new Point(relativeX, relativeY));
        }

        public override bool CanExecute(object parameter)
        {
            return MandalaViewModel.IsDrawing;
        }
    }
}