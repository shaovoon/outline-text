using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextDesignerCSLibrary
{
    public class TextGradOutlineStrategy : TextImplGetHeight
	{
		public TextGradOutlineStrategy()
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
		~TextGradOutlineStrategy()
		{
			Dispose(false);
		}
        public override ITextStrategy Clone()
		{
			TextGradOutlineStrategy p = new TextGradOutlineStrategy();
			if (m_bClrText)
				p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness);
			else
				p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness);

			return (ITextStrategy)(p);
		}

		public void Init(
			System.Drawing.Color clrText, 
			System.Drawing.Color clrOutline1, 
			System.Drawing.Color clrOutline2, 
			int nThickness )
		{
			m_clrText = clrText;
			m_bClrText = true;
			m_clrOutline1 = clrOutline1;
			m_clrOutline2 = clrOutline2;
			m_nThickness = nThickness; 
		}

		public void Init(
			System.Drawing.Brush brushText,
			System.Drawing.Color clrOutline1,
			System.Drawing.Color clrOutline2,
			int nThickness)
		{
			m_brushText = brushText;
			m_bClrText = false;
			m_clrOutline1 = clrOutline1;
			m_clrOutline2 = clrOutline2;
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

				List<Color> list = new List<Color>();
				CalculateGradient(
					m_clrOutline1,
					m_clrOutline2,
					m_nThickness,
					list);

				for (int i = m_nThickness; i >= 1; --i)
				{
					using (Pen pen1 = new Pen(list[i - 1], i))
					{
						pen1.LineJoin = LineJoin.Round;
						graphics.DrawPath(pen1, path);
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

				List<Color> list = new List<Color>();
				CalculateGradient(
						m_clrOutline1,
						m_clrOutline2,
						m_nThickness,
						list);

				for (int i = m_nThickness; i >= 1; --i)
				{
					using (Pen pen1 = new Pen(list[i - 1], i))
					{
						pen1.LineJoin = LineJoin.Round;
						graphics.DrawPath(pen1, path);
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

                List<Color> list = new List<Color>();
                CalculateGradient(
                    m_clrOutline1,
                    m_clrOutline2,
                    m_nThickness,
                    list);
                for (int i = m_nThickness; i >= 1; --i)
                {
                    System.Drawing.Color clr = list[i - 1];
                    using (Pen pen = new Pen(clr, i))
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

                List<Color> list = new List<Color>();
                CalculateGradient(
                    m_clrOutline1,
                    m_clrOutline2,
                    m_nThickness,
                    list);
                for (int i = m_nThickness; i >= 1; --i)
                {
                    System.Drawing.Color clr = list[i - 1];
                    using (Pen pen = new Pen(clr, i))
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

		void CalculateGradient(
			Color clr1,
			Color clr2,
			int nThickness,
			List<Color> list)
		{
			list.Clear();
			int nWidth = nThickness;
			int nHeight = 1;
			Rectangle rect = new Rectangle(0, 0, nWidth, nHeight);
			LinearGradientBrush brush = new LinearGradientBrush(rect,
				clr1, clr2, LinearGradientMode.Horizontal);

			using (Bitmap pImage = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb))
			{
				using (Graphics graphics = System.Drawing.Graphics.FromImage(pImage))
				{
					graphics.FillRectangle(brush, 0, 0, pImage.Width, pImage.Height);

					BitmapData bitmapData = new BitmapData();


					pImage.LockBits(
						rect,
						ImageLockMode.ReadOnly,
						PixelFormat.Format32bppArgb,
						bitmapData);

					unsafe
					{
						uint* pixels = (uint*)bitmapData.Scan0;

						if (pixels == null)
						{
							pImage.UnlockBits(bitmapData);
							return;
						}

						uint col = 0;
						int stride = bitmapData.Stride >> 2;
						for (uint row = 0; row < bitmapData.Height; ++row)
						{
							for (col = 0; col < bitmapData.Width; ++col)
							{
								uint index = (uint)(row * stride + col);
								uint color = pixels[index];
								Color gdiColor = Color.FromArgb((int)((color & 0xff0000) >> 16), (int)((color & 0xff00) >> 8), (int)(color & 0xff));
								list.Add(gdiColor);
							}
						}
					}
					pImage.UnlockBits(bitmapData);
				}
			}
		}

		protected System.Drawing.Color m_clrText;
		protected System.Drawing.Color m_clrOutline1;
		protected System.Drawing.Color m_clrOutline2;
		protected System.Drawing.Brush m_brushText;
		protected bool m_bClrText;
		protected bool disposed;
	}
}
