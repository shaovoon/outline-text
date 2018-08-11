using System;
using System.Windows;
using System.Windows.Media;

namespace TextDesignerWpf
{
    public class TextOutlineStrategy : ITextStrategy
    {
	    public TextOutlineStrategy()
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

	    ~TextOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextOutlineStrategy p = new TextOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline, m_nThickness);
            else
                p.Init(m_brushText, m_clrOutline, m_nThickness);

            return (ITextStrategy)(p);
        }


	    public void Init(
		    System.Windows.Media.Color clrText, 
		    System.Windows.Media.Color clrOutline, 
		    int nThickness )
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness; 
        }

        public void Init(
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline,
            int nThickness)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline = clrOutline;
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

            SolidColorBrush solidbrush = new SolidColorBrush(m_clrOutline);

            Pen pen = new Pen(solidbrush, m_nThickness);
            pen.LineJoin = PenLineJoin.Round;

            if (m_bClrText)
            {
                SolidColorBrush brush = new SolidColorBrush(m_clrText);
                graphics.DrawGeometry(brush, pen, path);
            }
            else
                graphics.DrawGeometry(m_brushText, pen, path);
         
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
        protected System.Windows.Media.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
