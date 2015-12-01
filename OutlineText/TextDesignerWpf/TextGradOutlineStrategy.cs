using System;
using System.Collections.Generic;
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
using System.Globalization;
using System.IO;

namespace TextDesignerWpf
{
    public class TextGradOutlineStrategy : ITextStrategy
    {
	    public TextGradOutlineStrategy()
        {
            m_nThickness=2;
            m_brushText = null;
            m_bClrText = true;
            disposed = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                disposed = true;
            }
        }
        ~TextGradOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextGradOutlineStrategy p = new TextGradOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness);
            else
                p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Windows.Media.Color clrText, 
		    System.Windows.Media.Color clrOutline1, 
		    System.Windows.Media.Color clrOutline2, 
		    int nThickness )
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness = nThickness; 
        }

        public void Init(
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline1,
            System.Windows.Media.Color clrOutline2,
            int nThickness)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness = nThickness;
        }

        public bool DrawString(
            System.Windows.Media.DrawingContext graphics,
            System.Windows.Media.FontFamily fontFamily,
            System.Windows.FontStyle fontStyle,
            System.Windows.FontWeight fontWeight,
            double fontSize,
            string strText,
            System.Windows.Point ptDraw,
            System.Globalization.CultureInfo ci)
        {
            Geometry path = GDIPath.CreateTextGeometry(strText, fontFamily, fontStyle, fontWeight, fontSize, ptDraw, ci);

            List<Color> list = new List<Color>();
            CalculateGradient(
                m_clrOutline1,
                m_clrOutline2,
                m_nThickness,
                list);

            for (int i = m_nThickness; i >= 1; --i)
            {
                SolidColorBrush solidbrush = new SolidColorBrush(list[i - 1]);

                Pen pen1 = new Pen(solidbrush, i);
                pen1.LineJoin = PenLineJoin.Round;
                if (m_bClrText)
                {
                    SolidColorBrush brush = new SolidColorBrush(m_clrText);
                    graphics.DrawGeometry(brush, pen1, path);
                }
                else
                    graphics.DrawGeometry(m_brushText, pen1, path);
            }

            return true;
        }


        public Rect MeasureString(
            System.Windows.Media.DrawingContext graphics,
            System.Windows.Media.FontFamily fontFamily,
            System.Windows.FontStyle fontStyle,
            System.Windows.FontWeight fontWeight,
            double fontSize,
            string strText,
            System.Windows.Point ptDraw,
            System.Globalization.CultureInfo ci,
            ref double fStartX,
            ref double fStartY,
            ref double fDestWidth,
            ref double fDestHeight)
        {
            Geometry path = GDIPath.CreateTextGeometry(strText, fontFamily, fontStyle, fontWeight, fontSize, ptDraw, ci);

            Pen pen = new Pen(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), m_nThickness);
            return path.GetRenderBounds(pen);
        }

        void CalculateGradient(
            Color clr1,
            Color clr2,
            int nThickness,
            List<Color> list)
        {
            for (int i = 0; i < nThickness + 1; i++)
            {
                int r, g, b;
                r = clr1.R + (i * (clr2.R - clr1.R) / nThickness);
                g = clr1.G + (i * (clr2.G - clr1.G) / nThickness);
                b = clr1.B + (i * (clr2.B - clr1.B) / nThickness);

                list.Add(Color.FromRgb((byte)(r), (byte)(g), (byte)(b)));
            }
        }


	    protected System.Windows.Media.Color m_clrText;
	    protected System.Windows.Media.Color m_clrOutline1;
	    protected System.Windows.Media.Color m_clrOutline2;
        protected int m_nThickness;
        protected System.Windows.Media.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
