using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public abstract class TextImplGetHeight : ITextStrategy
    {
        public abstract void Dispose();
        protected abstract void Dispose(bool disposing);

        public abstract ITextStrategy Clone();

        public abstract bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat);

        public abstract bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat);

        public abstract bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Point ptDraw);

        public abstract bool GdiDrawString(
            System.Drawing.Graphics pGraphics,
            LOGFONT pLogFont,
            string pszText,
            System.Drawing.Rectangle rtDraw);

        public bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, ptDraw, strFormat);

                fDestWidth = ptDraw.X;
                fDestHeight = ptDraw.Y;
                bool b = GDIPath.MeasureGraphicsPath(graphics, path, ref fStartX, ref fStartY, ref fDestWidth, ref fDestHeight);

                if (false == b)
                    return false;

                float pixelThick = 0.0f;
                float pixelThick2 = 0.0f;
                float fStartX2 = 0.0f;
                float fStartY2 = 0.0f;
                b = GDIPath.ConvertToPixels(graphics, m_nThickness, 0.0f, ref fStartX2, ref fStartY2, ref pixelThick, ref pixelThick2);

                if (false == b)
                    return false;

                fDestWidth += pixelThick;
                fDestHeight += pixelThick;
            }
            return true;
        }

        public bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(strText, fontFamily, (int)fontStyle, fontSize, rtDraw, strFormat);

                fDestWidth = rtDraw.Width;
                fDestHeight = rtDraw.Height;
                bool b = GDIPath.MeasureGraphicsPath(graphics, path, ref fStartX, ref fStartY, ref fDestWidth, ref fDestHeight);

                if (false == b)
                    return false;

                float pixelThick = 0.0f;
                float pixelThick2 = 0.0f;
                float fStartX2 = 0.0f;
                float fStartY2 = 0.0f;
                b = GDIPath.ConvertToPixels(graphics, m_nThickness, 0.0f, ref fStartX2, ref fStartY2, ref pixelThick, ref pixelThick2);

                if (false == b)
                    return false;

                fDestWidth += pixelThick;
                fDestHeight += pixelThick;
            }
            return true;
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

                pfDestWidth = ptDraw.X;
                pfDestHeight = ptDraw.Y;
                b = GDIPath.MeasureGraphicsPath(pGraphics, pPath, ref pfPixelsStartX, ref pfPixelsStartY, ref pfDestWidth, ref pfDestHeight);

                if (false == b)
                {
                    return false;
                }

                float pixelThick = 0.0f;
                float fStartX = 0.0f;
                float fStartY = 0.0f;
                float fDestHeight = 0.0f;
                b = GDIPath.ConvertToPixels(pGraphics, m_nThickness, 0.0f, ref fStartX, ref fStartY, ref pixelThick, ref fDestHeight);

                if (false == b)
                {
                    return false;
                }

                pfDestWidth += pixelThick;
                pfDestHeight += pixelThick;
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

                pfDestWidth = rtDraw.Left;
                pfDestHeight = rtDraw.Top;
                b = GDIPath.MeasureGraphicsPath(pGraphics, pPath, ref pfPixelsStartX, ref pfPixelsStartY, ref pfDestWidth, ref pfDestHeight);

                if (false == b)
                {
                    return false;
                }

                float pixelThick = 0.0f;
                float fStartX = 0.0f;
                float fStartY = 0.0f;
                float fDestHeight = 0.0f;

                b = GDIPath.ConvertToPixels(pGraphics, m_nThickness, 0.0f, ref fStartX, ref fStartY, ref pixelThick, ref fDestHeight);

                if (false == b)
                {
                    return false;
                }

                pfDestWidth += pixelThick;
                pfDestHeight += pixelThick;
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

                pfDestWidth = ptDraw.X;
                pfDestHeight = ptDraw.Y;
                b = GDIPath.MeasureGraphicsPathRealHeight(pGraphics, pPath, ref pfPixelsStartX, ref pfPixelsStartY, ref pfDestWidth, ref pfDestHeight);

                if (false == b)
                {
                    return false;
                }

                float pixelThick = 0.0f;
                float fStartX = 0.0f;
                float fStartY = 0.0f;
                float fDestHeight = 0.0f;
                b = GDIPath.ConvertToPixels(pGraphics, m_nThickness, 0.0f, ref fStartX, ref fStartY, ref pixelThick, ref fDestHeight);

                if (false == b)
                {
                    return false;
                }

                pfDestWidth += pixelThick;
                pfDestHeight += pixelThick;
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

                pfDestWidth = rtDraw.Left;
                pfDestHeight = rtDraw.Top;
                b = GDIPath.MeasureGraphicsPathRealHeight(pGraphics, pPath, ref pfPixelsStartX, ref pfPixelsStartY, ref pfDestWidth, ref pfDestHeight);

                if (false == b)
                {
                    return false;
                }

                float pixelThick = 0.0f;
                float fStartX = 0.0f;
                float fStartY = 0.0f;
                float fDestHeight = 0.0f;

                b = GDIPath.ConvertToPixels(pGraphics, m_nThickness, 0.0f, ref fStartX, ref fStartY, ref pixelThick, ref fDestHeight);

                if (false == b)
                {
                    return false;
                }

                pfDestWidth += pixelThick;
                pfDestHeight += pixelThick;
            }
            return true;
        }
        protected int m_nThickness;
    }
}
