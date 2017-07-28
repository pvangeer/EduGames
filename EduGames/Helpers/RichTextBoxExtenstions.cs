using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace EduGames.Helpers
{
    public static class RichTextBoxExtenstions
    {
        
        public static Color KlinkerColor = Colors.DodgerBlue;
        public static Color TextColor = Colors.Black;
        public static Color DubbeleKlinkersColor = Colors.DodgerBlue;
        public static Color DubbelKlankColor = Colors.Goldenrod;
        public static Color SamengesteldeKlankenColor = Colors.DarkOrange;

        public static bool UseColorHighlighting { get; set; }

        public static void ConfigureRichtTextBox(this RichTextBox richTextBox)
        {
            richTextBox.Document.TextAlignment = TextAlignment.Center;
            StartHighlighting(richTextBox);
        }

        public static void StartHighlighting(this RichTextBox richTextBox)
        {
            richTextBox.TextChanged += TextChangedHandler;
        }

        public static void StopHighlighting(this RichTextBox richTextBox)
        {
            richTextBox.TextChanged -= TextChangedHandler;
        }

        private static void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            ApplyStyleToText(sender as RichTextBox);
        }

        public static void ApplyStyleToText(this RichTextBox richTextBox)
        {
            if (richTextBox.Document == null)
            {
                return;
            }

            richTextBox.TextChanged -= TextChangedHandler;

            var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text.ToLower();
            richTextBox.Document.Blocks.Clear();

            if (!UseColorHighlighting)
            {
                richTextBox.AppendText(text);
                return;
            }

            var index = 0;
            while (index < text.Count() - 2)
            {
                if (CheckContainsKeyword(richTextBox, WordImageFactory.SamengesteldeKlanken, text, SamengesteldeKlankenColor, ref index))
                {
                    continue;
                }
                if (CheckContainsKeyword(richTextBox, WordImageFactory.DubbelKlanken, text, DubbelKlankColor, ref index))
                {
                    continue;
                }
                if (CheckContainsKeyword(richTextBox, WordImageFactory.DubbeleKlinkers, text, DubbeleKlinkersColor, ref index))
                {
                    continue;
                }

                var character = text.Substring(index,1);
                AppendFormattedText(richTextBox, character, WordImageFactory.Klinkers.Contains(character[0]) ? KlinkerColor : TextColor);
                index++;
            }
            
            // Get the current caret position.
            var caretPos = richTextBox.CaretPosition;

            // Set the TextPointer to the end of the current document.
            caretPos = caretPos.DocumentEnd;

            // Specify the new caret position at the end of the current document.
            richTextBox.CaretPosition = caretPos;

            richTextBox.TextChanged += TextChangedHandler;
        }

        private static void AppendFormattedText(RichTextBox richTextBox, string text, Color color)
        {
            var range = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd) { Text = text };
            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            richTextBox.ScrollToEnd();
        }

        private static bool CheckContainsKeyword(RichTextBox richTextBox, IEnumerable<string> keysList, string text, Color keyGroupColor, ref int index)
        {
            var i = index;
            if (index + 2 < text.Length - 2)
            {
                var keyIndex = keysList.ToList().IndexOf(text.Substring(i, 3));
                if (keyIndex > -1)
                {
                    AppendFormattedText(richTextBox, text.Substring(index, 3), keyGroupColor);
                    index += 3;
                    return true;
                }
            }
            if (index + 1 < text.Length - 2)
            {
                var keyIndex = keysList.ToList().IndexOf(text.Substring(i, 2));
                if (keyIndex > -1)
                {
                    AppendFormattedText(richTextBox, text.Substring(index, 2), keyGroupColor);
                    index += 2;
                    return true;
                }
            }
            return false;
        }
    }
}
