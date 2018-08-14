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

namespace HollowTextWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private int _TimerLoop = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0,0,0,0,500);
            timer.IsEnabled = true;
            timer.Start();
        }

        // Generating the hollow text effect where the text looks like cut out of canvas
        void timer_Tick(object sender, EventArgs e)
        {
            ++_TimerLoop;

            if (_TimerLoop > 4)
                _TimerLoop = 0;

            // Generating the outline strategy for displaying inside the hollow
            var strategyOutline = TextDesignerWpf.CanvasHelper.TextGradOutline(Color.FromRgb(255, 255, 255), Color.FromRgb(230, 230, 230), Color.FromRgb(100, 100, 100), 9);
            // Canvas to render the text. Note the canvas should always be transparent
            WriteableBitmap canvas = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");

            context.fontFamily = fontFamily;
            context.fontStyle = FontStyles.Normal;
            context.fontWeight = FontWeights.Bold;
            context.nfontSize = 56;

            context.pszText = "CUTOUT";
            context.ptDraw = new Point(0, 0);

            WriteableBitmap hollowImage = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            // Algorithm to shift the shadow outline in and then out continuous
            int shift = 0;
            if (_TimerLoop >= 0 && _TimerLoop <= 2)
                shift = _TimerLoop;
            else
                shift = 2 - (_TimerLoop - 2);

            // Draw the hollow (shadow) outline by shifting accordingly
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline, ref hollowImage, new Point(2 + shift, 2 + shift), context);

            // Generate the green mask for the cutout holes in the text
            WriteableBitmap maskImage = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            var strategyMask = TextDesignerWpf.CanvasHelper.TextOutline(MaskColor.Green, MaskColor.Green, 0);
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyMask, ref maskImage, new Point(0, 0), context);

            // Apply the hollowed image against the green mask on the canvas
            TextDesignerWpf.CanvasHelper.ApplyImageToMask(hollowImage, maskImage, canvas, MaskColor.Green, false);

            // Create a black outline only strategy and blit it onto the canvas to cover 
            // the unnatural outline from the gradient shadow
            //=============================================================================
            var strategyOutlineOnly = TextDesignerWpf.CanvasHelper.TextOnlyOutline(Color.FromRgb(0, 0, 0), 2, false);
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutlineOnly, ref canvas, new Point(0, 0), context);

            // Draw the transparent canvas onto the back buffer
            //===================================================
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(canvas.Width), (int)(canvas.Height), 96, 96, PixelFormats.Pbgra32);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            SolidColorBrush brush = new SolidColorBrush(Colors.Black);
            drawingContext.DrawRectangle(brush, new Pen(brush, 1.0), new Rect(0.0, 0.0, canvas.Width, canvas.Height));
            drawingContext.DrawImage(canvas, new Rect(0.0, 0.0, canvas.Width, canvas.Height));

            drawingContext.Close();

            bmp.Render(drawingVisual);

            // Finally blit the rendered image onto the window by assigning to the image control.
            image1.Source = bmp;

            // Release all the resources
            //============================
            strategyOutline.Dispose();
            strategyMask.Dispose();
            strategyOutlineOnly.Dispose();
        }
    }
}
