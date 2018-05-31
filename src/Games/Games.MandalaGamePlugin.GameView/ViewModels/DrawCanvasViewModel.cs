using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.GameView.Commands;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.GameView.ViewModels
{
    public class DrawCanvasViewModel : INotifyPropertyChanged
    {
        private Mandala mandala;

        public DrawCanvasViewModel(Mandala mandala)
        {
            PositionsList = new ObservableCollection<Point>();
            this.mandala = mandala;
            if (mandala != null)
            {
                mandala.PropertyChanged += MandalaPropertyChanged;
            }
        }

        private void MandalaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Mandala.MandalaGridResolution):
                    OnPropertyChanged(nameof(MandalaGridResolution));
                    break;
                case nameof(Mandala.CurrentElementColor):
                    OnPropertyChanged(nameof(CurrentElementColor));
                    break;
                case nameof(Mandala.CurrentElementStrokeThickness):
                    OnPropertyChanged(nameof(CurrentElementStrokeThickness));
                    break;
            }
        }

        public bool IsDrawing { get; set; }

        public ObservableCollection<Point> PositionsList { get; }

        public int MandalaGridResolution => mandala.MandalaGridResolution;

        public SolidColorBrush CurrentElementColor => new SolidColorBrush(mandala.CurrentElementColor);

        public Color PaintBrushStrokeColor => mandala.PaintBrushStrokeColor;

        public double PaintBrushStrokeThickness => mandala.PaintBrushStrokeThickness;

        public ICommand MouseDownHandler => new MouseDownCommand(this);

        public ICommand MouseMoveHandler => new MouseMoveCommand(this);

        public ICommand MouseUpHandler => new MouseUpCommand(this);

        public double CurrentElementStrokeThickness => mandala.CurrentElementStrokeThickness;

        public void AddNewMandalaElement(IMandalaElement item)
        {
            mandala.Elements.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
