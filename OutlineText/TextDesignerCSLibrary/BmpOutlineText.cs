using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextDesignerCSLibrary
{
    public class BmpOutlineText
    {
        public BmpOutlineText()
        {
            m_pbmpResult = null;
            m_pbmpMask = null;
            m_pbmpShadow = null;
            m_clrBkgd = Color.FromArgb(0, 255, 0);
            m_clrOutline = Color.FromArgb(255,0,0);
            m_clrText = Color.FromArgb(0,0,255);

            m_PngOutlineText = new PngOutlineText();
            m_PngShadow = new PngOutlineText();
        }

        public Bitmap Render(
	        uint nTextX, 
	        uint nTextY,
	        Bitmap pbmpText, 
	        uint nOutlineX, 
	        uint nOutlineY,
	        Bitmap pbmpOutline)
        {
	        if(pbmpText==null)
		        return null;
	        if(pbmpOutline==null)
		        return null;

	        m_pbmpResult = new Bitmap((int)(pbmpOutline.Width+nOutlineX), (int)(pbmpOutline.Height+nOutlineY), PixelFormat.Format32bppArgb);

	
	        
	        Bitmap png = m_PngOutlineText.GetPngImage();
	        BitmapData bitmapDataResult = new BitmapData();
	        BitmapData bitmapDataText = new BitmapData();
	        BitmapData bitmapDataOutline = new BitmapData();
	        BitmapData bitmapDataPng = new BitmapData();
	        Rectangle rectResult = new Rectangle(0, 0, m_pbmpResult.Width, m_pbmpResult.Height );
	        Rectangle rectText = new Rectangle(0, 0, pbmpText.Width, pbmpText.Height );
	        Rectangle rectOutline = new Rectangle(0, 0, pbmpOutline.Width, pbmpOutline.Height );
	        Rectangle rectPng = new Rectangle(0, 0, m_pbmpMask.Width, m_pbmpMask.Height );

	        m_pbmpResult.LockBits(
		        rectResult,
		        ImageLockMode.WriteOnly,
		        PixelFormat.Format32bppArgb,
		        bitmapDataResult );
	        pbmpText.LockBits(
		        rectText,
		        ImageLockMode.ReadOnly,
		        PixelFormat.Format32bppArgb,
		        bitmapDataText );
	        pbmpOutline.LockBits(
		        rectOutline,
		        ImageLockMode.ReadOnly,
		        PixelFormat.Format32bppArgb,
		        bitmapDataOutline );
	        png.LockBits(
		        rectPng,
		        ImageLockMode.ReadOnly,
		        PixelFormat.Format32bppArgb,
		        bitmapDataPng );

            unsafe
            {
	            uint* pixelsResult = (uint*)bitmapDataResult.Scan0;
	            uint* pixelsText = (uint*)bitmapDataText.Scan0;
	            uint* pixelsOutline = (uint*)bitmapDataOutline.Scan0;
	            uint* pixelsPng = (uint*)bitmapDataPng.Scan0;

	            if( pixelsResult==null || pixelsText==null || pixelsOutline==null || pixelsPng==null )
		            return null;

	            uint col = 0;
	            int strideResult = bitmapDataResult.Stride >> 2;
	            int strideOutline = bitmapDataOutline.Stride >> 2;
	            int strideText = bitmapDataText.Stride >> 2;
	            int stridePng = bitmapDataPng.Stride >> 2;

	            for(uint row = 0; row < bitmapDataResult.Height; ++row)
	            {
		            for(col = 0; col < bitmapDataResult.Width; ++col)
		            {
			            uint indexResult = (uint)(row * strideResult + col);
			            uint indexText = (uint)((row-nTextY)* strideText + (col-nTextX));
			            uint indexOutline = (uint)((row-nOutlineY) * strideOutline + (col-nOutlineX));
			            uint indexPng = (uint)(row * stridePng + col);
			            byte red = (byte)((pixelsPng[indexPng] & 0xff0000) >> 16);
			            byte blue = (byte)(pixelsPng[indexPng] & 0xff);

			            if(red>0&&blue>0)
			            {
				            uint nOutlineColor = pixelsOutline[indexOutline];
				            byte aOutline = (byte)((nOutlineColor & 0xff000000) >> 24);
				            byte rOutline = (byte)((nOutlineColor & 0xff0000) >> 16);
				            byte gOutline = (byte)((nOutlineColor & 0xff00) >> 8);
				            byte bOutline = (byte)(nOutlineColor & 0xff);

                            if (aOutline > 0)
                            {
                                aOutline = (byte)((red * aOutline) >> 8);
                                rOutline = (byte)((red * rOutline) >> 8);
                                gOutline = (byte)((red * gOutline) >> 8);
                                bOutline = (byte)((red * bOutline) >> 8);
                            }
                            else
                            {
                                rOutline = 0;
                                gOutline = 0;
                                bOutline = 0;
                            }

				            uint nTextColor = pixelsText[indexText];
				            byte aText = (byte)((nTextColor & 0xff000000) >> 24);
				            byte rText = (byte)((nTextColor & 0xff0000) >> 16);
				            byte gText = (byte)((nTextColor & 0xff00) >> 8);
				            byte bText = (byte)(nTextColor & 0xff);

                            if (aText > 0)
                            {
                                aText = (byte)((blue * aText) >> 8);
                                rText = (byte)((blue * rText) >> 8);
                                gText = (byte)((blue * gText) >> 8);
                                bText = (byte)((blue * bText) >> 8);
                            }
                            else
                            {
                                rText = 0;
                                gText = 0;
                                bText = 0;
                            }

                            if (aText > 0 && aOutline > 0)
                            {
                                pixelsResult[indexResult] = (uint)((0xff << 24) | (Clamp((uint)(rOutline + rText)) << 16) | (Clamp((uint)(gOutline + gText)) << 8) | Clamp((uint)(bOutline + bText)));
                            }
                            else if (aOutline > 0)
                                pixelsResult[indexResult] = (uint)((aOutline << 24) | (Clamp((uint)(rOutline + rText)) << 16) | (Clamp((uint)(gOutline + gText)) << 8) | Clamp((uint)(bOutline + bText)));
                            else if (aText > 0)
                                pixelsResult[indexResult] = (uint)((aText << 24) | (Clamp((uint)(rOutline + rText)) << 16) | (Clamp((uint)(gOutline + gText)) << 8) | Clamp((uint)(bOutline + bText)));
                            else
                                pixelsResult[indexResult] = 0;
			            }
			            else if(red>0)
			            {
				            uint nOutlineColor = pixelsOutline[indexOutline];
				            byte a = (byte)((nOutlineColor & 0xff000000) >> 24);
				            byte r = (byte)((nOutlineColor & 0xff0000) >> 16);
				            byte g = (byte)((nOutlineColor & 0xff00) >> 8);
				            byte b = (byte)(nOutlineColor & 0xff);

                            if (a > 0)
                                pixelsResult[indexResult] = (uint)((red << 24) | (r << 16) | (g << 8) | b);
                            else
                                pixelsResult[indexResult] = 0;
			            }
			            else if(blue>0)
			            {
				            uint nTextColor = pixelsText[indexText];
				            byte a = (byte)((nTextColor & 0xff000000) >> 24);
				            byte r = (byte)((nTextColor & 0xff0000) >> 16);
				            byte g = (byte)((nTextColor & 0xff00) >> 8);
				            byte b = (byte)(nTextColor & 0xff);

                            if (a > 0)
                                pixelsResult[indexResult] = (uint)((blue << 24) | (r << 16) | (g << 8) | b);
                            else
                                pixelsResult[indexResult] = 0;
			            }
			            else
			            {
				            pixelsResult[indexResult] = 0;
			            }
		            }
	            }
            }
	        png.UnlockBits(
		        bitmapDataPng );
	        pbmpOutline.UnlockBits(
		        bitmapDataOutline );
	        pbmpText.UnlockBits(
		        bitmapDataText );
	        m_pbmpResult.UnlockBits(
		        bitmapDataResult );

	        return m_pbmpResult;
        }

        public bool DrawString(
	        Graphics pGraphics, 
	        FontFamily pFontFamily,
	        FontStyle fontStyle,
	        int nfontSize,
	        string pszText, 
	        Point ptDraw, 
	        StringFormat pStrFormat,
	        int nThickness,
	        int nWidth,
	        int nHeight,
            bool bGlow,
            byte nGlowAlpha)
        {
            if (bGlow)
	            m_PngOutlineText.TextGlow(m_clrText, Color.FromArgb(nGlowAlpha, m_clrOutline.R, m_clrOutline.G, m_clrOutline.B), nThickness);
            else
                m_PngOutlineText.TextOutline(m_clrText, m_clrOutline, nThickness);
            m_PngOutlineText.EnableReflection(false);
	        m_PngOutlineText.EnableShadow(false);

	        m_pbmpMask = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

	        
	        Graphics graph = Graphics.FromImage(m_pbmpMask);
	        SolidBrush brush = new SolidBrush(m_clrBkgd);
	        graph.FillRectangle(brush, 0, 0, m_pbmpMask.Width, m_pbmpMask.Height);

	        m_PngOutlineText.SetPngImage(m_pbmpMask);
	
	        m_PngOutlineText.DrawString(
		        pGraphics, 
		        pFontFamily,
		        fontStyle,
		        nfontSize,
		        pszText, 
		        ptDraw, 
		        pStrFormat);

	        return true;

        }

        public bool DrawString(
	        Graphics pGraphics, 
	        FontFamily pFontFamily,
	        FontStyle fontStyle,
	        int nfontSize,
	        string pszText, 
	        Rectangle rtDraw,
	        StringFormat pStrFormat,
	        int nThickness,
	        int nWidth,
	        int nHeight,
            bool bGlow,
            byte nGlowAlpha)
        {
            if (bGlow)
                m_PngOutlineText.TextGlow(m_clrText, Color.FromArgb(nGlowAlpha, m_clrOutline.R, m_clrOutline.G, m_clrOutline.B), nThickness);
            else
                m_PngOutlineText.TextOutline(m_clrText, m_clrOutline, nThickness);
            m_PngOutlineText.EnableReflection(false);
	        m_PngOutlineText.EnableShadow(false);

	        m_pbmpMask = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

	        
	        Graphics graph = Graphics.FromImage(m_pbmpMask);
	        SolidBrush brush = new SolidBrush(m_clrBkgd);
	        graph.FillRectangle(brush, 0, 0, m_pbmpMask.Width, m_pbmpMask.Height);

	        m_PngOutlineText.SetPngImage(m_pbmpMask);

	        m_PngOutlineText.DrawString(
		        pGraphics, 
		        pFontFamily,
		        fontStyle,
		        nfontSize,
		        pszText, 
		        rtDraw,
		        pStrFormat);

	        return true;
        }

        public bool DrawString(
            Graphics pGraphics,
            FontFamily pFontFamily,
            FontStyle fontStyle,
            int nfontSize,
            string pszText,
            Point ptDraw,
            StringFormat pStrFormat,
            int nThickness,
            int nWidth,
            int nHeight,
            bool bGlow,
            byte nGlowAlpha,
            bool bShadow,
            Color clrShadow,
            int nShadowOffsetX,
            int nShadowOffsetY)
        {
            if (bGlow)
                m_PngOutlineText.TextGlow(m_clrText, Color.FromArgb(nGlowAlpha, m_clrOutline.R, m_clrOutline.G, m_clrOutline.B), nThickness);
            else
                m_PngOutlineText.TextOutline(m_clrText, m_clrOutline, nThickness);
            m_PngOutlineText.EnableReflection(false);
            m_PngOutlineText.EnableShadow(false);

           	if(bShadow)
	        {
		        m_PngShadow.SetNullTextEffect();
		        m_PngShadow.EnableShadow(true);
		        m_PngShadow.Shadow(clrShadow, nThickness,  new Point(nShadowOffsetX, nShadowOffsetY));
		        m_PngShadow.SetShadowBkgd(Color.FromArgb(0,0,0), nWidth, nHeight);
	        }
	        else
		        m_PngShadow.EnableShadow(false);

            m_pbmpMask = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);


            using (Graphics graph = Graphics.FromImage(m_pbmpMask))
            {
                using (SolidBrush brush = new SolidBrush(m_clrBkgd))
                {
                    graph.FillRectangle(brush, 0, 0, m_pbmpMask.Width, m_pbmpMask.Height);
                }
            }
            m_PngOutlineText.SetPngImage(m_pbmpMask);

            m_PngOutlineText.DrawString(
                pGraphics,
                pFontFamily,
                fontStyle,
                nfontSize,
                pszText,
                ptDraw,
                pStrFormat);

            if (bShadow)
            {
                m_pbmpShadow = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

                m_PngShadow.SetPngImage(m_pbmpShadow);

                m_PngShadow.DrawString(
                    pGraphics,
                    pFontFamily,
                    fontStyle,
                    nfontSize,
                    pszText,
                    ptDraw,
                    pStrFormat);
            }

            return true;
        }

        public bool DrawString(
            Graphics pGraphics,
            FontFamily pFontFamily,
            FontStyle fontStyle,
            int nfontSize,
            string pszText,
            Rectangle rtDraw,
            StringFormat pStrFormat,
            int nThickness,
            int nWidth,
            int nHeight,
            bool bGlow,
            byte nGlowAlpha,
            bool bShadow,
            Color clrShadow,
            int nShadowOffsetX,
            int nShadowOffsetY)
        {
            if (bGlow)
                m_PngOutlineText.TextGlow(m_clrText, Color.FromArgb(nGlowAlpha, m_clrOutline.R, m_clrOutline.G, m_clrOutline.B), nThickness);
            else
                m_PngOutlineText.TextOutline(m_clrText, m_clrOutline, nThickness);
            m_PngOutlineText.EnableReflection(false);
            m_PngOutlineText.EnableShadow(false);

            if (bShadow)
            {
                m_PngShadow.SetNullTextEffect();
                m_PngShadow.EnableShadow(true);
                m_PngShadow.Shadow(clrShadow, nThickness, new Point(nShadowOffsetX, nShadowOffsetY));
                m_PngShadow.SetShadowBkgd(Color.FromArgb(0, 0, 0), nWidth, nHeight);
            }
            else
                m_PngShadow.EnableShadow(false);

            m_pbmpMask = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

            using (Graphics graph = Graphics.FromImage(m_pbmpMask))
            {
                using (SolidBrush brush = new SolidBrush(m_clrBkgd))
                {
                    graph.FillRectangle(brush, 0, 0, m_pbmpMask.Width, m_pbmpMask.Height);
                }
            }
            m_PngOutlineText.SetPngImage(m_pbmpMask);

            m_PngOutlineText.DrawString(
                pGraphics,
                pFontFamily,
                fontStyle,
                nfontSize,
                pszText,
                rtDraw,
                pStrFormat);

           	if(bShadow)
	        {
		        m_pbmpShadow = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

		        m_PngShadow.SetPngImage(m_pbmpShadow);

		        m_PngShadow.DrawString(
			        pGraphics, 
			        pFontFamily,
			        fontStyle,
			        nfontSize,
			        pszText, 
			        rtDraw, 
			        pStrFormat);
	        }

            return true;
        }

        public static bool Measure(
	        Bitmap png, 
	        out uint nTextX, out uint nTextY, out uint nTextWidth, out uint nTextHeight,
	        out uint nOutlineX, out uint nOutlineY, out uint nOutlineWidth, out uint nOutlineHeight)
        {
            nTextX = nTextY = nTextWidth = nTextHeight = 0;
	        nOutlineX = nOutlineY = nOutlineWidth = nOutlineHeight = 0;
	        
	        BitmapData bitmapData = new BitmapData();
	        Rectangle rect = new Rectangle(0, 0, png.Width, png.Height );

	        png.LockBits(
		        rect,
		        ImageLockMode.ReadOnly,
		        PixelFormat.Format32bppArgb,
		        bitmapData );

            unsafe
            {
	            uint* pixels = (uint*)bitmapData.Scan0;

	            if( pixels==null )
		            return false;

	            uint col = 0;
	            int stride = bitmapData.Stride >> 2;
	            nTextX = 50000;
	            nTextY = 50000;
	            nTextWidth = 0;
	            nTextHeight = 0;

	            nOutlineX = 50000;
	            nOutlineY = 50000;
	            nOutlineWidth = 0;
	            nOutlineHeight = 0;
	            for(uint row = 0; row < bitmapData.Height; ++row)
	            {
                    uint total_row_len = (uint)(row * stride);
                    for (col = 0; col < bitmapData.Width; ++col)
		            {
			            uint index = total_row_len + col;
			            byte red = (byte)((pixels[index] & 0xff0000) >> 16);
			            byte blue = (byte)(pixels[index] & 0xff);

			            if(red>0)
			            {
				            if(col<nOutlineX)
					            nOutlineX = col;
				            if(row<nOutlineY)
					            nOutlineY = row;
				            if(col>nOutlineWidth)
					            nOutlineWidth = col;
				            if(row>nOutlineHeight)
					            nOutlineHeight = row;
			            }
			            if(blue>0)
			            {
				            if(col<nTextX)
					            nTextX = col;
				            if(row<nTextY)
					            nTextY = row;
				            if(col>nTextWidth)
					            nTextWidth = col;
				            if(row>nTextHeight)
					            nTextHeight = row;
			            }
		            }
	            }
            }
	        png.UnlockBits(bitmapData);

	        nTextWidth -= nTextX;
	        nTextHeight -= nTextY;

	        nOutlineWidth -= nOutlineX;
	        nOutlineHeight -= nOutlineY;

            ++nTextWidth;
            ++nTextHeight;

            ++nOutlineWidth;
            ++nOutlineHeight;

	        return true;
        }

        public Bitmap GetInternalMaskImage() 
        { 
            return m_pbmpMask; 
        }
        
        public Bitmap GetResultImage() 
        { 
            return m_pbmpResult; 
        }

        public Bitmap GetShadowImage()
        {
            return m_pbmpShadow;
        }
        
        private byte Clamp(uint val) 
	    { 
		    if(val>255) 
			    return 255; 
		    else 
			    return (byte)(val); 
	    }

	    private Bitmap m_pbmpResult;
	    private Bitmap m_pbmpMask;
        private Bitmap m_pbmpShadow;
        private Color m_clrBkgd;
	    private Color m_clrOutline;
	    private Color m_clrText;
	    private TextDesignerCSLibrary.PngOutlineText m_PngOutlineText;
        private TextDesignerCSLibrary.PngOutlineText m_PngShadow;

    }
}
