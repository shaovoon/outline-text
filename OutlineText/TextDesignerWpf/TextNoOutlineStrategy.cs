using System;
using System.Windows;
using System.Windows.Media;

namespace TextDesignerWpf
{
    public class TextNoOutlineStrategy : ITextStrategy
    {
	    public TextNoOutlineStrategy()
        {
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
	    ~TextNoOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextNoOutlineStrategy p = new TextNoOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText);
            else
                p.Init(m_brushText);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Windows.Media.Color clrText)
        {
            m_clrText = clrText;
            m_bClrText = true;
        }

        public void Init(
            System.Windows.Media.Brush brushText)
        {
            m_brushText = brushText;
            m_bClrText = false;
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

            if (m_bClrText)
            {
                SolidColorBrush brush = new SolidColorBrush(m_clrText);
                Pen pen = new Pen(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), 1.0);
                graphics.DrawGeometry(brush, pen, path);
            }
            else
            {
                Pen pen = new Pen(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), 1.0);
                graphics.DrawGeometry(m_brushText, pen, path);

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

            Pen pen = new Pen(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), 1.0);
            return path.GetRenderBounds(pen);
        }

	    protected System.Windows.Media.Color m_clrText;
        protected System.Windows.Media.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
