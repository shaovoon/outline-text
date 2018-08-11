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

namespace HollowTextWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private int _TimerLoop = 0;

        // Generating the hollow text effect where the text looks like cut out of canvas
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Generating the outline strategy for displaying inside the hollow
            var strategyOutline = Canvas.TextGradOutline(Color.FromArgb(255, 255, 255), Color.FromArgb(230, 230, 230), Color.FromArgb(100, 100, 100), 9, GradientType.Linear);

            Bitmap canvas = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");

            context.fontFamily = fontFamily;
            context.fontStyle = FontStyle.Bold;
            context.nfontSize = 56;

            context.pszText = "CUTOUT";
            context.ptDraw = new Point(0, 0);

            Bitmap hollowImage = Canvas.GenImage(ClientSize.Width, ClientSize.Height);

            // Algorithm to shift the shadow outline in and then out continuous
            int shift = 0;
            if (_TimerLoop >= 0 && _TimerLoop <= 2)
                shift = _TimerLoop;
            else
                shift = 2 - (_TimerLoop - 2);

            // Draw the hollow (shadow) outline by shifting accordingly
            Canvas.DrawTextImage(strategyOutline, hollowImage, new Point(2 + shift, 2 + shift), context);

            // Generate the green mask for the cutout holes in the text
            Bitmap maskImage = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            var strategyMask = Canvas.TextOutline(MaskColor.Green, MaskColor.Green, 0);
            Canvas.DrawTextImage(strategyMask, maskImage, new Point(0, 0), context);

            // Apply the hollowed image against the green mask on the canvas
            Canvas.ApplyImageToMask(hollowImage, maskImage, canvas, MaskColor.Green, false);

            Bitmap backBuffer = Canvas.GenImage(ClientSize.Width, ClientSize.Height, Color.FromArgb(0, 0, 0), 255);

            // Create a black outline only strategy and blit it onto the canvas to cover 
            // the unnatural outline from the gradient shadow
            //=============================================================================
            var strategyOutlineOnly = Canvas.TextOnlyOutline(Color.FromArgb(0, 0, 0), 2, false);
            Canvas.DrawTextImage(strategyOutlineOnly, canvas, new Point(0, 0), context);

            // Draw the transparent canvas onto the back buffer
            //===================================================
            Graphics graphics2 = Graphics.FromImage(backBuffer);
            graphics2.SmoothingMode = SmoothingMode.AntiAlias;
            graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics2.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Finally blit the rendered image onto the window
            e.Graphics.DrawImage(backBuffer, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            hollowImage.Dispose();
            maskImage.Dispose();
            canvas.Dispose();
            backBuffer.Dispose();

            strategyOutline.Dispose();
            strategyMask.Dispose();
            strategyOutlineOnly.Dispose();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++_TimerLoop;

            if (_TimerLoop > 4)
                _TimerLoop = 0;

            Invalidate();
        }
    }
}
