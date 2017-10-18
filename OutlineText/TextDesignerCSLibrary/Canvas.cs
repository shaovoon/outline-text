using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    /// <summary>
    /// Class to store the font info to pass as parameter to the drawing methods.
    /// </summary>
    public class TextContext
    {
        public TextContext()
        {
            fontFamily = new System.Drawing.FontFamily("Arial");
            fontStyle = System.Drawing.FontStyle.Regular;
            nfontSize = 20;
            pszText = null;
            ptDraw = new System.Drawing.Point(0, 0);
            strFormat = new System.Drawing.StringFormat();
        }

        /// <summary>
        /// fontFamily is the font family
        /// </summary>
        public System.Drawing.FontFamily fontFamily;
        /// <summary>
        /// fontStyle is the font style, eg, bold, italic or bold
        /// </summary>
        public System.Drawing.FontStyle fontStyle;
        /// <summary>
        /// nfontSize is font size
        /// </summary>
        public int nfontSize;
        /// <summary>
        /// pszText is the text to be displayed
        /// </summary>
        public string pszText;
        /// <summary>
        /// ptDraw is the point to draw
        /// </summary>
        public System.Drawing.Point ptDraw;
        /// <summary>
        /// strFormat is the string format
        /// </summary>
        public System.Drawing.StringFormat strFormat;
    }

    /// <summary>
    /// Class to draw text outlines
    /// </summary>
    public class Canvas
    {
        /// <summary>
        /// Generate Text Glow strategy
        /// </summary>
        /// <param name="clrText">is the color of the text</param>
        /// <param name="clrOutline">is the color of the glow outline</param>
        /// <param name="nThickness">is the thickness of the outline in pixels</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextGlow(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            TextGlowStrategy strat = new TextGlowStrategy();
            strat.Init(clrText, clrOutline, nThickness);

            return strat;
        }

        /// <summary>
        /// Generate Text Glow strategy
        /// </summary>
        /// <param name="brushText">is the brush of the text</param>
        /// <param name="clrOutline">is the color of the glow outline</param>
        /// <param name="nThickness">is the thickness of the outline in pixels</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextGlow(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            TextGlowStrategy strat = new TextGlowStrategy();
            strat.Init(brushText, clrOutline, nThickness);

            return strat;
        }

        /// <summary>
        /// Generate Text Outline strategy
        /// </summary>
        /// <param name="clrText">is the color of the text</param>
        /// <param name="clrOutline">is the color of the outline</param>
        /// <param name="nThickness">is the thickness of the outline in pixels</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextOutline(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            TextOutlineStrategy strat = new TextOutlineStrategy();
            strat.Init(clrText, clrOutline, nThickness);

            return strat;
        }

        /// <summary>
        /// Generate Text Outline strategy
        /// </summary>
        /// <param name="brushText">is the brush of the text</param>
        /// <param name="clrOutline">is the color of the outline</param>
        /// <param name="nThickness">is the thickness of the outline in pixels</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextOutline(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline,
            int nThickness)
        {
            TextOutlineStrategy strat = new TextOutlineStrategy();
            strat.Init(brushText, clrOutline, nThickness);

            return strat;
        }

        /// <summary>
        /// Setting Gradient Outlined Text effect
        /// </summary>
        /// <param name="clrText">is the text color</param>
        /// <param name="clrOutline1">is the inner outline color</param>
        /// <param name="clrOutline2">is the outer outline color</param>
        /// <param name="nThickness">is the outline thickness</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextGradOutline(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness)
        {
            TextGradOutlineStrategy strat = new TextGradOutlineStrategy();
            strat.Init(clrText, clrOutline1, clrOutline2, nThickness);

            return strat;
        }

        /// <summary>
        /// Setting Gradient Outlined Text effect
        /// </summary>
        /// <param name="brushText">is the text brush</param>
        /// <param name="clrOutline1">is the inner outline color</param>
        /// <param name="clrOutline2">is the outer outline color</param>
        /// <param name="nThickness">is the outline thickness</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextGradOutline(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness)
        {
            TextGradOutlineStrategy strat = new TextGradOutlineStrategy();
            strat.Init(brushText, clrOutline1, clrOutline2, nThickness);

            return strat;
        }

        /// <summary>
        /// Setting Gradient Outlined Text effect: Outline will be done after rendering text body
        /// </summary>
        /// <param name="clrText">is the text color</param>
        /// <param name="clrOutline1">is the inner outline color</param>
        /// <param name="clrOutline2">is the outer outline color</param>
        /// <param name="nThickness">is the outline thickness</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextGradOutlineLast(
            System.Drawing.Color clrText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            bool useCurveGradient)
        {
            TextGradOutlineLastStrategy strat = new TextGradOutlineLastStrategy();
            strat.Init(clrText, clrOutline1, clrOutline2, nThickness, useCurveGradient);

            return strat;
        }

        /// <summary>
        /// Setting Gradient Outlined Text effect: Outline will be done after rendering text body
        /// </summary>
        /// <param name="brushText">is the text brush</param>
        /// <param name="clrOutline1">is the inner outline color</param>
        /// <param name="clrOutline2">is the outer outline color</param>
        /// <param name="nThickness">is the outline thickness</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextGradOutlineLast(
            System.Drawing.Brush brushText,
            System.Drawing.Color clrOutline1,
            System.Drawing.Color clrOutline2,
            int nThickness,
            bool useCurveGradient)
        {
            TextGradOutlineLastStrategy strat = new TextGradOutlineLastStrategy();
            strat.Init(brushText, clrOutline1, clrOutline2, nThickness, useCurveGradient);

            return strat;
        }

        /// <summary>
        /// Setting just Text effect with no outline
        /// </summary>
        /// <param name="clrText">is the text color</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextNoOutline(
            System.Drawing.Color clrText)
        {
            TextNoOutlineStrategy strat = new TextNoOutlineStrategy();
            strat.Init(clrText);

            return strat;
        }

        /// <summary>
        /// Setting just Text effect with no outline
        /// </summary>
        /// <param name="brushText">is the text brush</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextNoOutline(
            System.Drawing.Brush brushText)
        {
            TextNoOutlineStrategy strat = new TextNoOutlineStrategy();
            strat.Init(brushText);

            return strat;
        }

        /// <summary>
        /// Setting Outlined Text effect with no text fill
        /// </summary>
        /// <param name="clrOutline">is the outline color</param>
        /// <param name="nThickness">is the outline thickness</param>
        /// <param name="bRoundedEdge">specifies rounded or sharp edges</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextOnlyOutline(
            System.Drawing.Color clrOutline,
            int nThickness,
            bool bRoundedEdge)
        {
            TextOnlyOutlineStrategy strat = new TextOnlyOutlineStrategy();
            strat.Init(clrOutline, nThickness, bRoundedEdge);

            return strat;
        }

        /// <summary>
        /// Generate a canvas image based on width and height
        /// </summary>
        /// <param name="width">is the image width</param>
        /// <param name="height">is the image height</param>
        /// <returns>a valid canvas image if successful</returns>
        public static System.Drawing.Bitmap GenImage(int width, int height)
        {
            return new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Generate a canvas image based on width and height
        /// </summary>
        /// <param name="width">is the image width</param>
        /// <param name="height">is the image height</param>
        /// <param name="colors">is the list of colors for the gradient</param>
        /// <param name="horizontal">specifies whether the gradient is horizontal</param>
        /// <returns>a valid canvas image if successful</returns>
        public static System.Drawing.Bitmap GenImage(int width, int height, List<System.Drawing.Color> colors, bool horizontal)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);

            DrawGradient.Draw(bmp, colors, horizontal);

            return bmp;
        }

        /// <summary>
        /// Generate a canvas image based on width and height
        /// </summary>
        /// <param name="width">is the image width</param>
        /// <param name="height">is the image height</param>
        /// <param name="clr">is the color to paint the image</param>
        /// <returns>a valid canvas image if successful</returns>
        public static System.Drawing.Bitmap GenImage(int width, int height, System.Drawing.Color clr)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData bitmapData = new BitmapData();
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            bmp.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapData);

            unsafe
            {
                uint* pixels = (uint*)bitmapData.Scan0;

                if (pixels == null)
                    return null;

                uint col = 0;
                int stride = bitmapData.Stride >> 2;
                uint color = (uint)(clr.A << 24 | clr.R << 16 | clr.G << 8 | clr.G);
                for (uint row = 0; row < bitmapData.Height; ++row)
                {
                    for (col = 0; col < bitmapData.Width; ++col)
                    {
                        uint index = (uint)(row * stride + col);

                        pixels[index] = color;
                    }
                }
            }
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        /// <summary>
        /// Generate a canvas image based on width and height
        /// </summary>
        /// <param name="width">is the image width</param>
        /// <param name="height">is the image height</param>
        /// <param name="clr">is the color to paint the image</param>
        /// <param name="alpha">is alpha of the color</param>
        /// <returns>a valid canvas image if successful</returns>
        public static System.Drawing.Bitmap GenImage(int width, int height, System.Drawing.Color clr, byte alpha)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData bitmapData = new BitmapData();
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            bmp.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapData);

            unsafe
            {
                uint* pixels = (uint*)bitmapData.Scan0;

                if (pixels == null)
                    return null;

                uint col = 0;
                int stride = bitmapData.Stride >> 2;
                uint color = (uint)(alpha << 24 | clr.R << 16 | clr.G << 8 | clr.G);
                for (uint row = 0; row < bitmapData.Height; ++row)
                {
                    for (col = 0; col < bitmapData.Width; ++col)
                    {
                        uint index = (uint)(row * stride + col);

                        pixels[index] = color;
                    }
                }
            }
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
        public static System.Drawing.Bitmap GenImage(int width, int height, System.Drawing.Drawing2D.LinearGradientBrush brush, byte alpha)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graph = Graphics.FromImage(bmp);
            graph.FillRectangle(brush, new Rectangle(0, 0, width, height));

            BitmapData bitmapData = new BitmapData();
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            bmp.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapData);

            unsafe
            {
                uint* pixels = (uint*)bitmapData.Scan0;

                if (pixels == null)
                    return null;

                uint col = 0;
                int stride = bitmapData.Stride >> 2;
                for (uint row = 0; row < bitmapData.Height; ++row)
                {
                    for (col = 0; col < bitmapData.Width; ++col)
                    {
                        uint index = (uint)(row * stride + col);

                        uint color = pixels[index] & 0xFFFFFF;

                        pixels[index] = color;
                    }
                }
            }
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        /// <summary>
        /// Generate mask image of the text strategy.
        /// </summary>
        /// <param name="strategy">is text strategy</param>
        /// <param name="width">is the mask image width</param>
        /// <param name="height">is the mask image height</param>
        /// <param name="offset">offsets the text (typically used for shadows)</param>
        /// <param name="textContext">is text context</param>
        /// <returns>a valid mask image if successful</returns>
        public static System.Drawing.Bitmap GenMask(
            ITextStrategy strategy,
            int width,
            int height,
            System.Drawing.Point offset,
            TextContext textContext)
        {
            if (strategy == null || textContext == null)
                return null;

            System.Drawing.Bitmap pBmp = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(pBmp))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                strategy.DrawString(graphics,
                    textContext.fontFamily,
                    textContext.fontStyle,
                    textContext.nfontSize,
                    textContext.pszText,
                    new System.Drawing.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                    textContext.strFormat);
            }

            return pBmp;
        }

        public static System.Drawing.Bitmap GenMask(
            ITextStrategy strategy,
            int width,
            int height,
            System.Drawing.Point offset,
            TextContext textContext, 
            Matrix mat)
        {
            if (strategy == null || textContext == null)
                return null;

            System.Drawing.Bitmap pBmp = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(pBmp))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.Transform = mat;

                strategy.DrawString(graphics,
                    textContext.fontFamily,
                    textContext.fontStyle,
                    textContext.nfontSize,
                    textContext.pszText,
                    new System.Drawing.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                    textContext.strFormat);

                graphics.ResetTransform();
            }

            return pBmp;
        }

        /// <summary>
        /// Measure the mask image based on the mask color.
        /// </summary>
        /// <param name="mask">is the mask image to be measured</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <param name="top">returns the topmost Y </param>
        /// <param name="left">returns the leftmost X</param>
        /// <param name="bottom">returns the bottommost Y</param>
        /// <param name="right">returns the rightmost X</param>
        /// <returns>true if successful</returns>
        public static bool MeasureMaskLength(
            System.Drawing.Bitmap mask,
            System.Drawing.Color maskColor,
            ref uint top,
            ref uint left,
            ref uint bottom,
            ref uint right)
        {
            top = 30000;
            left = 30000;
            bottom = 0;
            right = 0;

            if (mask == null)
                return false;

            BitmapData bitmapDataMask = new BitmapData();
            Rectangle rect = new Rectangle(0, 0, mask.Width, mask.Height);

            mask.LockBits(
                rect,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataMask);

            unsafe
            {
                uint* pixelsMask = (uint*)bitmapDataMask.Scan0;

                if (pixelsMask == null)
                    return false;

                uint col = 0;
                int stride = bitmapDataMask.Stride >> 2;
                for (uint row = 0; row < bitmapDataMask.Height; ++row)
                {
                    for (col = 0; col < bitmapDataMask.Width; ++col)
                    {
                        uint index = (uint)(row * stride + col);
                        byte nAlpha = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            nAlpha = (Byte)((pixelsMask[index] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            nAlpha = (Byte)((pixelsMask[index] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            nAlpha = (Byte)(pixelsMask[index] & 0xff);

                        if (nAlpha > 0)
                        {
                            if (col < left)
                                left = col;
                            if (row < top)
                                top = row;
                            if (col > right)
                                right = col;
                            if (row > bottom)
                                bottom = row;

                        }
                    }
                }
            }
            mask.UnlockBits(bitmapDataMask);

            return true;

        }
        /// <summary>
        /// Apply image to mask onto the canvas
        /// </summary>
        /// <param name="image">is the image to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <returns>true if successful</returns>
        public static bool ApplyImageToMask(
            System.Drawing.Bitmap image,
            System.Drawing.Bitmap mask,
            System.Drawing.Bitmap canvas,
            System.Drawing.Color maskColor,
            bool NoAlphaAtBoundary)
        {
            if (image == null || mask == null || canvas == null)
                return false;

            BitmapData bitmapDataImage = new BitmapData();
            BitmapData bitmapDataMask = new BitmapData();
            BitmapData bitmapDataCanvas = new BitmapData();
            Rectangle rectCanvas = new Rectangle(0, 0, canvas.Width, canvas.Height);
            Rectangle rectMask = new Rectangle(0, 0, mask.Width, mask.Height);
            Rectangle rectImage = new Rectangle(0, 0, image.Width, image.Height);

            image.LockBits(
                rectImage,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataImage);

            mask.LockBits(
                rectMask,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataMask);

            canvas.LockBits(
                rectCanvas,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataCanvas);

            unsafe
            {
                uint* pixelsImage = (uint*)bitmapDataImage.Scan0;
                uint* pixelsMask = (uint*)bitmapDataMask.Scan0;
                uint* pixelsCanvas = (uint*)bitmapDataCanvas.Scan0;

                if (pixelsImage == null || pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = bitmapDataCanvas.Stride >> 2;
                for (uint row = 0; row < bitmapDataCanvas.Height; ++row)
                {
                    for (col = 0; col < bitmapDataCanvas.Width; ++col)
                    {
                        if (row >= bitmapDataImage.Height || col >= bitmapDataImage.Width)
                            continue;
                        if (row >= bitmapDataMask.Height || col >= bitmapDataMask.Width)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)(row * (bitmapDataMask.Stride >> 2) + col);
                        uint indexImage = (uint)(row * (bitmapDataImage.Stride >> 2) + col);

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        if (maskByte > 0)
                        {
                            if (NoAlphaAtBoundary)
                            {
                                pixelsCanvas[index] = AlphablendNoAlphaAtBoundary(pixelsCanvas[index], pixelsImage[indexImage], (Byte)(pixelsMask[indexMask] >> 24), (Byte)(pixelsMask[indexMask] >> 24));
                            }
                            else
                            {
                                pixelsCanvas[index] = Alphablend(pixelsCanvas[index], pixelsImage[indexImage], (Byte)(pixelsMask[indexMask] >> 24), (Byte)(pixelsMask[indexMask] >> 24));
                            }
                        }
                    }
                }
            }
            canvas.UnlockBits(bitmapDataCanvas);
            mask.UnlockBits(bitmapDataMask);
            image.UnlockBits(bitmapDataImage);

            return true;
        }

        /// <summary>
        /// Apply color to mask onto the canvas
        /// </summary>
        /// <param name="clr">is the color to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <returns>true if successful</returns>
        public static bool ApplyColorToMask(
            System.Drawing.Color clr,
            System.Drawing.Bitmap mask,
            System.Drawing.Bitmap canvas,
            System.Drawing.Color maskColor)
        {
            if (mask == null || canvas == null)
                return false;

            BitmapData bitmapDataMask = new BitmapData();
            BitmapData bitmapDataCanvas = new BitmapData();
            Rectangle rectCanvas = new Rectangle(0, 0, canvas.Width, canvas.Height);
            Rectangle rectMask = new Rectangle(0, 0, mask.Width, mask.Height);

            mask.LockBits(
                rectMask,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataMask);

            canvas.LockBits(
                rectCanvas,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataCanvas);

            unsafe
            {
                uint* pixelsMask = (uint*)bitmapDataMask.Scan0;
                uint* pixelsCanvas = (uint*)bitmapDataCanvas.Scan0;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = bitmapDataCanvas.Stride >> 2;
                for (uint row = 0; row < bitmapDataCanvas.Height; ++row)
                {
                    for (col = 0; col < bitmapDataCanvas.Width; ++col)
                    {
                        if (row >= bitmapDataMask.Height || col >= bitmapDataMask.Width)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)(row * (bitmapDataMask.Stride >> 2) + col);

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        uint color = (uint)(0xff << 24 | clr.R << 16 | clr.G << 8 | clr.B);

                        if (maskByte > 0)
                            pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, (Byte)(pixelsMask[indexMask] >> 24), (Byte)(pixelsMask[indexMask] >> 24));
                    }
                }
            }
            canvas.UnlockBits(bitmapDataCanvas);
            mask.UnlockBits(bitmapDataMask);

            return true;
        }

        /// <summary>
        /// Apply color to mask onto the canvas
        /// </summary>
        /// <param name="clr">is the color to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <param name="offset">determine how much to offset the mask</param>
        /// <returns>true if successful</returns>
        public static bool ApplyColorToMask(
            System.Drawing.Color clr,
            System.Drawing.Bitmap mask,
            System.Drawing.Bitmap canvas,
            System.Drawing.Color maskColor,
            System.Drawing.Point offset)
        {
            if (mask == null || canvas == null)
                return false;

            BitmapData bitmapDataMask = new BitmapData();
            BitmapData bitmapDataCanvas = new BitmapData();
            Rectangle rectCanvas = new Rectangle(0, 0, canvas.Width, canvas.Height);
            Rectangle rectMask = new Rectangle(0, 0, mask.Width, mask.Height);

            mask.LockBits(
                rectMask,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataMask);

            canvas.LockBits(
                rectCanvas,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataCanvas);

            unsafe
            {
                uint* pixelsMask = (uint*)bitmapDataMask.Scan0;
                uint* pixelsCanvas = (uint*)bitmapDataCanvas.Scan0;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = bitmapDataCanvas.Stride >> 2;
                for (uint row = 0; row < bitmapDataCanvas.Height; ++row)
                {
                    for (col = 0; col < bitmapDataCanvas.Width; ++col)
                    {
                        if ((row - offset.Y) >= bitmapDataMask.Height || (col - offset.X) >= bitmapDataMask.Width ||
                            (row - offset.Y) < 0 || (col - offset.X) < 0)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)((row - offset.Y) * (bitmapDataMask.Stride >> 2) + (col - offset.X));

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        uint color = (uint)(0xff << 24 | clr.R << 16 | clr.G << 8 | clr.B);

                        if (maskByte > 0)
                            pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, (Byte)(pixelsMask[indexMask] >> 24), (Byte)(pixelsMask[indexMask] >> 24));
                    }
                }
            }
            canvas.UnlockBits(bitmapDataCanvas);
            mask.UnlockBits(bitmapDataMask);

            return true;
        }

        /// <summary>
        /// Apply shadow to mask onto the canvas
        /// </summary>
        /// <param name="clr">is the shadow color to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <returns>true if successful</returns>
        public static bool ApplyShadowToMask(
            System.Drawing.Color clrShadow,
            System.Drawing.Bitmap mask,
            System.Drawing.Bitmap canvas,
            System.Drawing.Color maskColor)
        {
            if (mask == null || canvas == null)
                return false;

            BitmapData bitmapDataMask = new BitmapData();
            BitmapData bitmapDataCanvas = new BitmapData();
            Rectangle rectCanvas = new Rectangle(0, 0, canvas.Width, canvas.Height);
            Rectangle rectMask = new Rectangle(0, 0, mask.Width, mask.Height);

            mask.LockBits(
                rectMask,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataMask);

            canvas.LockBits(
                rectCanvas,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataCanvas);

            unsafe
            {
                uint* pixelsMask = (uint*)bitmapDataMask.Scan0;
                uint* pixelsCanvas = (uint*)bitmapDataCanvas.Scan0;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = bitmapDataCanvas.Stride >> 2;
                for (uint row = 0; row < bitmapDataCanvas.Height; ++row)
                {
                    for (col = 0; col < bitmapDataCanvas.Width; ++col)
                    {
                        if (row >= bitmapDataMask.Height || col >= bitmapDataMask.Width)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)(row * (bitmapDataMask.Stride >> 2) + col);

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        uint color = (uint)(0xff << 24 | clrShadow.R << 16 | clrShadow.G << 8 | clrShadow.B);

                        if (maskByte > 0)
                        {
                            uint maskAlpha = (pixelsMask[indexMask] >> 24);
                            pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, (Byte)(maskAlpha), clrShadow.A);
                        }
                    }
                }
            }
            canvas.UnlockBits(bitmapDataCanvas);
            mask.UnlockBits(bitmapDataMask);

            return true;
        }

        /// <summary>
        /// Apply shadow to mask onto the canvas
        /// </summary>
        /// <param name="clr">is the shadow color to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <param name="offset">determine how much to offset the mask</param>
        /// <returns>true if successful</returns>
        public static bool ApplyShadowToMask(
            System.Drawing.Color clrShadow,
            System.Drawing.Bitmap mask,
            System.Drawing.Bitmap canvas,
            System.Drawing.Color maskColor,
            System.Drawing.Point offset)
        {
            if (mask == null || canvas == null)
                return false;

            BitmapData bitmapDataMask = new BitmapData();
            BitmapData bitmapDataCanvas = new BitmapData();
            Rectangle rectCanvas = new Rectangle(0, 0, canvas.Width, canvas.Height);
            Rectangle rectMask = new Rectangle(0, 0, mask.Width, mask.Height);

            mask.LockBits(
                rectMask,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataMask);

            canvas.LockBits(
                rectCanvas,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb,
                bitmapDataCanvas);

            unsafe
            {
                uint* pixelsMask = (uint*)bitmapDataMask.Scan0;
                uint* pixelsCanvas = (uint*)bitmapDataCanvas.Scan0;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = bitmapDataCanvas.Stride >> 2;
                for (uint row = 0; row < bitmapDataCanvas.Height; ++row)
                {
                    for (col = 0; col < bitmapDataCanvas.Width; ++col)
                    {
                        if ((row - offset.Y) >= bitmapDataMask.Height || (col - offset.X) >= bitmapDataMask.Width ||
                            (row - offset.Y) < 0 || (col - offset.X) < 0)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)((row - offset.Y) * (bitmapDataMask.Stride >> 2) + (col - offset.X));

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        uint color = (uint)(0xff << 24 | clrShadow.R << 16 | clrShadow.G << 8 | clrShadow.B);

                        if (maskByte > 0)
                        {
                            uint maskAlpha = (pixelsMask[indexMask] >> 24);
                            pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, (Byte)(maskAlpha), clrShadow.A);
                        }
                    }
                }
            }
            canvas.UnlockBits(bitmapDataCanvas);
            mask.UnlockBits(bitmapDataMask);

            return true;
        }

        /// <summary>
        /// Draw outline on image
        /// </summary>
        /// <param name="strategy">is text strategy</param>
        /// <param name="image">is the image to be draw upon</param>
        /// <param name="offset">offsets the text (typically used for shadows)</param>
        /// <param name="textContext">is text context</param>
        /// <returns>true if successful</returns>
        public static bool DrawTextImage(
            ITextStrategy strategy,
            System.Drawing.Bitmap image,
            System.Drawing.Point offset,
            TextContext textContext)
        {
            if (strategy == null || image == null || textContext == null)
                return false;

            bool bRet = false;
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                bRet = strategy.DrawString(graphics,
                    textContext.fontFamily,
                    textContext.fontStyle,
                    textContext.nfontSize,
                    textContext.pszText,
                    new System.Drawing.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                    textContext.strFormat);
            }

            return bRet;

        }

        public static bool DrawTextImage(
            ITextStrategy strategy,
            System.Drawing.Bitmap image,
            System.Drawing.Point offset,
            TextContext textContext, 
            Matrix mat)
        {
            if (strategy == null || image == null || textContext == null)
                return false;

            bool bRet = false;
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.Transform = mat;

                bRet = strategy.DrawString(graphics,
                    textContext.fontFamily,
                    textContext.fontStyle,
                    textContext.nfontSize,
                    textContext.pszText,
                    new System.Drawing.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                    textContext.strFormat);

                graphics.ResetTransform();
            }

            return bRet;

        }

        /// <summary>
        /// Set alpha to color
        /// </summary>
        /// <param name="dest">is destination color in ARGB</param>
        /// <param name="source">is source color in ARGB</param>
        /// <param name="nAlpha">is nAlpha is alpha channel</param>
        /// <returns></returns>
        private static UInt32 AddAlpha(UInt32 dest, UInt32 source, Byte nAlpha)
        {
            if (0 == nAlpha)
                return dest;

            if (255 == nAlpha)
                return source;

            Byte nSrcRed = (Byte)((source & 0xff0000) >> 16);
            Byte nSrcGreen = (Byte)((source & 0xff00) >> 8);
            Byte nSrcBlue = (Byte)((source & 0xff));

            Byte nRed = (Byte)(nSrcRed);
            Byte nGreen = (Byte)(nSrcGreen);
            Byte nBlue = (Byte)(nSrcBlue);

            return (UInt32)(nAlpha << 24 | nRed << 16 | nGreen << 8 | nBlue);
        }

        /// <summary>
        /// Performs Alphablend
        /// </summary>
        /// <param name="dest">is destination color in ARGB</param>
        /// <param name="source">is source color in ARGB</param>
        /// <param name="nAlpha">is nAlpha is alpha channel</param>
        /// <param name="nAlphaFinal">sets alpha channel of the returned UInt32</param>
        /// <returns>destination color</returns>
        private static UInt32 AlphablendNoAlphaAtBoundary(UInt32 dest, UInt32 source, Byte nAlpha, Byte nAlphaFinal)
        {
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

            return (UInt32)(0xff << 24 | nRed << 16 | nGreen << 8 | nBlue);
        }

        /// <summary>
        /// Performs Alphablend
        /// </summary>
        /// <param name="dest">is destination color in ARGB</param>
        /// <param name="source">is source color in ARGB</param>
        /// <param name="nAlpha">is nAlpha is alpha channel</param>
        /// <param name="nAlphaFinal">sets alpha channel of the returned UInt32</param>
        /// <returns>destination color</returns>
        private static UInt32 Alphablend(UInt32 dest, UInt32 source, Byte nAlpha, Byte nAlphaFinal)
        {
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

            return (UInt32)(nAlphaFinal << 24 | nRed << 16 | nGreen << 8 | nBlue);
        }
        /// <summary>
        /// Performs Alphablend
        /// </summary>
        /// <param name="dest">is destination color in ARGB</param>
        /// <param name="source">is source color in ARGB</param>
        /// <returns>destination color</returns>
        private static UInt32 PreMultipliedAlphablend(UInt32 dest, UInt32 source)
        {
            Byte Alpha = (Byte)((source & 0xff000000) >> 24);
            Byte nInvAlpha = (Byte)(255 - Alpha);

            Byte nSrcRed = (Byte)((source & 0xff0000) >> 16);
            Byte nSrcGreen = (Byte)((source & 0xff00) >> 8);
            Byte nSrcBlue = (Byte)((source & 0xff));

            Byte nDestRed = (Byte)((dest & 0xff0000) >> 16);
            Byte nDestGreen = (Byte)((dest & 0xff00) >> 8);
            Byte nDestBlue = (Byte)(dest & 0xff);

            Byte nRed = (Byte)(nSrcRed + ((nDestRed * nInvAlpha) >> 8));
            Byte nGreen = (Byte)(nSrcGreen + ((nDestGreen * nInvAlpha) >> 8));
            Byte nBlue = (Byte)(nSrcBlue + ((nDestBlue * nInvAlpha) >> 8));

            return (UInt32)(0xff << 24 | nRed << 16 | nGreen << 8 | nBlue);
        }

    }
}
