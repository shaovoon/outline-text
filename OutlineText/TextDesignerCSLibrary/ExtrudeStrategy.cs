using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class ExtrudeStrategy : TextImplGetHeight
    {
	    public ExtrudeStrategy()
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
        ~ExtrudeStrategy()
        {
            Dispose(false);
        }
        public override ITextStrategy Clone()
        {
            ExtrudeStrategy p = new ExtrudeStrategy();
            if (m_bClrText)
                p.Init(m_clrText, m_clrOutline, m_nThickness, m_nOffsetX, m_nOffsetY);
            else
                p.Init(m_brushText, m_clrOutline, m_nThickness, m_nOffsetX, m_nOffsetY);

            return (ITextStrategy)(p);
        }

	    public void Init(
		    System.Drawing.Color clrText, 
		    System.Drawing.Color clrOutline, 
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
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline,
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

        public override bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat)
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

	        for(int i=0; i<nOffset; ++i)
	        {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddString(strText, fontFamily, (int)fontStyle, fontSize,
                        new Point(ptDraw.X + ((i * (-m_nOffsetX)) / nOffset), ptDraw.Y + ((i * (-m_nOffsetY)) / nOffset)),
                        strFormat);

                    using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                    {
                        pen.LineJoin = LineJoin.Round;
                        graphics.DrawPath(pen, path);
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
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddString(strText, fontFamily, (int)fontStyle, fontSize,
                        new Rectangle(rtDraw.X + ((i * (-m_nOffsetX)) / nOffset), rtDraw.Y + ((i * (-m_nOffsetY)) / nOffset),
                            rtDraw.Width, rtDraw.Height),
                        strFormat);

                    using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                    {
                        pen.LineJoin = LineJoin.Round;
                        graphics.DrawPath(pen, path);
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
            }

            return true;
        }

        public override bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw)
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
                using (GraphicsPath pPath = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding))
                {
                    bool b = GDIPath.GetStringPath(
                        pGraphics,
                        pPath,
                        pszText,
                        pLogFont,
                        new Point(ptDraw.X + ((i * (-m_nOffsetX)) / nOffset), ptDraw.Y + ((i * (-m_nOffsetY)) / nOffset)));

                    if (false == b)
                    {
                        return false;
                    }

                    using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                    {
                        pen.LineJoin = LineJoin.Round;
                        pGraphics.DrawPath(pen, pPath);
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

            }
            return true;
        }

        public override bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw)
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
                using (GraphicsPath pPath = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding))
                {
                    bool b = GDIPath.GetStringPath(
                        pGraphics,
                        pPath,
                        pszText,
                        pLogFont,
                        new Rectangle(rtDraw.X + ((i * (-m_nOffsetX)) / nOffset), rtDraw.Y + ((i * (-m_nOffsetY)) / nOffset),
                            rtDraw.Width, rtDraw.Height));

                    if (false == b)
                    {
                        return false;
                    }

                    using (Pen pen = new Pen(m_clrOutline, m_nThickness))
                    {
                        pen.LineJoin = LineJoin.Round;
                        pGraphics.DrawPath(pen, pPath);
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
            }
            return true;
        }

	    protected System.Drawing.Color m_clrText;
	    protected System.Drawing.Color m_clrOutline;
        protected int m_nOffsetX;
	    protected int m_nOffsetY;
        protected System.Drawing.Brush m_brushText;
        protected bool m_bClrText;
        protected bool disposed;
    }
}
