using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextDesignerCSLibrary;

namespace Fake3DWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Create the extruded text effect
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create the outline strategy which is going to shift blit diagonally
            var strategyOutline = CanvasHelper.TextOutline(MaskColor.Blue, MaskColor.Blue, 4);

            Bitmap canvas = CanvasHelper.GenImage(ClientSize.Width, ClientSize.Height, Color.White, 0);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");

            context.fontFamily = fontFamily;
            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 40;

            context.pszText = "CODING MONKEY";
            context.ptDraw = new Point(0, 0);

            // the single mask outline
            Bitmap maskOutline = CanvasHelper.GenMask(strategyOutline, ClientSize.Width, ClientSize.Height, new Point(0, 0), context);
            // the mask to store all the single mask blitted diagonally
            Bitmap maskOutlineAll = CanvasHelper.GenImage(ClientSize.Width + 10, ClientSize.Height + 10);

            Graphics graphMaskAll = Graphics.FromImage(maskOutlineAll);

            // blit diagonally
            for (int i = 0; i < 7; ++i)
                graphMaskAll.DrawImage(maskOutline, i, i, ClientSize.Width, ClientSize.Height);

            // Measure the dimension of the big mask in order to generate the correct sized gradient image
            //=============================================================================================
            uint top = 0;
            uint bottom = 0;
            uint left = 0;
            uint right = 0;
            CanvasHelper.MeasureMaskLength(maskOutlineAll, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            right += 2;
            bottom += 2;

            // Generate the gradient image for the diagonal outline
            //=======================================================
            Bitmap gradImage = CanvasHelper.GenImage((int)(right - left), (int)(bottom - top));

            List<Color> listColors = new List<Color>();
            listColors.Add(Color.DarkGreen);
            listColors.Add(Color.YellowGreen);
            DrawGradient.Draw(gradImage, listColors, false);

            // Because Canvas::ApplyImageToMask requires all image to have same dimensions,
            // we have to blit our small gradient image onto a temp image as big as the canvas
            //===================================================================================
            Bitmap gradBlitted = CanvasHelper.GenImage(ClientSize.Width, ClientSize.Height);

            Graphics graphgradBlitted = Graphics.FromImage(gradBlitted);

            graphgradBlitted.DrawImage(gradImage, (int)left, (int)top, (int)(gradImage.Width), (int)(gradImage.Height));

            CanvasHelper.ApplyImageToMask(gradBlitted, maskOutlineAll, canvas, MaskColor.Blue, false);

            // Create strategy and mask image for the text body
            //===================================================
            var strategyText = CanvasHelper.TextNoOutline(MaskColor.Blue);
            Bitmap maskText = CanvasHelper.GenMask(strategyText, ClientSize.Width, ClientSize.Height, new Point(0, 0), context);

            // Measure the dimension required for text body using the mask
            //=============================================================
            top = 0;
            bottom = 0;
            left = 0;
            right = 0;
            CanvasHelper.MeasureMaskLength(maskText, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
            top -= 2;
            left -= 2;

            right += 2;
            bottom += 2;

            // Create the gradient brush for the text body
            LinearGradientBrush gradTextbrush = new LinearGradientBrush(new Rectangle((int)left, (int)top, (int)right, (int)bottom), Color.Orange, Color.OrangeRed, 90.0f);
            // Create the actual strategy for the text body used for rendering, with the gradient brush
            var strategyText2 = CanvasHelper.TextNoOutline(gradTextbrush);
            // Draw the newly created strategy onto the canvas
            CanvasHelper.DrawTextImage(strategyText2, canvas, new Point(0, 0), context);

            // Finally blit the rendered canvas onto the window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            maskOutline.Dispose();
            maskOutlineAll.Dispose();

            gradImage.Dispose();
            gradBlitted.Dispose();
            maskText.Dispose();

            canvas.Dispose();

            strategyText.Dispose();
            strategyText2.Dispose();
            strategyOutline.Dispose();

            gradTextbrush.Dispose();

        }
    }
}
