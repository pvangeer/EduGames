using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using NUnit.Framework;

namespace EduGamesTest2
{
    [TestFixture]
    public class TextRangeHighlightingTests
    {
        [Test, STAThread]
        public void TestTextRange()
        {
            var str = "school is leuk toch mama?";
            var rtb = new RichTextBox {AcceptsReturn = false};
            rtb.AppendText(str);
            var rtbText = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
            Assert.AreEqual(Enumerable.Concat(str,"\r\n"),rtbText);

            var startPosition = rtb.Document.ContentStart.GetPositionAtOffset(2);
            
            var start = new TextRange(startPosition, startPosition.GetPositionAtOffset(3)).Text;
            Assert.AreEqual(str.Take(3),start);

            var range = new TextRange(startPosition, startPosition.GetPositionAtOffset(3));
            range.ApplyPropertyValue(TextElement.ForegroundProperty,new SolidColorBrush(Colors.AliceBlue));

            var start2 = new TextRange(startPosition, startPosition.GetPositionAtOffset(3)).Text;
            Assert.AreNotEqual(str.Take(3), start2);

            var newStartPosition = rtb.Document.ContentStart.GetPositionAtOffset(2 + 1);
            var start3 = new TextRange(newStartPosition, newStartPosition.GetPositionAtOffset(3)).Text;
            Assert.AreNotEqual(str.Take(3),start3);
        }
    }
}
