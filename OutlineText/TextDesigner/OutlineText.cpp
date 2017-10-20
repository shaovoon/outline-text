/*
Text Designer Outline Text Library 

Copyright (c) 2009 Wong Shao Voon

The Code Project Open License (CPOL)
http://www.codeproject.com/info/cpol10.aspx
*/

#include "StdAfx.h"
#include "OutlineText.h"
#include "GDIPath.h"

using namespace TextDesigner;

OutlineText::OutlineText(void)
:
m_pBkgdBitmap(NULL),
m_clrShadow(Gdiplus::Color(0,0,0)),
m_bEnableShadow(false),
m_bDiffuseShadow(false),
m_nShadowThickness(2)
{
}

OutlineText::OutlineText(OutlineText* rhs)
{
	Init(rhs);
}

OutlineText& OutlineText::operator=(OutlineText& rhs)
{
	if((&rhs)==this)
		return *this;

	Init(&rhs);
	return *this;
}

void OutlineText::Init(OutlineText* rhs)
{
	m_pTextStrategy = std::shared_ptr<ITextStrategy>(rhs->m_pTextStrategy->Clone());
	m_pShadowStrategy = std::shared_ptr<ITextStrategy>(rhs->m_pShadowStrategy->Clone());
	m_pFontBodyShadow = std::shared_ptr<ITextStrategy>(rhs->m_pFontBodyShadow->Clone());
	m_pBkgdBitmap = rhs->m_pBkgdBitmap;
	m_clrShadow = rhs->m_clrShadow;
	m_bEnableShadow = rhs->m_bEnableShadow;
	m_bDiffuseShadow = rhs->m_bDiffuseShadow;
	m_nShadowThickness = rhs->m_nShadowThickness;
}

OutlineText::~OutlineText(void)
{
}

void OutlineText::TextGlow(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextGlowStrategy> pStrat = std::make_shared<TextGlowStrategy>();
	pStrat->Init(clrText,clrOutline,nThickness);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextGlow(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextGlowStrategy> pStrat = std::make_shared<TextGlowStrategy>();
	pStrat->Init(&brushText,clrOutline,nThickness);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextOutline(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextOutlineStrategy> pStrat = std::make_shared<TextOutlineStrategy>();
	pStrat->Init(clrText,clrOutline,nThickness);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextOutline(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextOutlineStrategy> pStrat = std::make_shared<TextOutlineStrategy>();
	pStrat->Init(&brushText,clrOutline,nThickness);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextDblOutline(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness1, 
	int nThickness2)
{
	std::shared_ptr<TextDblOutlineStrategy> pStrat = std::make_shared<TextDblOutlineStrategy>();
	pStrat->Init(clrText,clrOutline1,clrOutline2,nThickness1,nThickness2);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextDblOutline(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness1, 
	int nThickness2)
{
	std::shared_ptr<TextDblOutlineStrategy> pStrat = std::make_shared<TextDblOutlineStrategy>();
	pStrat->Init(&brushText,clrOutline1,clrOutline2,nThickness1,nThickness2);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextDblGlow(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness1, 
	int nThickness2)
{
	std::shared_ptr<TextDblGlowStrategy> pStrat = std::make_shared<TextDblGlowStrategy>();
	pStrat->Init(clrText,clrOutline1,clrOutline2,nThickness1,nThickness2);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextDblGlow(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness1, 
	int nThickness2)
{
	std::shared_ptr<TextDblGlowStrategy> pStrat = std::make_shared<TextDblGlowStrategy>();
	pStrat->Init(&brushText,clrOutline1,clrOutline2,nThickness1,nThickness2);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextGradOutline(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineStrategy> pStrat = std::make_shared<TextGradOutlineStrategy>();
	pStrat->Init(clrText,clrOutline1,clrOutline2,nThickness, gradType);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextGradOutline(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineStrategy> pStrat = std::make_shared<TextGradOutlineStrategy>();
	pStrat->Init(&brushText,clrOutline1,clrOutline2,nThickness, gradType);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextGradOutlineLast(
	Gdiplus::Color clrText,
	Gdiplus::Color clrOutline1,
	Gdiplus::Color clrOutline2,
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineLastStrategy> pStrat = std::make_shared<TextGradOutlineLastStrategy>();
	pStrat->Init(clrText, clrOutline1, clrOutline2, nThickness, gradType);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextGradOutlineLast(
	Gdiplus::Brush& brushText,
	Gdiplus::Color clrOutline1,
	Gdiplus::Color clrOutline2,
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineLastStrategy> pStrat = std::make_shared<TextGradOutlineLastStrategy>();
	pStrat->Init(&brushText, clrOutline1, clrOutline2, nThickness, gradType);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextNoOutline(Gdiplus::Color clrText)
{
	std::shared_ptr<TextNoOutlineStrategy> pStrat = std::make_shared<TextNoOutlineStrategy>();
	pStrat->Init(clrText);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextNoOutline(Gdiplus::Brush& brushText)
{
	std::shared_ptr<TextNoOutlineStrategy> pStrat = std::make_shared<TextNoOutlineStrategy>();
	pStrat->Init(&brushText);

	m_pTextStrategy = pStrat;
}

void OutlineText::TextOnlyOutline(
	Gdiplus::Color clrOutline, 
	int nThickness,
	bool bRoundedEdge)
{
	std::shared_ptr<TextOnlyOutlineStrategy> pStrat = std::make_shared<TextOnlyOutlineStrategy>();
	pStrat->Init(clrOutline, nThickness, bRoundedEdge);

	m_pTextStrategy = pStrat;
}

void OutlineText::Shadow(
	Gdiplus::Color color, 
	int nThickness,
	Gdiplus::Point ptOffset)
{
	std::shared_ptr<TextOutlineStrategy> pStrat = std::make_shared<TextOutlineStrategy>();
	pStrat->Init(Gdiplus::Color(0,0,0,0),color,nThickness);

	m_clrShadow = color;

	std::shared_ptr<TextOutlineStrategy> pFontBodyShadow = std::make_shared<TextOutlineStrategy>();
	pFontBodyShadow->Init(Gdiplus::Color(255,255,255),Gdiplus::Color(0,0,0,0),0);
	m_pFontBodyShadow = pFontBodyShadow;

	m_ptShadowOffset = ptOffset;
	m_pShadowStrategy = pStrat;
	m_bDiffuseShadow = false;
}

void OutlineText::DiffusedShadow(
	Gdiplus::Color color, 
	int nThickness,
	Gdiplus::Point ptOffset)
{
	std::shared_ptr<DiffusedShadowStrategy> pStrat = std::make_shared<DiffusedShadowStrategy>();
	pStrat->Init(Gdiplus::Color(0,0,0,0),color,nThickness,true);

	m_clrShadow = color;

	std::shared_ptr<DiffusedShadowStrategy> pFontBodyShadow = std::make_shared<DiffusedShadowStrategy>();
	pFontBodyShadow->Init(Gdiplus::Color(color.GetA(),255,255),Gdiplus::Color(0,0,0,0),0,true);
	m_pFontBodyShadow = pFontBodyShadow;

	m_ptShadowOffset = ptOffset;
	m_pShadowStrategy = pStrat;
	m_bDiffuseShadow = true;
	m_bExtrudeShadow = false;
	m_nShadowThickness = nThickness;
}

void OutlineText::Extrude(
	Gdiplus::Color color, 
	int nThickness,
	Gdiplus::Point ptOffset)
{
	std::shared_ptr<ExtrudeStrategy> pStrat = std::make_shared<ExtrudeStrategy>();
	pStrat->Init(Gdiplus::Color(0,0,0,0),color,nThickness,ptOffset.X,ptOffset.Y);

	m_clrShadow = color;

	std::shared_ptr<ExtrudeStrategy> pFontBodyShadow = std::make_shared<ExtrudeStrategy>();
	pFontBodyShadow->Init(Gdiplus::Color(color.GetA(),255,255),Gdiplus::Color(0,0,0,0),0,ptOffset.X,ptOffset.Y);
	m_pFontBodyShadow = pFontBodyShadow;

	m_ptShadowOffset = ptOffset;
	m_pShadowStrategy = pStrat;
	m_bExtrudeShadow = true;
	m_bDiffuseShadow = false;
	m_nShadowThickness = nThickness;
}

bool OutlineText::DrawString(
	Gdiplus::Graphics* pGraphics, 
	Gdiplus::FontFamily* pFontFamily,
	Gdiplus::FontStyle fontStyle,
	int nfontSize,
	const wchar_t*pszText, 
	Gdiplus::Point ptDraw, 
	Gdiplus::StringFormat* pStrFormat)
{
	if(m_bEnableShadow&&m_pShadowStrategy)
	{
		m_pShadowStrategy->DrawString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			Gdiplus::Point(ptDraw.X+m_ptShadowOffset.X, ptDraw.Y+m_ptShadowOffset.Y),
			pStrFormat);
	}

	if(m_bEnableShadow&&m_pBkgdBitmap&&m_pFontBodyShadow)
	{
		RenderFontShadow(
			pGraphics,
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			Gdiplus::Point(ptDraw.X+m_ptShadowOffset.X, ptDraw.Y+m_ptShadowOffset.Y),
			pStrFormat);
	}

	if(m_pTextStrategy)
	{
		return m_pTextStrategy->DrawString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			ptDraw, 
			pStrFormat);
	}

	return false;
}

bool OutlineText::DrawString(
	Gdiplus::Graphics* pGraphics, 
	Gdiplus::FontFamily* pFontFamily,
	Gdiplus::FontStyle fontStyle,
	int nfontSize,
	const wchar_t*pszText, 
	Gdiplus::Rect rtDraw, 
	Gdiplus::StringFormat* pStrFormat)
{
	if(m_bEnableShadow&&m_pShadowStrategy)
	{
		m_pShadowStrategy->DrawString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			Gdiplus::Rect(rtDraw.X+m_ptShadowOffset.X, rtDraw.Y+m_ptShadowOffset.Y,rtDraw.Width,rtDraw.Height),
			pStrFormat);
	}

	if(m_bEnableShadow&&m_pBkgdBitmap&&m_pFontBodyShadow)
	{
		RenderFontShadow(
			pGraphics,
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			Gdiplus::Rect(rtDraw.X+m_ptShadowOffset.X, rtDraw.Y+m_ptShadowOffset.Y,rtDraw.Width,rtDraw.Height),
			pStrFormat);
	}

	if(m_pTextStrategy)
	{
		return m_pTextStrategy->DrawString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			rtDraw, 
			pStrFormat);
	}

	return false;
}

bool OutlineText::GdiDrawString(
	Gdiplus::Graphics* pGraphics, 
	LOGFONTW* pLogFont,
	const wchar_t*pszText, 
	Gdiplus::Point ptDraw)
{
	if(m_bEnableShadow&&m_pShadowStrategy)
	{
		m_pShadowStrategy->GdiDrawString(
			pGraphics, 
			pLogFont,
			pszText, 
			Gdiplus::Point(ptDraw.X+m_ptShadowOffset.X, ptDraw.Y+m_ptShadowOffset.Y));
	}

	if(m_bEnableShadow&&m_pBkgdBitmap&&m_pFontBodyShadow)
	{
		GdiRenderFontShadow(
			pGraphics, 
			pLogFont,
			pszText, 
			Gdiplus::Point(ptDraw.X+m_ptShadowOffset.X, ptDraw.Y+m_ptShadowOffset.Y));
	}

	if(m_pTextStrategy)
	{
		return m_pTextStrategy->GdiDrawString(
			pGraphics, 
			pLogFont,
			pszText, 
			ptDraw);
	}

	return false;
}

bool OutlineText::GdiDrawString(
	Gdiplus::Graphics* pGraphics, 
	LOGFONTW* pLogFont,
	const wchar_t*pszText, 
	Gdiplus::Rect rtDraw)
{
	if(m_bEnableShadow&&m_pShadowStrategy)
	{
		m_pShadowStrategy->GdiDrawString(
			pGraphics, 
			pLogFont,
			pszText, 
			Gdiplus::Rect(rtDraw.X+m_ptShadowOffset.X, rtDraw.Y+m_ptShadowOffset.Y,rtDraw.Width,rtDraw.Height));
	}

	if(m_bEnableShadow&&m_pBkgdBitmap&&m_pFontBodyShadow)
	{
		GdiRenderFontShadow(
			pGraphics, 
			pLogFont,
			pszText, 
			Gdiplus::Rect(rtDraw.X+m_ptShadowOffset.X, rtDraw.Y+m_ptShadowOffset.Y,rtDraw.Width,rtDraw.Height));
	}

	if(m_pTextStrategy)
	{
		return m_pTextStrategy->GdiDrawString(
			pGraphics, 
			pLogFont,
			pszText, 
			rtDraw);
	}

	return false;
}

void OutlineText::SetShadowBkgd(std::shared_ptr<Gdiplus::Bitmap>& pBitmap)
{
	m_pBkgdBitmap = pBitmap;
}

void OutlineText::SetShadowBkgd(Gdiplus::Color clrBkgd, int nWidth, int nHeight)
{
	m_pBkgdBitmap = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(nWidth, nHeight, PixelFormat32bppARGB));

	using namespace Gdiplus;

	Gdiplus::Graphics graphics((Gdiplus::Image*)(m_pBkgdBitmap.get()));
	Gdiplus::SolidBrush brush(clrBkgd);
	graphics.FillRectangle(&brush, 0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );
}

void OutlineText::EnableShadow(bool bEnable)
{
	m_bEnableShadow = bEnable;
}


void OutlineText::RenderFontShadow(	
	Gdiplus::Graphics* pGraphics, 
	Gdiplus::FontFamily* pFontFamily,
	Gdiplus::FontStyle fontStyle,
	int nfontSize,
	const wchar_t*pszText, 
	Gdiplus::Point ptDraw, 
	Gdiplus::StringFormat* pStrFormat)
{
	Gdiplus::Bitmap* pBmpMask = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Bitmap* pBmpFontBodyBackup = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Graphics graphicsMask((Gdiplus::Image*)(pBmpMask));
	Gdiplus::SolidBrush brushBlack(Gdiplus::Color(0,0,0));
	graphicsMask.FillRectangle(&brushBlack, 0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	Gdiplus::Bitmap* pBmpDisplay = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);
	Gdiplus::Graphics graphicsBkgd((Gdiplus::Image*)(pBmpDisplay));

	graphicsMask.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsMask.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsMask.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsMask.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsMask.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsMask.SetPageUnit(pGraphics->GetPageUnit());
	graphicsMask.SetPageScale(pGraphics->GetPageScale());

	graphicsBkgd.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsBkgd.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsBkgd.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsBkgd.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsBkgd.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsBkgd.SetPageUnit(pGraphics->GetPageUnit());
	graphicsBkgd.SetPageScale(pGraphics->GetPageScale());

	m_pFontBodyShadow->DrawString(
		&graphicsMask, 
		pFontFamily,
		fontStyle,
		nfontSize,
		pszText, 
		ptDraw, 
		pStrFormat);

	m_pShadowStrategy->DrawString(		
		&graphicsBkgd, 
		pFontFamily,
		fontStyle,
		nfontSize,
		pszText, 
		ptDraw, 
		pStrFormat);


	UINT* pixelsSrc = NULL;
	UINT* pixelsDest = NULL;
	UINT* pixelsMask = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataSrc;
	BitmapData bitmapDataDest;
	BitmapData bitmapDataMask;
	Rect rect(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	pBmpFontBodyBackup->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataSrc );

	pBmpDisplay->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataDest );

	pBmpMask->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataMask );


	// Write to the temporary buffer provided by LockBits.
	pixelsSrc = (UINT*)bitmapDataSrc.Scan0;
	pixelsDest = (UINT*)bitmapDataDest.Scan0;
	pixelsMask = (UINT*)bitmapDataMask.Scan0;

	if( !pixelsSrc || !pixelsDest || !pixelsMask)
		return;

	UINT col = 0;
	int stride = bitmapDataDest.Stride >> 2;
	if(m_bDiffuseShadow&&!m_bExtrudeShadow)
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
				{
					UINT clrtotal = clrShadow;
					for(int i=2;i<=m_nShadowThickness; ++i)
						pixelsSrc[index] = Alphablend(pixelsSrc[index],clrtotal,m_clrShadow.GetA());

					pixelsDest[index] = pixelsSrc[index];
				}
			}
		}
	}
	else
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
					pixelsDest[index] = Alphablend(pixelsSrc[index],clrShadow,m_clrShadow.GetA());
			}
		}
	}

	pBmpMask->UnlockBits(&bitmapDataMask);
	pBmpDisplay->UnlockBits(&bitmapDataDest);
	pBmpFontBodyBackup->UnlockBits(&bitmapDataSrc);

	pGraphics->DrawImage(pBmpDisplay,0,0,pBmpDisplay->GetWidth(),pBmpDisplay->GetHeight());

	if(pBmpMask)
	{
		delete pBmpMask;
		pBmpMask = NULL;
	}
	if(pBmpFontBodyBackup)
	{
		delete pBmpFontBodyBackup;
		pBmpFontBodyBackup = NULL;
	}
	if(pBmpDisplay)
	{
		delete pBmpDisplay;
		pBmpDisplay = NULL;
	}
}

void OutlineText::RenderFontShadow(	
	Gdiplus::Graphics* pGraphics, 
	Gdiplus::FontFamily* pFontFamily,
	Gdiplus::FontStyle fontStyle,
	int nfontSize,
	const wchar_t*pszText, 
	Gdiplus::Rect rtDraw, 
	Gdiplus::StringFormat* pStrFormat)
{
	Gdiplus::Bitmap* pBmpMask = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Bitmap* pBmpFontBodyBackup = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Graphics graphicsMask((Gdiplus::Image*)(pBmpMask));
	Gdiplus::SolidBrush brushBlack(Gdiplus::Color(0,0,0));
	graphicsMask.FillRectangle(&brushBlack, 0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	Gdiplus::Bitmap* pBmpDisplay = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);
	Gdiplus::Graphics graphicsBkgd((Gdiplus::Image*)(pBmpDisplay));

	graphicsMask.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsMask.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsMask.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsMask.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsMask.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsMask.SetPageUnit(pGraphics->GetPageUnit());
	graphicsMask.SetPageScale(pGraphics->GetPageScale());

	graphicsBkgd.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsBkgd.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsBkgd.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsBkgd.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsBkgd.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsBkgd.SetPageUnit(pGraphics->GetPageUnit());
	graphicsBkgd.SetPageScale(pGraphics->GetPageScale());

	m_pFontBodyShadow->DrawString(
		&graphicsMask, 
		pFontFamily,
		fontStyle,
		nfontSize,
		pszText, 
		rtDraw, 
		pStrFormat);

	m_pShadowStrategy->DrawString(		
		&graphicsBkgd, 
		pFontFamily,
		fontStyle,
		nfontSize,
		pszText, 
		rtDraw, 
		pStrFormat);


	UINT* pixelsSrc = NULL;
	UINT* pixelsDest = NULL;
	UINT* pixelsMask = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataSrc;
	BitmapData bitmapDataDest;
	BitmapData bitmapDataMask;
	Rect rect(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	pBmpFontBodyBackup->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataSrc );

	pBmpDisplay->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataDest );

	pBmpMask->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataMask );


	// Write to the temporary buffer provided by LockBits.
	pixelsSrc = (UINT*)bitmapDataSrc.Scan0;
	pixelsDest = (UINT*)bitmapDataDest.Scan0;
	pixelsMask = (UINT*)bitmapDataMask.Scan0;

	if( !pixelsSrc || !pixelsDest || !pixelsMask)
		return;

	UINT col = 0;
	int stride = bitmapDataDest.Stride >> 2;
	if(m_bDiffuseShadow&&!m_bExtrudeShadow)
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
				{
					UINT clrtotal = clrShadow;
					for(int i=2;i<=m_nShadowThickness; ++i)
						pixelsSrc[index] = Alphablend(pixelsSrc[index],clrtotal,m_clrShadow.GetA());

					pixelsDest[index] = pixelsSrc[index];
				}
			}
		}
	}
	else
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
					pixelsDest[index] = Alphablend(pixelsSrc[index],clrShadow,m_clrShadow.GetA());
			}
		}
	}
	pBmpMask->UnlockBits(&bitmapDataMask);
	pBmpDisplay->UnlockBits(&bitmapDataDest);
	pBmpFontBodyBackup->UnlockBits(&bitmapDataSrc);

	pGraphics->DrawImage(pBmpDisplay,0,0,pBmpDisplay->GetWidth(),pBmpDisplay->GetHeight());

	if(pBmpMask)
	{
		delete pBmpMask;
		pBmpMask = NULL;
	}
	if(pBmpFontBodyBackup)
	{
		delete pBmpFontBodyBackup;
		pBmpFontBodyBackup = NULL;
	}
	if(pBmpDisplay)
	{
		delete pBmpDisplay;
		pBmpDisplay = NULL;
	}
}

void OutlineText::GdiRenderFontShadow(	
   Gdiplus::Graphics* pGraphics, 
   LOGFONTW* pLogFont,
   const wchar_t*pszText, 
   Gdiplus::Point ptDraw)
{
	Gdiplus::Bitmap* pBmpMask = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Bitmap* pBmpFontBodyBackup = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Graphics graphicsMask((Gdiplus::Image*)(pBmpMask));
	Gdiplus::SolidBrush brushBlack(Gdiplus::Color(0,0,0));
	graphicsMask.FillRectangle(&brushBlack, 0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	Gdiplus::Bitmap* pBmpDisplay = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);
	Gdiplus::Graphics graphicsBkgd((Gdiplus::Image*)(pBmpDisplay));

	graphicsMask.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsMask.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsMask.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsMask.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsMask.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsMask.SetPageUnit(pGraphics->GetPageUnit());
	graphicsMask.SetPageScale(pGraphics->GetPageScale());

	graphicsBkgd.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsBkgd.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsBkgd.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsBkgd.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsBkgd.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsBkgd.SetPageUnit(pGraphics->GetPageUnit());
	graphicsBkgd.SetPageScale(pGraphics->GetPageScale());

	m_pFontBodyShadow->GdiDrawString(
		&graphicsMask, 
		pLogFont,
		pszText, 
		ptDraw );

	m_pShadowStrategy->GdiDrawString(		
		&graphicsBkgd, 
		pLogFont,
		pszText, 
		ptDraw );


	UINT* pixelsSrc = NULL;
	UINT* pixelsDest = NULL;
	UINT* pixelsMask = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataSrc;
	BitmapData bitmapDataDest;
	BitmapData bitmapDataMask;
	Rect rect(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	pBmpFontBodyBackup->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataSrc );

	pBmpDisplay->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataDest );

	pBmpMask->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataMask );


	// Write to the temporary buffer provided by LockBits.
	pixelsSrc = (UINT*)bitmapDataSrc.Scan0;
	pixelsDest = (UINT*)bitmapDataDest.Scan0;
	pixelsMask = (UINT*)bitmapDataMask.Scan0;

	if( !pixelsSrc || !pixelsDest || !pixelsMask)
		return;

	UINT col = 0;
	int stride = bitmapDataDest.Stride >> 2;
	if(m_bDiffuseShadow&&!m_bExtrudeShadow)
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
				{
					UINT clrtotal = clrShadow;
					for(int i=2;i<=m_nShadowThickness; ++i)
						pixelsSrc[index] = Alphablend(pixelsSrc[index],clrtotal,m_clrShadow.GetA());

					pixelsDest[index] = pixelsSrc[index];
				}
			}
		}
	}
	else
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
					pixelsDest[index] = Alphablend(pixelsSrc[index],clrShadow,m_clrShadow.GetA());
			}
		}
	}
	pBmpMask->UnlockBits(&bitmapDataMask);
	pBmpDisplay->UnlockBits(&bitmapDataDest);
	pBmpFontBodyBackup->UnlockBits(&bitmapDataSrc);

	pGraphics->DrawImage(pBmpDisplay,0,0,pBmpDisplay->GetWidth(),pBmpDisplay->GetHeight());

	if(pBmpMask)
	{
		delete pBmpMask;
		pBmpMask = NULL;
	}
	if(pBmpFontBodyBackup)
	{
		delete pBmpFontBodyBackup;
		pBmpFontBodyBackup = NULL;
	}
	if(pBmpDisplay)
	{
		delete pBmpDisplay;
		pBmpDisplay = NULL;
	}
}

void OutlineText::GdiRenderFontShadow(	
   Gdiplus::Graphics* pGraphics, 
   LOGFONTW* pLogFont,
   const wchar_t*pszText, 
   Gdiplus::Rect rtDraw)
{
	Gdiplus::Bitmap* pBmpMask = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Bitmap* pBmpFontBodyBackup = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);

	Gdiplus::Graphics graphicsMask((Gdiplus::Image*)(pBmpMask));
	Gdiplus::SolidBrush brushBlack(Gdiplus::Color(0,0,0));
	graphicsMask.FillRectangle(&brushBlack, 0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	Gdiplus::Bitmap* pBmpDisplay = 
		m_pBkgdBitmap->Clone(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight(), PixelFormat32bppARGB);
	Gdiplus::Graphics graphicsBkgd((Gdiplus::Image*)(pBmpDisplay));

	graphicsMask.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsMask.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsMask.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsMask.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsMask.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsMask.SetPageUnit(pGraphics->GetPageUnit());
	graphicsMask.SetPageScale(pGraphics->GetPageScale());

	graphicsBkgd.SetCompositingMode(pGraphics->GetCompositingMode());
	graphicsBkgd.SetCompositingQuality(pGraphics->GetCompositingQuality());
	graphicsBkgd.SetInterpolationMode(pGraphics->GetInterpolationMode());
	graphicsBkgd.SetSmoothingMode(pGraphics->GetSmoothingMode());
	graphicsBkgd.SetTextRenderingHint(pGraphics->GetTextRenderingHint());
	graphicsBkgd.SetPageUnit(pGraphics->GetPageUnit());
	graphicsBkgd.SetPageScale(pGraphics->GetPageScale());

	m_pFontBodyShadow->GdiDrawString(
		&graphicsMask, 
		pLogFont,
		pszText, 
		rtDraw );

	m_pShadowStrategy->GdiDrawString(		
		&graphicsBkgd, 
		pLogFont,
		pszText, 
		rtDraw);

	UINT* pixelsSrc = NULL;
	UINT* pixelsDest = NULL;
	UINT* pixelsMask = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataSrc;
	BitmapData bitmapDataDest;
	BitmapData bitmapDataMask;
	Rect rect(0, 0, m_pBkgdBitmap->GetWidth(), m_pBkgdBitmap->GetHeight() );

	pBmpFontBodyBackup->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataSrc );

	pBmpDisplay->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataDest );

	pBmpMask->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataMask );


	// Write to the temporary buffer provided by LockBits.
	pixelsSrc = (UINT*)bitmapDataSrc.Scan0;
	pixelsDest = (UINT*)bitmapDataDest.Scan0;
	pixelsMask = (UINT*)bitmapDataMask.Scan0;

	if( !pixelsSrc || !pixelsDest || !pixelsMask)
		return;

	UINT col = 0;
	int stride = bitmapDataDest.Stride >> 2;
	if(m_bDiffuseShadow&&!m_bExtrudeShadow)
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
				{
					UINT clrtotal = clrShadow;
					for(int i=2;i<=m_nShadowThickness; ++i)
						pixelsSrc[index] = Alphablend(pixelsSrc[index],clrtotal,m_clrShadow.GetA());

					pixelsDest[index] = pixelsSrc[index];
				}
			}
		}
	}
	else
	{
		for(UINT row = 0; row < bitmapDataDest.Height; ++row)
		{
			for(col = 0; col < bitmapDataDest.Width; ++col)
			{
				using namespace Gdiplus;
				UINT index = row * stride + col;
				BYTE nAlpha = pixelsMask[index] & 0xff;
				UINT clrShadow = 0xff000000 | m_clrShadow.GetR()<<16 | m_clrShadow.GetG()<<8 | m_clrShadow.GetB();
				if(nAlpha>0)
					pixelsDest[index] = Alphablend(pixelsSrc[index],clrShadow,m_clrShadow.GetA());
			}
		}
	}
	pBmpMask->UnlockBits(&bitmapDataMask);
	pBmpDisplay->UnlockBits(&bitmapDataDest);
	pBmpFontBodyBackup->UnlockBits(&bitmapDataSrc);

	pGraphics->DrawImage(pBmpDisplay,0,0,pBmpDisplay->GetWidth(),pBmpDisplay->GetHeight());

	if(pBmpMask)
	{
		delete pBmpMask;
		pBmpMask = NULL;
	}
	if(pBmpFontBodyBackup)
	{
		delete pBmpFontBodyBackup;
		pBmpFontBodyBackup = NULL;
	}
	if(pBmpDisplay)
	{
		delete pBmpDisplay;
		pBmpDisplay = NULL;
	}
}

inline UINT OutlineText::Alphablend(UINT dest, UINT source, BYTE nAlpha)
{
	if( 0 == nAlpha )
		return dest;

	if( 255 == nAlpha )
		return source;

	BYTE nInvAlpha = ~nAlpha;

	BYTE nSrcRed   = (source & 0xff0000) >> 16; 
	BYTE nSrcGreen = (source & 0xff00) >> 8; 
	BYTE nSrcBlue  = (source & 0xff); 

	BYTE nDestRed   = (dest & 0xff0000) >> 16; 
	BYTE nDestGreen = (dest & 0xff00) >> 8; 
	BYTE nDestBlue  = (dest & 0xff); 

	BYTE nRed  = ( nSrcRed   * nAlpha + nDestRed * nInvAlpha   )>>8;
	BYTE nGreen= ( nSrcGreen * nAlpha + nDestGreen * nInvAlpha )>>8;
	BYTE nBlue = ( nSrcBlue  * nAlpha + nDestBlue * nInvAlpha  )>>8;

	return 0xff000000 | nRed << 16 | nGreen << 8 | nBlue;
}

bool OutlineText::MeasureString(
	Gdiplus::Graphics* pGraphics, 
	Gdiplus::FontFamily* pFontFamily,
	Gdiplus::FontStyle fontStyle,
	int nfontSize,
	const wchar_t*pszText, 
	Gdiplus::Point ptDraw, 
	Gdiplus::StringFormat* pStrFormat,
	float* pfPixelsStartX,
	float* pfPixelsStartY,
	float* pfDestWidth,
	float* pfDestHeight )
{
	float fDestWidth1 = 0.0f;
	float fDestHeight1 = 0.0f;

	bool b = false;
	if(m_pTextStrategy)
	{
		b = m_pTextStrategy->MeasureString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			ptDraw, 
			pStrFormat,
			pfPixelsStartX,
			pfPixelsStartY,
			&fDestWidth1,
			&fDestHeight1 );

		if(!b)
			return false;
	}

	float fDestWidth2 = 0.0f;
	float fDestHeight2 = 0.0f;
	if(m_bEnableShadow)
	{
		bool b = m_pShadowStrategy->MeasureString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			ptDraw, 
			pStrFormat,
			NULL,
			NULL,
			&fDestWidth2,
			&fDestHeight2 );

		if(b)
		{
			float fDestWidth3 = 0.0f;
			float fDestHeight3 = 0.0f;
			b = GDIPath::ConvertToPixels(pGraphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				NULL,NULL,&fDestWidth3,&fDestHeight3);
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
		*pfDestWidth = fDestWidth1;
		*pfDestHeight = fDestHeight1;
	}
	else
	{
		*pfDestWidth = fDestWidth2;
		*pfDestHeight = fDestHeight2;
	}

	return true;
}

bool OutlineText::MeasureString(
	Gdiplus::Graphics* pGraphics, 
	Gdiplus::FontFamily* pFontFamily,
	Gdiplus::FontStyle fontStyle,
	int nfontSize,
	const wchar_t*pszText, 
	Gdiplus::Rect rtDraw,
	Gdiplus::StringFormat* pStrFormat,
	float* pfPixelsStartX,
	float* pfPixelsStartY,
	float* pfDestWidth,
	float* pfDestHeight )
{
	float fDestWidth1 = 0.0f;
	float fDestHeight1 = 0.0f;
	bool b = false;
	if(m_pTextStrategy)
	{
		b = m_pTextStrategy->MeasureString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			rtDraw, 
			pStrFormat,
			pfPixelsStartX,
			pfPixelsStartY,
			&fDestWidth1,
			&fDestHeight1 );

		if(!b)
			return false;
	}

	float fDestWidth2 = 0.0f;
	float fDestHeight2 = 0.0f;
	if(m_bEnableShadow)
	{
		bool b = m_pShadowStrategy->MeasureString(
			pGraphics, 
			pFontFamily,
			fontStyle,
			nfontSize,
			pszText, 
			rtDraw, 
			pStrFormat,
			NULL,
			NULL,
			&fDestWidth2,
			&fDestHeight2 );

		if(b)
		{
			float fDestWidth3 = 0.0f;
			float fDestHeight3 = 0.0f;
			b = GDIPath::ConvertToPixels(pGraphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				NULL,NULL,&fDestWidth3,&fDestHeight3);
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
		*pfDestWidth = fDestWidth1;
		*pfDestHeight = fDestHeight1;
	}
	else
	{
		*pfDestWidth = fDestWidth2;
		*pfDestHeight = fDestHeight2;
	}

	return true;
}

bool OutlineText::GdiMeasureString(
	Gdiplus::Graphics* pGraphics, 
	LOGFONTW* pLogFont,
	const wchar_t*pszText, 
	Gdiplus::Point ptDraw,
	float* pfPixelsStartX,
	float* pfPixelsStartY,
	float* pfDestWidth,
	float* pfDestHeight )
{
	float fDestWidth1 = 0.0f;
	float fDestHeight1 = 0.0f;
	bool b = false;
	if(m_pTextStrategy)
	{
		b = m_pTextStrategy->GdiMeasureString(
			pGraphics, 
			pLogFont,
			pszText, 
			ptDraw,
			pfPixelsStartX,
			pfPixelsStartY,
			&fDestWidth1,
			&fDestHeight1 );

		if(!b)
			return false;
	}

	float fDestWidth2 = 0.0f;
	float fDestHeight2 = 0.0f;
	if(m_bEnableShadow)
	{
		bool b = m_pShadowStrategy->GdiMeasureString(
			pGraphics, 
			pLogFont,
			pszText, 
			ptDraw,
			NULL,
			NULL,
			&fDestWidth2,
			&fDestHeight2 );

		if(b)
		{
			float fDestWidth3 = 0.0f;
			float fDestHeight3 = 0.0f;
			b = GDIPath::ConvertToPixels(pGraphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				NULL,NULL,&fDestWidth3,&fDestHeight3);
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
		*pfDestWidth = fDestWidth1;
		*pfDestHeight = fDestHeight1;
	}
	else
	{
		*pfDestWidth = fDestWidth2;
		*pfDestHeight = fDestHeight2;
	}

	return true;
}

bool OutlineText::GdiMeasureString(
	Gdiplus::Graphics* pGraphics, 
	LOGFONTW* pLogFont,
	const wchar_t*pszText, 
	Gdiplus::Rect rtDraw,
	float* pfPixelsStartX,
	float* pfPixelsStartY,
	float* pfDestWidth,
	float* pfDestHeight )
{
	float fDestWidth1 = 0.0f;
	float fDestHeight1 = 0.0f;
	bool b = false;
	if(m_pTextStrategy)
	{
		b = m_pTextStrategy->GdiMeasureString(
			pGraphics, 
			pLogFont,
			pszText, 
			rtDraw,
			pfPixelsStartX,
			pfPixelsStartY,
			&fDestWidth1,
			&fDestHeight1 );

		if(!b)
			return false;
	}

	float fDestWidth2 = 0.0f;
	float fDestHeight2 = 0.0f;
	if(m_bEnableShadow)
	{
		bool b = m_pShadowStrategy->GdiMeasureString(
			pGraphics, 
			pLogFont,
			pszText, 
			rtDraw,
			NULL,
			NULL,
			&fDestWidth2,
			&fDestHeight2 );

		if(b)
		{
			float fDestWidth3 = 0.0f;
			float fDestHeight3 = 0.0f;
			b = GDIPath::ConvertToPixels(pGraphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				NULL,NULL,&fDestWidth3,&fDestHeight3);
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
		*pfDestWidth = fDestWidth1;
		*pfDestHeight = fDestHeight1;
	}
	else
	{
		*pfDestWidth = fDestWidth2;
		*pfDestHeight = fDestHeight2;
	}

	return true;
}

bool OutlineText::GdiMeasureStringRealHeight(
	Gdiplus::Graphics* pGraphics, 
	LOGFONTW* pLogFont,
	const wchar_t*pszText, 
	Gdiplus::Point ptDraw,
	float* pfPixelsStartX,
	float* pfPixelsStartY,
	float* pfDestWidth,
	float* pfDestHeight )
{
	float fDestWidth1 = 0.0f;
	float fDestHeight1 = 0.0f;
	bool b = false;
	if(m_pTextStrategy)
	{
		b = m_pTextStrategy->GdiMeasureStringRealHeight(
			pGraphics, 
			pLogFont,
			pszText, 
			ptDraw,
			pfPixelsStartX,
			pfPixelsStartY,
			&fDestWidth1,
			&fDestHeight1 );

		if(!b)
			return false;
	}

	float fDestWidth2 = 0.0f;
	float fDestHeight2 = 0.0f;
	if(m_bEnableShadow)
	{
		bool b = m_pShadowStrategy->GdiMeasureStringRealHeight(
			pGraphics, 
			pLogFont,
			pszText, 
			ptDraw,
			NULL,
			NULL,
			&fDestWidth2,
			&fDestHeight2 );

		if(b)
		{
			float fDestWidth3 = 0.0f;
			float fDestHeight3 = 0.0f;
			b = GDIPath::ConvertToPixels(pGraphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				NULL,NULL,&fDestWidth3,&fDestHeight3);
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
		*pfDestWidth = fDestWidth1;
		*pfDestHeight = fDestHeight1;
	}
	else
	{
		*pfDestWidth = fDestWidth2;
		*pfDestHeight = fDestHeight2;
	}

	return true;
}

bool OutlineText::GdiMeasureStringRealHeight(
	Gdiplus::Graphics* pGraphics, 
	LOGFONTW* pLogFont,
	const wchar_t*pszText, 
	Gdiplus::Rect rtDraw,
	float* pfPixelsStartX,
	float* pfPixelsStartY,
	float* pfDestWidth,
	float* pfDestHeight )
{
	float fDestWidth1 = 0.0f;
	float fDestHeight1 = 0.0f;
	bool b = false;
	if(m_pTextStrategy)
	{
		b = m_pTextStrategy->GdiMeasureStringRealHeight(
			pGraphics, 
			pLogFont,
			pszText, 
			rtDraw,
			pfPixelsStartX,
			pfPixelsStartY,
			&fDestWidth1,
			&fDestHeight1 );

		if(!b)
			return false;
	}

	float fDestWidth2 = 0.0f;
	float fDestHeight2 = 0.0f;
	if(m_bEnableShadow)
	{
		bool b = m_pShadowStrategy->GdiMeasureStringRealHeight(
			pGraphics, 
			pLogFont,
			pszText, 
			rtDraw,
			NULL,
			NULL,
			&fDestWidth2,
			&fDestHeight2 );

		if(b)
		{
			float fDestWidth3 = 0.0f;
			float fDestHeight3 = 0.0f;
			b = GDIPath::ConvertToPixels(pGraphics,m_ptShadowOffset.X,m_ptShadowOffset.Y,
				NULL,NULL,&fDestWidth3,&fDestHeight3);
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
		*pfDestWidth = fDestWidth1;
		*pfDestHeight = fDestHeight1;
	}
	else
	{
		*pfDestWidth = fDestWidth2;
		*pfDestHeight = fDestHeight2;
	}

	return true;
}
