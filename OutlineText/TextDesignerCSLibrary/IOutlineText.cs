using System;
using System.Collections.Generic;
using System.Text;

namespace TextDesignerCSLibrary
{
    public interface IOutlineText : IDisposable
    {
	void TextGlow(
		System.Drawing.Color clrText, 
		System.Drawing.Color clrOutline, 
		int nThickness);

    void TextGlow(
        System.Drawing.Brush brushText,
        System.Drawing.Color clrOutline,
        int nThickness);
    
    void TextOutline(
		System.Drawing.Color clrText, 
		System.Drawing.Color clrOutline, 
		int nThickness);

    void TextOutline(
        System.Drawing.Brush brushText,
        System.Drawing.Color clrOutline,
        int nThickness);
    
    void TextDblOutline(
		System.Drawing.Color clrText, 
		System.Drawing.Color clrOutline1, 
		System.Drawing.Color clrOutline2, 
		int nThickness1, 
		int nThickness2);

    void TextDblOutline(
        System.Drawing.Brush brushText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        int nThickness2);

    void TextDblGlow(
        System.Drawing.Color clrText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        int nThickness2);

    void TextDblGlow(
        System.Drawing.Brush brushText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        int nThickness2);

    void TextGradOutline(
        System.Drawing.Color clrText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        GradientType gradType);

    void TextGradOutline(
        System.Drawing.Brush brushText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        GradientType gradType);

    void TextGradOutlineLast(
        System.Drawing.Color clrText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        GradientType gradType);

    void TextGradOutlineLast(
        System.Drawing.Brush brushText,
        System.Drawing.Color clrOutline1,
        System.Drawing.Color clrOutline2,
        int nThickness1,
        GradientType gradType);

    void TextNoOutline(System.Drawing.Color clrText);

    void TextNoOutline(System.Drawing.Brush brushText);

    void TextOnlyOutline(
        System.Drawing.Color clrOutline,
        int nThickness,
        bool bRoundedEdge);
    
    void SetShadowBkgd(System.Drawing.Bitmap pBitmap);

	void SetShadowBkgd(System.Drawing.Color clrBkgd, int nWidth, int nHeight);

    void SetNullTextEffect();

	void SetNullShadow();
	
	void EnableShadow(bool bEnable);

	void Shadow(
		System.Drawing.Color color, 
		int nThickness,
		System.Drawing.Point ptOffset);

	bool DrawString(
		System.Drawing.Graphics graphics, 
		System.Drawing.FontFamily fontFamily,
		System.Drawing.FontStyle fontStyle,
		int nfontSize,
		string strText, 
		System.Drawing.Point ptDraw, 
		System.Drawing.StringFormat strFormat);

	bool DrawString(
		System.Drawing.Graphics graphics, 
		System.Drawing.FontFamily fontFamily,
		System.Drawing.FontStyle fontStyle,
		int nfontSize,
		string strText, 
		System.Drawing.Rectangle rtDraw,
		System.Drawing.StringFormat pStrFormat);

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
		int nfontSize,
		string strText, 
		System.Drawing.Point ptDraw, 
		System.Drawing.StringFormat strFormat,
        ref float fStartX,
        ref float fStartY,
        ref float fDestWidth,
		ref float fDestHeight );

	bool MeasureString(
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
		ref float fDestHeight );

    }
}
