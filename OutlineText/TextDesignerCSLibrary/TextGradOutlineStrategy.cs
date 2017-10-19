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
            m_bUseCurvedGradient = false;
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
                p.Init(m_clrText, m_clrOutline1, m_clrOutline2, m_nThickness, m_bUseCurvedGradient);
            else
                p.Init(m_brushText, m_clrOutline1, m_clrOutline2, m_nThickness, m_bUseCurvedGradient);

            return (ITextStrategy)(p);
		}

        public void Init(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            bool useCurveGradient)
        {
            m_clrText = clrText;
            m_bClrText = true;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness = nThickness;
            m_bUseCurvedGradient = useCurveGradient;
        }

        public void Init(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            bool useCurveGradient)
        {
            m_brushText = brushText;
            m_bClrText = false;
            m_clrOutline1 = clrOutline1;
            m_clrOutline2 = clrOutline2;
            m_nThickness = nThickness;
            m_bUseCurvedGradient = useCurveGradient;
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
                if (m_bUseCurvedGradient)
                    TextGradOutlineLastStrategy.CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    TextGradOutlineLastStrategy.CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

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
                if (m_bUseCurvedGradient)
                    TextGradOutlineLastStrategy.CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    TextGradOutlineLastStrategy.CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

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
                if (m_bUseCurvedGradient)
                    TextGradOutlineLastStrategy.CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    TextGradOutlineLastStrategy.CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

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
                if (m_bUseCurvedGradient)
                    TextGradOutlineLastStrategy.CalculateCurvedGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);
                else
                    TextGradOutlineLastStrategy.CalculateGradient(m_clrOutline1, m_clrOutline2, m_nThickness, list);

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

		protected System.Drawing.Color m_clrText;
		protected System.Drawing.Color m_clrOutline1;
		protected System.Drawing.Color m_clrOutline2;
		protected System.Drawing.Brush m_brushText;
		protected bool m_bClrText;
        protected bool m_bUseCurvedGradient;
        protected bool disposed;
	}
}
