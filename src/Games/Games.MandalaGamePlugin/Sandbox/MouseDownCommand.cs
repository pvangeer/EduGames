using System;
using System.Windows;
using Games.MandalaGamePlugin.ModelView;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class MouseDownCommand : MouseCommandBase
    {
        public MouseDownCommand(MandalaViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            if (!(parameter is MouseTrackingObject trackingInformation))
            {
                return;
            }

            MandalaViewModel.DrawObjectViewModel.PositionsList.Clear();
            MandalaViewModel.DrawObjectViewModel.IsDrawing = true;

            var minWidthHeightRadius = Math.Min(trackingInformation.ElementWidth / 2.0, trackingInformation.ElementHeight / 2.0);
            var relativeX = (trackingInformation.MousePosition.X - trackingInformation.ElementWidth/2.0) / minWidthHeightRadius;
            var relativeY = (trackingInformation.ElementHeight/2.0 - trackingInformation.MousePosition.Y) / minWidthHeightRadius;
            MandalaViewModel.DrawObjectViewModel.PositionsList.Add(new Point(relativeX, relativeY));
        }
    }
}