using System;
using System.Windows;
using System.Windows.Media;

namespace TextDesignerWpf
{
    public class ExtrudeStrategy : ITextStrategy
    {
	    public ExtrudeStrategy()
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
        ~ExtrudeStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            ExtrudeStrategy p = new ExtrudeStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline, m_nThickness, m_nOffsetX, m_nOffsetY);
            else
                p.Init(m_brushText, m_clrOutline, m_nThickness, m_nOffsetX, m_nOffsetY);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Windows.Media.Color clrText, 
		    System.Windows.Media.Color clrOutline, 
		    int nThickness,
            int nOffsetX,
        	int nOffsetY )
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness;
            m_nOffsetX = nOffsetX;
            m_nOffsetY = nOffsetY;
        }

        public void Init(
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline,
            int nThickness,
            int nOffsetX,
            int nOffsetY)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness;
            m_nOffsetX = nOffsetX;
            m_nOffsetY = nOffsetY;
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
            int nOffset = Math.Abs(m_nOffsetX);
            if (Math.Abs(m_nOffsetX) == Math.Abs(m_nOffsetY))
            {
                nOffset = Math.Abs(m_nOffsetX);
            }
            else if (Math.Abs(m_nOffsetX) > Math.Abs(m_nOffsetY))
            {
                nOffset = Math.Abs(m_nOffsetY);
            }
            else if (Math.Abs(m_nOffsetX) < Math.Abs(m_nOffsetY))
            {
                nOffset = Math.Abs(m_nOffsetX);
            }

            for (int i = 0; i < nOffset; ++i)
            {
                Geometry path = GDIPath.CreateTextGeometry(strText, fontFamily, fontStyle, fontWeight, fontSize,
                    new Point(ptDraw.X + ((i * (-m_nOffsetX)) / nOffset), ptDraw.Y + ((i * (-m_nOffsetY)) / nOffset)), ci);

                SolidColorBrush solidbrush = new SolidColorBrush(m_clrOutline);

                Pen pen = new Pen(solidbrush, m_nThickness);
                pen.LineJoin = PenLineJoin.Round;

                if (m_bClrText)
                {
                    SolidColorBrush brush = new SolidColorBrush(m_clrText);
                    graphics.DrawGeometry(brush, pen, path);

                }
                else
                {
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
        protected int m_nOffsetX;
	    protected int m_nOffsetY;
        protected System.Windows.Media.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
