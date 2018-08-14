using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using TextDesignerCSLibrary;
using System.Drawing.Text;

namespace AquarionWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Create rainbow Text Effect in Aquarion EVOL anime
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create the outline strategy which is used later on for measuring 
            // the size of text in order to generate a correct sized gradient image
            var strategyOutline2 = CanvasHelper.TextOutline(MaskColor.Blue, MaskColor.Blue, 8);
            
            Bitmap canvas = CanvasHelper.GenImage(ClientSize.Width, ClientSize.Height, Color.White, 0);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            // Load a font from its file into private collection, 
            // instead of from system font collection
            //=============================================================
            PrivateFontCollection fontcollection = new PrivateFontCollection();

            string szFontFile = "..\\..\\..\\CommonFonts\\Ruzicka TypeK.ttf";

            fontcollection.AddFontFile(szFontFile);

            if(fontcollection.Families.Count()>0)
                context.fontFamily = fontcollection.Families[0];
            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 36;

            context.pszText = "I cross over the deep blue void";
            context.ptDraw = new Point(0, 0);

            // Generate the mask image for measuring the size of the text image required
            //============================================================================
            Bitmap maskOutline2 = CanvasHelper.GenMask(strategyOutline2, ClientSize.Width, ClientSize.Height, new Point(0, 0), context);

            uint top = 0;
            uint bottom = 0;
            uint left = 0;
            uint right = 0;
            CanvasHelper.MeasureMaskLength(maskOutline2, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            bottom += 2;
            right += 2;

            // Generate the gradient image
            //=============================
            Bitmap bmpGrad = new Bitmap((int)(right - left), (int)(bottom - top), PixelFormat.Format32bppArgb);
            List<Color> list = new List<Color>();
            list.Add(Color.FromArgb(255, 0, 0));
            list.Add(Color.FromArgb(0, 0, 255));
            list.Add(Color.FromArgb(0, 255, 0));
            DrawGradient.Draw(bmpGrad, list, true);

            // Because Canvas::ApplyImageToMask requires the all images to have equal dimension,
            // we need to blit our new gradient image onto a larger image to be same size as canvas image
            //==============================================================================================
            Bitmap bmpGrad2 = new Bitmap(ClientSize.Width, ClientSize.Height, PixelFormat.Format32bppArgb);
            Graphics graphGrad = Graphics.FromImage(bmpGrad2);
            graphGrad.SmoothingMode = SmoothingMode.AntiAlias;
            graphGrad.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphGrad.DrawImage(bmpGrad, (int)left, (int)top, (int)(right - left), (int)(bottom - top));

            // Apply the rainbow text against the blue mask onto the canvas
            CanvasHelper.ApplyImageToMask(bmpGrad2, maskOutline2, canvas, MaskColor.Blue, false);

            // Draw the (white body and black outline) text onto the canvas
            //==============================================================
            var strategyOutline1 = CanvasHelper.TextOutline(Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0), 4);
            CanvasHelper.DrawTextImage(strategyOutline1, canvas, new Point(0, 0), context);

            // Finally blit the rendered image onto the window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            e.Graphics.Dispose();

            bmpGrad.Dispose();
            bmpGrad2.Dispose();
            canvas.Dispose();
            
            maskOutline2.Dispose();
            strategyOutline2.Dispose();
            strategyOutline1.Dispose();

        }
    }
}
