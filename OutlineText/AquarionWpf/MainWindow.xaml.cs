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

namespace AquarionWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Create rainbow Text Effect in Aquarion EVOL anime
        public MainWindow()
        {
            InitializeComponent();

            // Create the outline strategy which is used later on for measuring 
            // the size of text in order to generate a correct sized gradient image
            var strategyOutline2 = TextDesignerWpf.CanvasHelper.TextOutline(MaskColor.Blue, MaskColor.Blue, 6);

            WriteableBitmap canvas = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            // Load a font from its resource, 
            // instead of from system font collection
            //=============================================================
            FontFamily fontFamily = new FontFamily(new Uri("pack://application:,,,/resources/"), "./#Ruzicka Freehand LT Std");
            context.fontFamily = fontFamily;
            context.fontStyle = FontStyles.Normal;
            context.fontWeight = FontWeights.Regular;
            context.nfontSize = 46;

            context.pszText = "I cross over the deep blue void";
            context.ptDraw = new Point(0, 0);

            // Generate the mask image for measuring the size of the text image required
            //============================================================================
            WriteableBitmap maskOutline2 = TextDesignerWpf.CanvasHelper.GenMask(strategyOutline2, (int)(image1.Width), (int)(image1.Height), new Point(0, 0), context);

            uint top = 0;
            uint bottom = 0;
            uint left = 0;
            uint right = 0;
            TextDesignerWpf.CanvasHelper.MeasureMaskLength(maskOutline2, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            bottom += 2;
            right += 2;

            // Generate the gradient image
            //=============================
            WriteableBitmap bmpGrad = new WriteableBitmap((int)(right - left), (int)(bottom - top), 96.0, 96.0, PixelFormats.Pbgra32, null);
            List<Color> list = new List<Color>();
            list.Add(Color.FromRgb(255, 0, 0));
            list.Add(Color.FromRgb(0, 0, 255));
            list.Add(Color.FromRgb(0, 255, 0));
            DrawGradient.Draw(ref bmpGrad, list, true);

            // Because Canvas::ApplyImageToMask requires the all images to have equal dimension,
            // we need to blit our new gradient image onto a larger image to be same size as canvas image
            //==============================================================================================
            WriteableBitmap bmpGrad2 = new WriteableBitmap((int)(image1.Width), (int)(image1.Height), 96.0, 96.0, PixelFormats.Pbgra32, null);
            byte[] pixels = new byte[bmpGrad.PixelHeight * bmpGrad.PixelWidth * bmpGrad.Format.BitsPerPixel / 8];
            bmpGrad.CopyPixels(pixels, bmpGrad.BackBufferStride, 0);
            bmpGrad2.WritePixels(new Int32Rect((int)left, (int)top, (int)(right - left), (int)(bottom - top)), pixels, bmpGrad.BackBufferStride, 0);

            // Apply the rainbow text against the blue mask onto the canvas
            TextDesignerWpf.CanvasHelper.ApplyImageToMask(bmpGrad2, maskOutline2, canvas, MaskColor.Blue, false);

            // Draw the (white body and black outline) text onto the canvas
            //==============================================================
            ITextStrategy strategyOutline1 = TextDesignerWpf.CanvasHelper.TextOutline(Color.FromRgb(255, 255, 255), Color.FromRgb(0, 0, 0), 1);
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline1, ref canvas, new Point(0, 0), context);

            // Finally blit the rendered image onto the window by assigning canvas to the image control
            image1.Source = canvas;
        }
    }
}
