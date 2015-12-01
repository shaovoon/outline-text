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
using System.Drawing.Text;

namespace DirtyWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Create dirty text effect
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Bitmap canvas = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Load the dirty image from file
            Bitmap canvasDirty = new Bitmap("..\\..\\..\\CommonImages\\dirty-texture.png");

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");
            context.fontFamily = fontFamily;
            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 48;

            context.pszText = "DIRTY";
            context.ptDraw = new Point(5, 70);

            // Load the texture image from file
            Bitmap texture = new Bitmap("..\\..\\..\\CommonImages\\texture_blue.jpg");

            Bitmap texture2 = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Draw the texture against the red dirty mask onto the 2nd texture
            Canvas.ApplyImageToMask(texture, canvasDirty, texture2, MaskColor.Red, false);
		    TextureBrush textureBrush = new TextureBrush(texture2);

		    Bitmap textureShadow = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Draw the gray color against the red dirty mask onto the shadow texture
            Canvas.ApplyColorToMask(Color.FromArgb(0xaa, 0xcc, 0xcc, 0xcc), canvasDirty, textureShadow, MaskColor.Red);
            // Create texture brush for the shadow
            TextureBrush shadowBrush = new TextureBrush(textureShadow);

            // Create strategy for the shadow with the shadow brush
            var strategyShadow = Canvas.TextNoOutline(shadowBrush);

		    Bitmap canvasTemp = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Draw the shadow image first onto the temp canvas
            Canvas.DrawTextImage(strategyShadow, canvasTemp, new Point(0, 0), context);

            // Create strategy for the text body
            var strategy = Canvas.TextNoOutline(textureBrush);
            // Draw text body
		    Canvas.DrawTextImage(strategy, canvas, new Point(0,0), context);

            // Draw the shadow image (canvasTemp) shifted -3, -3
            e.Graphics.DrawImage(canvasTemp, 3, 3, ClientSize.Width - 3, ClientSize.Height - 3);
            // Then draw the rendered image onto window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            e.Graphics.Dispose();
            texture.Dispose();
            texture2.Dispose();
            textureShadow.Dispose();

            canvasDirty.Dispose();

            canvas.Dispose();
            canvasTemp.Dispose();

            strategyShadow.Dispose();
            strategy.Dispose();

            textureBrush.Dispose();
            shadowBrush.Dispose();
        }
    }
}
