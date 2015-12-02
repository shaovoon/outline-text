#pragma once

#include "../TextDesigner/OutlineText.h"
#include "../TextDesigner/PngOutlineText.h"

enum TextEffect
{
	TextGlow,
	SingleOutline,
	DblOutline,
	GradOutline,
	NoOutline,
	OnlyOutline,
	DblGlow
};

enum TextPath
{
	GdiPlus,
	Gdi
};

// CMyScrollView view

class CMyScrollView : public CScrollView
{
	DECLARE_DYNCREATE(CMyScrollView)

public:
	bool SetImage(const wchar_t* pszFile);
	void SetBkgdColor(Gdiplus::Color color);
	void SetTextColor1(Gdiplus::Color color);
	void SetTextColor2(Gdiplus::Color color);
	void SetOutlineColor1(Gdiplus::Color color);
	void SetOutlineColor2(Gdiplus::Color color);
	void SetTextEffect(TextEffect textEffect);
	void SetTextPath(TextPath textPath);
	void SetText(const CString& str);
	void SetOutlineThickness1(int nThick1);
	void SetOutlineThickness2(int nThick2);
	void SetOutlineAlpha1(BYTE nAlpha);
	void SetOutlineAlpha2(BYTE nAlpha);
	void SetTextPosX(int nTextPosX);
	void SetTextPosY(int nTextPosY);
	void SetTextAngle(int nTextAngle);
	void SetLogFont(LOGFONTW *logFont);
	void SetFontSize(int nSize);
	void SetEnableShadow(bool bEnable);
	void SetShadowAlpha(int nAlpha);
	void SetShadowOffsetX(int nOffsetX);
	void SetShadowOffsetY(int nOffsetY);
	void SetShadowBkgdBmp(std::shared_ptr<Gdiplus::Bitmap>& pBmp);
	void SetShadowColor(Gdiplus::Color color);
	void SetShadowThickness(int nThick);
	void EnablePngRendering(bool bEnable);
	void EnableTextGradient(bool bEnable);
	void SetDiffusedShadow(bool bEnable);
	void SetExtrudedText(bool bEnable);
	void Render(bool bEnable);
	CString ConvBackSlashToForwardSlash(const CString& strSrc);
	void SetBegAlpha(float f);
	void SetEndAlpha(float f);
	void SetShown(float f);
	void SetGap(int n);
	void EnableReflection(bool bEnable);

	bool SavePngImage(const CString& szFile);
	bool SaveRefPngImage(const CString& szFile);


private:
	bool SetTextEffect(TextDesigner::IOutlineText* pOutlineText, Gdiplus::Brush* pBrush);

public:
	CString GetImage();
	Gdiplus::Color GetBkgdColor();
	Gdiplus::Color GetTextColor1();
	Gdiplus::Color GetTextColor2();
	Gdiplus::Color GetOutlineColor1();
	Gdiplus::Color GetOutlineColor2();
	TextEffect GetTextEffect();
	TextPath GetTextPath();
	CString GetText();
	int GetOutlineThickness1();
	int GetOutlineThickness2();
	BYTE GetOutlineAlpha1();
	BYTE GetOutlineAlpha2();
	int GetTextPosX();
	int GetTextPosY();
	int GetTextAngle();
	LOGFONTW* GetLogFont();
	int GetFontSize();
	bool GetEnableShadow();
	int GetShadowAlpha();
	int GetShadowOffsetX();
	int GetShadowOffsetY();
	Gdiplus::Bitmap* GetShadowBkgdBmp();
	Gdiplus::Color GetShadowColor();
	int GetShadowThickness();
	bool IsEnablePngRendering();
	bool IsEnableTextGradient();
	bool GetDiffusedShadow();
	bool GetExtrudedText();
	float GetBegAlpha();
	float GetEndAlpha();
	float GetShown();
	int GetGap();
	bool IsEnableReflection();



protected:
	CMyScrollView();           // protected constructor used by dynamic creation
	virtual ~CMyScrollView();
	std::shared_ptr<Gdiplus::Bitmap> m_pImage;
	Gdiplus::Color m_clrBkgd;
	Gdiplus::Color m_clrText;
	Gdiplus::Color m_clrText2;
	Gdiplus::Color m_clrOutline1;
	Gdiplus::Color m_clrOutline2;
	TextEffect m_TextEffect;
	TextPath m_TextPath;
	CString m_szText;
	bool m_bRender;
	TextDesigner::OutlineText m_OutlineText;
	TextDesigner::PngOutlineText m_PngOutlineText;
	BYTE m_nOutlineAlpha1;
	BYTE m_nOutlineAlpha2;
	int m_nOutlineThickness1;
	int m_nOutlineThickness2;
	int m_nTextPosX;
	int m_nTextPosY;
	int m_nTextAngle;
	LOGFONTW m_LogFont;
	int m_nFontSize;
	int m_nShadowOffsetX;
	int m_nShadowOffsetY;
	int m_nShadowAlpha;
	bool m_bEnableShadow;
	std::shared_ptr<Gdiplus::Bitmap> m_pShadowBkgdBmp;
	Gdiplus::Color m_clrShadow;
	int m_nShadowThickness;
	std::shared_ptr<Gdiplus::Bitmap> m_pPngImage;
	bool m_bPngImage;
	bool m_bTextGradient;
	bool m_bDirty;
	bool m_bCorrectPngRendering;
	Gdiplus::Bitmap* m_pBkgdBmp;
	bool m_bDiffusedShadow;
	bool m_bExtrudedText;
	CString m_szImageFile;
	std::shared_ptr<Gdiplus::LinearGradientBrush> m_pGradientBrush;
	float m_fStartAlpha;
	float m_fEndAlpha;
	float m_fShown;
	int m_nGap;
	bool m_bEnableReflection;

public:
#ifdef _DEBUG
	virtual void AssertValid() const;
#ifndef _WIN32_WCE
	virtual void Dump(CDumpContext& dc) const;
#endif
#endif

public:
	virtual void OnDraw(CDC* pDC);      // overridden to draw this view
	virtual void OnInitialUpdate();     // first time after construct

	DECLARE_MESSAGE_MAP()
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
};


