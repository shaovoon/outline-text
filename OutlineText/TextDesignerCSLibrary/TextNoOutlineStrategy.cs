using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public class TextNoOutlineStrategy : TextImplGetHeight
	{
		public TextNoOutlineStrategy()
		{
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
		~TextNoOutlineStrategy()
		{
			Dispose(false);
		}
        public override ITextStrategy Clone()
		{
			TextNoOutlineStrategy p = new TextNoOutlineStrategy();
			if (m_bClrText)
				p.Init(m_clrText);
			else
				p.Init(m_brushText);

			return (ITextStrategy)(p);
		}

		public void Init(
			System.Drawing.Color clrText)
		{
			m_clrText = clrText;
			m_bClrText = true;
		}

		public void Init(
			System.Drawing.Brush brushText)
		{
			m_brushText = brushText;
			m_bClrText = false;
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
		protected System.Drawing.Brush m_brushText;
		protected bool m_bClrText;
		protected bool disposed;
	}
}
