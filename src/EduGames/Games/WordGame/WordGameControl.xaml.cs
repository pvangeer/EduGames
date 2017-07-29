using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core.Data.Helpers;

namespace EduGames.Games.WordGame
{
    /// <summary>
    /// Interaction logic for WordGameControl.xaml
    /// </summary>
    public partial class WordGameControl : UserControl
    {
        public bool ShowSamples
        {
            get { return ImagesListBox.Visibility == Visibility.Visible; }
            set { ImagesListBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        private bool validword;
        
        public WordGameControl()
        {
            InitializeComponent();
            validword = true;
            ShowSamples = false;
            RichTextBox.AppendText("Maak een woord");
            var range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.LightGray));
            range.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            
            RichTextBox.ConfigureRichtTextBox();
        }

        private void TextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (validword && e.Key != Key.Enter && e.Key != Key.Space)
            {
                RichTextBox.Document.Blocks.Clear();
                validword = false;
            }

            string text = GetCurrentText();
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                validword = RefreshImage(text);
                e.Handled = true;
            }

            RefreshImagesListBox(text);
        }

        private string GetCurrentText()
        {
            var text = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text;
            if (text.EndsWith("\r\n"))
            {
                text = text.Substring(0, text.Length - 2);
            }
            return text;
        }

        private void RefreshImagesListBox(string text)
        {
            var images = WordImageFactory.AllImages;
            var imagesToShow = images.Where(i => i.Key.StartsWith(text)).Select(i => new Uri(i.Value)).ToArray();

            var bitmapImages = ImagesListBox.Items.OfType<BitmapImage>().ToList();
            var currentImages = bitmapImages.Select(im => im.UriSource);
            var imagesToRemove = currentImages.Intersect(imagesToShow).ToArray();
            foreach (var uri in imagesToRemove)
            {
                ImagesListBox.Items.Remove(bitmapImages.First(b => b.UriSource == uri));
            }

            foreach (var uri in imagesToShow.Except(currentImages))
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = uri;
                bi.DecodePixelWidth = 64;
                bi.DecodePixelHeight = 64;
                bi.EndInit();
                
                ImagesListBox.Items.Add(bi);
            }
        }

        private bool RefreshImage(string text)
        {
            try
            {
                var allImages = WordImageFactory.AllImages;
                if (allImages.ContainsKey(text))
                {
                    var imageLocation = allImages[text];
                    Image.Source = new BitmapImage(new Uri(imageLocation));
                    return true;
                }
            }
            catch (Exception)
            {
                Image.Source = null;
            }
            return false;
        }

        private void ControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (double.IsNaN(Width) || double.IsNaN(Height) || Width < 0 || Height < 0)
            {
                return;
            }
            Image.MaxWidth = Width;
            Image.MaxHeight = Height - 100;
        }
    }
}
