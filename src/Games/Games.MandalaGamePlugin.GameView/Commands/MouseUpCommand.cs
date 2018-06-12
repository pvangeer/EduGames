using System.Linq;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.ViewModels;

namespace Games.MandalaGamePlugin.GameView.Commands
{
    public class MouseUpCommand : MouseCommandBase
    {
        public MouseUpCommand(DrawCanvasViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            MandalaViewModel.AddNewMandalaElement(new MandalaPolygonElement("Getrokken lijn")
            {
                Points = MandalaViewModel.PositionsList.ToList()
            });

            MandalaViewModel.PositionsList.Clear();
            MandalaViewModel.IsDrawing = false;
        }

        public override bool CanExecute(object parameter)
        {
            return MandalaViewModel.IsDrawing;
        }
    }
}