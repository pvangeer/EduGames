using System.Linq;
using Games.MandalaGamePlugin.Model;
using Games.MandalaGamePlugin.ModelView;

namespace Games.MandalaGamePlugin.Sandbox
{
    public class MouseUpCommand : MouseCommandBase
    {
        public MouseUpCommand(MandalaViewModel mandalaViewModel) : base(mandalaViewModel) { }

        public override void Execute(object parameter)
        {
            MandalaViewModel.AddNewMandalaElement(new MandalaPolygonElement("Getrokken lijn")
            {
                StrokeColor = MandalaViewModel.PaintBrushStrokeColor,
                StrokeThickness = MandalaViewModel.PaintBrushStrokeThickness,
                NumberOfDubplications = MandalaViewModel.MandalaGridResolution,
                Points = MandalaViewModel.DrawObjectViewModel.PositionsList.ToList()
            });

            MandalaViewModel.DrawObjectViewModel.PositionsList.Clear();
            MandalaViewModel.DrawObjectViewModel.IsDrawing = false;
        }

        public override bool CanExecute(object parameter)
        {
            return MandalaViewModel.DrawObjectViewModel.IsDrawing;
        }
    }
}