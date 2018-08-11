using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class TextDblOutlineStrategy : TextImplGetHeight
    {
	    public TextDblOutlineStrategy()
        {
            m_nThickness1=2;
            m_nThickness2=2;
            m_brushText = null;
            m_bClrText = true;
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
                    if (m_brushText != null)
                    {
                        m_brushText.Dispose();
                    }
                }

                disposed = true;
            }
        }
        ~TextDblOutlineStrategy()
        {
            Dispose(false);
        }
        public override ITextStrategy Clone()
        {
            TextDblOutlineStrategy p = new TextDblOutlineStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness1, m_nThickness2);
            else
                p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness1, m_nThickness2);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrText, 
		    System.Drawing.Color clrOutline1, 
		    System.Drawing.Color clrOutline2, 
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
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
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

                using (Pen pen2 = new Pen(m_clrOutline2, m_nThickness1 + m_nThickness2))
                {
                    pen2.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen2, path);
                }
                using (Pen pen1 = new Pen(m_clrOutline1, m_nThickness1))
                {
                    pen1.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen1, path);
                }

                if (m_bClrText)
                {
                    using (SolidBrush brush = new SolidBrush(m_clrText))
                    {
                        graphics.FillPath(brush, path);
                    }
                }
                else
                    graphics.FillPath(m_brushText, path);
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

                using (Pen pen2 = new Pen(m_clrOutline2, m_nThickness1 + m_nThickness2))
                {
                    pen2.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen2, path);
                }
                using (Pen pen1 = new Pen(m_clrOutline1, m_nThickness1))
                {
                    pen1.LineJoin = LineJoin.Round;
                    graphics.DrawPath(pen1, path);
                }

                if (m_bClrText)
                {
                    using (SolidBrush brush = new SolidBrush(m_clrText))
                    {
                        graphics.FillPath(brush, path);
                    }
                }
                else
                    graphics.FillPath(m_brushText, path);
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

                using (Pen pen2 = new Pen(m_clrOutline2, m_nThickness1 + m_nThickness2))
                {
                    pen2.LineJoin = LineJoin.Round;
                    pGraphics.DrawPath(pen2, pPath);
                }
                using (Pen pen1 = new Pen(m_clrOutline1, m_nThickness1))
                {
                    pen1.LineJoin = LineJoin.Round;
                    pGraphics.DrawPath(pen1, pPath);
                }

                if (m_bClrText)
                {
                    using (SolidBrush brush = new SolidBrush(m_clrText))
                    {
                        pGraphics.FillPath(brush, pPath);
                    }
                }
                else
                {
                    pGraphics.FillPath(m_brushText, pPath);
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

                using (Pen pen2 = new Pen(m_clrOutline2, m_nThickness1 + m_nThickness2))
                {
                    pen2.LineJoin = LineJoin.Round;
                    pGraphics.DrawPath(pen2, pPath);
                }
                using (Pen pen1 = new Pen(m_clrOutline1, m_nThickness1))
                {
                    pen1.LineJoin = LineJoin.Round;
                    pGraphics.DrawPath(pen1, pPath);
                }

                if (m_bClrText)
                {
                    using (SolidBrush brush = new SolidBrush(m_clrText))
                    {
                        pGraphics.FillPath(brush, pPath);
                    }
                }
                else
                {
                    pGraphics.FillPath(m_brushText, pPath);
                }
            }
            return true;
        }

	    protected System.Drawing.Color m_clrText;
	    protected System.Drawing.Color m_clrOutline1;
	    protected System.Drawing.Color m_clrOutline2;
	    protected int m_nThickness1;
	    protected int m_nThickness2;
        protected System.Drawing.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
