using System;
using System.Windows;
using System.Windows.Media;

namespace TextDesignerWpf
{
    public class TextDblOutlineStrategy : ITextStrategy
    {
	    public TextDblOutlineStrategy()
        {
            m_nThickness1=2;
            m_nThickness2=2;
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
        ~TextDblOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextDblOutlineStrategy p = new TextDblOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness1, m_nThickness2);
            else
                p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness1, m_nThickness2);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Windows.Media.Color clrText, 
		    System.Windows.Media.Color clrOutline1, 
		    System.Windows.Media.Color clrOutline2, 
		    int nThickness1,
		    int nThickness2 )
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness1 = nThickness1; 
            m_nThickness2 = nThickness2; 
        }

        public void Init(
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline1,
            System.Windows.Media.Color clrOutline2,
            int nThickness1,
            int nThickness2)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness1 = nThickness1;
            m_nThickness2 = nThickness2;
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

            SolidColorBrush solidbrush2 = new SolidColorBrush(m_clrOutline2);

            Pen pen2 = new Pen(solidbrush2, m_nThickness1 + m_nThickness2);
            pen2.LineJoin = PenLineJoin.Round;
            if (m_bClrText)
            {
                SolidColorBrush brush = new SolidColorBrush(m_clrText);
                graphics.DrawGeometry(brush, pen2, path);
            }
            else
                graphics.DrawGeometry(m_brushText, pen2, path);
            SolidColorBrush solidbrush1 = new SolidColorBrush(m_clrOutline1);
            Pen pen1 = new Pen(solidbrush1, m_nThickness1);
            pen1.LineJoin = PenLineJoin.Round;
            if (m_bClrText)
            {
                SolidColorBrush brush = new SolidColorBrush(m_clrText);
                graphics.DrawGeometry(brush, pen1, path);
            }
            else
                graphics.DrawGeometry(m_brushText, pen1, path);
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

            Pen pen = new Pen(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), m_nThickness1+m_nThickness2);
            return path.GetRenderBounds(pen);
        }

	    protected System.Windows.Media.Color m_clrText;
	    protected System.Windows.Media.Color m_clrOutline1;
	    protected System.Windows.Media.Color m_clrOutline2;
	    protected int m_nThickness1;
	    protected int m_nThickness2;
        protected System.Windows.Media.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
