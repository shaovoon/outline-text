using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.IO;

namespace TextDesignerWpf
{
    /// <summary>
    /// Class to store the font info to pass as parameter to the drawing methods.
    /// </summary>
    public class TextContext
    {
        public TextContext()
        {
            fontFamily = new System.Windows.Media.FontFamily("Arial");
            fontStyle = new System.Windows.FontStyle();
            nfontSize = 20;
            pszText = null;
            ptDraw = new System.Windows.Point(0, 0);
            ci = new System.Globalization.CultureInfo("en-US");
        }

        /// <summary>
        /// fontFamily is the font family
        /// </summary>
        public System.Windows.Media.FontFamily fontFamily;
        /// <summary>
        /// fontStyle is the font style, eg, bold, italic or bold
        /// </summary>
        public System.Windows.FontStyle fontStyle;
        /// <summary>
        /// Font weight
        /// </summary>
        public System.Windows.FontWeight fontWeight;
        /// <summary>
        /// nfontSize is font size
        /// </summary>
        public double nfontSize;
        /// <summary>
        /// pszText is the text to be displayed
        /// </summary>
        public string pszText;
        /// <summary>
        /// ptDraw is the point to draw
        /// </summary>
        public System.Windows.Point ptDraw;
        /// <summary>
        /// strFormat is the string format
        /// </summary>
        public System.Globalization.CultureInfo ci;
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
            System.Windows.Media.Color clrText,
            System.Windows.Media.Color clrOutline,
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
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline,
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
            System.Windows.Media.Color clrText,
            System.Windows.Media.Color clrOutline,
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
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline,
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
            System.Windows.Media.Color clrText,
            System.Windows.Media.Color clrOutline1,
            System.Windows.Media.Color clrOutline2,
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
            System.Windows.Media.Brush brushText,
            System.Windows.Media.Color clrOutline1,
            System.Windows.Media.Color clrOutline2,
            int nThickness)
        {
            TextGradOutlineStrategy strat = new TextGradOutlineStrategy();
            strat.Init(brushText, clrOutline1, clrOutline2, nThickness);

            return strat;
        }

        /// <summary>
        /// Setting just Text effect with no outline
        /// </summary>
        /// <param name="clrText">is the text color</param>
        /// <returns>valid ITextStrategy pointer if successful</returns>
        public static ITextStrategy TextNoOutline(
            System.Windows.Media.Color clrText)
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
            System.Windows.Media.Brush brushText)
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
            System.Windows.Media.Color clrOutline,
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
        public static System.Windows.Media.Imaging.WriteableBitmap GenImage(int width, int height)
        {
            return new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32, null);
        }

        /// <summary>
        /// Generate a canvas image based on width and height
        /// </summary>
        /// <param name="width">is the image width</param>
        /// <param name="height">is the image height</param>
        /// <param name="colors">is the list of colors for the gradient</param>
        /// <param name="horizontal">specifies whether the gradient is horizontal</param>
        /// <returns>a valid canvas image if successful</returns>
        public static System.Windows.Media.Imaging.WriteableBitmap GenImage(int width, int height, List<System.Windows.Media.Color> colors, bool horizontal)
        {
            WriteableBitmap bmp = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32, null);

            DrawGradient.Draw(ref bmp, colors, horizontal);

            return bmp;
        }

        /// <summary>
        /// Generate a canvas image based on width and height
        /// </summary>
        /// <param name="width">is the image width</param>
        /// <param name="height">is the image height</param>
        /// <param name="clr">is the color to paint the image</param>
        /// <returns>a valid canvas image if successful</returns>
        public static System.Windows.Media.Imaging.WriteableBitmap GenImage(int width, int height, System.Windows.Media.Color clr)
        {
            WriteableBitmap bmp = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32, null);

            if (bmp == null)
                return null;

            bmp.Lock();

            unsafe
            {
                uint* pixels = (uint*)bmp.BackBuffer;

                if (pixels == null)
                    return null;

                uint col = 0;
                int stride = bmp.BackBufferStride >> 2;
                uint color = (uint)(clr.A << 24 | clr.R << 16 | clr.G << 8 | clr.B);
                for (uint row = 0; row < bmp.Height; ++row)
                {
                    for (col = 0; col < bmp.Width; ++col)
                    {
                        uint index = (uint)(row * stride + col);

                        pixels[index] = color;
                    }
                }
            }
            bmp.Unlock();

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
        public static System.Windows.Media.Imaging.WriteableBitmap GenImage(int width, int height, System.Windows.Media.Color clr, byte alpha)
        {
            WriteableBitmap bmp = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32, null);

            if (bmp == null)
                return null;

            bmp.Lock();

            unsafe
            {
                uint* pixels = (uint*)bmp.BackBuffer;

                if (pixels == null)
                    return null;

                uint col = 0;
                int stride = bmp.BackBufferStride >> 2;
                uint color = (uint)(alpha << 24 | clr.R << 16 | clr.G << 8 | clr.B);
                for (uint row = 0; row < bmp.Height; ++row)
                {
                    for (col = 0; col < bmp.Width; ++col)
                    {
                        uint index = (uint)(row * stride + col);

                        pixels[index] = color;
                    }
                }
            }
            bmp.Unlock();

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
        public static System.Windows.Media.Imaging.WriteableBitmap GenMask(
            ITextStrategy strategy,
            int width,
            int height,
            System.Windows.Point offset,
            TextContext textContext)
        {
            if (strategy == null || textContext == null)
                return null;

            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();

            DrawingContext drawingContext = drawingVisual.RenderOpen();

            strategy.DrawString(drawingContext,
                textContext.fontFamily,
                textContext.fontStyle,
                textContext.fontWeight,
                textContext.nfontSize,
                textContext.pszText,
                new System.Windows.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                textContext.ci);

            drawingContext.Close();

            bmp.Render(drawingVisual);

            return new System.Windows.Media.Imaging.WriteableBitmap(bmp);
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
        public static System.Windows.Media.Imaging.WriteableBitmap GenMask(
            ITextStrategy strategy,
            int width,
            int height,
            System.Windows.Point offset,
            TextContext textContext,
            System.Windows.Media.Matrix mat)
        {
            if (strategy == null || textContext == null)
                return null;

            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();

            DrawingContext drawingContext = drawingVisual.RenderOpen();

            MatrixTransform mat_trans = new MatrixTransform();
            mat_trans.Matrix = mat;

            drawingContext.PushTransform(mat_trans);

            strategy.DrawString(drawingContext,
                textContext.fontFamily,
                textContext.fontStyle,
                textContext.fontWeight,
                textContext.nfontSize,
                textContext.pszText,
                new System.Windows.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                textContext.ci);

            drawingContext.Pop();

            drawingContext.Close();

            bmp.Render(drawingVisual);

            return new System.Windows.Media.Imaging.WriteableBitmap(bmp);
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
            System.Windows.Media.Imaging.WriteableBitmap mask,
            System.Windows.Media.Color maskColor,
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

            mask.Lock();

            unsafe
            {
                uint* pixelsMask = (uint*)mask.BackBuffer;

                if (pixelsMask == null)
                    return false;

                uint col = 0;
                int stride = mask.BackBufferStride >> 2;
                for (uint row = 0; row < mask.Height; ++row)
                {
                    for (col = 0; col < mask.Width; ++col)
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
            mask.Unlock();

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
            System.Windows.Media.Imaging.WriteableBitmap image,
            System.Windows.Media.Imaging.WriteableBitmap mask,
            System.Windows.Media.Imaging.WriteableBitmap canvas,
            System.Windows.Media.Color maskColor,
            bool NoAlphaAtBoundary)
        {
            if (image == null || mask == null || canvas == null)
                return false;

            image.Lock();

            mask.Lock();

            canvas.Lock();

            unsafe
            {
                uint* pixelsImage = (uint*)image.BackBuffer;
                uint* pixelsMask = (uint*)mask.BackBuffer;
                uint* pixelsCanvas = (uint*)canvas.BackBuffer;

                if (pixelsImage == null || pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = canvas.BackBufferStride >> 2;
                for (uint row = 0; row < canvas.Height; ++row)
                {
                    for (col = 0; col < canvas.Width; ++col)
                    {
                        if (row >= image.Height || col >= image.Width)
                            continue;
                        if (row >= mask.Height || col >= mask.Width)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)(row * (mask.BackBufferStride >> 2) + col);
                        uint indexImage = (uint)(row * (image.BackBufferStride >> 2) + col);

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
            canvas.Unlock();
            mask.Unlock();
            image.Unlock();

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
            System.Windows.Media.Color clr,
            System.Windows.Media.Imaging.WriteableBitmap mask,
            System.Windows.Media.Imaging.WriteableBitmap canvas,
            System.Windows.Media.Color maskColor)
        {
            if (mask == null || canvas == null)
                return false;


            mask.Lock();

            canvas.Lock();

            unsafe
            {
                uint* pixelsMask = (uint*)mask.BackBuffer;
                uint* pixelsCanvas = (uint*)canvas.BackBuffer;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = canvas.BackBufferStride >> 2;
                for (uint row = 0; row < canvas.Height; ++row)
                {
                    for (col = 0; col < canvas.Width; ++col)
                    {
                        if (row >= mask.Height || col >= mask.Width)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)(row * (mask.BackBufferStride >> 2) + col);

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        uint color = (uint)(0xff << 24 | clr.R << 16 | clr.G << 8 | clr.B);

                        if(maskByte>0)
                            pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, (Byte)(pixelsMask[indexMask] >> 24), (Byte)(pixelsMask[indexMask] >> 24));
                    }
                }
            }
            canvas.Unlock();
            mask.Unlock();

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
            System.Windows.Media.Color clr,
            System.Windows.Media.Imaging.WriteableBitmap mask,
            System.Windows.Media.Imaging.WriteableBitmap canvas,
            System.Windows.Media.Color maskColor,
            System.Windows.Point offset)
        {
            if (mask == null || canvas == null)
                return false;


            mask.Lock();

            canvas.Lock();

            unsafe
            {
                uint* pixelsMask = (uint*)mask.BackBuffer;
                uint* pixelsCanvas = (uint*)canvas.BackBuffer;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                int col = 0;
                int stride = canvas.BackBufferStride >> 2;
                for (int row = 0; row < canvas.Height; ++row)
                {
                    for (col = 0; col < canvas.Width; ++col)
                    {
                        if (row - (int)offset.Y >= mask.Height || col - (int)offset.X >= mask.Width ||
                            row - (int)offset.Y < 0 || col - (int)offset.X < 0)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)((row - (int)offset.Y) * (mask.BackBufferStride >> 2) + (col- (int)offset.X));

                        byte maskByte = 0;

                        if (MaskColor.IsEqual(maskColor, MaskColor.Red))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff0000) >> 16);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Green))
                            maskByte = (Byte)((pixelsMask[indexMask] & 0xff00) >> 8);
                        else if (MaskColor.IsEqual(maskColor, MaskColor.Blue))
                            maskByte = (Byte)(pixelsMask[indexMask] & 0xff);

                        uint color = (uint)(0xff << 24 | clr.R << 16 | clr.G << 8 | clr.B);

                        if (maskByte > 0)
                        {
                            pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, (Byte)(pixelsMask[indexMask] >> 24), (Byte)(pixelsMask[indexMask] >> 24));
                        }
                    }
                }
            }
            canvas.Unlock();
            mask.Unlock();

            return true;
        }

        /// <summary>
        /// Apply shadow to mask onto the canvas
        /// </summary>
        /// <param name="clr">is the color to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <returns>true if successful</returns>
        public static bool ApplyShadowToMask(
            System.Windows.Media.Color clrShadow,
            System.Windows.Media.Imaging.WriteableBitmap mask,
            System.Windows.Media.Imaging.WriteableBitmap canvas,
            System.Windows.Media.Color maskColor)
        {
            if (mask == null || canvas == null)
                return false;


            mask.Lock();

            canvas.Lock();

            unsafe
            {
                uint* pixelsMask = (uint*)mask.BackBuffer;
                uint* pixelsCanvas = (uint*)canvas.BackBuffer;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                uint col = 0;
                int stride = canvas.BackBufferStride >> 2;
                for (uint row = 0; row < canvas.Height; ++row)
                {
                    for (col = 0; col < canvas.Width; ++col)
                    {
                        if (row >= mask.Height || col >= mask.Width)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)(row * (mask.BackBufferStride >> 2) + col);

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
            canvas.Unlock();
            mask.Unlock();

            return true;
        }

        /// <summary>
        /// Apply shadow to mask onto the canvas
        /// </summary>
        /// <param name="clrShadow">is the color to be used</param>
        /// <param name="mask">is the mask image to be read</param>
        /// <param name="canvas">is the destination image to be draw upon</param>
        /// <param name="maskColor">is mask color used in mask image</param>
        /// <param name="offset">determine how much to offset the mask</param>
        /// <returns>true if successful</returns>
        public static bool ApplyShadowToMask(
            System.Windows.Media.Color clrShadow,
            System.Windows.Media.Imaging.WriteableBitmap mask,
            System.Windows.Media.Imaging.WriteableBitmap canvas,
            System.Windows.Media.Color maskColor,
            System.Windows.Point offset)
        {
            if (mask == null || canvas == null)
                return false;


            mask.Lock();

            canvas.Lock();

            unsafe
            {
                uint* pixelsMask = (uint*)mask.BackBuffer;
                uint* pixelsCanvas = (uint*)canvas.BackBuffer;

                if (pixelsMask == null || pixelsCanvas == null)
                    return false;

                int col = 0;
                int stride = canvas.BackBufferStride >> 2;
                for (int row = 0; row < canvas.Height; ++row)
                {
                    for (col = 0; col < canvas.Width; ++col)
                    {
                        if (row - (int)offset.Y >= mask.Height || col - (int)offset.X >= mask.Width ||
                            row - (int)offset.Y < 0 || col - (int)offset.X < 0)
                            continue;

                        uint index = (uint)(row * stride + col);
                        uint indexMask = (uint)((row - (int)offset.Y) * (mask.BackBufferStride >> 2) + (col - (int)offset.X));

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
            canvas.Unlock();
            mask.Unlock();

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
            ref System.Windows.Media.Imaging.WriteableBitmap image,
            System.Windows.Point offset,
            TextContext textContext)
        {
            if (strategy == null || image == null || textContext == null)
                return false;

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(image.Width), (int)(image.Height), 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();

            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawImage(image, new Rect(0.0, 0.0, image.Width, image.Height));

            bool bRet = strategy.DrawString(drawingContext,
                textContext.fontFamily,
                textContext.fontStyle,
                textContext.fontWeight,
                textContext.nfontSize,
                textContext.pszText,
                new System.Windows.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                textContext.ci);

            drawingContext.Close();

            bmp.Render(drawingVisual);

            image = new System.Windows.Media.Imaging.WriteableBitmap(bmp);

            return bRet;
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
            ref System.Windows.Media.Imaging.WriteableBitmap image,
            System.Windows.Point offset,
            TextContext textContext,
            System.Windows.Media.Matrix mat)
        {
            if (strategy == null || image == null || textContext == null)
                return false;

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(image.Width), (int)(image.Height), 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingVisual = new DrawingVisual();

            DrawingContext drawingContext = drawingVisual.RenderOpen();

            MatrixTransform mat_trans = new MatrixTransform();
            mat_trans.Matrix = mat;

            drawingContext.PushTransform(mat_trans);

            drawingContext.DrawImage(image, new Rect(0.0, 0.0, image.Width, image.Height));

            bool bRet = strategy.DrawString(drawingContext,
                textContext.fontFamily,
                textContext.fontStyle,
                textContext.fontWeight,
                textContext.nfontSize,
                textContext.pszText,
                new System.Windows.Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
                textContext.ci);

            drawingContext.Pop();

            drawingContext.Close();

            bmp.Render(drawingVisual);

            image = new System.Windows.Media.Imaging.WriteableBitmap(bmp);

            return bRet;
        }

        /// <summary>
        /// Set alpha to color
        /// </summary>
        /// <param name="dest">is destination color in ARGB</param>
        /// <param name="source">is source color in ARGB</param>
        /// <param name="nAlpha">is nAlpha is alpha channel</param>
        /// <returns>destination color</returns>
        private static UInt32 AddAlpha(UInt32 dest, UInt32 source, Byte nAlpha)
        {
            if (0 == nAlpha)
                return dest;

            if (255 == nAlpha)
                return source;

            Byte nInvAlpha = (Byte)(~nAlpha);

            Byte nSrcRed = (Byte)((source & 0xff0000) >> 16);
            Byte nSrcGreen = (Byte)((source & 0xff00) >> 8);
            Byte nSrcBlue = (Byte)((source & 0xff));

            Byte nRed = (Byte)(nSrcRed );
            Byte nGreen = (Byte)(nSrcGreen );
            Byte nBlue = (Byte)(nSrcBlue );

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
        /// Performs PreMultipliedAlphablend
        /// </summary>
        /// <param name="dest">is destination color in ARGB</param>
        /// <param name="source">is source color in ARGB</param>
        /// <returns>destination color</returns>
        private static UInt32 PreMultipliedAlphablend(UInt32 dest, UInt32 source)
        {
            Byte Alpha = (Byte)((source & 0xff000000) >> 24);
            Byte nInvAlpha = (Byte)(255-Alpha);

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
