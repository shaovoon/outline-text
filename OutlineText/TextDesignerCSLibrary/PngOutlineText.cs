using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TextDesignerCSLibrary
{
    public class PngOutlineText : IOutlineText
    {
        public PngOutlineText()
        {
            m_pTextStrategy = null;
            m_pShadowStrategy = null;
            m_pShadowStrategyMask = null;
            m_pFontBodyShadow = null;
            m_pFontBodyShadowMask = null;
            m_pBkgdBitmap = null;
            m_pPngBitmap = null;
            m_clrShadow = System.Drawing.Color.FromArgb(0, 0, 0);
            m_bEnableShadow = false;
            m_bDiffuseShadow = false;
            m_nShadowThickness = 2;

            m_pReflectionPngBitmap = null;
            m_bEnableReflection = false;
            m_fBegAlpha=0.6f;
            m_fEndAlpha=0.01f;
            m_fShown=0.8f;
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
                    if (m_pTextStrategy != null)
                        m_pTextStrategy.Dispose();
                    if (m_pShadowStrategy != null)
                        m_pShadowStrategy.Dispose();
                    if (m_pShadowStrategyMask != null)
                        m_pShadowStrategyMask.Dispose();
                    if (m_pFontBodyShadow != null)
                        m_pFontBodyShadow.Dispose();
                    if (m_pFontBodyShadowMask != null)
                        m_pFontBodyShadowMask.Dispose();
                    if (m_pBkgdBitmap != null)
                        m_pBkgdBitmap.Dispose();
                    if (m_pBkgdBitmap != null)
                        m_pBkgdBitmap.Dispose();
                }

                disposed = true;
            }
        }

        ~PngOutlineText()
        {
            Dispose(false);
        }
        public PngOutlineText(PngOutlineText rhs)
        {
            if (rhs.m_pTextStrategy != null) m_pTextStrategy = rhs.m_pTextStrategy.Clone(); else m_pTextStrategy = null;
            if (rhs.m_pShadowStrategy != null) m_pShadowStrategy = rhs.m_pShadowStrategy.Clone(); else m_pShadowStrategy = null;
            if (rhs.m_pShadowStrategyMask != null) m_pShadowStrategyMask = rhs.m_pShadowStrategyMask.Clone(); else m_pShadowStrategyMask = null;
            if (rhs.m_pFontBodyShadow != null) m_pFontBodyShadow = rhs.m_pFontBodyShadow.Clone(); else m_pFontBodyShadow = null;
            if (rhs.m_pFontBodyShadowMask != null) m_pFontBodyShadowMask = rhs.m_pFontBodyShadowMask.Clone(); else m_pFontBodyShadowMask = null;
            m_pBkgdBitmap = rhs.m_pBkgdBitmap;
            m_pPngBitmap = (System.Drawing.Bitmap)(rhs.m_pPngBitmap.Clone());
            m_clrShadow = rhs.m_clrShadow;
            m_bEnableShadow = rhs.m_bEnableShadow;
            m_bDiffuseShadow = rhs.m_bDiffuseShadow;
            m_nShadowThickness = rhs.m_nShadowThickness;

            m_pReflectionPngBitmap = (System.Drawing.Bitmap)(rhs.m_pReflectionPngBitmap.Clone()); ;
            m_bEnableReflection = rhs.m_bEnableReflection;
            m_fBegAlpha = rhs.m_fBegAlpha;
            m_fEndAlpha = rhs.m_fEndAlpha;
            m_fShown = rhs.m_fShown;
            disposed = false;
        }

        public void TextGlow(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
	        TextGlowStrategy pStrat = new TextGlowStrategy();
	        pStrat.Init(clrText,clrOutline,nThickness);

	        m_pTextStrategy = pStrat;
        }

        public void TextGlow(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            TextGlowStrategy pStrat = new TextGlowStrategy();
            pStrat.Init(brushText, clrOutline, nThickness);

            m_pTextStrategy = pStrat;
        }

        public void TextOutline(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline,
            int nThickness)
            {
	            TextOutlineStrategy pStrat = new TextOutlineStrategy();
	            pStrat.Init(clrText,clrOutline,nThickness);

	            m_pTextStrategy = pStrat;
            }

        public void TextOutline(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            TextOutlineStrategy pStrat = new TextOutlineStrategy();
            pStrat.Init(brushText, clrOutline, nThickness);

            m_pTextStrategy = pStrat;
        }

        public void TextDblOutline(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness1,
            int nThickness2)
        {
	        TextDblOutlineStrategy pStrat = new TextDblOutlineStrategy();
	        pStrat.Init(clrText,clrOutline1,clrOutline2,nThickness1,nThickness2);

	        m_pTextStrategy = pStrat;
        }

        public void TextDblOutline(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness1,
            int nThickness2)
        {
            TextDblOutlineStrategy pStrat = new TextDblOutlineStrategy();
            pStrat.Init(brushText, clrOutline1, clrOutline2, nThickness1, nThickness2);

            m_pTextStrategy = pStrat;
        }

        public void TextDblGlow(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness1,
            int nThickness2)
        {
            TextDblGlowStrategy pStrat = new TextDblGlowStrategy();
            pStrat.Init(clrText, clrOutline1, clrOutline2, nThickness1, nThickness2);

            m_pTextStrategy = pStrat;
        }

        public void TextDblGlow(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness1,
            int nThickness2)
        {
            TextDblGlowStrategy pStrat = new TextDblGlowStrategy();
            pStrat.Init(brushText, clrOutline1, clrOutline2, nThickness1, nThickness2);

            m_pTextStrategy = pStrat;
        }

        public void TextGradOutline(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            GradientType gradType)
        {
            TextGradOutlineStrategy pStrat = new TextGradOutlineStrategy();
            pStrat.Init(clrText, clrOutline1, clrOutline2, nThickness, gradType);

            m_pTextStrategy = pStrat;
        }

        public void TextGradOutline(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            GradientType gradType)
        {
            TextGradOutlineStrategy pStrat = new TextGradOutlineStrategy();
            pStrat.Init(brushText, clrOutline1, clrOutline2, nThickness, gradType);

            m_pTextStrategy = pStrat;
        }

        public void TextGradOutlineLast(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            GradientType gradType)
        {
            TextGradOutlineLastStrategy pStrat = new TextGradOutlineLastStrategy();
            pStrat.Init(clrText, clrOutline1, clrOutline2, nThickness, gradType);

            m_pTextStrategy = pStrat;
        }

        public void TextGradOutlineLast(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            GradientType gradType)
        {
            TextGradOutlineLastStrategy pStrat = new TextGradOutlineLastStrategy();
            pStrat.Init(brushText, clrOutline1, clrOutline2, nThickness, gradType);

            m_pTextStrategy = pStrat;
        }

        public void TextNoOutline(System.Drawing.Color clrText)
        {
            TextNoOutlineStrategy pStrat = new TextNoOutlineStrategy();
            pStrat.Init(clrText);

            m_pTextStrategy = pStrat;
        }

        public void TextNoOutline(System.Drawing.Brush brushText)
        {
            TextNoOutlineStrategy pStrat = new TextNoOutlineStrategy();
            pStrat.Init(brushText);

            m_pTextStrategy = pStrat;
        }

        public void TextOnlyOutline(
            System.Drawing.Color clrOutline,
            int nThickness,
            bool bRoundedEdge)
        {
            TextOnlyOutlineStrategy pStrat = new TextOnlyOutlineStrategy();
            pStrat.Init(clrOutline, nThickness, bRoundedEdge);

            m_pTextStrategy = pStrat;
        }

        public void SetShadowBkgd(System.Drawing.Bitmap pBitmap)
        {
	        m_pBkgdBitmap = pBitmap;
        }

        public void SetShadowBkgd(System.Drawing.Color clrBkgd, int nWidth, int nHeight)
        {
	        m_pBkgdBitmap = new System.Drawing.Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(m_pBkgdBitmap))
            {
                using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(clrBkgd))
                {
                    graphics.FillRectangle(brush, 0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);
                }
            }
        }

        public void SetNullTextEffect()
        {
            m_pTextStrategy = null;
        }
        
        public void SetNullShadow()
        {
	        m_pFontBodyShadow = null;
	        m_pShadowStrategy = null;
            m_pShadowStrategyMask = null;
        }

        public void EnableShadow(bool bEnable)
        {
	        m_bEnableShadow = bEnable;
        }

        public bool IsShadowEnabled() 
        { 
            return m_bEnableShadow; 
        }

        public void Shadow(
            System.Drawing.Color color,
            int nThickness,
            System.Drawing.Point ptOffset)
        {
	        TextOutlineStrategy pStrat = new TextOutlineStrategy();
	        pStrat.Init(System.Drawing.Color.FromArgb(0,0,0,0),color,nThickness);

	        m_ptShadowOffset = ptOffset;
	        m_pShadowStrategy = pStrat;

	        TextOutlineStrategy pStrat2 = new TextOutlineStrategy();
	        pStrat2.Init(
		        System.Drawing.Color.FromArgb(0,0,0,0),
		        System.Drawing.Color.FromArgb(color.A,255,255,255),
		        nThickness);

	        m_pShadowStrategyMask = pStrat2;

	        m_clrShadow = color;

	        TextOutlineStrategy pFontBodyShadow = new TextOutlineStrategy();
	        pFontBodyShadow.Init(System.Drawing.Color.FromArgb(255,255,255),System.Drawing.Color.FromArgb(0,0,0,0),0);
	        m_pFontBodyShadow = pFontBodyShadow;

	        TextOutlineStrategy pFontBodyShadowMask = new TextOutlineStrategy();
            pFontBodyShadowMask.Init(System.Drawing.Color.FromArgb(color.A, 255, 255, 255), System.Drawing.Color.FromArgb(0, 0, 0, 0), 0);
	        m_pFontBodyShadowMask = pFontBodyShadowMask;
	        m_bDiffuseShadow = false;
        }

        public void DiffusedShadow(
	        System.Drawing.Color color, 
	        int nThickness,
	        System.Drawing.Point ptOffset)
        {
	        DiffusedShadowStrategy pStrat = new DiffusedShadowStrategy();
            pStrat.Init(System.Drawing.Color.FromArgb(0, 0, 0, 0), color, nThickness, false);

	        m_ptShadowOffset = ptOffset;
	        m_pShadowStrategy = pStrat;

	        DiffusedShadowStrategy pStrat2 = new DiffusedShadowStrategy();
	        pStrat2.Init(
		        System.Drawing.Color.FromArgb(0,0,0,0),
		        System.Drawing.Color.FromArgb(color.A,255,255,255),
		        nThickness,
		        true);

	        m_pShadowStrategyMask = pStrat2;

	        m_clrShadow = color;

	        DiffusedShadowStrategy pFontBodyShadow = new DiffusedShadowStrategy();
	        pFontBodyShadow.Init(System.Drawing.Color.FromArgb(255,255,255),System.Drawing.Color.FromArgb(0,0,0,0),nThickness,false);

	        m_pFontBodyShadow = pFontBodyShadow;

	        DiffusedShadowStrategy pFontBodyShadowMask = new DiffusedShadowStrategy();
	        pFontBodyShadowMask.Init(System.Drawing.Color.FromArgb(color.A,255,255,255),System.Drawing.Color.FromArgb(0,0,0,0),
		        nThickness,false);

	        m_pFontBodyShadowMask = pFontBodyShadowMask;
	        m_bDiffuseShadow = true;
	        m_bExtrudeShadow = false;
	        m_nShadowThickness = nThickness;
        }

        public void Extrude(
	        System.Drawing.Color color, 
	        int nThickness,
	        System.Drawing.Point ptOffset)
        {
	        ExtrudeStrategy pStrat = new ExtrudeStrategy();
	        pStrat.Init(System.Drawing.Color.FromArgb(0,0,0,0), color, nThickness, ptOffset.X, ptOffset.Y);

	        m_ptShadowOffset = ptOffset;
	        m_pShadowStrategy = pStrat;

	        ExtrudeStrategy pStrat2 = new ExtrudeStrategy();
	        pStrat2.Init(
		        System.Drawing.Color.FromArgb(0,0,0,0),
		        System.Drawing.Color.FromArgb(color.A,255,255,255),
		        nThickness,
		        ptOffset.X, ptOffset.Y);

	        m_pShadowStrategyMask = pStrat2;

	        m_clrShadow = color;

	        ExtrudeStrategy pFontBodyShadow = new ExtrudeStrategy();
	        pFontBodyShadow.Init(System.Drawing.Color.FromArgb(255,255,255),System.Drawing.Color.FromArgb(0,0,0,0), nThickness, ptOffset.X, ptOffset.Y);

	        m_pFontBodyShadow = pFontBodyShadow;

	        ExtrudeStrategy pFontBodyShadowMask = new ExtrudeStrategy();
	        pFontBodyShadowMask.Init(System.Drawing.Color.FromArgb(color.A,255,255,255), System.Drawing.Color.FromArgb(0,0,0,0), 
		        nThickness, ptOffset.X, ptOffset.Y);

	        m_pFontBodyShadowMask = pFontBodyShadowMask;
	        m_bExtrudeShadow = true;
	        m_bDiffuseShadow = false;
	        m_nShadowThickness = nThickness;
        }

        public void SetPngImage(System.Drawing.Bitmap pBitmap)
        {
	        m_pPngBitmap = pBitmap;
        }

        public System.Drawing.Bitmap GetPngImage()
        {
	        return m_pPngBitmap;
        }
        
        public System.Drawing.Bitmap GetReflectionPngImage()
        {
	        return m_pReflectionPngBitmap;
        }

        public bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int nfontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat)
        {
	        if(graphics==null) return false;

	        System.Drawing.Graphics pGraphicsDrawn=null;
	        System.Drawing.Bitmap pBmpDrawn=null;

	        if(m_bEnableShadow&&m_pBkgdBitmap!=null&&m_pFontBodyShadow!=null&&m_pShadowStrategy!=null&&m_pShadowStrategyMask!=null)
	        {
		        System.Drawing.Graphics pGraphicsMask=null;
		        System.Drawing.Bitmap pBmpMask=null;

		        bool b = RenderTransShadowA( graphics, ref pGraphicsMask, ref pBmpMask, ref pGraphicsDrawn, ref pBmpDrawn);

		        if(!b) return false;

		        b = RenderFontShadow(
			        pGraphicsDrawn,
			        pGraphicsMask,
			        pBmpDrawn,
			        pBmpMask,
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        new System.Drawing.Point(ptDraw.X+m_ptShadowOffset.X, ptDraw.Y+m_ptShadowOffset.Y),
			        strFormat);

		        if(!b) 
		        {
			        pGraphicsMask=null;
			        pGraphicsDrawn=null;
			        pBmpDrawn=null;
			        return false;
		        }

		        b = RenderTransShadowB( graphics, pGraphicsMask, pBmpMask, pGraphicsDrawn, pBmpDrawn);

		        pGraphicsMask=null;
		        pGraphicsDrawn=null;
		        pBmpDrawn=null;

		        if(!b) return false;
	        }

	        if(m_pTextStrategy!=null)
	        {
		        System.Drawing.Graphics pGraphicsPng = System.Drawing.Graphics.FromImage(m_pPngBitmap);

		        pGraphicsPng.CompositingMode = graphics.CompositingMode;
		        pGraphicsPng.CompositingQuality = graphics.CompositingQuality;
		        pGraphicsPng.InterpolationMode = graphics.InterpolationMode;
		        pGraphicsPng.SmoothingMode = graphics.SmoothingMode;
		        pGraphicsPng.TextRenderingHint = graphics.TextRenderingHint;
		        pGraphicsPng.PageUnit = graphics.PageUnit;
		        pGraphicsPng.PageScale = graphics.PageScale;


		        bool b = m_pTextStrategy.DrawString(
			        pGraphicsPng, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        ptDraw, 
			        strFormat);

		        if(!b)
			        return false;
	        }

            if (m_bEnableReflection)
                ProcessReflection();

	        return true;
        }

        public bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int nfontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat)
        {
	        if(graphics==null) return false;

	        System.Drawing.Graphics pGraphicsDrawn=null;
	        System.Drawing.Bitmap pBmpDrawn=null;

	        if(m_bEnableShadow&&m_pBkgdBitmap!=null&&m_pFontBodyShadow!=null&&m_pShadowStrategy!=null&&m_pShadowStrategyMask!=null)
	        {
		        System.Drawing.Graphics pGraphicsMask=null;
		        System.Drawing.Bitmap pBmpMask=null;

		        bool b = RenderTransShadowA( graphics, ref pGraphicsMask, ref pBmpMask, ref pGraphicsDrawn, ref pBmpDrawn);

		        if(!b) return false;

		        b = RenderFontShadow(
			        pGraphicsDrawn,
			        pGraphicsMask,
			        pBmpDrawn,
			        pBmpMask,
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        new Rectangle(rtDraw.X+m_ptShadowOffset.X, rtDraw.Y+m_ptShadowOffset.Y,rtDraw.Width,rtDraw.Height),
			        strFormat);

		        if(!b) 
		        {
			        pGraphicsMask=null;
			        pGraphicsDrawn=null;
			        pBmpDrawn=null;
			        return false;
		        }

		        b = RenderTransShadowB( graphics, pGraphicsMask, pBmpMask, pGraphicsDrawn, pBmpDrawn);

		        pGraphicsMask=null;
		        pGraphicsDrawn=null;
		        pBmpDrawn=null;

		        if(!b) return false;
	        }

	        if(m_pTextStrategy!=null)
	        {
                using (System.Drawing.Graphics pGraphicsPng = System.Drawing.Graphics.FromImage(m_pPngBitmap))
                {
                    pGraphicsPng.CompositingMode = graphics.CompositingMode;
                    pGraphicsPng.CompositingQuality = graphics.CompositingQuality;
                    pGraphicsPng.InterpolationMode = graphics.InterpolationMode;
                    pGraphicsPng.SmoothingMode = graphics.SmoothingMode;
                    pGraphicsPng.TextRenderingHint = graphics.TextRenderingHint;
                    pGraphicsPng.PageUnit = graphics.PageUnit;
                    pGraphicsPng.PageScale = graphics.PageScale;


                    bool b = m_pTextStrategy.DrawString(
                        pGraphicsPng,
                        fontFamily,
                        fontStyle,
                        nfontSize,
                        strText,
                        rtDraw,
                        strFormat);

                    if (!b)
                        return false;
                }
	        }

            if (m_bEnableReflection)
                ProcessReflection();

	        return true;
        }

        public bool GdiDrawString(
           System.Drawing.Graphics graphics,
           LOGFONT pLogFont,
           string pszText,
           System.Drawing.Point ptDraw)
        {
            if (graphics == null) return false;

            if (m_bEnableShadow && m_pBkgdBitmap != null && m_pFontBodyShadow != null && m_pShadowStrategy != null && m_pShadowStrategyMask != null)
            {
                System.Drawing.Graphics pGraphicsMask = null;
                System.Drawing.Bitmap pBmpMask = null;
                System.Drawing.Graphics pGraphicsDrawn = null;
                System.Drawing.Bitmap pBmpDrawn = null;

                bool b = RenderTransShadowA(graphics, ref pGraphicsMask, ref pBmpMask, ref pGraphicsDrawn, ref pBmpDrawn);

                if (!b) return false;
                b = GdiRenderFontShadow(
                    pGraphicsDrawn,
                    pGraphicsMask,
                    pBmpDrawn,
                    pBmpMask,
                    pLogFont,
                    pszText,
                    new Point(ptDraw.X + m_ptShadowOffset.X, ptDraw.Y + m_ptShadowOffset.Y));

                if (!b)
                {
                    pGraphicsMask.Dispose();
                    pGraphicsDrawn.Dispose();
                    pBmpDrawn.Dispose();
                    return false;
                }

                b = RenderTransShadowB(graphics, pGraphicsMask, pBmpMask, pGraphicsDrawn, pBmpDrawn);

                pGraphicsMask.Dispose();
                pGraphicsDrawn.Dispose();
                pBmpDrawn.Dispose();

                if (!b) return false;
            }

            if (m_pTextStrategy != null)
            {
                using (System.Drawing.Graphics pGraphicsPng = Graphics.FromImage((Image)(m_pPngBitmap)))
                {

                    pGraphicsPng.CompositingMode = graphics.CompositingMode;
                    pGraphicsPng.CompositingQuality = graphics.CompositingQuality;
                    pGraphicsPng.InterpolationMode = graphics.InterpolationMode;
                    pGraphicsPng.SmoothingMode = graphics.SmoothingMode;
                    pGraphicsPng.TextRenderingHint = graphics.TextRenderingHint;
                    pGraphicsPng.PageUnit = graphics.PageUnit;
                    pGraphicsPng.PageScale = graphics.PageScale;

                    bool b = m_pTextStrategy.GdiDrawString(
                        pGraphicsPng,
                        pLogFont,
                        pszText,
                        ptDraw);

                    if (!b)
                        return false;
                }
            }

            if (m_bEnableReflection)
                ProcessReflection();

            return true;
        }

        public bool GdiDrawString(
            System.Drawing.Graphics graphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw)
        {
            if (graphics == null) return false;

            if (m_bEnableShadow && m_pBkgdBitmap != null && m_pFontBodyShadow != null && m_pShadowStrategy != null && m_pShadowStrategyMask != null)
            {
                System.Drawing.Graphics pGraphicsMask = null;
                System.Drawing.Bitmap pBmpMask = null;
                System.Drawing.Graphics pGraphicsDrawn = null;
                System.Drawing.Bitmap pBmpDrawn = null;

                bool b = RenderTransShadowA(graphics, ref pGraphicsMask, ref pBmpMask, ref pGraphicsDrawn, ref pBmpDrawn);

                if (!b) return false;

                b = GdiRenderFontShadow(
                    pGraphicsDrawn,
                    pGraphicsMask,
                    pBmpDrawn,
                    pBmpMask,
                    pLogFont,
                    pszText,
                    new Rectangle(rtDraw.X + m_ptShadowOffset.X, rtDraw.Y + m_ptShadowOffset.Y, rtDraw.Width, rtDraw.Height));

                if (!b)
                {
                    pGraphicsMask.Dispose();
                    pGraphicsDrawn.Dispose();
                    pBmpDrawn.Dispose();
                    return false;
                }

                b = RenderTransShadowB(graphics, pGraphicsMask, pBmpMask, pGraphicsDrawn, pBmpDrawn);

                pGraphicsMask.Dispose();
                pGraphicsDrawn.Dispose();
                pBmpDrawn.Dispose();

                if (!b) return false;
            }

            if (m_pTextStrategy != null)
            {
                using (System.Drawing.Graphics pGraphicsPng = Graphics.FromImage((Image)(m_pPngBitmap)))
                {
                    pGraphicsPng.CompositingMode = graphics.CompositingMode;
                    pGraphicsPng.CompositingQuality = graphics.CompositingQuality;
                    pGraphicsPng.InterpolationMode = graphics.InterpolationMode;
                    pGraphicsPng.SmoothingMode = graphics.SmoothingMode;
                    pGraphicsPng.TextRenderingHint = graphics.TextRenderingHint;
                    pGraphicsPng.PageUnit = graphics.PageUnit;
                    pGraphicsPng.PageScale = graphics.PageScale;

                    bool b = m_pTextStrategy.GdiDrawString(
                        pGraphicsPng,
                        pLogFont,
                        pszText,
                        rtDraw);

                    if (!b)
                        return false;
                }
            }

            if (m_bEnableReflection)
                ProcessReflection();

            return true;
        }

        public bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int nfontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight)
        {
	        float fDestWidth1 = 0.0f;
	        float fDestHeight1 = 0.0f;
	        bool b = false;
            if (m_pTextStrategy != null)
            {
                b = m_pTextStrategy.MeasureString(
                    graphics,
                    fontFamily,
                    fontStyle,
                    nfontSize,
                    strText,
                    ptDraw,
                    strFormat,
                    ref fStartX,
                    ref fStartY,
                    ref fDestWidth1,
                    ref fDestHeight1);

                if (!b)
                    return false;
            }

	        float fDestWidth2 = 0.0f;
	        float fDestHeight2 = 0.0f;
            float fStartX2 = 0.0f;
            float fStartY2 = 0.0f;
            if (m_bEnableShadow)
	        {
		        b = m_pShadowStrategy.MeasureString(
			        graphics, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        ptDraw, 
			        strFormat,
                    ref fStartX2,
                    ref fStartY2,
			        ref fDestWidth2,
			        ref fDestHeight2 );

		        if(b)
		        {
			        float fDestWidth3 = 0.0f;
			        float fDestHeight3 = 0.0f;
                    float fStartX3 = 0.0f;
                    float fStartY3 = 0.0f;
			        b = GDIPath.ConvertToPixels(graphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				        ref fStartX3, ref fStartY3, ref fDestWidth3, ref fDestHeight3);
			        if(b)
			        {
				        fDestWidth2 += fDestWidth3;
				        fDestHeight2 += fDestHeight3;
			        }
		        }
		        else
			        return false;
	        }

	        if(fDestWidth1>fDestWidth2 || fDestHeight1>fDestHeight2)
	        {
		        fDestWidth = fDestWidth1;
		        fDestHeight = fDestHeight1;
	        }
	        else
	        {
		        fDestWidth = fDestWidth2;
		        fDestHeight = fDestHeight2;
	        }

	        return true;
        }

        public bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int nfontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight)
        {
	        float fDestWidth1 = 0.0f;
	        float fDestHeight1 = 0.0f;
	        bool b = false;
            if (m_pTextStrategy != null)
            {
                b = m_pTextStrategy.MeasureString(
                    graphics,
                    fontFamily,
                    fontStyle,
                    nfontSize,
                    strText,
                    rtDraw,
                    strFormat,
                    ref fStartX,
                    ref fStartY,
                    ref fDestWidth1,
                    ref fDestHeight1);

                if (!b)
                    return false;
            }

	        float fDestWidth2 = 0.0f;
	        float fDestHeight2 = 0.0f;
            float fStartX2 = 0.0f;
            float fStartY2 = 0.0f;
            if (m_bEnableShadow)
	        {
		        b = m_pShadowStrategy.MeasureString(
			        graphics, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        rtDraw, 
			        strFormat,
                    ref fStartX2,
                    ref fStartY2,
			        ref fDestWidth2,
			        ref fDestHeight2 );

		        if(b)
		        {
			        float fDestWidth3 = 0.0f;
			        float fDestHeight3 = 0.0f;
                    float fStartX3 = 0.0f;
                    float fStartY3 = 0.0f;
			        b = GDIPath.ConvertToPixels(graphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
                        ref fStartX3, ref fStartY3, ref fDestWidth3, ref fDestHeight3);
			        if(b)
			        {
				        fDestWidth2 += fDestWidth3;
				        fDestHeight2 += fDestHeight3;
			        }
		        }
		        else
			        return false;
	        }

	        if(fDestWidth1>fDestWidth2 || fDestHeight1>fDestHeight2)
	        {
		        fDestWidth = fDestWidth1;
		        fDestHeight = fDestHeight1;
	        }
	        else
	        {
		        fDestWidth = fDestWidth2;
		        fDestHeight = fDestHeight2;
	        }

	        return true;
        }

        bool RenderTransShadowA(
	        System.Drawing.Graphics pGraphics,
	        ref System.Drawing.Graphics ppGraphicsMask,
	        ref System.Drawing.Bitmap ppBmpMask,
	        ref System.Drawing.Graphics ppGraphicsDrawn,
	        ref System.Drawing.Bitmap ppBmpDrawn)
        {
	        if(pGraphics==null) return false;

            Rectangle rectbmp = new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);
	        ppBmpMask = 
		        m_pPngBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb);

	        ppGraphicsMask = System.Drawing.Graphics.FromImage(ppBmpMask);
	        System.Drawing.SolidBrush brushBlack = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0,0,0));
	        ppGraphicsMask.FillRectangle(brushBlack, 0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height );

	        ppGraphicsMask.CompositingMode = pGraphics.CompositingMode;
	        ppGraphicsMask.CompositingQuality = pGraphics.CompositingQuality;
	        ppGraphicsMask.InterpolationMode = pGraphics.InterpolationMode;
	        ppGraphicsMask.SmoothingMode = pGraphics.SmoothingMode;
	        ppGraphicsMask.TextRenderingHint = pGraphics.TextRenderingHint;
	        ppGraphicsMask.PageUnit = pGraphics.PageUnit;
	        ppGraphicsMask.PageScale = pGraphics.PageScale;

	        ppBmpDrawn = 
		        m_pPngBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb);

	        ppGraphicsDrawn = System.Drawing.Graphics.FromImage(ppBmpDrawn);
	        System.Drawing.SolidBrush brushWhite = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255,255,255));
	        ppGraphicsDrawn.FillRectangle(brushWhite, 0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height );

	        ppGraphicsDrawn.CompositingMode=pGraphics.CompositingMode;
	        ppGraphicsDrawn.CompositingQuality=pGraphics.CompositingQuality;
	        ppGraphicsDrawn.InterpolationMode=pGraphics.InterpolationMode;
	        ppGraphicsDrawn.SmoothingMode=pGraphics.SmoothingMode;
	        ppGraphicsDrawn.TextRenderingHint=pGraphics.TextRenderingHint;
	        ppGraphicsDrawn.PageUnit=pGraphics.PageUnit;
	        ppGraphicsDrawn.PageScale=pGraphics.PageScale;

	        return true;
        }

        bool RenderTransShadowB(
	        System.Drawing.Graphics pGraphics,
	        System.Drawing.Graphics pGraphicsMask,
	        System.Drawing.Bitmap pBmpMask,
	        System.Drawing.Graphics pGraphicsDrawn,
	        System.Drawing.Bitmap pBmpDrawn)
        {
	        if(pGraphics==null||pGraphicsMask==null||pBmpMask==null||pGraphicsDrawn==null||pBmpDrawn==null)
		        return false;

            unsafe
            {
                UInt32* pixelsSrc = null;
                UInt32* pixelsDest = null;
                UInt32* pixelsMask = null;
                UInt32* pixelsDrawn = null;

                BitmapData bitmapDataDest = new BitmapData();
                BitmapData bitmapDataMask = new BitmapData();
                BitmapData bitmapDataDrawn = new BitmapData();
                Rectangle rect = new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);

                m_pPngBitmap.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb,
                    bitmapDataDest);

                pBmpMask.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb,
                    bitmapDataMask);

                pBmpDrawn.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb,
                    bitmapDataDrawn);

                // Write to the temporary buffer provided by LockBits.
                pixelsDest = (UInt32*)bitmapDataDest.Scan0;
                pixelsMask = (UInt32*)bitmapDataMask.Scan0;
                pixelsDrawn = (UInt32*)bitmapDataDrawn.Scan0;

                if (pixelsDest == null || pixelsMask == null || pixelsDrawn == null)
                {
                    return false;
                }

                UInt32 col = 0;
                int stride = bitmapDataDest.Stride >> 2;
                for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                {
                    UInt32 total_row_len = (UInt32)(row * stride);
                    for (col = 0; col < bitmapDataDest.Width; ++col)
                    {
                        UInt32 index = total_row_len + col;
                        Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                        if (nAlpha > 0)
                        {
                            UInt32 nDrawn = (UInt32)
                                (nAlpha << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                            nDrawn &= 0x00ffffff;
                            pixelsDest[index] = (UInt32) (nDrawn | nAlpha << 24);
                        }
                    }
                }

                pBmpDrawn.UnlockBits(bitmapDataDrawn);
                pBmpMask.UnlockBits(bitmapDataMask);
                m_pPngBitmap.UnlockBits(bitmapDataDest);

                pBmpMask = null;
            }
	        return true;
        }

        bool RenderFontShadow(	
	        System.Drawing.Graphics pGraphicsDrawn, 
	        System.Drawing.Graphics pGraphicsMask,
	        System.Drawing.Bitmap pBitmapDrawn,
	        System.Drawing.Bitmap pBitmapMask,
	        System.Drawing.FontFamily pFontFamily,
	        System.Drawing.FontStyle fontStyle,
	        int nfontSize,
	        string strText, 
	        System.Drawing.Point ptDraw, 
	        System.Drawing.StringFormat strFormat)
        {
	        if(pGraphicsDrawn == null || pGraphicsMask == null || pBitmapDrawn == null || pBitmapMask == null) return false;

            Rectangle rectbmp = new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);
	        using(System.Drawing.Bitmap pBitmapShadowMask =
                m_pPngBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
                {
                    using (System.Drawing.Graphics pGraphicsShadowMask = System.Drawing.Graphics.FromImage(pBitmapShadowMask))
                    {
                        using (System.Drawing.SolidBrush brushBlack = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)))
                        {
                            pGraphicsShadowMask.FillRectangle(brushBlack, 0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);

                            pGraphicsShadowMask.CompositingMode = pGraphicsDrawn.CompositingMode;
                            pGraphicsShadowMask.CompositingQuality = pGraphicsDrawn.CompositingQuality;
                            pGraphicsShadowMask.InterpolationMode = pGraphicsDrawn.InterpolationMode;
                            pGraphicsShadowMask.SmoothingMode = pGraphicsDrawn.SmoothingMode;
                            pGraphicsShadowMask.TextRenderingHint = pGraphicsDrawn.TextRenderingHint;
                            pGraphicsShadowMask.PageUnit = pGraphicsDrawn.PageUnit;
                            pGraphicsShadowMask.PageScale = pGraphicsDrawn.PageScale;

                            bool b = false;

                            b = m_pFontBodyShadowMask.DrawString(
                                    pGraphicsMask,
                                    pFontFamily,
                                    fontStyle,
                                    nfontSize,
                                    strText,
                                    ptDraw,
                                    strFormat);

                            if (!b) return false;

                            b = m_pShadowStrategyMask.DrawString(
                                    pGraphicsShadowMask,
                                    pFontFamily,
                                    fontStyle,
                                    nfontSize,
                                    strText,
                                    ptDraw,
                                    strFormat);

                            if (!b) return false;

                            b = m_pFontBodyShadow.DrawString(
                                    pGraphicsDrawn,
                                    pFontFamily,
                                    fontStyle,
                                    nfontSize,
                                    strText,
                                    ptDraw,
                                    strFormat);

                            if (!b) return false;

                            b = m_pShadowStrategy.DrawString(
                                    pGraphicsDrawn,
                                    pFontFamily,
                                    fontStyle,
                                    nfontSize,
                                    strText,
                                    ptDraw,
                                    strFormat);

                            if (!b) return false;

                            unsafe
                            {
                                UInt32* pixelsDest = null;
                                UInt32* pixelsMask = null;
                                UInt32* pixelsShadowMask = null;

                                BitmapData bitmapDataDest = new BitmapData();
                                BitmapData bitmapDataMask = new BitmapData();
                                BitmapData bitmapDataShadowMask = new BitmapData();
                                Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                                pBitmapDrawn.LockBits(
                                    rect,
                                    ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb,
                                    bitmapDataDest);

                                pBitmapMask.LockBits(
                                    rect,
                                    ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb,
                                    bitmapDataMask);

                                pBitmapShadowMask.LockBits(
                                    rect,
                                    ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb,
                                    bitmapDataShadowMask);

                                pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                                pixelsMask = (UInt32*)(bitmapDataMask.Scan0);
                                pixelsShadowMask = (UInt32*)(bitmapDataShadowMask.Scan0);

                                if (pixelsDest == null || pixelsMask == null || pixelsShadowMask == null)
                                    return false;

                                UInt32 col = 0;
                                int stride = bitmapDataDest.Stride >> 2;
                                for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                {
                                    UInt32 total_row_len = (UInt32)(row * stride);
                                    for (col = 0; col < bitmapDataDest.Width; ++col)
                                    {
                                        UInt32 index = total_row_len + col;
                                        Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                        Byte nAlphaShadow = (Byte)(pixelsShadowMask[index] & 0xff);
                                        if (nAlpha > 0 && nAlpha > nAlphaShadow)
                                        {
                                            pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                        }
                                        else if (nAlphaShadow > 0)
                                        {
                                            pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                            pixelsMask[index] = pixelsShadowMask[index];
                                        }
                                    }
                                }

                                pBitmapShadowMask.UnlockBits(bitmapDataShadowMask);
                                pBitmapMask.UnlockBits(bitmapDataMask);
                                pBitmapDrawn.UnlockBits(bitmapDataDest);

                            }
                        }
                    }
            }

	        return true;
        }

        bool RenderFontShadow(
            System.Drawing.Graphics pGraphicsDrawn,
            System.Drawing.Graphics pGraphicsMask,
            System.Drawing.Bitmap pBitmapDrawn,
            System.Drawing.Bitmap pBitmapMask,
            System.Drawing.FontFamily pFontFamily,
            System.Drawing.FontStyle fontStyle,
            int nfontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat)
        {
            if (pGraphicsDrawn == null || pGraphicsMask == null || pBitmapDrawn == null || pBitmapMask == null) return false;

            Rectangle rectbmp = new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);
            using (System.Drawing.Bitmap pBitmapShadowMask =
                m_pPngBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Graphics pGraphicsShadowMask = System.Drawing.Graphics.FromImage(pBitmapShadowMask))
                {


                    using (System.Drawing.SolidBrush brushBlack = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)))
                    {


                        pGraphicsShadowMask.FillRectangle(brushBlack, 0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);

                        pGraphicsShadowMask.CompositingMode = pGraphicsDrawn.CompositingMode;
                        pGraphicsShadowMask.CompositingQuality = pGraphicsDrawn.CompositingQuality;
                        pGraphicsShadowMask.InterpolationMode = pGraphicsDrawn.InterpolationMode;
                        pGraphicsShadowMask.SmoothingMode = pGraphicsDrawn.SmoothingMode;
                        pGraphicsShadowMask.TextRenderingHint = pGraphicsDrawn.TextRenderingHint;
                        pGraphicsShadowMask.PageUnit = pGraphicsDrawn.PageUnit;
                        pGraphicsShadowMask.PageScale = pGraphicsDrawn.PageScale;

                        bool b = false;

                        b = m_pFontBodyShadowMask.DrawString(
                                pGraphicsMask,
                                pFontFamily,
                                fontStyle,
                                nfontSize,
                                strText,
                                rtDraw,
                                strFormat);

                        if (!b) return false;

                        b = m_pShadowStrategyMask.DrawString(
                                pGraphicsShadowMask,
                                pFontFamily,
                                fontStyle,
                                nfontSize,
                                strText,
                                rtDraw,
                                strFormat);

                        if (!b) return false;

                        b = m_pFontBodyShadow.DrawString(
                                pGraphicsDrawn,
                                pFontFamily,
                                fontStyle,
                                nfontSize,
                                strText,
                                rtDraw,
                                strFormat);

                        if (!b) return false;

                        b = m_pShadowStrategy.DrawString(
                                pGraphicsDrawn,
                                pFontFamily,
                                fontStyle,
                                nfontSize,
                                strText,
                                rtDraw,
                                strFormat);

                        if (!b) return false;

                        unsafe
                        {
                            UInt32* pixelsDest = null;
                            UInt32* pixelsMask = null;
                            UInt32* pixelsShadowMask = null;

                            BitmapData bitmapDataDest = new BitmapData();
                            BitmapData bitmapDataMask = new BitmapData();
                            BitmapData bitmapDataShadowMask = new BitmapData();
                            Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                            pBitmapDrawn.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataDest);

                            pBitmapMask.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataMask);

                            pBitmapShadowMask.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataShadowMask);

                            pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                            pixelsMask = (UInt32*)(bitmapDataMask.Scan0);
                            pixelsShadowMask = (UInt32*)(bitmapDataShadowMask.Scan0);

                            if (pixelsDest == null || pixelsMask == null || pixelsShadowMask == null)
                                return false;

                            UInt32 col = 0;
                            int stride = bitmapDataDest.Stride >> 2;
                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                            {
                                UInt32 total_row_len = (UInt32)(row * stride);
                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                {
                                    UInt32 index = total_row_len + col;
                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                    Byte nAlphaShadow = (Byte)(pixelsShadowMask[index] & 0xff);
                                    if (nAlpha > 0 && nAlpha > nAlphaShadow)
                                    {
                                        pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                    }
                                    else if (nAlphaShadow > 0)
                                    {
                                        pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                        pixelsMask[index] = pixelsShadowMask[index];
                                    }
                                }
                            }

                            pBitmapShadowMask.UnlockBits(bitmapDataShadowMask);
                            pBitmapMask.UnlockBits(bitmapDataMask);
                            pBitmapDrawn.UnlockBits(bitmapDataDest);

                        }
                    }
                }
            }
            return true;
        }

        public bool GdiRenderFontShadow(
            System.Drawing.Graphics pGraphicsDrawn,
            System.Drawing.Graphics pGraphicsMask,
            System.Drawing.Bitmap pBitmapDrawn,
            System.Drawing.Bitmap pBitmapMask,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw)
        {
            if (pGraphicsDrawn == null || pGraphicsMask == null || pBitmapDrawn == null || pBitmapMask == null) return false;

            using (System.Drawing.Bitmap pBitmapShadowMask =
                m_pPngBitmap.Clone(new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height), PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Graphics pGraphicsShadowMask = Graphics.FromImage((Image)(pBitmapShadowMask)))
                {
                    using (System.Drawing.SolidBrush brushBlack = new SolidBrush(Color.FromArgb(0, 0, 0)))
                    {
                        pGraphicsShadowMask.FillRectangle(brushBlack, 0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);

                        pGraphicsShadowMask.CompositingMode = pGraphicsDrawn.CompositingMode;
                        pGraphicsShadowMask.CompositingQuality = pGraphicsDrawn.CompositingQuality;
                        pGraphicsShadowMask.InterpolationMode = pGraphicsDrawn.InterpolationMode;
                        pGraphicsShadowMask.SmoothingMode = pGraphicsDrawn.SmoothingMode;
                        pGraphicsShadowMask.TextRenderingHint = pGraphicsDrawn.TextRenderingHint;
                        pGraphicsShadowMask.PageUnit = pGraphicsDrawn.PageUnit;
                        pGraphicsShadowMask.PageScale = pGraphicsDrawn.PageScale;

                        bool b = false;

                        b = m_pFontBodyShadowMask.GdiDrawString(
                            pGraphicsMask,
                            pLogFont,
                            pszText,
                            ptDraw);

                        if (!b) return false;

                        b = m_pShadowStrategyMask.GdiDrawString(
                            pGraphicsShadowMask,
                            pLogFont,
                            pszText,
                            ptDraw);

                        if (!b) return false;

                        b = m_pFontBodyShadow.GdiDrawString(
                            pGraphicsDrawn,
                            pLogFont,
                            pszText,
                            ptDraw);

                        if (!b) return false;

                        b = m_pShadowStrategy.GdiDrawString(
                            pGraphicsDrawn,
                            pLogFont,
                            pszText,
                            ptDraw);

                        if (!b) return false;

                        unsafe
                        {
                            UInt32* pixelsDest = null;
                            UInt32* pixelsMask = null;
                            UInt32* pixelsShadowMask = null;

                            BitmapData bitmapDataDest = new BitmapData();
                            BitmapData bitmapDataMask = new BitmapData();
                            BitmapData bitmapDataShadowMask = new BitmapData();
                            Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                            pBitmapDrawn.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataDest);

                            pBitmapMask.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataMask);

                            pBitmapShadowMask.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataShadowMask);

                            pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                            pixelsMask = (UInt32*)(bitmapDataMask.Scan0);
                            pixelsShadowMask = (UInt32*)(bitmapDataShadowMask.Scan0);

                            if (pixelsDest == null || pixelsMask == null || pixelsShadowMask == null)
                                return false;

                            UInt32 col = 0;
                            int stride = bitmapDataDest.Stride >> 2;
                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                            {
                                UInt32 total_row_len = (UInt32)(row * stride);
                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                {
                                    UInt32 index = total_row_len + col;
                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                    Byte nAlphaShadow = (Byte)(pixelsShadowMask[index] & 0xff);
                                    if (nAlpha > 0 && nAlpha > nAlphaShadow)
                                    {
                                        pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                    }
                                    else if (nAlphaShadow > 0)
                                    {
                                        pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                        pixelsMask[index] = pixelsShadowMask[index];
                                    }
                                }
                            }

                            pBitmapShadowMask.UnlockBits(bitmapDataShadowMask);
                            pBitmapMask.UnlockBits(bitmapDataMask);
                            pBitmapDrawn.UnlockBits(bitmapDataDest);

                        }

                        if (pGraphicsShadowMask != null)
                        {
                            pGraphicsShadowMask.Dispose();
                        }

                        if (pBitmapShadowMask != null)
                        {
                            pBitmapShadowMask.Dispose();
                        }
                    }
                }
            }

            return true;
        }

        public bool GdiRenderFontShadow(
            System.Drawing.Graphics pGraphicsDrawn,
            System.Drawing.Graphics pGraphicsMask,
            System.Drawing.Bitmap pBitmapDrawn,
            System.Drawing.Bitmap pBitmapMask,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw)
        {
            if (pGraphicsDrawn == null || pGraphicsMask == null || pBitmapDrawn == null || pBitmapMask == null) return false;

            using (System.Drawing.Bitmap pBitmapShadowMask =
                m_pPngBitmap.Clone(new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height), PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Graphics pGraphicsShadowMask = Graphics.FromImage((Image)(pBitmapShadowMask)))
                {
                    using (System.Drawing.SolidBrush brushBlack = new SolidBrush(Color.FromArgb(0, 0, 0)))
                    {
                        pGraphicsShadowMask.FillRectangle(brushBlack, 0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height);

                        pGraphicsShadowMask.CompositingMode = pGraphicsDrawn.CompositingMode;
                        pGraphicsShadowMask.CompositingQuality = pGraphicsDrawn.CompositingQuality;
                        pGraphicsShadowMask.InterpolationMode = pGraphicsDrawn.InterpolationMode;
                        pGraphicsShadowMask.SmoothingMode = pGraphicsDrawn.SmoothingMode;
                        pGraphicsShadowMask.TextRenderingHint = pGraphicsDrawn.TextRenderingHint;
                        pGraphicsShadowMask.PageUnit = pGraphicsDrawn.PageUnit;
                        pGraphicsShadowMask.PageScale = pGraphicsDrawn.PageScale;

                        bool b = false;

                        b = m_pFontBodyShadowMask.GdiDrawString(
                            pGraphicsMask,
                            pLogFont,
                            pszText,
                            rtDraw);

                        if (!b) return false;

                        b = m_pShadowStrategyMask.GdiDrawString(
                            pGraphicsShadowMask,
                            pLogFont,
                            pszText,
                            rtDraw);

                        if (!b) return false;

                        b = m_pFontBodyShadow.GdiDrawString(
                            pGraphicsDrawn,
                            pLogFont,
                            pszText,
                            rtDraw);

                        if (!b) return false;

                        b = m_pShadowStrategy.GdiDrawString(
                            pGraphicsDrawn,
                            pLogFont,
                            pszText,
                            rtDraw);

                        if (!b) return false;

                        unsafe
                        {
                            UInt32* pixelsDest = null;
                            UInt32* pixelsMask = null;
                            UInt32* pixelsShadowMask = null;

                            BitmapData bitmapDataDest = new BitmapData();
                            BitmapData bitmapDataMask = new BitmapData();
                            BitmapData bitmapDataShadowMask = new BitmapData();
                            Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                            pBitmapDrawn.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataDest);

                            pBitmapMask.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataMask);

                            pBitmapShadowMask.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                PixelFormat.Format32bppArgb,
                                bitmapDataShadowMask);

                            pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                            pixelsMask = (UInt32*)(bitmapDataMask.Scan0);
                            pixelsShadowMask = (UInt32*)(bitmapDataShadowMask.Scan0);

                            if (pixelsDest == null || pixelsMask == null || pixelsShadowMask == null)
                                return false;

                            UInt32 col = 0;
                            int stride = bitmapDataDest.Stride >> 2;
                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                            {
                                UInt32 total_row_len = (UInt32)(row * stride);
                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                {
                                    UInt32 index = total_row_len + col;
                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                    Byte nAlphaShadow = (Byte)(pixelsShadowMask[index] & 0xff);
                                    if (nAlpha > 0 && nAlpha > nAlphaShadow)
                                    {
                                        pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                    }
                                    else if (nAlphaShadow > 0)
                                    {
                                        pixelsDest[index] = (UInt32)(0xff << 24 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                        pixelsMask[index] = pixelsShadowMask[index];
                                    }
                                }
                            }

                            pBitmapShadowMask.UnlockBits(bitmapDataShadowMask);
                            pBitmapMask.UnlockBits(bitmapDataMask);
                            pBitmapDrawn.UnlockBits(bitmapDataDest);

                        }

                        if (pGraphicsShadowMask != null)
                        {
                            pGraphicsShadowMask.Dispose();
                        }

                        if (pBitmapShadowMask != null)
                        {
                            pBitmapShadowMask.Dispose();
                        }

                    }
                }
            }
            return true;
        }


        UInt32 Alphablend(UInt32 dest, UInt32 source, Byte nAlpha)
        {
            if (0 == nAlpha)
                return dest;

            if (255 == nAlpha)
                return source;

            Byte nInvAlpha = (Byte)(~nAlpha);

            Byte nSrcRed = (Byte)((source & 0xff0000) >> 16);
            Byte nSrcGreen = (Byte)((source & 0xff00) >> 8);
            Byte nSrcBlue = (Byte)((source & 0xff));

            Byte nDestRed = (Byte)((dest & 0xff0000) >> 16);
            Byte nDestGreen = (Byte)((dest & 0xff00) >> 8);
            Byte nDestBlue = (Byte)(dest & 0xff);

            Byte nRed = (Byte)((nSrcRed * nAlpha + nDestRed * nInvAlpha) >> 8);
            Byte nGreen = (Byte)((nSrcGreen * nAlpha + nDestGreen * nInvAlpha) >> 8);
            Byte nBlue = (Byte)((nSrcBlue * nAlpha + nDestBlue * nInvAlpha) >> 8);

            return (UInt32)(0xff000000 | nRed << 16 | nGreen << 8 | nBlue);
        }

        public void EnableReflection(bool bEnable)
        {
            m_bEnableReflection = bEnable;
        }

        bool IsReflectionEnabled() 
        { 
            return m_bEnableReflection; 
        }

        public void Reflection(float fBegAlpha, float fEndAlpha, float fShown)
        {
        	m_fBegAlpha = fBegAlpha;
        	m_fEndAlpha = fEndAlpha;
        	m_fShown = fShown;
        }

        protected bool ProcessReflection()
        {
            if (m_pPngBitmap != null)
            {
                if (m_pReflectionPngBitmap != null)
                {
                    m_pReflectionPngBitmap = null;
                }

                m_pReflectionPngBitmap = m_pPngBitmap.Clone(new Rectangle(0, 0, m_pPngBitmap.Width, m_pPngBitmap.Height), PixelFormat.Format32bppArgb);
                m_pReflectionPngBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);


                BitmapData bitmapData = new BitmapData();
                Rectangle rect = new Rectangle(0, 0, m_pReflectionPngBitmap.Width, m_pReflectionPngBitmap.Height);

                m_pReflectionPngBitmap.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb,
                    bitmapData);

                unsafe
                {
                    UInt32* pixels = null;
                    // Write to the buffer provided by LockBits.
                    pixels = (UInt32*)bitmapData.Scan0;

                    if (pixels == null)
                        return false;

                    UInt32 end = (UInt32)(m_fShown * bitmapData.Height);
                    UInt32 nMultiplyAlpha = 0;
                    float diff = m_fBegAlpha - m_fEndAlpha;

                    UInt32 col = 0;
                    int stride = bitmapData.Stride >> 2;
                    for (UInt32 row = 0; row < bitmapData.Height; ++row)
                    {
                        if (row < end)
                        {
                            if (m_fBegAlpha == m_fEndAlpha)
                                nMultiplyAlpha = (UInt32)(m_fBegAlpha * 255);
                            else
                            {
                                if (m_fBegAlpha > m_fEndAlpha)
                                    nMultiplyAlpha = (UInt32)((m_fBegAlpha - (diff * row / end)) * 255);
                                else
                                    nMultiplyAlpha = (UInt32)((m_fBegAlpha + (diff * row / end)) * 255);
                            }
                        }
                        else
                            nMultiplyAlpha = 0;

                        UInt32 total_row_len = (UInt32)(row * stride);
                        for (col = 0; col < bitmapData.Width; ++col)
                        {
                            UInt32 index = total_row_len + col;
                            UInt32 nAlpha = (pixels[index] & 0xff000000) >> 24;

                            nAlpha = nAlpha * nMultiplyAlpha / 255;
                            pixels[index] &= 0xffffff;
                            pixels[index] |= (nAlpha << 24);
                        }
                    }
                }
                m_pReflectionPngBitmap.UnlockBits(bitmapData);
                return true;
            }
            return false;
        }
        public bool GdiMeasureString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw,
            ref float pfPixelsStartX,
            ref float pfPixelsStartY,
            ref float pfDestWidth,
            ref float pfDestHeight)
        {
            float fDestWidth1 = 0.0f;
            float fDestHeight1 = 0.0f;
            bool b = false;
            if (m_pTextStrategy != null)
            {
                b = m_pTextStrategy.GdiMeasureString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    ptDraw,
                    ref pfPixelsStartX,
                    ref pfPixelsStartY,
                    ref fDestWidth1,
                    ref fDestHeight1);

                if (!b)
                    return false;
            }

            float pfPixelsStartX2 = 0.0f;
            float pfPixelsStartY2 = 0.0f;
            float fDestWidth2 = 0.0f;
            float fDestHeight2 = 0.0f;
            if (m_bEnableShadow)
            {
                b = m_pShadowStrategy.GdiMeasureString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    ptDraw,
                    ref pfPixelsStartX2,
                    ref pfPixelsStartY2,
                    ref fDestWidth2,
                    ref fDestHeight2);

                if (b)
                {
                    float pfPixelsStartX3 = 0.0f;
                    float pfPixelsStartY3 = 0.0f;
                    float fDestWidth3 = 0.0f;
                    float fDestHeight3 = 0.0f;
                    b = GDIPath.ConvertToPixels(pGraphics, m_ptShadowOffset.X, m_ptShadowOffset.Y,
                        ref pfPixelsStartX3, ref pfPixelsStartY3, ref fDestWidth3, ref fDestHeight3);
                    if (b)
                    {
                        fDestWidth2 += fDestWidth3;
                        fDestHeight2 += fDestHeight3;
                    }
                }
                else
                    return false;
            }

            if (fDestWidth1 > fDestWidth2 || fDestHeight1 > fDestHeight2)
            {
                pfDestWidth = fDestWidth1;
                pfDestHeight = fDestHeight1;
            }
            else
            {
                pfDestWidth = fDestWidth2;
                pfDestHeight = fDestHeight2;
            }

            return true;
        }

        public bool GdiMeasureString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw,
            ref float pfPixelsStartX,
            ref float pfPixelsStartY,
            ref float pfDestWidth,
            ref float pfDestHeight)
        {
            float fDestWidth1 = 0.0f;
            float fDestHeight1 = 0.0f;
            bool b = false;
            if (m_pTextStrategy != null)
            {
                b = m_pTextStrategy.GdiMeasureString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    rtDraw,
                    ref pfPixelsStartX,
                    ref pfPixelsStartY,
                    ref fDestWidth1,
                    ref fDestHeight1);

                if (!b)
                    return false;
            }

            float pfPixelsStartX2 = 0.0f;
            float pfPixelsStartY2 = 0.0f;
            float fDestWidth2 = 0.0f;
            float fDestHeight2 = 0.0f;
            if (m_bEnableShadow)
            {
                b = m_pShadowStrategy.GdiMeasureString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    rtDraw,
                    ref pfPixelsStartX2,
                    ref pfPixelsStartY2,
                    ref fDestWidth2,
                    ref fDestHeight2);


                if (b)
                {
                    float pfPixelsStartX3 = 0.0f;
                    float pfPixelsStartY3 = 0.0f;
                    float fDestWidth3 = 0.0f;
                    float fDestHeight3 = 0.0f;
                    b = GDIPath.ConvertToPixels(pGraphics, m_ptShadowOffset.X, m_ptShadowOffset.Y,
                        ref pfPixelsStartX3, ref pfPixelsStartY3, ref fDestWidth3, ref fDestHeight3);
                    if (b)
                    {
                        fDestWidth2 += fDestWidth3;
                        fDestHeight2 += fDestHeight3;
                    }
                }
                else
                    return false;
            }

            if (fDestWidth1 > fDestWidth2 || fDestHeight1 > fDestHeight2)
            {
                pfDestWidth = fDestWidth1;
                pfDestHeight = fDestHeight1;
            }
            else
            {
                pfDestWidth = fDestWidth2;
                pfDestHeight = fDestHeight2;
            }

            return true;
        }

        public bool GdiMeasureStringRealHeight(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw,
            ref float pfPixelsStartX,
            ref float pfPixelsStartY,
            ref float pfDestWidth,
            ref float pfDestHeight)
        {
            float fDestWidth1 = 0.0f;
            float fDestHeight1 = 0.0f;
            bool b = false;
            if (m_pTextStrategy != null)
            {
                b = m_pTextStrategy.GdiMeasureStringRealHeight(
                    pGraphics,
                    pLogFont,
                    pszText,
                    ptDraw,
                    ref pfPixelsStartX,
                    ref pfPixelsStartY,
                    ref fDestWidth1,
                    ref fDestHeight1);

                if (!b)
                    return false;
            }

            float pfPixelsStartX2 = 0.0f;
            float pfPixelsStartY2 = 0.0f;
            float fDestWidth2 = 0.0f;
            float fDestHeight2 = 0.0f;
            if (m_bEnableShadow)
            {
                b = m_pShadowStrategy.GdiMeasureStringRealHeight(
                    pGraphics,
                    pLogFont,
                    pszText,
                    ptDraw,
                    ref pfPixelsStartX2,
                    ref pfPixelsStartY2,
                    ref fDestWidth2,
                    ref fDestHeight2);

                if (b)
                {
                    float pfPixelsStartX3 = 0.0f;
                    float pfPixelsStartY3 = 0.0f;

                    float fDestWidth3 = 0.0f;
                    float fDestHeight3 = 0.0f;
                    b = GDIPath.ConvertToPixels(pGraphics, m_ptShadowOffset.X, m_ptShadowOffset.Y,
                        ref pfPixelsStartX3, ref pfPixelsStartY3, ref fDestWidth3, ref fDestHeight3);
                    if (b)
                    {
                        fDestWidth2 += fDestWidth3;
                        fDestHeight2 += fDestHeight3;
                    }
                }
                else
                    return false;
            }

            if (fDestWidth1 > fDestWidth2 || fDestHeight1 > fDestHeight2)
            {
                pfDestWidth = fDestWidth1;
                pfDestHeight = fDestHeight1;
            }
            else
            {
                pfDestWidth = fDestWidth2;
                pfDestHeight = fDestHeight2;
            }

            return true;
        }

        public bool GdiMeasureStringRealHeight(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw,
            ref float pfPixelsStartX,
            ref float pfPixelsStartY,
            ref float pfDestWidth,
            ref float pfDestHeight)
        {
            float fDestWidth1 = 0.0f;
            float fDestHeight1 = 0.0f;
            bool b = false;
            if (m_pTextStrategy != null)
            {
                b = m_pTextStrategy.GdiMeasureStringRealHeight(
                    pGraphics,
                    pLogFont,
                    pszText,
                    rtDraw,
                    ref pfPixelsStartX,
                    ref pfPixelsStartY,
                    ref fDestWidth1,
                    ref fDestHeight1);

                if (!b)
                    return false;
            }

            float pfPixelsStartX2 = 0.0f;
            float pfPixelsStartY2 = 0.0f;
            float fDestWidth2 = 0.0f;
            float fDestHeight2 = 0.0f;
            if (m_bEnableShadow)
            {
                b = m_pShadowStrategy.GdiMeasureStringRealHeight(
                    pGraphics,
                    pLogFont,
                    pszText,
                    rtDraw,
                    ref pfPixelsStartX2,
                    ref pfPixelsStartY2,
                    ref fDestWidth2,
                    ref fDestHeight2);

                if (b)
                {
                    float pfPixelsStartX3 = 0.0f;
                    float pfPixelsStartY3 = 0.0f;
                    float fDestWidth3 = 0.0f;
                    float fDestHeight3 = 0.0f;
                    b = GDIPath.ConvertToPixels(pGraphics, m_ptShadowOffset.X, m_ptShadowOffset.Y,
                        ref pfPixelsStartX3, ref pfPixelsStartY3, ref fDestWidth3, ref fDestHeight3);
                    if (b)
                    {
                        fDestWidth2 += fDestWidth3;
                        fDestHeight2 += fDestHeight3;
                    }
                }
                else
                    return false;
            }

            if (fDestWidth1 > fDestWidth2 || fDestHeight1 > fDestHeight2)
            {
                pfDestWidth = fDestWidth1;
                pfDestHeight = fDestHeight1;
            }
            else
            {
                pfDestWidth = fDestWidth2;
                pfDestHeight = fDestHeight2;
            }

            return true;
        }


        protected ITextStrategy m_pTextStrategy;
        protected ITextStrategy m_pShadowStrategy;
        protected ITextStrategy m_pShadowStrategyMask;
        protected ITextStrategy m_pFontBodyShadow;
        protected ITextStrategy m_pFontBodyShadowMask;
        protected System.Drawing.Point m_ptShadowOffset;
        protected System.Drawing.Color m_clrShadow;
        protected System.Drawing.Bitmap m_pBkgdBitmap;
        protected System.Drawing.Bitmap m_pPngBitmap;
        protected System.Drawing.Bitmap m_pReflectionPngBitmap;
        protected bool m_bEnableShadow;
        protected bool m_bDiffuseShadow;
        protected int m_nShadowThickness;
        protected bool m_bExtrudeShadow;
        protected bool m_bEnableReflection;
        protected float m_fBegAlpha;
        protected float m_fEndAlpha;
        protected float m_fShown;
        protected bool disposed;
    }
}
