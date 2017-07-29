using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Games.MandalaGamePlugin.ModelView
{
    public static class MandalaSaveHelper
    {
        public static void SaveMandala(Canvas canvas)
        {
            var filename = GetFileName();
            if (filename == null)
            {
                return;
            }
            var ext = Path.GetExtension(filename).ToLower();

            var renderTargetBitmap = PrepareRenderTargetBitmap(canvas);

            BitmapEncoder bitmapEncoder = null;
            switch (ext)
            {
                case ".png":
                    bitmapEncoder = new PngBitmapEncoder();
                    break;
                case ".gif":
                    bitmapEncoder = new GifBitmapEncoder();
                    break;
                case ".jpg":
                case ".jpeg":
                    bitmapEncoder = new JpegBitmapEncoder();
                    break;
                case ".bmp":
                    bitmapEncoder = new BmpBitmapEncoder();
                    break;
                case ".tiff":
                case ".tif":
                    bitmapEncoder = new TiffBitmapEncoder();
                    break;
            }

            if (bitmapEncoder == null)
            {
                return;
            }

            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            try
            {
                MemoryStream memoryStream = new MemoryStream();

                bitmapEncoder.Save(memoryStream);
                memoryStream.Close();

                File.WriteAllBytes(filename, memoryStream.ToArray());
            }
            catch (Exception err)
            {
                // ignored
            }
        }

        private static RenderTargetBitmap PrepareRenderTargetBitmap(Canvas canvas)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(canvas);
            double dpi = 96d;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int) bounds.Width, (int) bounds.Height, dpi, dpi,
                PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(canvas);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);
            return rtb;
        }

        private static string GetFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Bestand|*.png|JPeg Bestand|*.jpg|Bitmap Bestand|*.bmp|Gif Bestand|*.gif|TIFF Bestand|*.tiff",
                Title = "Kies een bestandsnaam om de mandala op te slaan"
            };
            saveFileDialog.ShowDialog();

            return saveFileDialog.FileName != "" ? saveFileDialog.FileName : null;
        }
    }
}
