using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using TextDesignerCSLibrary;
using System.Drawing.Text;

namespace InnerOutlineWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create the outline strategy which is used later on for measuring 
            // the size of text in order to generate a correct sized gradient image
            var strategyOutline2 = Canvas.TextNoOutline(MaskColor.Blue);

            Bitmap canvas = Canvas.GenImage(ClientSize.Width, ClientSize.Height, Color.White, 0);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");
            context.fontFamily = fontFamily;
            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 54;

            context.pszText = "VACATION";
            context.ptDraw = new Point(0, 0);

            var strategyOutline3 = Canvas.TextOutline(Color.Black, Color.Black, 4);
            Canvas.DrawTextImage(strategyOutline3, canvas, new Point(0, 0), context);

            // Generate the mask image for measuring the size of the text image required
            //============================================================================
            Bitmap maskOutline2 = Canvas.GenMask(strategyOutline2, ClientSize.Width, ClientSize.Height, new Point(0, 0), context);

            uint top = 0;
            uint bottom = 0;
            uint left = 0;
            uint right = 0;
            Canvas.MeasureMaskLength(maskOutline2, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            bottom += 2;
            right += 2;

            Color light_purple = Color.FromArgb(192, 201, 250);
            Color dark_purple = Color.FromArgb(136, 137, 196);

            using (Bitmap text = Canvas.GenImage(ClientSize.Width, ClientSize.Height, dark_purple))
            {
                using (var strategyText2 = Canvas.TextGradOutlineLast(light_purple, dark_purple, light_purple, 7))
                {
                    Canvas.DrawTextImage(strategyText2, text, new Point(0, 0), context);
                    Canvas.ApplyImageToMask(text, maskOutline2, canvas, MaskColor.Blue, true);
                }
            }

            // Finally blit the rendered image onto the window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            e.Graphics.Dispose();

            canvas.Dispose();

            maskOutline2.Dispose();
            strategyOutline2.Dispose();
            strategyOutline3.Dispose();

        }
    }
}
