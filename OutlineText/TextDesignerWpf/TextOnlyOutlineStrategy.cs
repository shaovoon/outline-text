using System;
using System.Windows;
using System.Windows.Media;

namespace TextDesignerWpf
{
    public class TextOnlyOutlineStrategy : ITextStrategy
    {
        public TextOnlyOutlineStrategy()
        {
            m_nThickness=2;
            m_bRoundedEdge = false;
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

        ~TextOnlyOutlineStrategy()
        {
            Dispose(false);
        }
        public ITextStrategy Clone()
        {
            TextOnlyOutlineStrategy p = new TextOnlyOutlineStrategy();
            p.Init(m_clrOutline, m_nThickness, m_bRoundedEdge);

            return (ITextStrategy)(p);
        }

        public void Init(
            System.Windows.Media.Color clrOutline, 
            int nThickness,
            bool bRoundedEdge)
        {
            m_clrOutline = clrOutline;
            m_nThickness = nThickness; 
            m_bRoundedEdge = bRoundedEdge;
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
            SolidColorBrush transbrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            graphics.DrawGeometry(transbrush, pen, path);
         
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

        protected System.Windows.Media.Color m_clrOutline;
        protected int m_nThickness;
        protected bool m_bClrText;
        protected bool m_bRoundedEdge;
        protected bool disposed;
    }
}
