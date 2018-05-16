using System.Windows.Controls;
using Games.MandalaGamePlugin.Model;

namespace Games.MandalaGamePlugin.Sandbox
{
    /// <summary>
    /// Interaction logic for GridCircleItemsControl.xaml
    /// </summary>
    public partial class GridCircleItemsControl : UserControl
    {
        public GridCircleItemsControl()
        {
            InitializeComponent();
            this.DataContext = new MandalaGridViewModel();
        }
    }
}
