using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TextDesignerCSLibrary
{
    public interface ITextStrategy : IDisposable
    {
       	ITextStrategy Clone();

        bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat);

        bool DrawString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string strText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat);

        bool GdiDrawString(
		    System.Drawing.Graphics pGraphics, 
		    LOGFONT pLogFont,
		    string pszText, 
		    System.Drawing.Point ptDraw);

	    bool GdiDrawString(
		    System.Drawing.Graphics pGraphics, 
		    LOGFONT pLogFont,
		    string pszText, 
		    System.Drawing.Rectangle rtDraw);

        bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string pszText,
            System.Drawing.Point ptDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight);

        bool MeasureString(
            System.Drawing.Graphics graphics,
            System.Drawing.FontFamily fontFamily,
            System.Drawing.FontStyle fontStyle,
            int fontSize,
            string pszText,
            System.Drawing.Rectangle rtDraw,
            System.Drawing.StringFormat strFormat,
            ref float fStartX,
            ref float fStartY,
            ref float fDestWidth,
            ref float fDestHeight);

        bool GdiMeasureString(
		    System.Drawing.Graphics pGraphics, 
		    LOGFONT pLogFont,
		    string pszText, 
		    System.Drawing.Point ptDraw,
            ref float pfPixelsStartX,
            ref float pfPixelsStartY,
            ref float pfDestWidth,
            ref float pfDestHeight);

	    bool GdiMeasureString(
		    System.Drawing.Graphics pGraphics, 
		    LOGFONT pLogFont,
		    string pszText, 
		    System.Drawing.Rectangle rtDraw,
		    ref float pfPixelsStartX,
		    ref float pfPixelsStartY,
		    ref float pfDestWidth,
		    ref float pfDestHeight );

        bool GdiMeasureStringRealHeight(
		    System.Drawing.Graphics pGraphics, 
		    LOGFONT pLogFont,
		    string pszText, 
		    System.Drawing.Point ptDraw,
            ref float pfPixelsStartX,
            ref float pfPixelsStartY,
            ref float pfDestWidth,
            ref float pfDestHeight);

    	bool GdiMeasureStringRealHeight(
		    System.Drawing.Graphics pGraphics, 
		    LOGFONT pLogFont,
		    string pszText, 
		    System.Drawing.Rectangle rtDraw,
		    ref float pfPixelsStartX,
		    ref float pfPixelsStartY,
		    ref float pfDestWidth,
            ref float pfDestHeight );
    }
}
