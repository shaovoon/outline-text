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

            Bitmap canvas = CanvasHelper.GenImage(ClientSize.Width, ClientSize.Height, Color.White, 0);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");
            context.fontFamily = fontFamily;
            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 54;

            context.pszText = "VACATION";
            context.ptDraw = new Point(0, 0);

            Color light_purple = Color.FromArgb(102, 159, 206);
            Color dark_purple = Color.FromArgb(35, 68, 95);
            using (var strategyOutline3 = CanvasHelper.TextGradOutline(light_purple, dark_purple, light_purple, 9, GradientType.Linear))
            {
                CanvasHelper.DrawTextImage(strategyOutline3, canvas, new Point(0, 0), context);
            }

            Bitmap maskOutline2;
            using (var strategyOutline2 = CanvasHelper.TextNoOutline(MaskColor.Blue))
            {
                maskOutline2 = CanvasHelper.GenMask(strategyOutline2, ClientSize.Width, ClientSize.Height, new Point(0, 0), context);
            }
            uint top = 0;
            uint bottom = 0;
            uint left = 0;
            uint right = 0;
            CanvasHelper.MeasureMaskLength(maskOutline2, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            bottom += 2;
            right += 2;
            Color light_yellow = Color.FromArgb(255, 227, 85);
            Color dark_yellow = Color.FromArgb(243, 163, 73);
            using (Bitmap text = CanvasHelper.GenImage(ClientSize.Width, ClientSize.Height, dark_yellow))
            {
                using (var strategyText2 = CanvasHelper.TextGradOutlineLast(light_yellow, dark_yellow, light_yellow, 9, GradientType.Sinusoid))
                {
                    CanvasHelper.DrawTextImage(strategyText2, text, new Point(0, 0), context);
                    CanvasHelper.ApplyImageToMask(text, maskOutline2, canvas, MaskColor.Blue, true);
                }
            }

            // Finally blit the rendered image onto the window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            e.Graphics.Dispose();

            canvas.Dispose();

            maskOutline2.Dispose();
        }
    }
}
