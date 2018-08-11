using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    class TextGlowStrategy : TextImplGetHeight
    {
        public TextGlowStrategy()
        {
            m_nThickness=2;
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

	    ~TextGlowStrategy()
        {
            Dispose(false);
        }
        public override ITextStrategy Clone()
        {
            TextGlowStrategy p = new TextGlowStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline, m_nThickness);
            else
                p.Init(m_brushText, m_clrOutline, m_nThickness);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrText, 
		    System.Drawing.Color clrOutline, 
		    int nThickness )
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness; 
        }

        public void Init(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline = clrOutline;
            m_nThickness = nThickness;
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

                for (int i = 1; i <= m_nThickness; ++i)
                {
                    using (Pen pen = new Pen(m_clrOutline, i))
                    {
                        pen.LineJoin = LineJoin.Round;
                        graphics.DrawPath(pen, path);
                    }
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

                for (int i = 1; i <= m_nThickness; ++i)
                {
                    using (Pen pen = new Pen(m_clrOutline, i))
                    {
                        pen.LineJoin = LineJoin.Round;
                        graphics.DrawPath(pen, path);
                    }
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

                for (int i = 1; i <= m_nThickness; ++i)
                {
                    using (Pen pen = new Pen(m_clrOutline, i))
                    {
                        pen.LineJoin = LineJoin.Round;
                        pGraphics.DrawPath(pen, pPath);
                    }
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

                for (int i = 1; i <= m_nThickness; ++i)
                {
                    using (Pen pen = new Pen(m_clrOutline, i))
                    {
                        pen.LineJoin = LineJoin.Round;
                        pGraphics.DrawPath(pen, pPath);
                    }
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
	    protected System.Drawing.Color m_clrOutline;
        protected System.Drawing.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
