using System.Windows.Controls;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.Sandbox
{
    /// <summary>
    /// Interaction logic for CanvasListBoxControl.xaml
    /// </summary>
    public partial class CanvasListBoxControl : UserControl
    {
        public CanvasListBoxControl()
        {
            InitializeComponent();
            this.DataContext = new MandalaGridViewModel();
        }
    }
}
