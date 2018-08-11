using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextDesignerCSLibrary
{
    public enum GradientType
    {
        Linear,
        Sinusoid
    }
    public class TextGradOutlineLastStrategy : TextImplGetHeight
	{
		public TextGradOutlineLastStrategy()
		{
			m_nThickness=2;
			m_brushText = null;
			m_bClrText = true;
            m_GradientType = GradientType.Linear;
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
		~TextGradOutlineLastStrategy()
		{
			Dispose(false);
		}
        public override ITextStrategy Clone()
		{
			TextGradOutlineLastStrategy p = new TextGradOutlineLastStrategy();
			if (m_bClrText)
				p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness, m_GradientType);
			else
				p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness, m_GradientType);

			return (ITextStrategy)(p);
		}

		public void Init(
			System.Drawing.Color clrText, 
			System.Drawing.Color clrOutline1, 
			System.Drawing.Color clrOutline2, 
			int nThickness,
            GradientType useCurveGradient)
		{
			m_clrText = clrText;
			m_bClrText = true;
			m_clrOutline1 = clrOutline1;
			m_clrOutline2 = clrOutline2;
			m_nThickness = nThickness;
            m_GradientType = useCurveGradient;
        }

		public void Init(
			System.Drawing.Brush brushText,
			System.Drawing.Color clrOutline1,
			System.Drawing.Color clrOutline2,
			int nThickness,
            GradientType useCurveGradient)
		{
			m_brushText = brushText;
			m_bClrText = false;
			m_clrOutline1 = clrOutline1;
			m_clrOutline2 = clrOutline2;
			m_nThickness = nThickness;
            m_GradientType = useCurveGradient;
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
                if(m_GradientType == GradientType.Sinusoid)
				    CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

                if (m_bClrText)
				{
					using (SolidBrush brush = new SolidBrush(m_clrText))
					{
						graphics.FillPath(brush, path);
					}
				}
				else
					graphics.FillPath(m_brushText, path);

				for (int i = m_nThickness; i >= 1; --i)
				{
					using (Pen pen1 = new Pen(list[i - 1], i))
					{
						pen1.LineJoin = LineJoin.Round;
						graphics.DrawPath(pen1, path);
					}
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

				List<Color> list = new List<Color>();
                if (m_GradientType == GradientType.Sinusoid)
                    CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

                if (m_bClrText)
				{
					using (SolidBrush brush = new SolidBrush(m_clrText))
					{
						graphics.FillPath(brush, path);
					}
				}
				else
					graphics.FillPath(m_brushText, path);

				for (int i = m_nThickness; i >= 1; --i)
				{
					using (Pen pen1 = new Pen(list[i - 1], i))
					{
						pen1.LineJoin = LineJoin.Round;
						graphics.DrawPath(pen1, path);
					}
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

                List<Color> list = new List<Color>();
                if (m_GradientType == GradientType.Sinusoid)
                    CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

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

                for (int i = m_nThickness; i >= 1; --i)
                {
                    System.Drawing.Color clr = list[i - 1];
                    using (Pen pen = new Pen(clr, i))
                    {
                        pen.LineJoin = LineJoin.Round;
                        pGraphics.DrawPath(pen, pPath);
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
                if (m_GradientType==GradientType.Sinusoid)
                    CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

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

                for (int i = m_nThickness; i >= 1; --i)
                {
                    System.Drawing.Color clr = list[i - 1];
                    using (Pen pen = new Pen(clr, i))
                    {
                        pen.LineJoin = LineJoin.Round;
                        pGraphics.DrawPath(pen, pPath);
                    }
                }
            }
			return true;
		}

        public static void CalculateGradient(
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

        public static void CalculateCurvedGradient(
            Color clr1,
            Color clr2,
            int nThickness,
            List<Color> list)
        {
            list.Clear();
            if (nThickness == 0)
                return;
            for (int i = 0; i < nThickness; ++i)
            {
                double degree = i / (double)(nThickness) * 90.0;
                double percent = 1.0 - Math.Sin(GetRadians(degree));
                double inv_percent = 1.0 - percent;
                int r = (int)((clr1.R * percent) + (clr2.R * inv_percent));
                byte rb = Clamp(r);
                int g = (int)((clr1.G * percent) + (clr2.G * inv_percent));
                byte gb = Clamp(g);
                int b = (int)((clr1.B * percent) + (clr2.B * inv_percent));
                byte bb = Clamp(b);
                list.Add(Color.FromArgb(rb,gb,bb));
            }
        }

        public static byte Clamp(int comp)
        {
            byte val = 0;
            if (comp < 0)
                val = 0;
            else if (comp > 255)
                val = 255;
            else
                val = (byte)comp;

            return val;
        }

        public static double GetRadians(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }
        protected System.Drawing.Color m_clrText;
		protected System.Drawing.Color m_clrOutline1;
		protected System.Drawing.Color m_clrOutline2;
		protected System.Drawing.Brush m_brushText;
		protected bool m_bClrText;
        protected GradientType m_GradientType;
		protected bool disposed;
	}
}
