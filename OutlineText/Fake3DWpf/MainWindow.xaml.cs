using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextDesignerWpf;

namespace Fake3DWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Create the extruded text effect
        public MainWindow()
        {
            InitializeComponent();

            // Create the outline strategy which is going to shift blit diagonally
            var strategyOutline = TextDesignerWpf.Canvas.TextOutline(MaskColor.Blue, MaskColor.Blue, 4);

            WriteableBitmap canvas = TextDesignerWpf.Canvas.GenImage((int)(image1.Width), (int)(image1.Height), Colors.White, 0);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");

            context.fontFamily = fontFamily;
            context.fontStyle = FontStyles.Normal;
            context.fontWeight = FontWeights.Regular;
            context.nfontSize = 40;

            context.pszText = "CODING MONKEY";
            context.ptDraw = new Point(0, 0);

            // the single mask outline
            WriteableBitmap maskOutline = TextDesignerWpf.Canvas.GenMask(strategyOutline, (int)(image1.Width), (int)(image1.Height), new Point(0, 0), context);
            // the mask to store all the single mask blitted diagonally
            WriteableBitmap maskOutlineAll = TextDesignerWpf.Canvas.GenImage((int)(image1.Width) + 10, (int)(image1.Height) + 10);

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(maskOutlineAll.Width), (int)(maskOutlineAll.Height), 96, 96, PixelFormats.Pbgra32);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            // blit diagonally
            for (int i = 0; i < 7; ++i)
                drawingContext.DrawImage(maskOutline, new Rect((double)(i), (double)(i), maskOutline.Width, maskOutline.Height - 0.0));

            drawingContext.Close();

            bmp.Render(drawingVisual);

            maskOutlineAll = new WriteableBitmap(bmp);

            // Measure the dimension of the big mask in order to generate the correct sized gradient image
            //=============================================================================================
            uint top = 0;
            uint bottom = 0;
            uint left = 0;
            uint right = 0;
            TextDesignerWpf.Canvas.MeasureMaskLength(maskOutlineAll, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            right += 2;
            bottom += 2;

            // Generate the gradient image for the diagonal outline
            //=======================================================
            WriteableBitmap gradImage = TextDesignerWpf.Canvas.GenImage((int)(right - left), (int)(bottom - top));

            List<Color> listColors = new List<Color>();
            listColors.Add(Colors.DarkGreen);
            listColors.Add(Colors.YellowGreen);
            DrawGradient.Draw(ref gradImage, listColors, false);

            // Because Canvas::ApplyImageToMask requires all image to have same dimensions,
            // we have to blit our small gradient image onto a temp image as big as the canvas
            //===================================================================================
            WriteableBitmap gradBlitted = TextDesignerWpf.Canvas.GenImage((int)(image1.Width), (int)(image1.Height));

            byte[] pixels2 = new byte[gradImage.PixelHeight * gradImage.PixelWidth * gradImage.Format.BitsPerPixel / 8];
            gradImage.CopyPixels(pixels2, gradImage.BackBufferStride, 0);
            gradBlitted.WritePixels(new Int32Rect((int)left, (int)top, (int)(gradImage.Width), (int)(gradImage.Height)), pixels2, gradImage.BackBufferStride, 0);

            TextDesignerWpf.Canvas.ApplyImageToMask(gradBlitted, maskOutlineAll, canvas, MaskColor.Blue, false);

            // Create strategy and mask image for the text body
            //===================================================
            var strategyText = TextDesignerWpf.Canvas.TextNoOutline(MaskColor.Blue);
            WriteableBitmap maskText = TextDesignerWpf.Canvas.GenMask(strategyText, (int)(image1.Width), (int)(image1.Height), new Point(0, 0), context);

            // Measure the dimension required for text body using the mask
            //=============================================================
            top = 0;
            bottom = 0;
            left = 0;
            right = 0;
            TextDesignerWpf.Canvas.MeasureMaskLength(maskText, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            top -= 2;
            left -= 2;

            right += 2;
            bottom += 2;

            GradientStopCollection coll = new GradientStopCollection();
            GradientStop stop = new GradientStop(Colors.Orange, 0.0);
            coll.Add(stop);
            stop = new GradientStop(Colors.OrangeRed, 1.0);
            coll.Add(stop);

            // Create the gradient brush for the text body
            LinearGradientBrush gradTextbrush = new LinearGradientBrush(coll, 90.0);

            // Create the actual strategy for the text body used for rendering, with the gradient brush
            var strategyText2 = TextDesignerWpf.Canvas.TextNoOutline(gradTextbrush);

            // Draw the newly created strategy onto the canvas
            TextDesignerWpf.Canvas.DrawTextImage(strategyText2, ref canvas, new Point(0, 0), context);

            // Finally blit the rendered image onto the window by assigning canvas to the image control
            image1.Source = canvas;
        }
    }
}
