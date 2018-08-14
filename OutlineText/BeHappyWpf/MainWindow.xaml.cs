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

namespace BeHappyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Draw the BE HAPPY effect from the BE HAPPY soap opera
        public MainWindow()
        {
            InitializeComponent();

            // Create canvas to be rendered
            WriteableBitmap canvas = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            // Create canvas for the green outermost outline
            WriteableBitmap canvasOuter = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            // Create canvas for the white inner outline
            WriteableBitmap canvasInner = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            // Load a font from its resource into private collection, 
            // instead of from system font collection
            //=============================================================
            FontFamily fontFamily = new FontFamily(new Uri("pack://application:,,,/resources/"), "./#Alba Regular");
            context.fontFamily = fontFamily;
            context.fontStyle = FontStyles.Normal;
            context.fontWeight = FontWeights.Regular;
            context.nfontSize = 48;

            context.pszText = "bE";
            context.ptDraw = new Point(63, 0);

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(image1.Width), (int)(image1.Height), 96, 96, PixelFormats.Pbgra32);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create the outer strategy
            var strategyOutline2 = TextDesignerWpf.CanvasHelper.TextOutline(Colors.LightSeaGreen, Colors.LightSeaGreen, 16);
            // Draw the bE text (outer green outline)
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline2, ref canvasOuter, new Point(0, 0), context);

            context.pszText = "Happy";
            context.ptDraw = new Point(8, 48);
            // Draw the Happy text (outer green outline)
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline2, ref canvasOuter, new Point(0, 0), context);

            // blit the canvasOuter all the way down (5 pixels down)
            //========================================================
            drawingContext.DrawImage(canvasOuter, new Rect(0.0, 0.0, canvasOuter.Width, canvasOuter.Height - 0.0));
            drawingContext.DrawImage(canvasOuter, new Rect(0.0, 1.0, canvasOuter.Width, canvasOuter.Height - 1.0));
            drawingContext.DrawImage(canvasOuter, new Rect(0.0, 2.0, canvasOuter.Width, canvasOuter.Height - 2.0));
            drawingContext.DrawImage(canvasOuter, new Rect(0.0, 3.0, canvasOuter.Width, canvasOuter.Height - 3.0));
            drawingContext.DrawImage(canvasOuter, new Rect(0.0, 4.0, canvasOuter.Width, canvasOuter.Height - 4.0));
            drawingContext.DrawImage(canvasOuter, new Rect(0.0, 5.0, canvasOuter.Width, canvasOuter.Height - 5.0));

            context.pszText = "bE";
            context.ptDraw = new Point(63, 0);

            // Create the inner white strategy
            var strategyOutline1 = TextDesignerWpf.CanvasHelper.TextOutline(Colors.White, Colors.White, 8);
            // Draw the bE text (inner white outline)
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline1, ref canvasInner, new Point(0, 0), context);
            context.pszText = "Happy";
            context.ptDraw = new Point(8, 48);
            // Draw the Happy text (inner white outline)
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline1, ref canvasInner, new Point(0, 0), context);

            // blit the canvasInner all the way down (5 pixels down)
            //========================================================
            drawingContext.DrawImage(canvasInner, new Rect(0.0, 0.0, canvasInner.Width, canvasInner.Height - 0.0));
            drawingContext.DrawImage(canvasInner, new Rect(0.0, 1.0, canvasInner.Width, canvasInner.Height - 1.0));
            drawingContext.DrawImage(canvasInner, new Rect(0.0, 2.0, canvasInner.Width, canvasInner.Height - 2.0));
            drawingContext.DrawImage(canvasInner, new Rect(0.0, 3.0, canvasInner.Width, canvasInner.Height - 3.0));
            drawingContext.DrawImage(canvasInner, new Rect(0.0, 4.0, canvasInner.Width, canvasInner.Height - 4.0));
            drawingContext.DrawImage(canvasInner, new Rect(0.0, 5.0, canvasInner.Width, canvasInner.Height - 5.0));

            image1.Source = canvasInner;
            // Create the strategy for green text body
            var strategyOutline = TextDesignerWpf.CanvasHelper.TextOutline(Colors.LightSeaGreen, Colors.LightSeaGreen, 1);

            context.pszText = "bE";
            context.ptDraw = new Point(63, 0);
            // Draw the bE text (text body)
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline, ref canvas, new Point(0, 0), context);

            context.pszText = "Happy";
            context.ptDraw = new Point(8, 48);
            // Draw the Happy text (text body)
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyOutline, ref canvas, new Point(0, 0), context);

            drawingContext.DrawImage(canvas, new Rect(0.0, 0.0, canvas.Width, canvas.Height));

            drawingContext.Close();

            bmp.Render(drawingVisual);

            // Finally blit the rendered canvas onto the window by assigning bmp to image control
            image1.Source = bmp;
        }
    }
}
