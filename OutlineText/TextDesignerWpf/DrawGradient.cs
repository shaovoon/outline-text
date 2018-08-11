using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TextDesignerWpf
{
    public class DrawGradient
    {
        public static bool Draw(ref System.Windows.Media.Imaging.WriteableBitmap bmp, List<Color> colors, bool horizontal)
        {
            if (colors.Count == 0)
                return false;

            if (colors.Count == 1)
            {
                Rect rect = new Rect();
                rect.Width = bmp.Width;
                rect.Height = bmp.Height;

                SolidColorBrush brush = new SolidColorBrush(colors[0]);

                RenderTargetBitmap bmp1 = new RenderTargetBitmap((int)(bmp.Width), (int)(bmp.Height), 96, 96, PixelFormats.Pbgra32);

                DrawingVisual drawingVisual = new DrawingVisual();

                DrawingContext drawingContext = drawingVisual.RenderOpen();

                Pen pen = new Pen(brush, 1.0);
                drawingContext.DrawRectangle(brush, pen, rect);

                drawingContext.Close();

                bmp1.Render(drawingVisual);

                bmp = new System.Windows.Media.Imaging.WriteableBitmap(bmp1);
            }
            else if (horizontal)
            {
                int gradRectNum = colors.Count - 1;

                Rect rect = new Rect();
                rect.Width = bmp.Width;
                rect.Height = bmp.Height;
                GradientStopCollection coll = new GradientStopCollection();
                double step = 1.0 / gradRectNum;
                double x = 0.0;
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0, 0);

                brush.EndPoint = new Point(1, 1);
                for (int j = 0; j < colors.Count(); ++j)
                {
                    GradientStop stop = new GradientStop(colors[j], x);
                    //coll.Add(stop);
                    brush.GradientStops.Add(stop);
                    x += step;
                }
                //LinearGradientBrush brush = new LinearGradientBrush(coll, 180.0);

                RenderTargetBitmap bmp1 = new RenderTargetBitmap((int)(bmp.Width), (int)(bmp.Height), 96, 96, PixelFormats.Pbgra32);

                DrawingVisual drawingVisual = new DrawingVisual();

                DrawingContext drawingContext = drawingVisual.RenderOpen();

                Pen pen = new Pen(brush, 1.0);
                drawingContext.DrawRectangle(brush, pen, rect);

                drawingContext.Close();

                bmp1.Render(drawingVisual);

                bmp = new System.Windows.Media.Imaging.WriteableBitmap(bmp1);
            }
            else
            {
                int gradRectNum = colors.Count - 1;

                Rect rect = new Rect();
                rect.Width = bmp.Width;
                rect.Height = bmp.Height;
                GradientStopCollection coll = new GradientStopCollection();
                double step = 1.0 / gradRectNum;
                double x = 0.0;
                for (int j = 0; j < colors.Count(); ++j)
                {
                    GradientStop stop = new GradientStop(colors[j], x);
                    coll.Add(stop);
                    x += step;
                }
                LinearGradientBrush brush = new LinearGradientBrush(coll, 90.0);

                RenderTargetBitmap bmp1 = new RenderTargetBitmap((int)(bmp.Width), (int)(bmp.Height), 96, 96, PixelFormats.Pbgra32);

                DrawingVisual drawingVisual = new DrawingVisual();

                DrawingContext drawingContext = drawingVisual.RenderOpen();

                Pen pen = new Pen(brush, 1.0);
                drawingContext.DrawRectangle(brush, pen, rect);

                drawingContext.Close();

                bmp1.Render(drawingVisual);

                bmp = new System.Windows.Media.Imaging.WriteableBitmap(bmp1);
            }

            return true;
        }
    }
}
