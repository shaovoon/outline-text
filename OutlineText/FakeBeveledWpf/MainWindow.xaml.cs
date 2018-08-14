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

namespace FakeBeveledWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Draw Faked Beveled effect
        public MainWindow()
        {
            InitializeComponent();

            WriteableBitmap canvas = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            // Load a font from resource
            //===============================
            FontFamily fontFamily = new FontFamily(new Uri("pack://application:,,,/resources/"), "./#Segoe Print");
            
            context.fontFamily = fontFamily;
            context.fontStyle = FontStyles.Normal;
            context.fontWeight = FontWeights.Regular;
            context.nfontSize = 38;

            context.pszText = "Love Like Magic";
            context.ptDraw = new Point(0, 0);

            // Draw the main outline
            //==========================================================
            ITextStrategy mainOutline = TextDesignerWpf.CanvasHelper.TextOutline(Color.FromRgb(235, 10, 230), Color.FromRgb(235, 10, 230), 4);
            TextDesignerWpf.CanvasHelper.DrawTextImage(mainOutline, ref canvas, new Point(4, 4), context);

            // Draw the small bright outline shifted (-2, -2)
            //==========================================================
            ITextStrategy mainBright = TextDesignerWpf.CanvasHelper.TextOutline(Color.FromRgb(252, 173, 250), Color.FromRgb(252, 173, 250), 2);
            TextDesignerWpf.CanvasHelper.DrawTextImage(mainBright, ref canvas, new Point(2, 2), context);

            // Draw the small dark outline shifted (+2, +2)
            //==========================================================
            ITextStrategy mainDark = TextDesignerWpf.CanvasHelper.TextOutline(Color.FromRgb(126, 5, 123), Color.FromRgb(126, 5, 123), 2);
            TextDesignerWpf.CanvasHelper.DrawTextImage(mainDark, ref canvas, new Point(6, 6), context);

            // Draw the smallest outline (color same as main outline)
            //==========================================================
            ITextStrategy mainInner = TextDesignerWpf.CanvasHelper.TextOutline(Color.FromRgb(235, 10, 230), Color.FromRgb(235, 10, 230), 2);
            TextDesignerWpf.CanvasHelper.DrawTextImage(mainInner, ref canvas, new Point(4, 4), context);

            // Finally blit the rendered canvas onto the window by assigning the canvas to the image control
            image1.Source = canvas;
        }
    }
}
