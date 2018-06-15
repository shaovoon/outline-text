using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextDesignerCSLibrary
{
    public class OutlineText : IOutlineText
    {
        public OutlineText()
        {
            m_pTextStrategy = null;
            m_pShadowStrategy = null;
            m_pFontBodyShadow = null;
            m_pBkgdBitmap = null;
            m_clrShadow = System.Drawing.Color.FromArgb(0,0,0);
            m_bEnableShadow = false;
            m_bDiffuseShadow = false;
            m_nShadowThickness = 2;
            disposed = false;
        }
        public OutlineText(OutlineText rhs)
        {
            if (rhs.m_pTextStrategy != null) m_pTextStrategy = rhs.m_pTextStrategy.Clone(); else m_pTextStrategy = null;
            if (rhs.m_pShadowStrategy != null) m_pShadowStrategy = rhs.m_pShadowStrategy.Clone(); else m_pShadowStrategy = null;
            if (rhs.m_pFontBodyShadow != null) m_pFontBodyShadow = rhs.m_pFontBodyShadow.Clone(); else m_pFontBodyShadow = null;
            m_pBkgdBitmap = rhs.m_pBkgdBitmap;
            m_clrShadow = rhs.m_clrShadow;
            m_bEnableShadow = rhs.m_bEnableShadow;
            m_bDiffuseShadow = rhs.m_bDiffuseShadow;
            m_nShadowThickness = rhs.m_nShadowThickness;
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
                    if (m_pFontBodyShadow != null)
                        m_pFontBodyShadow.Dispose();
                    if (m_pBkgdBitmap != null)
                        m_pBkgdBitmap.Dispose();
                }

                disposed = true;
            }
        }

        ~OutlineText()
        {
            Dispose(false);
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
            m_pBkgdBitmap = new System.Drawing.Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

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

	        m_clrShadow = color;

	        TextOutlineStrategy pFontBodyShadow = new TextOutlineStrategy();
            pFontBodyShadow.Init(System.Drawing.Color.FromArgb(255, 255, 255), System.Drawing.Color.FromArgb(0, 0, 0, 0), 0);
	        m_pFontBodyShadow = pFontBodyShadow;

	        m_ptShadowOffset = ptOffset;
	        m_pShadowStrategy = pStrat;
	        m_bDiffuseShadow = false;
        }

        public void DiffusedShadow(
	        System.Drawing.Color color, 
	        int nThickness,
	        System.Drawing.Point ptOffset)
        {
	        DiffusedShadowStrategy pStrat = new DiffusedShadowStrategy();
	        pStrat.Init(System.Drawing.Color.FromArgb(0,0,0,0),color,nThickness,true);

	        m_clrShadow = color;

	        DiffusedShadowStrategy pFontBodyShadow = new DiffusedShadowStrategy();
            pFontBodyShadow.Init(System.Drawing.Color.FromArgb(color.A, 255, 255), System.Drawing.Color.FromArgb(0, 0, 0, 0), 0, true);
	        m_pFontBodyShadow = pFontBodyShadow;

	        m_ptShadowOffset = ptOffset;
	        m_pShadowStrategy = pStrat;
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
            pStrat.Init(System.Drawing.Color.FromArgb(0, 0, 0, 0), color, nThickness, ptOffset.X, ptOffset.Y);

	        m_clrShadow = color;

	        ExtrudeStrategy pFontBodyShadow = new ExtrudeStrategy();
            pFontBodyShadow.Init(System.Drawing.Color.FromArgb(color.A, 255, 255), System.Drawing.Color.FromArgb(0, 0, 0, 0), 0, ptOffset.X, ptOffset.Y);
	        m_pFontBodyShadow = pFontBodyShadow;

	        m_ptShadowOffset = ptOffset;
	        m_pShadowStrategy = pStrat;
	        m_bExtrudeShadow = true;
	        m_bDiffuseShadow = false;
	        m_nShadowThickness = nThickness;
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
             if (m_bEnableShadow && m_pShadowStrategy != null)
	        {
		        m_pShadowStrategy.DrawString(
			        graphics, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        new System.Drawing.Point(ptDraw.X+m_ptShadowOffset.X, ptDraw.Y+m_ptShadowOffset.Y),
			        strFormat);
	        }

            if (m_bEnableShadow && m_pBkgdBitmap != null && m_pFontBodyShadow != null)
	        {
		        RenderFontShadow(
			        graphics,
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText,
                    new System.Drawing.Point(ptDraw.X + m_ptShadowOffset.X, ptDraw.Y + m_ptShadowOffset.Y),
			        strFormat);
	        }

            if (m_pTextStrategy != null)
	        {
		        return m_pTextStrategy.DrawString(
			        graphics, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        ptDraw, 
			        strFormat);
	        }

	        return false;
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
	        if(m_bEnableShadow &&m_pShadowStrategy!=null)
	        {
		        m_pShadowStrategy.DrawString(
			        graphics, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        new System.Drawing.Rectangle(rtDraw.X+m_ptShadowOffset.X, rtDraw.Y+m_ptShadowOffset.Y,rtDraw.Width,rtDraw.Height),
			        strFormat);
	        }

            if (m_bEnableShadow && m_pBkgdBitmap != null && m_pFontBodyShadow != null)
	        {
		        RenderFontShadow(
			        graphics,
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText,
                    new System.Drawing.Rectangle(rtDraw.X + m_ptShadowOffset.X, rtDraw.Y + m_ptShadowOffset.Y, rtDraw.Width, rtDraw.Height),
			        strFormat);
	        }

            if (m_pTextStrategy != null)
	        {
		        return m_pTextStrategy.DrawString(
			        graphics, 
			        fontFamily,
			        fontStyle,
			        nfontSize,
			        strText, 
			        rtDraw, 
			        strFormat);
	        }

	        return false;
        }

        public bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw)
        {
            if (m_bEnableShadow && m_pShadowStrategy != null)
            {
                m_pShadowStrategy.GdiDrawString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    new Point(ptDraw.X + m_ptShadowOffset.X, ptDraw.Y + m_ptShadowOffset.Y));
            }

            if (m_bEnableShadow && m_pBkgdBitmap != null && m_pFontBodyShadow != null)
            {
                GdiRenderFontShadow(
                    pGraphics,
                    pLogFont,
                    pszText,
                    new Point(ptDraw.X + m_ptShadowOffset.X, ptDraw.Y + m_ptShadowOffset.Y));
            }

            if (m_pTextStrategy != null)
            {
                return m_pTextStrategy.GdiDrawString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    ptDraw);
            }

            return false;
        }

        public bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw)
        {
            if (m_bEnableShadow && m_pShadowStrategy != null)
            {
                m_pShadowStrategy.GdiDrawString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    new Rectangle(rtDraw.X + m_ptShadowOffset.X, rtDraw.Y + m_ptShadowOffset.Y, rtDraw.Width, rtDraw.Height));
            }

            if (m_bEnableShadow && m_pBkgdBitmap != null && m_pFontBodyShadow != null)
            {
                GdiRenderFontShadow(
                    pGraphics,
                    pLogFont,
                    pszText,
                    new Rectangle(rtDraw.X + m_ptShadowOffset.X, rtDraw.Y + m_ptShadowOffset.Y, rtDraw.Width, rtDraw.Height));
            }

            if (m_pTextStrategy != null)
            {
                return m_pTextStrategy.GdiDrawString(
                    pGraphics,
                    pLogFont,
                    pszText,
                    rtDraw);
            }

            return false;
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
            if(m_pTextStrategy!=null)
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
    		        ref fDestHeight1 );
    	        if(!b)
    		        return false;
            }


	        float fDestWidth2 = 0.0f;
	        float fDestHeight2 = 0.0f;
            float fStartX2 = 0.0f;
            float fStartY2 = 0.0f;
	        if(m_bEnableShadow)
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
                    b = GDIPath.ConvertToPixels(graphics, m_ptShadowOffset.X, m_ptShadowOffset.Y,
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
	        if(m_bEnableShadow)
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

    void RenderFontShadow(	
	    System.Drawing.Graphics graphics, 
	    System.Drawing.FontFamily fontFamily,
	    System.Drawing.FontStyle fontStyle,
	    int nfontSize,
	    string strText, 
	    System.Drawing.Point ptDraw, 
	    System.Drawing.StringFormat strFormat)
    {
        Rectangle rectbmp = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);
        using (System.Drawing.Bitmap pBmpMask =
            m_pBkgdBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
        {

            using (System.Drawing.Bitmap pBmpFontBodyBackup =
                m_pBkgdBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Graphics graphicsMask = System.Drawing.Graphics.FromImage(pBmpMask))
                {
                    using (System.Drawing.SolidBrush brushBlack = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)))
                    {
                        graphicsMask.FillRectangle(brushBlack, 0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);


                        using (System.Drawing.Bitmap pBmpDisplay =
                            m_pBkgdBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
                        {
                            using (System.Drawing.Graphics graphicsBkgd = System.Drawing.Graphics.FromImage(pBmpDisplay))
                            {

                                graphicsMask.CompositingMode = graphics.CompositingMode;
                                graphicsMask.CompositingQuality = graphics.CompositingQuality;
                                graphicsMask.InterpolationMode = graphics.InterpolationMode;
                                graphicsMask.SmoothingMode = graphics.SmoothingMode;
                                graphicsMask.TextRenderingHint = graphics.TextRenderingHint;
                                graphicsMask.PageUnit = graphics.PageUnit;
                                graphicsMask.PageScale = graphics.PageScale;

                                graphicsBkgd.CompositingMode = graphics.CompositingMode;
                                graphicsBkgd.CompositingQuality = graphics.CompositingQuality;
                                graphicsBkgd.InterpolationMode = graphics.InterpolationMode;
                                graphicsBkgd.SmoothingMode = graphics.SmoothingMode;
                                graphicsBkgd.TextRenderingHint = graphics.TextRenderingHint;
                                graphicsBkgd.PageUnit = graphics.PageUnit;
                                graphicsBkgd.PageScale = graphics.PageScale;

                                m_pFontBodyShadow.DrawString(
                                    graphicsMask,
                                    fontFamily,
                                    fontStyle,
                                    nfontSize,
                                    strText,
                                    ptDraw,
                                    strFormat);

                                m_pShadowStrategy.DrawString(
                                    graphicsBkgd,
                                    fontFamily,
                                    fontStyle,
                                    nfontSize,
                                    strText,
                                    ptDraw,
                                    strFormat);

                                unsafe
                                {
                                    UInt32* pixelsSrc = null;
                                    UInt32* pixelsDest = null;
                                    UInt32* pixelsMask = null;

                                    BitmapData bitmapDataSrc = new BitmapData();
                                    BitmapData bitmapDataDest = new BitmapData();
                                    BitmapData bitmapDataMask = new BitmapData();
                                    Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                                    pBmpFontBodyBackup.LockBits(
                                        rect,
                                        ImageLockMode.WriteOnly,
                                        PixelFormat.Format32bppArgb,
                                        bitmapDataSrc);

                                    pBmpDisplay.LockBits(
                                        rect,
                                        ImageLockMode.WriteOnly,
                                        PixelFormat.Format32bppArgb,
                                        bitmapDataDest);

                                    pBmpMask.LockBits(
                                        rect,
                                        ImageLockMode.WriteOnly,
                                        PixelFormat.Format32bppArgb,
                                        bitmapDataMask);


                                    // Write to the temporary buffer provided by LockBits.
                                    pixelsSrc = (UInt32*)(bitmapDataSrc.Scan0);
                                    pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                                    pixelsMask = (UInt32*)(bitmapDataMask.Scan0);

                                    if (pixelsSrc == null || pixelsDest == null || pixelsMask == null)
                                        return;

                                    UInt32 col = 0;
                                    int stride = bitmapDataDest.Stride >> 2;
                                    if (m_bDiffuseShadow && m_bExtrudeShadow == false)
                                    {
                                        for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                        {
                                            UInt32 total_row_len = (UInt32)(row * stride);
                                            for (col = 0; col < bitmapDataDest.Width; ++col)
                                            {
                                                UInt32 index = total_row_len + col;
                                                Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                if (nAlpha > 0)
                                                {
                                                    UInt32 clrtotal = clrShadow;
                                                    for (int i = 2; i <= m_nShadowThickness; ++i)
                                                        pixelsSrc[index] = Alphablend(pixelsSrc[index], clrtotal, m_clrShadow.A);

                                                    pixelsDest[index] = pixelsSrc[index];
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                        {
                                            UInt32 total_row_len = (UInt32)(row * stride);
                                            for (col = 0; col < bitmapDataDest.Width; ++col)
                                            {
                                                UInt32 index = total_row_len + col;
                                                Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                if (nAlpha > 0)
                                                    pixelsDest[index] = Alphablend(pixelsSrc[index], clrShadow, m_clrShadow.A);
                                            }
                                        }
                                    }

                                    pBmpMask.UnlockBits(bitmapDataMask);
                                    pBmpDisplay.UnlockBits(bitmapDataDest);
                                    pBmpFontBodyBackup.UnlockBits(bitmapDataSrc);

                                    graphics.DrawImage(pBmpDisplay, 0, 0, pBmpDisplay.Width, pBmpDisplay.Height);

                                }
                            }
                        }
                    }
                }
            }
        }
    }

        void RenderFontShadow(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int nfontSize,
            string pszText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat)
        {
            Rectangle rectbmp = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);
            using (System.Drawing.Bitmap pBmpMask =
                m_pBkgdBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Bitmap pBmpFontBodyBackup =
                    m_pBkgdBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
                {

                    using (System.Drawing.Graphics graphicsMask = System.Drawing.Graphics.FromImage(pBmpMask))
                    {
                        using (System.Drawing.SolidBrush brushBlack = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)))
                        {
                            graphicsMask.FillRectangle(brushBlack, 0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                            using (System.Drawing.Bitmap pBmpDisplay =
                                m_pBkgdBitmap.Clone(rectbmp, PixelFormat.Format32bppArgb))
                            {
                                using (System.Drawing.Graphics graphicsBkgd = System.Drawing.Graphics.FromImage(pBmpDisplay))
                                {

                                    graphicsMask.CompositingMode = graphics.CompositingMode;
                                    graphicsMask.CompositingQuality = graphics.CompositingQuality;
                                    graphicsMask.InterpolationMode = graphics.InterpolationMode;
                                    graphicsMask.SmoothingMode = graphics.SmoothingMode;
                                    graphicsMask.TextRenderingHint = graphics.TextRenderingHint;
                                    graphicsMask.PageUnit = graphics.PageUnit;
                                    graphicsMask.PageScale = graphics.PageScale;

                                    graphicsBkgd.CompositingMode = graphics.CompositingMode;
                                    graphicsBkgd.CompositingQuality = graphics.CompositingQuality;
                                    graphicsBkgd.InterpolationMode = graphics.InterpolationMode;
                                    graphicsBkgd.SmoothingMode = graphics.SmoothingMode;
                                    graphicsBkgd.TextRenderingHint = graphics.TextRenderingHint;
                                    graphicsBkgd.PageUnit = graphics.PageUnit;
                                    graphicsBkgd.PageScale = graphics.PageScale;

                                    m_pFontBodyShadow.DrawString(
                                        graphicsMask,
                                        fontFamily,
                                        fontStyle,
                                        nfontSize,
                                        pszText,
                                        rtDraw,
                                        strFormat);

                                    m_pShadowStrategy.DrawString(
                                        graphicsBkgd,
                                        fontFamily,
                                        fontStyle,
                                        nfontSize,
                                        pszText,
                                        rtDraw,
                                        strFormat);

                                    unsafe
                                    {
                                        UInt32* pixelsSrc = null;
                                        UInt32* pixelsDest = null;
                                        UInt32* pixelsMask = null;

                                        BitmapData bitmapDataSrc = new BitmapData();
                                        BitmapData bitmapDataDest = new BitmapData();
                                        BitmapData bitmapDataMask = new BitmapData();
                                        Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                                        pBmpFontBodyBackup.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataSrc);

                                        pBmpDisplay.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataDest);

                                        pBmpMask.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataMask);


                                        // Write to the temporary buffer provided by LockBits.
                                        pixelsSrc = (UInt32*)(bitmapDataSrc.Scan0);
                                        pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                                        pixelsMask = (UInt32*)(bitmapDataMask.Scan0);

                                        if (pixelsSrc == null || pixelsDest == null || pixelsMask == null)
                                            return;

                                        UInt32 col = 0;
                                        int stride = bitmapDataDest.Stride >> 2;
                                        if (m_bDiffuseShadow && m_bExtrudeShadow == false)
                                        {
                                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                            {
                                                UInt32 total_row_len = (UInt32)(row * stride);
                                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                                {
                                                    UInt32 index = total_row_len + col;
                                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                    UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                    if (nAlpha > 0)
                                                    {
                                                        UInt32 clrtotal = clrShadow;
                                                        for (int i = 2; i <= m_nShadowThickness; ++i)
                                                            pixelsSrc[index] = Alphablend(pixelsSrc[index], clrtotal, m_clrShadow.A);

                                                        pixelsDest[index] = pixelsSrc[index];
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                            {
                                                UInt32 total_row_len = (UInt32)(row * stride);
                                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                                {
                                                    UInt32 index = total_row_len + col;
                                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                    UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                    if (nAlpha > 0)
                                                        pixelsDest[index] = Alphablend(pixelsSrc[index], clrShadow, m_clrShadow.A);
                                                }
                                            }
                                        }

                                        pBmpMask.UnlockBits(bitmapDataMask);
                                        pBmpDisplay.UnlockBits(bitmapDataDest);
                                        pBmpFontBodyBackup.UnlockBits(bitmapDataSrc);

                                        graphics.DrawImage(pBmpDisplay, 0, 0, pBmpDisplay.Width, pBmpDisplay.Height);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void GdiRenderFontShadow(
           System.Drawing.Graphics graphics,
           LOGFONT pLogFont,
           string pszText,
           System.Drawing.Point ptDraw)
        {
            using (System.Drawing.Bitmap pBmpMask =
                m_pBkgdBitmap.Clone(new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height), PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Bitmap pBmpFontBodyBackup =
                    m_pBkgdBitmap.Clone(new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height), PixelFormat.Format32bppArgb))
                {
                    using (System.Drawing.Graphics graphicsMask = Graphics.FromImage((Image)(pBmpMask)))
                    {
                        using (System.Drawing.SolidBrush brushBlack = new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)))
                        {
                            graphicsMask.FillRectangle(brushBlack, 0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                            using (System.Drawing.Bitmap pBmpDisplay =
                                m_pBkgdBitmap.Clone(new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height), PixelFormat.Format32bppArgb))
                            {
                                using (System.Drawing.Graphics graphicsBkgd = Graphics.FromImage((Image)(pBmpDisplay)))
                                {
                                    graphicsMask.CompositingMode = graphics.CompositingMode;
                                    graphicsMask.CompositingQuality = graphics.CompositingQuality;
                                    graphicsMask.InterpolationMode = graphics.InterpolationMode;
                                    graphicsMask.SmoothingMode = graphics.SmoothingMode;
                                    graphicsMask.TextRenderingHint = graphics.TextRenderingHint;
                                    graphicsMask.PageUnit = graphics.PageUnit;
                                    graphicsMask.PageScale = graphics.PageScale;

                                    graphicsBkgd.CompositingMode = graphics.CompositingMode;
                                    graphicsBkgd.CompositingQuality = graphics.CompositingQuality;
                                    graphicsBkgd.InterpolationMode = graphics.InterpolationMode;
                                    graphicsBkgd.SmoothingMode = graphics.SmoothingMode;
                                    graphicsBkgd.TextRenderingHint = graphics.TextRenderingHint;
                                    graphicsBkgd.PageUnit = graphics.PageUnit;
                                    graphicsBkgd.PageScale = graphics.PageScale;

                                    m_pFontBodyShadow.GdiDrawString(
                                        graphicsMask,
                                        pLogFont,
                                        pszText,
                                        ptDraw);

                                    m_pShadowStrategy.GdiDrawString(
                                        graphicsBkgd,
                                        pLogFont,
                                        pszText,
                                        ptDraw);

                                    unsafe
                                    {

                                        UInt32* pixelsSrc = null;
                                        UInt32* pixelsDest = null;
                                        UInt32* pixelsMask = null;

                                        BitmapData bitmapDataSrc = new BitmapData();
                                        BitmapData bitmapDataDest = new BitmapData();
                                        BitmapData bitmapDataMask = new BitmapData();
                                        Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);
                                        pBmpFontBodyBackup.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataSrc);

                                        pBmpDisplay.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataDest);

                                        pBmpMask.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataMask);


                                        // Write to the temporary buffer provided by LockBits.
                                        pixelsSrc = (UInt32*)(bitmapDataSrc.Scan0);
                                        pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                                        pixelsMask = (UInt32*)(bitmapDataMask.Scan0);

                                        if (pixelsSrc == null || pixelsDest == null || pixelsMask == null)
                                            return;

                                        UInt32 col = 0;
                                        int stride = bitmapDataDest.Stride >> 2;
                                        if (m_bDiffuseShadow && m_bExtrudeShadow == false)
                                        {
                                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                            {
                                                UInt32 total_row_len = (UInt32)(row * stride);
                                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                                {
                                                    UInt32 index = total_row_len + col;
                                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                    UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                    if (nAlpha > 0)
                                                    {
                                                        UInt32 clrtotal = clrShadow;
                                                        for (int i = 2; i <= m_nShadowThickness; ++i)
                                                            pixelsSrc[index] = Alphablend(pixelsSrc[index], clrtotal, m_clrShadow.A);

                                                        pixelsDest[index] = pixelsSrc[index];
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                            {
                                                UInt32 total_row_len = (UInt32)(row * stride);
                                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                                {
                                                    UInt32 index = total_row_len + col;
                                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                    UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                    if (nAlpha > 0)
                                                        pixelsDest[index] = Alphablend(pixelsSrc[index], clrShadow, m_clrShadow.A);
                                                }
                                            }
                                        }

                                        pBmpMask.UnlockBits(bitmapDataMask);
                                        pBmpDisplay.UnlockBits(bitmapDataDest);
                                        pBmpFontBodyBackup.UnlockBits(bitmapDataSrc);

                                        graphics.DrawImage(pBmpDisplay, 0, 0, pBmpDisplay.Width, pBmpDisplay.Height);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GdiRenderFontShadow(
           System.Drawing.Graphics graphics,
           LOGFONT pLogFont,
           string pszText,
           System.Drawing.Rectangle rtDraw)
        {
            using (System.Drawing.Bitmap pBmpMask =
                m_pBkgdBitmap.Clone(new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height), PixelFormat.Format32bppArgb))
            {

                using (System.Drawing.Bitmap pBmpFontBodyBackup =
                    m_pBkgdBitmap.Clone(new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height), PixelFormat.Format32bppArgb))
                {
                    using (System.Drawing.Graphics graphicsMask = Graphics.FromImage((Image)(pBmpMask)))
                    {
                        using (System.Drawing.SolidBrush brushBlack = new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)))
                        {
                            graphicsMask.FillRectangle(brushBlack, 0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);

                            using (System.Drawing.Bitmap pBmpDisplay =
                                m_pBkgdBitmap.Clone(new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height), PixelFormat.Format32bppArgb))
                            {
                                using (System.Drawing.Graphics graphicsBkgd = Graphics.FromImage((Image)(pBmpDisplay)))
                                {
                                    graphicsMask.CompositingMode = graphics.CompositingMode;
                                    graphicsMask.CompositingQuality = graphics.CompositingQuality;
                                    graphicsMask.InterpolationMode = graphics.InterpolationMode;
                                    graphicsMask.SmoothingMode = graphics.SmoothingMode;
                                    graphicsMask.TextRenderingHint = graphics.TextRenderingHint;
                                    graphicsMask.PageUnit = graphics.PageUnit;
                                    graphicsMask.PageScale = graphics.PageScale;

                                    graphicsBkgd.CompositingMode = graphics.CompositingMode;
                                    graphicsBkgd.CompositingQuality = graphics.CompositingQuality;
                                    graphicsBkgd.InterpolationMode = graphics.InterpolationMode;
                                    graphicsBkgd.SmoothingMode = graphics.SmoothingMode;
                                    graphicsBkgd.TextRenderingHint = graphics.TextRenderingHint;
                                    graphicsBkgd.PageUnit = graphics.PageUnit;
                                    graphicsBkgd.PageScale = graphics.PageScale;

                                    m_pFontBodyShadow.GdiDrawString(
                                        graphicsMask,
                                        pLogFont,
                                        pszText,
                                        rtDraw);

                                    m_pShadowStrategy.GdiDrawString(
                                        graphicsBkgd,
                                        pLogFont,
                                        pszText,
                                        rtDraw);

                                    unsafe
                                    {

                                        UInt32* pixelsSrc = null;
                                        UInt32* pixelsDest = null;
                                        UInt32* pixelsMask = null;

                                        BitmapData bitmapDataSrc = new BitmapData();
                                        BitmapData bitmapDataDest = new BitmapData();
                                        BitmapData bitmapDataMask = new BitmapData();
                                        Rectangle rect = new Rectangle(0, 0, m_pBkgdBitmap.Width, m_pBkgdBitmap.Height);
                                        pBmpFontBodyBackup.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataSrc);

                                        pBmpDisplay.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataDest);

                                        pBmpMask.LockBits(
                                            rect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format32bppArgb,
                                            bitmapDataMask);


                                        // Write to the temporary buffer provided by LockBits.
                                        pixelsSrc = (UInt32*)(bitmapDataSrc.Scan0);
                                        pixelsDest = (UInt32*)(bitmapDataDest.Scan0);
                                        pixelsMask = (UInt32*)(bitmapDataMask.Scan0);

                                        if (pixelsSrc == null || pixelsDest == null || pixelsMask == null)
                                            return;

                                        UInt32 col = 0;
                                        int stride = bitmapDataDest.Stride >> 2;
                                        if (m_bDiffuseShadow && m_bExtrudeShadow == false)
                                        {
                                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                            {
                                                UInt32 total_row_len = (UInt32)(row * stride);
                                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                                {
                                                    UInt32 index = total_row_len + col;
                                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                    UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                    if (nAlpha > 0)
                                                    {
                                                        UInt32 clrtotal = clrShadow;
                                                        for (int i = 2; i <= m_nShadowThickness; ++i)
                                                            pixelsSrc[index] = Alphablend(pixelsSrc[index], clrtotal, m_clrShadow.A);

                                                        pixelsDest[index] = pixelsSrc[index];
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (UInt32 row = 0; row < bitmapDataDest.Height; ++row)
                                            {
                                                UInt32 total_row_len = (UInt32)(row * stride);
                                                for (col = 0; col < bitmapDataDest.Width; ++col)
                                                {
                                                    UInt32 index = total_row_len + col;
                                                    Byte nAlpha = (Byte)(pixelsMask[index] & 0xff);
                                                    UInt32 clrShadow = (UInt32)(0xff000000 | m_clrShadow.R << 16 | m_clrShadow.G << 8 | m_clrShadow.B);
                                                    if (nAlpha > 0)
                                                        pixelsDest[index] = Alphablend(pixelsSrc[index], clrShadow, m_clrShadow.A);
                                                }
                                            }
                                        }

                                        pBmpMask.UnlockBits(bitmapDataMask);
                                        pBmpDisplay.UnlockBits(bitmapDataDest);
                                        pBmpFontBodyBackup.UnlockBits(bitmapDataSrc);

                                        graphics.DrawImage(pBmpDisplay, 0, 0, pBmpDisplay.Width, pBmpDisplay.Height);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        UInt32 Alphablend(UInt32 dest, UInt32 source, Byte nAlpha)
        {
	        if( 0 == nAlpha )
		        return dest;

	        if( 255 == nAlpha )
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
        protected ITextStrategy m_pFontBodyShadow;
        protected System.Drawing.Point m_ptShadowOffset;
        protected System.Drawing.Color m_clrShadow;
        protected System.Drawing.Bitmap m_pBkgdBitmap;
        protected bool m_bEnableShadow;
        protected bool m_bDiffuseShadow;
        protected int m_nShadowThickness;
        protected bool m_bExtrudeShadow;
        protected bool disposed;

    }
}
