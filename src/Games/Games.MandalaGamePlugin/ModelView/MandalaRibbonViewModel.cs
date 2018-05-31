using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Games.MandalaGamePlugin.Data;
using Games.MandalaGamePlugin.Model;

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
            get { return mandala.GridBrushStrokeColor; }
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

        public bool ShowGrid
        {
            get { return mandala.ShowGrid; }
            set
            {
                mandala.ShowGrid = value;
                OnPropertyChanged(nameof(ShowGrid));
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
            get { return mandala.CurrentElementStrokeThickness; }
            set
            {
                mandala.CurrentElementStrokeThickness = value;
                OnPropertyChanged(nameof(CurrentElementStrokeThickness));
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
