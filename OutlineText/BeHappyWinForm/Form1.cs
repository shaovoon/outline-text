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

namespace BeHappyWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Draw the BE HAPPY effect from the BE HAPPY soap opera
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create canvas to be rendered
            Bitmap canvas = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Create canvas for the green outermost outline
            Bitmap canvasOuter = Canvas.GenImage(ClientSize.Width, ClientSize.Height);
            // Create canvas for the white inner outline
            Bitmap canvasInner = Canvas.GenImage(ClientSize.Width, ClientSize.Height);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            // Load a font from its file into private collection, 
            // instead of from system font collection
            //=============================================================
            PrivateFontCollection fontcollection = new PrivateFontCollection();

            string szFontFile = "..\\..\\..\\CommonFonts\\ALBA____.TTF";

            fontcollection.AddFontFile(szFontFile);
            if (fontcollection.Families.Count() > 0)
                context.fontFamily = fontcollection.Families[0];
            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 48;

            context.pszText = "bE";
            context.ptDraw = new Point(55, 0);

            // Create the outer strategy to draw the bE text
            var strategyOutline2 = Canvas.TextOutline(Color.LightSeaGreen, Color.LightSeaGreen, 16);
            // Draw the bE text (outer green outline)
            Canvas.DrawTextImage(strategyOutline2, canvasOuter, new Point(0, 0), context);
            context.pszText = "Happy";
            context.ptDraw = new Point(0, 48);
            // Draw the Happy text (outer green outline)
            Canvas.DrawTextImage(strategyOutline2, canvasOuter, new Point(0, 0), context);

            // blit the canvasOuter all the way down (5 pixels down)
            //========================================================
            Graphics graphicsCanvas = Graphics.FromImage(canvas);
            graphicsCanvas.SmoothingMode = SmoothingMode.AntiAlias;
            graphicsCanvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphicsCanvas.DrawImage(canvasOuter, 0, 0, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasOuter, 0, 1, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasOuter, 0, 2, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasOuter, 0, 3, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasOuter, 0, 4, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasOuter, 0, 5, ClientSize.Width, ClientSize.Height);
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            context.pszText = "bE";
            context.ptDraw = new Point(55, 0);

            // Create the inner white strategy
            var strategyOutline1 = Canvas.TextOutline(Color.White, Color.White, 8);
            // Draw the bE text (inner white outline)
            Canvas.DrawTextImage(strategyOutline1, canvasInner, new Point(0, 0), context);

            context.pszText = "Happy";
            context.ptDraw = new Point(0, 48);
            // Draw the Happy text (inner white outline)
            Canvas.DrawTextImage(strategyOutline1, canvasInner, new Point(0, 0), context);

            // blit the canvasInner all the way down (5 pixels down)
            //========================================================
            graphicsCanvas.DrawImage(canvasInner, 0, 0, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasInner, 0, 1, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasInner, 0, 2, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasInner, 0, 3, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasInner, 0, 4, ClientSize.Width, ClientSize.Height);
            graphicsCanvas.DrawImage(canvasInner, 0, 5, ClientSize.Width, ClientSize.Height);
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Create the strategy for green text body
            var strategyOutline = Canvas.TextOutline(Color.LightSeaGreen, Color.LightSeaGreen, 1);

            context.pszText = "bE";
            context.ptDraw = new Point(55, 0);
            // Draw the bE text (text body)
            Canvas.DrawTextImage(strategyOutline, canvas, new Point(0, 0), context);

            context.pszText = "Happy";
            context.ptDraw = new Point(0, 48);
            // Draw the Happy text (text body)
            Canvas.DrawTextImage(strategyOutline, canvas, new Point(0, 0), context);

            // Finally blit the rendered canvas onto the window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            e.Graphics.Dispose();

            canvasOuter.Dispose();

            canvasInner.Dispose();

            canvas.Dispose();

            strategyOutline2.Dispose();
            strategyOutline1.Dispose();
            strategyOutline.Dispose();

        }
    }
}
