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
    class DiffusedShadowStrategy : ITextStrategy
    {
	    public DiffusedShadowStrategy()
        {
            m_nThickness=8;
            m_bOutlinetext = false;
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
        ~DiffusedShadowStrategy()
        {
            Dispose(false);
        }

        public ITextStrategy Clone()
        {
            DiffusedShadowStrategy p = new DiffusedShadowStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline, m_nThickness, m_bOutlinetext);
            else
                p.Init(m_brushText, m_clrOutline, m_nThickness, m_bOutlinetext);

            return (ITextStrategy)(p);
        }

        public void Init(
		    System.Windows.Media.Color clrText, 
		    System.Windows.Media.Color clrOutline, 
		    int nThickness,
		    bool bOutlinetext)
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness;
            m_bOutlinetext = bOutlinetext;
        }

        public void Init(
            System.Windows.Media.Brush brushText, 
            System.Windows.Media.Color clrOutline,
            int nThickness,
            bool bOutlinetext)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness;
            m_bOutlinetext = bOutlinetext;
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

            for (int i = 1; i <= m_nThickness; ++i)
            {
                SolidColorBrush solidbrush = new SolidColorBrush(m_clrOutline);

                Pen pen = new Pen(solidbrush, i);
                pen.LineJoin = PenLineJoin.Round;
                SolidColorBrush brush = new SolidColorBrush(m_clrText);
                graphics.DrawGeometry(brush, pen, path);
            }

            if (m_bOutlinetext == false)
            {
                for (int i = 1; i <= m_nThickness; ++i)
                {
                    if (m_bClrText)
                    {
                        SolidColorBrush brush = new SolidColorBrush(m_clrText);
                        SolidColorBrush solidbrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                        Pen pen = new Pen(solidbrush, 1.0);
                        graphics.DrawGeometry(brush, pen, path);
                    }
                    else
                    {
                        SolidColorBrush solidbrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                        Pen pen = new Pen(solidbrush, 1.0);
                        graphics.DrawGeometry(m_brushText, pen, path);

                    }
                }
            }
            else
            {
                if (m_bClrText)
                {
                    SolidColorBrush brush = new SolidColorBrush(m_clrText);
                    SolidColorBrush solidbrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                    Pen pen = new Pen(solidbrush, 1.0);
                    graphics.DrawGeometry(brush, pen, path);
                }
                else
                {
                    SolidColorBrush solidbrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                    Pen pen = new Pen(solidbrush, 1.0);
                    graphics.DrawGeometry(m_brushText, pen, path);
                }
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


	    protected System.Windows.Media.Color m_clrText;
	    protected System.Windows.Media.Color m_clrOutline;
	    protected int m_nThickness;
        protected bool m_bOutlinetext;
        protected System.Windows.Media.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
