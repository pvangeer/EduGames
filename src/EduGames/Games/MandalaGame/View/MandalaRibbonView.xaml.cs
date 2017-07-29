using System.Windows;
using System.Windows.Controls;
using EduGames.Games.MandalaGame.ModelView;
using Fluent;

namespace EduGames.Games.MandalaGame.View
{
    /// <summary>
    /// Interaction logic for MandalaRibbonView.xaml
    /// </summary>
    public partial class MandalaRibbonView : UserControl
    {
        private MandalaRibbonViewModel viewModel;
        
        public MandalaRibbonView()
        {
            InitializeComponent();
        }

        public MandalaRibbonViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
                RibbonTabItem.DataContext = viewModel;
            }
        }

        /// <summary>
        /// This is a temp fix for a problem in fluent that causes the SelectedColor to be null when achieving this via a command binding.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedColorChanged(object sender, RoutedEventArgs e)
        {
            var colorGallery = sender as ColorGallery;
            if (colorGallery != null && viewModel != null)
            {
                switch ((string)colorGallery.Tag)
                {
                    case "Background":
                        viewModel.BackgroundColor = colorGallery.SelectedColor;
                        break;
                    case "GridStrokeColor":
                        viewModel.GridBrushStrokeColor = colorGallery.SelectedColor;
                        break;
                    case "ElementBrushStrokeColor":
                        viewModel.ElementBrushStrokeColor = colorGallery.SelectedColor;
                        break;
                }
            }
        }
    }
}
