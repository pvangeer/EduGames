using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using EduGames.Helpers;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;

namespace EduGames.Games.FlickerGame
{
    /// <summary>
    /// Interaction logic for FlickerGameControl.xaml
    /// </summary>
    public partial class FlickerGameControl : UserControl
    {
        public Color PanelColorCorrect = Colors.ForestGreen;
        public Color PanelColorIncorrect = Colors.LightCoral;
        public Brush RichTextBoxColorShowWord = null;
        public Brush PanelColorNutral = null;
        public int ShowWordTime = 3000;
        public Color RichTextBoxColorHideWord = Colors.LightGray;
        public Color PanelColorMouseOver = Colors.LightSteelBlue;
        public Color FirstColorProgressBar = Colors.Green;
        public Color LastColorProgressBar = Colors.Red;

        private static readonly Action EmptyDelegate = delegate { };
        private static readonly Random RandomGenerator = new Random();
        private readonly Dictionary<int, Image> imageDictionary;
        private readonly Dictionary<StackPanel, int> panelDictionary;

        private Dictionary<int, BitmapImage> imageSources;
        
        private int correctImageIndex = -1;
        private int wordIndex;
        private bool correctImageClicked;
        
        public FlickerGameControl()
        {
            InitializeComponent();
            imageDictionary = new Dictionary<int, Image>
            {
                {0,Image1},
                {1,Image2},
                {2,Image3}
            };
            panelDictionary = new Dictionary<StackPanel, int>
            {
                {StackPanel1,0},
                {StackPanel2,1},
                {StackPanel3,2}
            };
            RichTextBox.Document.TextAlignment = TextAlignment.Center;
            RichTextBox.IsReadOnly = true;

            RichTextBox.ConfigureRichtTextBox();
        }

        public void ShowNewWord()
        {
            ClearComponents();

            // pick word
            var images = WordImageFactory.Images;
            wordIndex = RandomGenerator.Next(images.Count);
            var correctWord = images.Keys.ElementAt(wordIndex);
            correctImageIndex = RandomGenerator.Next(imageDictionary.Count);

            // Choose other images and assign to image controls
            var usedImages = new List<int> { wordIndex };
            imageSources = new Dictionary<int,BitmapImage>();
            foreach (var imageIndex in imageDictionary.Keys)
            {
                if (imageIndex == correctImageIndex)
                {
                    imageSources[imageIndex] = new BitmapImage(new Uri(images.Values.ElementAt(wordIndex)));
                    continue;
                }
                var nextImageIndex = GetOtherImage(images,usedImages);
                usedImages.Add(nextImageIndex);
                imageSources[imageIndex] = new BitmapImage(new Uri(images.Values.ElementAt(nextImageIndex)));
            }

            ShowWordAndWait(correctWord, true);
        }

        public void RepeatWord()
        {
            if (correctImageClicked)
            {
                // TODO: Wat is hier logisch? Doe niets of nieuw woord omdat iemand verkeerd heeft geklikt?
                ShowNewWord();
            }
            // TODO: If the images are changed, repeat will lead to problems
            ShowWordAndWait(WordImageFactory.Images.Keys.ElementAt(wordIndex), false);
        }

        private Color GetColorFromGradientBrush(int percentage)
        {
            var offset = percentage/100.0;

            var color = new Color
            {
                ScA = (float) ((offset)*(LastColorProgressBar.ScA - FirstColorProgressBar.ScA) + FirstColorProgressBar.ScA),
                ScR = (float)((offset) * (LastColorProgressBar.ScR - FirstColorProgressBar.ScR) + FirstColorProgressBar.ScR),
                ScG = (float)((offset) * (LastColorProgressBar.ScG - FirstColorProgressBar.ScG) + FirstColorProgressBar.ScG),
                ScB = (float)((offset) * (LastColorProgressBar.ScB - FirstColorProgressBar.ScB) + FirstColorProgressBar.ScB),
            };
            return color;
        }

        private void ShowWordAndWait(string correctWord, bool newWordShown)
        {
            // Show word in textbox
            RichTextBox.Document.Blocks.Clear();
            RichTextBox.AppendText(correctWord);
            RichTextBox.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);

            // Wait specified time
            var worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += CountdownMiliseconds;
            worker.ProgressChanged += delegate(object sender, ProgressChangedEventArgs args)
            {
                if (correctImageClicked)
                {
                    worker.CancelAsync();
                    ProgressBar.Value = 100;
                    ProgressBar.Foreground = new SolidColorBrush(GetColorFromGradientBrush(100));
                    return;
                }
                ProgressBar.Value = args.ProgressPercentage;
                ProgressBar.Foreground = new SolidColorBrush(GetColorFromGradientBrush(args.ProgressPercentage));
                if (args.ProgressPercentage == 100)
                {
                    ProcessWaitingDoneNewImage(newWordShown);
                }
            };

            worker.RunWorkerAsync();
        }

        private void ProcessWaitingDoneNewImage(bool newWordShown)
        {
            RichTextBox.Document.Blocks.Clear();

            if (newWordShown)
            {
                foreach (var imageIndex in imageDictionary.Keys)
                {
                    imageDictionary[imageIndex].Source = imageSources[imageIndex];
                }
            }
            RichTextBox.Background = new SolidColorBrush(RichTextBoxColorHideWord);
        }

        private void CountdownMiliseconds(object sender, DoWorkEventArgs e)
        {
            var startTime = DateTime.Now;
            var stopTime = startTime.AddMilliseconds(ShowWordTime);
            var totalTime = (stopTime - startTime).TotalMilliseconds;
            var currentValue = 0;
            while (DateTime.Now <= stopTime)
            {
                var value = (int)(((DateTime.Now - startTime).TotalMilliseconds / totalTime) * 100);
                if (currentValue < value)
                {
                    (sender as BackgroundWorker).ReportProgress(value);
                    currentValue = value;
                }
            }
            if (currentValue != 100)
            {
                (sender as BackgroundWorker).ReportProgress(100);
            }
        }

        private void ClearComponents()
        {
            correctImageClicked = false;
            foreach (var image in imageDictionary.Values)
            {
                image.Source = null;
            }
            foreach (var panel in panelDictionary.Keys)
            {
                panel.Background = PanelColorNutral;
            }
            RichTextBox.Document.Blocks.Clear();
            RichTextBox.Background = RichTextBoxColorShowWord;
            ProgressBar.Value = 0;
            Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);    
        }

        private int GetOtherImage(Dictionary<string, string> images, List<int> usedImages)
        {
            var nextImageIndex = -1;
            while (nextImageIndex == -1 || usedImages.Contains(nextImageIndex))
            {
                nextImageIndex = RandomGenerator.Next(images.Count);
            }
            return nextImageIndex;
        }

        #region Event handlers
        private void ImageOnMouseEnter(object sender, MouseEventArgs e)
        {
            if (correctImageClicked)
            {
                return;
            }
            var panel = sender as StackPanel;
            panel.Background = new SolidColorBrush(PanelColorMouseOver);
        }

        private void ImageOnMouseLeave(object sender, MouseEventArgs e)
        {
            if (correctImageClicked)
            {
                return;
            }
            var panel = sender as StackPanel;
            panel.Background = PanelColorNutral;
        }

        private void PanelMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correctImageClicked)
            {
                return;
            }
            var panel = sender as StackPanel;
            if (panelDictionary[panel] == correctImageIndex)
            {
                panel.Background = new SolidColorBrush(PanelColorCorrect);
                RichTextBox.Document.Blocks.Clear();
                RichTextBox.AppendText(WordImageFactory.Images.Keys.ElementAt(wordIndex));
                RichTextBox.Background = RichTextBoxColorShowWord;
                correctImageClicked = true;
            }
            else
            {
                panel.Background = new SolidColorBrush(PanelColorIncorrect);
            }
        }
        #endregion
    }
}
