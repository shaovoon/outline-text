using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class TextOnlyOutlineStrategy : TextImplGetHeight
    {
	    public TextOnlyOutlineStrategy()
        {
            m_nThickness=2;
            m_bRoundedEdge = false;
            disposed = false;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
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
        public override ITextStrategy Clone()
        {
            TextOnlyOutlineStrategy p = new TextOnlyOutlineStrategy();
            p.Init(m_clrOutline, m_nThickness, m_bRoundedEdge);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrOutline, 
		    int nThickness,
            bool bRoundedEdge)
        {
            m_clrOutline = clrOutline;
            m_nThickness = nThickness; 
            m_bRoundedEdge = bRoundedEdge;
        }

        public override bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, ptDraw, strFormat);

                using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen, path);
                }
            }
	        return true;
        }


        public override bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, rtDraw, strFormat);

                using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen, path);
                }
            }
	        return true;
        }

        public override bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw)
        {
            using (GraphicsPath pPath = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding))
            {
                bool b = GDIPath.GetStringPath(
                    pGraphics,
                    pPath,
                    pszText,
                    pLogFont,
                    ptDraw);

                if (false == b)
                {
                    return false;
                }

                using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                {
                    if (m_bRoundedEdge)
                        pen.LineJoin = LineJoin.Round;
                    pGraphics.DrawPath(pen, pPath);
                }
            }
            return true;
        }

        public override bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw)
        {
            using (GraphicsPath pPath = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding))
            {
                bool b = GDIPath.GetStringPath(
                    pGraphics,
                    pPath,
                    pszText,
                    pLogFont,
                    rtDraw);

                if (false == b)
                {
                    return false;
                }

                using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                {
                    if (m_bRoundedEdge)
                        pen.LineJoin = LineJoin.Round;
                    pGraphics.DrawPath(pen, pPath);
                }
            }
            return true;
        }

	    protected System.Drawing.Color m_clrOutline;
        protected bool m_bClrText;
        protected bool m_bRoundedEdge;
        protected bool disposed;
    }
}
