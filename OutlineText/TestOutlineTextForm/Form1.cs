using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TextDesignerCSLibrary;

// My scratch pad for testing dotNet APIs.

namespace TestOutlineTextForm
{
    public partial class Form1 : Form
    {
        private Bitmap bmpSrc;

        public Form1()
        {
            InitializeComponent();
            // Load Image
            bmpSrc = Resource.PaintSky;
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Drawing the back ground color
            Color m_clrBkgd = Color.FromArgb(255, 255, 255);
            SolidBrush brushBkgnd = new SolidBrush(m_clrBkgd);
            e.Graphics.FillRectangle(brushBkgnd, 0, 0, this.ClientSize.Width, this.ClientSize.Width);

            PngOutlineText m_PngOutlineText = new PngOutlineText();
            m_PngOutlineText.SetPngImage(new Bitmap(ClientSize.Width, ClientSize.Height));
            m_PngOutlineText.TextGradOutline(
                Color.FromArgb(255, 128, 64),
                Color.FromArgb(255, 64, 0, 64),
                Color.FromArgb(255, 255, 128, 255),
                10);
            m_PngOutlineText.EnableReflection(false);

            m_PngOutlineText.EnableShadow(true);
            //Rem to SetNullShadow() to release memory if a previous shadow has been set.
            m_PngOutlineText.SetNullShadow();
            m_PngOutlineText.Shadow(
                Color.FromArgb(128, 0, 0, 0), 8,
                new Point(4, 4));
            LOGFONT m_LogFont = new LOGFONT();
            m_LogFont.lfFaceName = "Arial Black";
            m_LogFont.lfHeight = -48;

            m_LogFont.lfOrientation = 0;
            m_LogFont.lfEscapement = 0;
            m_LogFont.lfItalic = false;
            float fStartX = 0.0f;
            float fStartY = 0.0f;
            float fDestWidth = 0.0f;
            float fDestHeight = 0.0f;
            m_PngOutlineText.GdiMeasureString(
                e.Graphics,
                m_LogFont,
                "TEXT DESIGNER",
                new Point(10, 10),
                ref fStartX,
                ref fStartY,
                ref fDestWidth,
                ref fDestHeight);
            m_PngOutlineText.SetShadowBkgd(m_clrBkgd, (int)fDestWidth+10, (int)fDestHeight+10);
            LinearGradientBrush gradientBrush = new LinearGradientBrush(new RectangleF(fStartX, fStartY, fDestWidth - fStartX, fDestHeight - fStartY),
                    Color.FromArgb(255, 128, 64), Color.FromArgb(255, 0, 0), LinearGradientMode.Vertical);
            m_PngOutlineText.TextGradOutline(
                gradientBrush,
                Color.FromArgb(255, 64, 0, 64),
                Color.FromArgb(255, 255, 128, 255),
                10);

            m_PngOutlineText.GdiDrawString(
                e.Graphics,
                m_LogFont,
                "TEXT DESIGNER",
                new Point(10, 10));

            e.Graphics.DrawImage(m_PngOutlineText.GetPngImage(), new Point(0, 0));

            brushBkgnd.Dispose();
            e.Graphics.Dispose();
        }
        private void OnResize(object sender, EventArgs e)
        {
            Invalidate(true);
        }
    }
}