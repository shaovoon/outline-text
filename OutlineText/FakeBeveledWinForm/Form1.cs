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

namespace FakeBeveledWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Draw Faked Beveled effect
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Bitmap canvas = Canvas.GenImage(ClientSize.Width, ClientSize.Height);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            // Load a font from its file into private collection, 
            // instead of from system font collection
            //=============================================================
            PrivateFontCollection fontcollection = new PrivateFontCollection();

            string szFontFile = "..\\..\\..\\CommonFonts\\Segoe Print.TTF";

            fontcollection.AddFontFile(szFontFile);
            if (fontcollection.Families.Count() > 0)
                context.fontFamily = fontcollection.Families[0];

            context.fontStyle = FontStyle.Regular;
            context.nfontSize = 38;

            context.pszText = "Love Like Magic";
            context.ptDraw = new Point(0, 0);

            // Draw the main outline
            //==========================================================
            ITextStrategy mainOutline = Canvas.TextOutline(Color.FromArgb(235, 10, 230), Color.FromArgb(235, 10, 230), 4);
            Canvas.DrawTextImage(mainOutline, canvas, new Point(4, 4), context);

            // Draw the small bright outline shifted (-2, -2)
            //==========================================================
            ITextStrategy mainBright = Canvas.TextOutline(Color.FromArgb(252, 173, 250), Color.FromArgb(252, 173, 250), 2);
            Canvas.DrawTextImage(mainBright, canvas, new Point(2, 2), context);

            // Draw the small dark outline shifted (+2, +2)
            //==========================================================
            ITextStrategy mainDark = Canvas.TextOutline(Color.FromArgb(126, 5, 123), Color.FromArgb(126, 5, 123), 2);
            Canvas.DrawTextImage(mainDark, canvas, new Point(6, 6), context);

            // Draw the smallest outline (color same as main outline)
            //==========================================================
            ITextStrategy mainInner = Canvas.TextOutline(Color.FromArgb(235, 10, 230), Color.FromArgb(235, 10, 230), 2);
            Canvas.DrawTextImage(mainInner, canvas, new Point(4, 4), context);

            // Finally blit the rendered canvas onto the window
            e.Graphics.DrawImage(canvas, 0, 0, ClientSize.Width, ClientSize.Height);

            // Release all the resources
            //============================
            canvas.Dispose();

            mainOutline.Dispose();
            mainBright.Dispose();
            mainDark.Dispose();
            mainInner.Dispose();

        }
    }
}
