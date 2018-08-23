using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;

namespace Games.MandalaGamePlugin.ModelView
{
    public class MandalaRibbonViewModel: INotifyPropertyChanged
    {
        private readonly Mandala mandala;
        private readonly RevertLastChangeCommand revertLastChangeCommand;

        public MandalaRibbonViewModel()
        {
            mandala = new Mandala();
            revertLastChangeCommand = new RevertLastChangeCommand(mandala);
        }

        public MandalaRibbonViewModel(Mandala mandala)
        {
            this.mandala = mandala;
            revertLastChangeCommand = new RevertLastChangeCommand(mandala);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Color? BackgroundColor
        {
            get { return mandala.BackgroundColor; }
            set
            {
                mandala.BackgroundColor = value ?? Colors.Transparent;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public Color? GridBrushStrokeColor
        {
            get => mandala.GridBrushStrokeColor;
            set
            {
                mandala.GridBrushStrokeColor = value ?? Colors.Transparent;
                OnPropertyChanged(nameof(GridBrushStrokeColor));
            }
        }

        public Color? ElementBrushStrokeColor
        {
            get { return mandala.CurrentElementColor; }
            set
            {
                mandala.CurrentElementColor = value ?? Colors.Transparent;
                OnPropertyChanged(nameof(ElementBrushStrokeColor));
            }
        }

        public Dictionary<int, string> PossibleResolutions => new Dictionary<int, string>
        {
            {1, "1"},
            {2, "2"},
            {4, "4"},
            {6, "6"},
            {8, "8"},
            {12, "12"},
            {16, "16"},
            {20, "20"},
            {24, "24"},
            {36, "36"}
        };

        public bool ShowMandalaGrid
        {
            get => mandala.ShowMandalaGrid;
            set
            {
                mandala.ShowMandalaGrid = value;
                OnPropertyChanged(nameof(ShowMandalaGrid));
            }
        }

        public bool ShowGridCircles
        {
            get => mandala.ShowGridCircles;
            set
            {
                mandala.ShowGridCircles = value;
                OnPropertyChanged(nameof(ShowGridCircles));
            }
        }

        public int MandalaGridResolution
        {
            get { return mandala.MandalaGridResolution; }
            set
            {
                mandala.MandalaGridResolution = value;
                OnPropertyChanged(nameof(MandalaGridResolution));
            }
        }

        public int CurrentElementStrokeThickness
        {
            get => mandala.CurrentElementStrokeThickness;
            set
            {
                mandala.CurrentElementStrokeThickness = value;
                OnPropertyChanged(nameof(CurrentElementStrokeThickness));
            }
        }

        public int GridStrokeThickness
        {
            get => mandala.GridBrushStrokeThickness;
            set
            {
                mandala.GridBrushStrokeThickness = value;
                OnPropertyChanged(nameof(GridStrokeThickness));
            }
        }

        public ObservableCollection<IMandalaElement> ElementsList => mandala.Elements;

        public ICommand RevertLastChange => revertLastChangeCommand;

        public ICommand NewMandalaCommand => new NewMandalaCommand(this);

        public ICommand SaveMandalaCommand => new SaveMandalaCommand(this);



        public event EventHandler<EventArgs> SaveMandalaRequested;

        public void RaisSaveMandalaRequest()
        {
            SaveMandalaRequested?.Invoke(this,EventArgs.Empty);
        }

        public event EventHandler<EventArgs> NewMandalaRequested;

        public void RaiseNewMandalaRequested()
        {
            NewMandalaRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
