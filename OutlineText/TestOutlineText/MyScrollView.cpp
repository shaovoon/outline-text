// MyScrollView.cpp : implementation file
//

#include "stdafx.h"
#include "MyScrollView.h"

// CMyScrollView

IMPLEMENT_DYNCREATE(CMyScrollView, CScrollView)

CMyScrollView::CMyScrollView()
:
m_pImage(NULL),
m_clrBkgd(255,255,255),
m_clrText(255,255,255),
m_clrText2(255,255,255),
m_clrOutline1(0,0,0),
m_clrOutline2(0,0,0),
m_TextEffect(SingleOutline),
m_TextPath(GdiPlus),
m_nOutlineAlpha1(255),
m_nOutlineAlpha2(255),
m_nOutlineThickness1(4),
m_nOutlineThickness2(4),
m_nTextPosX(10),
m_nTextPosY(10),
m_nTextAngle(0),
m_nFontSize(10),
m_nShadowOffsetX(4),
m_nShadowOffsetY(4),
m_nShadowAlpha(128),
m_bEnableShadow(false),
m_pShadowBkgdBmp(NULL),
m_clrShadow(128,128,128),
m_nShadowThickness(4),
m_pPngImage(NULL),
m_bPngImage(true),
m_bTextGradient(false),
m_bDirty(true),
m_bCorrectPngRendering(false),
m_pBkgdBmp(NULL),
m_bDiffusedShadow(false),
m_bExtrudedText(false),
m_pGradientBrush(NULL),
m_fStartAlpha(0.4f),
m_fEndAlpha(0.01f),
m_fShown(0.7f),
m_nGap(0),
m_bEnableReflection(false)
{

}

CMyScrollView::~CMyScrollView()
{
}


BEGIN_MESSAGE_MAP(CMyScrollView, CScrollView)
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()


// CMyScrollView drawing

void CMyScrollView::OnInitialUpdate()
{
	CScrollView::OnInitialUpdate();

	CSize sizeTotal;
	sizeTotal.cx = 700;
	sizeTotal.cy = 300;
	SetScrollSizes(MM_TEXT, sizeTotal);
}

void CMyScrollView::OnDraw(CDC* pDC)
{
	CDocument* pDoc = GetDocument();
	
	using namespace Gdiplus;
	CRect rect;
	GetClientRect(rect);
	CBrush brush(RGB(m_clrBkgd.GetR(),m_clrBkgd.GetG(),m_clrBkgd.GetB())) ;
	pDC->FillRect(&rect,&brush);

	if(m_bRender)
	{
		if(m_bPngImage)
		{
			TRACE(_T("Start of Text Rendering\n"));
			Graphics graphics(pDC->GetSafeHdc());
			graphics.SetSmoothingMode(SmoothingModeAntiAlias);
			graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);
			graphics.SetPageUnit(UnitPixel);

			if(m_pImage)
			{
				graphics.DrawImage(m_pImage.get(),0,0,m_pImage->GetWidth(),m_pImage->GetHeight());
			}

			SetTextEffect(&m_PngOutlineText, NULL);

			if(m_bEnableReflection)
			{
				m_PngOutlineText.EnableReflection(true);
				m_PngOutlineText.Reflection(m_fStartAlpha, m_fEndAlpha, m_fShown);
			}
			else
				m_PngOutlineText.EnableReflection(false);

			m_PngOutlineText.EnableShadow(m_bEnableShadow);
			if(m_bEnableShadow)
			{
				if(m_bExtrudedText)
				{
					m_PngOutlineText.Extrude(
						Gdiplus::Color(m_nShadowAlpha,m_clrShadow.GetR(),m_clrShadow.GetG(),m_clrShadow.GetB()), 
						m_nShadowThickness,
						Gdiplus::Point(m_nShadowOffsetX,m_nShadowOffsetY));
				}
				else if(m_bDiffusedShadow)
				{
					m_PngOutlineText.DiffusedShadow(
						Gdiplus::Color(m_nShadowAlpha,m_clrShadow.GetR(),m_clrShadow.GetG(),m_clrShadow.GetB()), 
						m_nShadowThickness,
						Gdiplus::Point(m_nShadowOffsetX,m_nShadowOffsetY));
				}
				else
				{
					m_PngOutlineText.Shadow(
						Gdiplus::Color(m_nShadowAlpha,m_clrShadow.GetR(),m_clrShadow.GetG(),m_clrShadow.GetB()), 
						m_nShadowThickness,
						Gdiplus::Point(m_nShadowOffsetX,m_nShadowOffsetY));
				}

				//if(m_pImage)
				//{
				//	m_PngOutlineText.SetShadowBkgd(m_pImage);
				//}
				//else
				//{
					//CRect rect;
					//this->GetClientRect(&rect);
					//m_PngOutlineText.SetShadowBkgd(
					//	Gdiplus::Color(m_clrBkgd.GetR(),m_clrBkgd.GetG(),m_clrBkgd.GetB()),
					//	GetTotalSize().cx,GetTotalSize().cy);
				//}
			}

			CString szDbgFont=m_LogFont.lfFaceName; 
			wchar_t buf[20];
			memset(buf,0,sizeof(buf));
			_itow_s(m_nFontSize,buf,10);
			szDbgFont += _T(" ");
			szDbgFont += buf;
			if(m_LogFont.lfWeight==FW_BOLD&&m_LogFont.lfItalic)
			{
				szDbgFont += _T(" Bold Italic\n");
			}
			else if(m_LogFont.lfWeight==FW_BOLD)
			{
				szDbgFont += _T(" Bold\n");
			}
			else if(m_LogFont.lfItalic)
			{
				szDbgFont += _T(" Italic\n");
			}
			else
			{
				szDbgFont += _T(" Regular\n");
			}
			TRACE(szDbgFont);
			if(m_TextPath==GdiPlus)
			{
				TRACE(_T("GDI+ Path\n"));
				FontFamily fontFamily(m_LogFont.lfFaceName);
				FontStyle fontStyle = FontStyleRegular;
				if(m_LogFont.lfWeight==FW_BOLD&&m_LogFont.lfItalic)
				{
					fontStyle = FontStyleBoldItalic;
				}
				else if(m_LogFont.lfWeight==FW_BOLD)
				{
					fontStyle = FontStyleBold;
				}
				else if(m_LogFont.lfItalic)
				{
					fontStyle = FontStyleItalic;
				}
				else
				{
					fontStyle = FontStyleRegular;
				}

				StringFormat strFormat;
				//if(m_bDirty == false&&m_pPngImage)
				//{
				//	graphics.DrawImage(m_pPngImage,0,0,m_pPngImage->GetWidth(),m_pPngImage->GetHeight());
				//	TRACE(_T("End of Text Rendering\n");
				//	return;
				//}

				float fWidth=0.0f;
				float fHeight=0.0f;
				m_PngOutlineText.MeasureString(&graphics,&fontFamily,fontStyle, 
					m_nFontSize, m_szText, Gdiplus::Point(0,0), &strFormat,
					NULL, NULL, &fWidth, &fHeight);
				if(m_bDirty)
					m_pPngImage = std::shared_ptr<Bitmap>(new Bitmap(fWidth, fHeight, PixelFormat32bppARGB));

				if(!m_pPngImage)
					return;

				//CSize size = GetTotalSize();
				//m_pPngImage = new Gdiplus::Bitmap(size.cx,size.cy,PixelFormat32bppARGB);
				m_PngOutlineText.SetPngImage(m_pPngImage);
				m_PngOutlineText.SetShadowBkgd(
					Gdiplus::Color(m_clrBkgd.GetR(),m_clrBkgd.GetG(),m_clrBkgd.GetB()),
					m_pPngImage->GetWidth(), m_pPngImage->GetHeight());

				if(!m_bEnableShadow)
				{
					if(m_bDirty)
					{
						if(m_bTextGradient)
						{
							float fStartX = 0.0f;
							float fStartY = 0.0f;
							float fDestWidth = 0.0f;
							float fDestHeight = 0.0f;
							m_PngOutlineText.MeasureString(
								&graphics,
								&fontFamily,
								fontStyle,
								m_nFontSize,
								m_szText,
								Gdiplus::Point(0, 0),
								&strFormat,
								&fStartX,
								&fStartY,
								&fDestWidth,
								&fDestHeight);

							if(m_pGradientBrush)
							{
								//delete m_pGradientBrush;
								m_pGradientBrush = NULL;
							}

							m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
								fDestWidth - fStartX, fDestHeight - fStartY),
								Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
								Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
								LinearGradientModeVertical));

							SetTextEffect(&m_PngOutlineText, m_pGradientBrush.get());
					    }
						m_PngOutlineText.DrawString(
							&graphics,&fontFamily,fontStyle,m_nFontSize,m_szText,Gdiplus::Point(0,0), &strFormat);
					}
					graphics.DrawImage(m_pPngImage.get(), (float)m_nTextPosX, (float)m_nTextPosY, 
						(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());

					if(m_bEnableReflection)
					{
						std::shared_ptr<Bitmap> pReflectBmp = m_PngOutlineText.GetReflectionPngImage();
						if(pReflectBmp)
						{
							graphics.DrawImage(pReflectBmp.get(), (float)m_nTextPosX, (float)m_nTextPosY+pReflectBmp->GetHeight()+m_nGap, 
								(float)pReflectBmp->GetWidth(), (float)pReflectBmp->GetHeight());
						}
					}
				}
				else
				{
					int nShadowOffsetX = 0;
					if(m_nShadowOffsetX<0)
						nShadowOffsetX = -m_nShadowOffsetX;
					int nShadowOffsetY = 0;
					if(m_nShadowOffsetY<0)
						nShadowOffsetY = -m_nShadowOffsetY;
					if(m_bDirty)
					{
						if(m_bTextGradient)
						{
							float fStartX = 0.0f;
							float fStartY = 0.0f;
							float fDestWidth = 0.0f;
							float fDestHeight = 0.0f;
							m_PngOutlineText.MeasureString(
								&graphics,
								&fontFamily,
								fontStyle,
								m_nFontSize,
								m_szText,
								Gdiplus::Point(0,0),
								&strFormat,
								&fStartX,
								&fStartY,
								&fDestWidth,
								&fDestHeight);

							if(m_pGradientBrush)
							{
								//delete m_pGradientBrush;
								m_pGradientBrush = NULL;
							}

							m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY,
								fDestWidth - fStartX, fDestHeight - fStartY),
								Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
								Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
								LinearGradientModeVertical));

							SetTextEffect(&m_PngOutlineText, m_pGradientBrush.get());
						}
						m_PngOutlineText.DrawString(
							&graphics,&fontFamily,fontStyle,m_nFontSize,m_szText,Gdiplus::Point(nShadowOffsetX,nShadowOffsetY), &strFormat);
					}
					graphics.DrawImage(m_pPngImage.get(), (float)(m_nTextPosX-nShadowOffsetX), (float)(m_nTextPosY-nShadowOffsetY), 
						(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());

					if(m_bEnableReflection)
					{
						std::shared_ptr<Bitmap> pReflectBmp = m_PngOutlineText.GetReflectionPngImage();
						if(pReflectBmp)
						{
							graphics.DrawImage(pReflectBmp.get(), (float)m_nTextPosX, (float)m_nTextPosY+pReflectBmp->GetHeight()+m_nGap, 
								(float)pReflectBmp->GetWidth(), (float)pReflectBmp->GetHeight());
						}
					}
				}
				//m_PngOutlineText.SavePngFile(_T("d:\\MyPngImage.png");
			}
			else
			{
				TRACE(_T("GDI Path\n"));
				//if(m_bDirty == false&&m_pPngImage)
				//{
				//	graphics.DrawImage(m_pPngImage,0,0,m_pPngImage->GetWidth(),m_pPngImage->GetHeight());
				//	TRACE(_T("End of Text Rendering\n");
				//	return;
				//}

				m_LogFont.lfOrientation = m_nTextAngle;
				m_LogFont.lfEscapement = m_nTextAngle;

				float fWidthBig=0.0f;
				float fHeightBig=0.0f;
				float fWidth=0.0f;
				float fHeight=0.0f;
				if(m_nTextAngle!=0)
				{
					m_LogFont.lfOrientation = 0;
					m_LogFont.lfEscapement = 0;
					m_PngOutlineText.GdiMeasureStringRealHeight(&graphics, &m_LogFont, m_szText,Gdiplus::Point(0,0),
						NULL, NULL, &fWidth, &fHeight);
					m_LogFont.lfOrientation = m_nTextAngle;
					m_LogFont.lfEscapement = m_nTextAngle;
					m_PngOutlineText.GdiMeasureString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(0,0),
						NULL, NULL, &fWidthBig, &fHeightBig);
					if(m_bDirty)
						m_pPngImage = std::shared_ptr<Gdiplus::Bitmap>(new Bitmap(fWidthBig+10.0f, fHeightBig+10.0f, PixelFormat32bppARGB));
				}
				else
				{
					m_PngOutlineText.GdiMeasureString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(0,0),
						NULL, NULL, &fWidth, &fHeight);
					if(!m_pPngImage)
						m_pPngImage = std::shared_ptr<Gdiplus::Bitmap>(new Bitmap(fWidth, fHeight, PixelFormat32bppARGB));
				}

				if(!m_pPngImage)
					return;

				//CSize size = GetTotalSize();
				//m_pPngImage = new Gdiplus::Bitmap(size.cx,size.cy,PixelFormat32bppARGB);
				m_PngOutlineText.SetPngImage(m_pPngImage);
				m_PngOutlineText.SetShadowBkgd(
					Gdiplus::Color(m_clrBkgd.GetR(),m_clrBkgd.GetG(),m_clrBkgd.GetB()),
					m_pPngImage->GetWidth(), m_pPngImage->GetHeight());

				if(!m_bEnableShadow)
				{
					if(m_nTextAngle!=0)
					{
						if(m_bDirty)
						{
							if(m_bTextGradient)
							{
								float fStartX = 0.0f;
								float fStartY = 0.0f;
								float fDestWidth = 0.0f;
								float fDestHeight = 0.0f;
								m_PngOutlineText.GdiMeasureString(
									&graphics,
									&m_LogFont,
									m_szText,
									Gdiplus::Point(0,0),
									&fStartX,
									&fStartY,
									&fDestWidth,
									&fDestHeight);

								m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
									fDestWidth - fStartX, fDestHeight - fStartY),
									Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
									Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
									LinearGradientModeVertical));

								SetTextEffect(&m_PngOutlineText, m_pGradientBrush.get());							}
							m_PngOutlineText.GdiDrawString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(0,fHeightBig-fHeight-5.0f));
						}
						//SolidBrush brGreen(Color(0,255,0));
						//graphics.FillRectangle(&brGreen,(float)m_nTextPosX, (float)(m_nTextPosY-(fHeightBig-fHeight-5.0f)), 
						//	(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());
						graphics.DrawImage(m_pPngImage.get(), (float)m_nTextPosX, (float)(m_nTextPosY-(fHeightBig-fHeight-5.0f)), 
							(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());

						if(m_bEnableReflection)
						{
							std::shared_ptr<Bitmap> pReflectBmp = m_PngOutlineText.GetReflectionPngImage();
							if(pReflectBmp)
							{
								graphics.DrawImage(pReflectBmp.get(), (float)m_nTextPosX, (float)m_nTextPosY-(fHeightBig-fHeight-5.0f)+pReflectBmp->GetHeight()+m_nGap, 
									(float)pReflectBmp->GetWidth(), (float)pReflectBmp->GetHeight());
							}
						}

					}
					else
					{
						if(m_bDirty)
						{
							if(m_bTextGradient)
							{
								float fStartX = 0.0f;
								float fStartY = 0.0f;
								float fDestWidth = 0.0f;
								float fDestHeight = 0.0f;
								m_PngOutlineText.GdiMeasureString(
									&graphics,
									&m_LogFont,
									m_szText,
									Gdiplus::Point(0, 0),
									&fStartX,
									&fStartY,
									&fDestWidth,
									&fDestHeight);

								if(m_pGradientBrush)
								{
									//delete m_pGradientBrush;
									m_pGradientBrush = NULL;
								}

								m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
									fDestWidth - fStartX, fDestHeight - fStartY),
									Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
									Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
									LinearGradientModeVertical));

								SetTextEffect(&m_PngOutlineText, m_pGradientBrush.get());
							}
							m_PngOutlineText.GdiDrawString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(0,0));
						}
						graphics.DrawImage(m_pPngImage.get(), (float)m_nTextPosX, (float)m_nTextPosY, 
							(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());

						if(m_bEnableReflection)
						{
							std::shared_ptr<Bitmap> pReflectBmp = m_PngOutlineText.GetReflectionPngImage();
							if(pReflectBmp)
							{
								graphics.DrawImage(pReflectBmp.get(), (float)m_nTextPosX, (float)m_nTextPosY+pReflectBmp->GetHeight()+m_nGap, 
									(float)pReflectBmp->GetWidth(), (float)pReflectBmp->GetHeight());
							}
						}
					}
				}
				else
				{
					if(m_nTextAngle!=0)
					{
						int nShadowOffsetX = 0;
						if(m_nShadowOffsetX<0)
							nShadowOffsetX = -m_nShadowOffsetX;
						int nShadowOffsetY = 0;
						if(m_nShadowOffsetY<0)
							nShadowOffsetY = -m_nShadowOffsetY;
						if(m_bDirty)
						{
							if(m_bTextGradient)
							{
								float fStartX = 0.0f;
								float fStartY = 0.0f;
								float fDestWidth = 0.0f;
								float fDestHeight = 0.0f;
								m_PngOutlineText.GdiMeasureString(
									&graphics,
									&m_LogFont,
									m_szText,
									Gdiplus::Point(nShadowOffsetX,nShadowOffsetY),
									&fStartX,
									&fStartY,
									&fDestWidth,
									&fDestHeight);

								if(m_pGradientBrush)
								{
									//delete m_pGradientBrush;
									m_pGradientBrush = NULL;
								}

								m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
									fDestWidth - fStartX, fDestHeight - fStartY),
									Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
									Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
									LinearGradientModeVertical));

								SetTextEffect(&m_PngOutlineText, m_pGradientBrush.get());
							}
							m_PngOutlineText.GdiDrawString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(nShadowOffsetX,nShadowOffsetY+(fHeightBig-fHeight-5.0f)));
						}
						graphics.DrawImage(m_pPngImage.get(), (float)(m_nTextPosX-nShadowOffsetX), (float)(m_nTextPosY-nShadowOffsetY-(fHeightBig-fHeight-5.0f)), 
							(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());

						if(m_bEnableReflection)
						{
							std::shared_ptr<Bitmap> pReflectBmp = m_PngOutlineText.GetReflectionPngImage();
							if(pReflectBmp)
							{
								graphics.DrawImage(pReflectBmp.get(), (float)(m_nTextPosX-nShadowOffsetX), (float)m_nTextPosY-nShadowOffsetY-(fHeightBig-fHeight-5.0f)+pReflectBmp->GetHeight()+m_nGap, 
									(float)pReflectBmp->GetWidth(), (float)pReflectBmp->GetHeight());
							}
						}

					}
					else
					{
						int nShadowOffsetX = 0;
						if(m_nShadowOffsetX<0)
							nShadowOffsetX = -m_nShadowOffsetX;
						int nShadowOffsetY = 0;
						if(m_nShadowOffsetY<0)
							nShadowOffsetY = -m_nShadowOffsetY;
						if(m_bDirty)
						{
							if(m_bTextGradient)
							{
								float fStartX = 0.0f;
								float fStartY = 0.0f;
								float fDestWidth = 0.0f;
								float fDestHeight = 0.0f;
								m_PngOutlineText.GdiMeasureString(
									&graphics,
									&m_LogFont,
									m_szText,
									Gdiplus::Point(nShadowOffsetX,nShadowOffsetY),
									&fStartX,
									&fStartY,
									&fDestWidth,
									&fDestHeight);

								if(m_pGradientBrush)
								{
									//delete m_pGradientBrush;
									m_pGradientBrush = NULL;
								}

								m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
									fDestWidth - fStartX, fDestHeight - fStartY),
									Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
									Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
									LinearGradientModeVertical));

								SetTextEffect(&m_PngOutlineText, m_pGradientBrush.get());							}
							m_PngOutlineText.GdiDrawString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(nShadowOffsetX,nShadowOffsetY));
						}
						graphics.DrawImage(m_pPngImage.get(), (float)(m_nTextPosX-nShadowOffsetX), (float)(m_nTextPosY-nShadowOffsetY), 
							(float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());

						if(m_bEnableReflection)
						{
							std::shared_ptr<Bitmap> pReflectBmp = m_PngOutlineText.GetReflectionPngImage();
							if(pReflectBmp)
							{
								graphics.DrawImage(pReflectBmp.get(), (float)(m_nTextPosX-nShadowOffsetX), (float)m_nTextPosY-nShadowOffsetY+pReflectBmp->GetHeight()+m_nGap, 
									(float)pReflectBmp->GetWidth(), (float)pReflectBmp->GetHeight());
							}
						}

					}
				}
				//m_PngOutlineText.SavePngFile(_T("d:\\MyPngImage.png");
			}
			m_bDirty = false;
			TRACE(_T("End of Text Rendering\n"));
		}
		else
		{
			TRACE(_T("Start of Text Rendering\n"));
			Graphics graphics(pDC->GetSafeHdc());
			graphics.SetSmoothingMode(SmoothingModeAntiAlias);
			graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);
			if(m_pImage)
			{
				graphics.DrawImage(m_pImage.get(),0,0,m_pImage->GetWidth(),m_pImage->GetHeight());
			}

			SetTextEffect(&m_OutlineText, NULL);

			m_OutlineText.EnableShadow(m_bEnableShadow);
			if(m_bEnableShadow)
			{
				if(m_bExtrudedText)
				{
					m_OutlineText.Extrude(
						Gdiplus::Color(m_nShadowAlpha,m_clrShadow.GetR(),m_clrShadow.GetG(),m_clrShadow.GetB()), 
						m_nShadowThickness,
						Gdiplus::Point(m_nShadowOffsetX,m_nShadowOffsetY));
				}
				else if(m_bDiffusedShadow)
				{
					m_OutlineText.DiffusedShadow(
						Gdiplus::Color(m_nShadowAlpha,m_clrShadow.GetR(),m_clrShadow.GetG(),m_clrShadow.GetB()), 
						m_nShadowThickness,
						Gdiplus::Point(m_nShadowOffsetX,m_nShadowOffsetY));
				}
				else
				{
					m_OutlineText.Shadow(
						Gdiplus::Color(m_nShadowAlpha,m_clrShadow.GetR(),m_clrShadow.GetG(),m_clrShadow.GetB()), 
						m_nShadowThickness,
						Gdiplus::Point(m_nShadowOffsetX,m_nShadowOffsetY));
				}

				if(m_pImage)
				{
					m_OutlineText.SetShadowBkgd(m_pImage);
				}
				else
				{
					//CRect rect;
					//this->GetClientRect(&rect);
					m_OutlineText.SetShadowBkgd(
						Gdiplus::Color(m_clrBkgd.GetR(),m_clrBkgd.GetG(),m_clrBkgd.GetB()),
						GetTotalSize().cx,GetTotalSize().cy);
				}
			}

			CString szDbgFont=m_LogFont.lfFaceName; 
			wchar_t buf[20];
			memset(buf,0,sizeof(buf));
			_itow_s(m_nFontSize,buf,10);
			szDbgFont += _T(" ");
			szDbgFont += buf;
			if(m_LogFont.lfWeight==FW_BOLD&&m_LogFont.lfItalic)
			{
				szDbgFont += _T(" Bold Italic\n");
			}
			else if(m_LogFont.lfWeight==FW_BOLD)
			{
				szDbgFont += _T(" Bold\n");
			}
			else if(m_LogFont.lfItalic)
			{
				szDbgFont += _T(" Italic\n");
			}
			else
			{
				szDbgFont += _T(" Regular\n");
			}
			TRACE(szDbgFont);
			if(m_TextPath==GdiPlus)
			{
				TRACE(_T("GDI+ Path\n"));
				FontFamily fontFamily(m_LogFont.lfFaceName);
				FontStyle fontStyle = FontStyleRegular;
				if(m_LogFont.lfWeight==FW_BOLD&&m_LogFont.lfItalic)
				{
					fontStyle = FontStyleBoldItalic;
				}
				else if(m_LogFont.lfWeight==FW_BOLD)
				{
					fontStyle = FontStyleBold;
				}
				else if(m_LogFont.lfItalic)
				{
					fontStyle = FontStyleItalic;
				}
				else
				{
					fontStyle = FontStyleRegular;
				}

				StringFormat strFormat;

				if(m_bTextGradient)
				{
					float fStartX = 0.0f;
					float fStartY = 0.0f;
					float fDestWidth = 0.0f;
					float fDestHeight = 0.0f;
					m_OutlineText.MeasureString(
						&graphics,
						&fontFamily,
						fontStyle,
						m_nFontSize,
						m_szText,
						Gdiplus::Point(m_nTextPosX, m_nTextPosY),
						&strFormat,
						&fStartX,
						&fStartY,
						&fDestWidth,
						&fDestHeight);

					if(m_pGradientBrush)
					{
						//delete m_pGradientBrush;
						m_pGradientBrush = NULL;
					}

					m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
						fDestWidth - fStartX, fDestHeight - fStartY),
						Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
						Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
						LinearGradientModeVertical));

					//graphics.FillRectangle(m_pGradientBrush, Gdiplus::Rect(fStartX, fStartY, 
					//	fDestWidth - (fStartX - m_nTextPosX), fDestHeight - (fStartY - m_nTextPosY)));

					SetTextEffect(&m_OutlineText, m_pGradientBrush.get());
				}

				m_OutlineText.DrawString(
					&graphics,&fontFamily,fontStyle,m_nFontSize,m_szText,Gdiplus::Point(m_nTextPosX,m_nTextPosY), &strFormat);
			}
			else
			{
				TRACE(_T("GDI Path\n"));
				m_LogFont.lfOrientation = m_nTextAngle;
				m_LogFont.lfEscapement = m_nTextAngle;
				if(m_bTextGradient)
				{
					float fStartX = 0.0f;
					float fStartY = 0.0f;
					float fDestWidth = 0.0f;
					float fDestHeight = 0.0f;
					m_OutlineText.GdiMeasureString(
						&graphics,
						&m_LogFont,
						m_szText,
						Gdiplus::Point(m_nTextPosX, m_nTextPosY),
						&fStartX,
						&fStartY,
						&fDestWidth,
						&fDestHeight);

					if(m_pGradientBrush)
					{
						//delete m_pGradientBrush;
						m_pGradientBrush = NULL;
					}

					m_pGradientBrush = std::shared_ptr<LinearGradientBrush>(new LinearGradientBrush(Gdiplus::Rect(fStartX, fStartY, 
						fDestWidth - (fStartX - m_nTextPosX), fDestHeight - (fStartY - m_nTextPosY)),
						Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
						Color(m_clrText2.GetR(),m_clrText2.GetG(),m_clrText2.GetB()),
						LinearGradientModeVertical));

					SetTextEffect(&m_OutlineText, m_pGradientBrush.get());
				}
				m_OutlineText.GdiDrawString(&graphics, &m_LogFont, m_szText,Gdiplus::Point(m_nTextPosX,m_nTextPosY));
			}
			m_bDirty = false;
			TRACE(_T("End of Text Rendering\n"));
		}
	}
}


// CMyScrollView diagnostics

#ifdef _DEBUG
void CMyScrollView::AssertValid() const
{
	CScrollView::AssertValid();
}

#ifndef _WIN32_WCE
void CMyScrollView::Dump(CDumpContext& dc) const
{
	CScrollView::Dump(dc);
}
#endif
#endif //_DEBUG

CString CMyScrollView::GetImage()
{
	return m_szImageFile;
}

bool CMyScrollView::SetImage(const wchar_t* pszFile)
{
	m_szImageFile = pszFile;

	m_pImage = std::shared_ptr<Gdiplus::Bitmap>(Gdiplus::Bitmap::FromFile(pszFile));

	CSize sizeTotal;
	sizeTotal.cx = m_pImage->GetWidth();
	sizeTotal.cy = m_pImage->GetHeight();
	SetScrollSizes(MM_TEXT, sizeTotal);

	m_bDirty = true;

	Invalidate(FALSE);
	
	return m_pImage != NULL;
}

Gdiplus::Color CMyScrollView::GetBkgdColor()
{
	return m_clrBkgd;
}

void CMyScrollView::SetBkgdColor(Gdiplus::Color color)
{
	if(m_pImage)
	{
		m_pImage = NULL;
	}

	m_clrBkgd = color;
	m_bDirty = true;
	Invalidate(FALSE);
}

Gdiplus::Color CMyScrollView::GetTextColor1()
{
	return m_clrText;
}

void CMyScrollView::SetTextColor1(Gdiplus::Color color)
{
	if(m_clrText.GetValue() == color.GetValue())
		return;

	m_clrText = color;
	m_bDirty = true;
	Invalidate(FALSE);
}

Gdiplus::Color CMyScrollView::GetTextColor2()
{
	return m_clrText2;
}

void CMyScrollView::SetTextColor2(Gdiplus::Color color)
{
	if(m_clrText2.GetValue() == color.GetValue())
		return;

	m_clrText2 = color;
	m_bDirty = true;
	Invalidate(FALSE);
}

Gdiplus::Color CMyScrollView::GetOutlineColor1()
{
	return m_clrOutline1;
}

void CMyScrollView::SetOutlineColor1(Gdiplus::Color color)
{
	if(m_clrOutline1.GetValue() == color.GetValue())
		return;

	m_clrOutline1 = color;
	m_bDirty = true;
	Invalidate(FALSE);
}

Gdiplus::Color CMyScrollView::GetOutlineColor2()
{
	return m_clrOutline2;
}

void CMyScrollView::SetOutlineColor2(Gdiplus::Color color)
{
	if(m_clrOutline2.GetValue() == color.GetValue())
		return;

	m_clrOutline2 = color;
	m_bDirty = true;
	Invalidate(FALSE);
}

BOOL CMyScrollView::OnEraseBkgnd(CDC* pDC)
{
	CRect rect;
	GetClientRect(rect);
	CBrush brush(RGB(m_clrBkgd.GetR(),m_clrBkgd.GetG(),m_clrBkgd.GetB()));
	pDC->FillRect(&rect,&brush);
	return TRUE;

	//return CScrollView::OnEraseBkgnd(pDC);
}

TextEffect CMyScrollView::GetTextEffect()
{
	return m_TextEffect;
}

void CMyScrollView::SetTextEffect(TextEffect textEffect)
{
	if(m_TextEffect == textEffect)
		return;

	m_TextEffect = textEffect;
	m_bDirty = true;
	Invalidate(FALSE);
}

TextPath CMyScrollView::GetTextPath()
{
	return m_TextPath;
}

void CMyScrollView::SetTextPath(TextPath textPath)
{
	if(m_TextPath == textPath)
		return;

	m_TextPath = textPath;
	m_bDirty = true;
	Invalidate(FALSE);
}

CString CMyScrollView::GetText()
{
	return m_szText;
}

void CMyScrollView::SetText(const CString& str)
{
    CString WorkString;
    int StrLen = str.GetLength ();
    int i = 0;
    for ( i = 0; i < StrLen; ++ i )
    {
        if (
            ( _T('\\') == str [i] )
            && ( i + 2 < StrLen )
            && (
                ( _T('n') == str [i + 1] )
                || ( _T('r') == str [i + 1] )
                || ( _T('t') == str [i + 1] )
                )
            )
        {
            switch (str [i + 1])
            {
            case _T('n'):
                {
                    WorkString += _T('\n');
                }
                break;
            case _T('r'):
                {
                    WorkString += _T('\r');
                }
                break;
            case _T('t'):
                {
                    WorkString += _T('\t');
                }
                break;
            }
            i += 1;
        }
        else
        {
            WorkString += str [i];
        }
    }

    m_szText = WorkString;
	m_bDirty = true;
	Invalidate(FALSE);
}

void CMyScrollView::Render(bool bEnable)
{
	if(m_bRender==bEnable)
		return;
	
	m_bRender=bEnable;
	if(m_bRender)
		Invalidate(FALSE);
}

BYTE CMyScrollView::GetOutlineAlpha1()
{
	return m_nOutlineAlpha1;
}

void CMyScrollView::SetOutlineAlpha1(BYTE nAlpha)
{
	if(m_nOutlineAlpha1 == nAlpha)
		return;

	m_nOutlineAlpha1 = nAlpha;
	m_bDirty = true;
	Invalidate(FALSE);
}

BYTE CMyScrollView::GetOutlineAlpha2()
{
	return m_nOutlineAlpha2;
}

void CMyScrollView::SetOutlineAlpha2(BYTE nAlpha)
{
	if(m_nOutlineAlpha2 == nAlpha)
		return;

	m_nOutlineAlpha2 = nAlpha;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetOutlineThickness1()
{
	return m_nOutlineThickness1;
}

void CMyScrollView::SetOutlineThickness1(int nThick1)
{
	if(m_nOutlineThickness1 == nThick1)
		return;

	m_nOutlineThickness1 = nThick1;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetOutlineThickness2()
{
	return m_nOutlineThickness2;
}

void CMyScrollView::SetOutlineThickness2(int nThick2)
{
	if(m_nOutlineThickness2 == nThick2)
		return;

	m_nOutlineThickness2 = nThick2;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetTextPosX()
{
	return m_nTextPosX;
}

void CMyScrollView::SetTextPosX(int nTextPosX)
{
	if(m_nTextPosX == nTextPosX)
		return;

	m_nTextPosX = nTextPosX;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetTextPosY()
{
	return m_nTextPosY;
}

void CMyScrollView::SetTextPosY(int nTextPosY)
{
	if(m_nTextPosY == nTextPosY)
		return;

	m_nTextPosY = nTextPosY;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetTextAngle()
{
	return m_nTextAngle;
}

void CMyScrollView::SetTextAngle(int nTextAngle)
{
	m_nTextAngle = nTextAngle*10;
	m_bDirty = true;
	Invalidate(FALSE);
}

LOGFONTW* CMyScrollView::GetLogFont()
{
	return &m_LogFont;
}

void CMyScrollView::SetLogFont(LOGFONTW *logFont)
{
	memcpy(&m_LogFont,logFont, sizeof(LOGFONTW));
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetFontSize()
{
	return m_nFontSize;
}

void CMyScrollView::SetFontSize(int nSize)
{
	if(m_nFontSize == nSize)
		return;

	m_nFontSize = nSize;
	m_bDirty = true;
	Invalidate(FALSE);
}

bool CMyScrollView::GetEnableShadow()
{
	return m_bEnableShadow;
}

void CMyScrollView::SetEnableShadow(bool bEnable)
{
	if(m_bEnableShadow == bEnable)
		return;

	m_bEnableShadow = bEnable;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetShadowAlpha()
{
	return m_nShadowAlpha;
}

void CMyScrollView::SetShadowAlpha(int nAlpha)
{
	if(m_nShadowAlpha == nAlpha)
		return;

	m_nShadowAlpha = nAlpha;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetShadowOffsetX()
{
	return m_nShadowOffsetX;
}

void CMyScrollView::SetShadowOffsetX(int nOffsetX)
{
	if(m_nShadowOffsetX == nOffsetX)
		return;

	m_nShadowOffsetX = nOffsetX;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetShadowOffsetY()
{
	return m_nShadowOffsetY;
}

void CMyScrollView::SetShadowOffsetY(int nOffsetY)
{
	if(m_nShadowOffsetY == nOffsetY)
		return;

	m_nShadowOffsetY = nOffsetY;
	m_bDirty = true;
	Invalidate(FALSE);
}

Gdiplus::Bitmap* CMyScrollView::GetShadowBkgdBmp()
{
	return m_pShadowBkgdBmp.get();
}

void CMyScrollView::SetShadowBkgdBmp(std::shared_ptr<Gdiplus::Bitmap>& pBmp)
{
	m_pShadowBkgdBmp = pBmp;
	m_bDirty = true;
	Invalidate(FALSE);
}

Gdiplus::Color CMyScrollView::GetShadowColor()
{
	return m_clrShadow;
}

void CMyScrollView::SetShadowColor(Gdiplus::Color color)
{
	if(m_clrShadow.GetValue() == color.GetValue())
		return;

	m_clrShadow = color;
	m_bDirty = true;
	Invalidate(FALSE);
}

int CMyScrollView::GetShadowThickness()
{
	return m_nShadowThickness;
}

void CMyScrollView::SetShadowThickness(int nThick)
{
	if(m_nShadowThickness == nThick)
		return;

	m_nShadowThickness = nThick;
	m_bDirty = true;
	Invalidate(FALSE);
}

bool CMyScrollView::IsEnablePngRendering()
{
	return m_bPngImage;
}

void CMyScrollView::EnablePngRendering(bool bEnable)
{
	if(m_bPngImage == bEnable)
		return;

	m_bPngImage = bEnable;
	m_bDirty = true;
	Invalidate(FALSE);
}

bool CMyScrollView::IsEnableTextGradient()
{
	return m_bTextGradient;
}

void CMyScrollView::EnableTextGradient(bool bEnable)
{
	if(m_bTextGradient == bEnable)
		return;

	m_bTextGradient = bEnable;
	m_bDirty = true;
	Invalidate(FALSE);
}

bool CMyScrollView::GetDiffusedShadow()
{
	return m_bDiffusedShadow;
}

void CMyScrollView::SetDiffusedShadow(bool bEnable)
{
	if(m_bDiffusedShadow == bEnable)
		return;

	m_bDiffusedShadow = bEnable;
	m_bDirty = true;
	Invalidate(FALSE);
}

bool CMyScrollView::GetExtrudedText()
{
	return m_bExtrudedText;
}

void CMyScrollView::SetExtrudedText(bool bEnable)
{
	if(m_bExtrudedText == bEnable)
		return;

	m_bExtrudedText = bEnable;
	m_bDirty = true;
	Invalidate(FALSE);
}

CString CMyScrollView::ConvBackSlashToForwardSlash(const CString& strSrc)
{
	CString strDest = _T("");
	for(int i=0; i<strSrc.GetLength(); ++i)
	{
		if(strSrc.GetAt(i)=='\\')
			strDest += '/';
		else
			strDest += strSrc.GetAt(i);
	}
	return strDest;
}

bool CMyScrollView::SetTextEffect(TextDesigner::IOutlineText* pOutlineText, Gdiplus::Brush* pBrush)
{
	if(!pOutlineText)
		return false;

	//pOutlineText->TextNullEffect();
	using namespace Gdiplus;
	if(pBrush==NULL)
	{
		if(m_TextEffect==SingleOutline)
		{
			TRACE(_T("Single Outline Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Text Color:%d,%d,%d, Outline Color:%d,%d,%d,%d, Thick:%d\n"),
				m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB(),
				m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB(),
				m_nOutlineThickness1 );
			TRACE(szDbg);
			pOutlineText->TextOutline(
				Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				m_nOutlineThickness1);
		}
		else if(m_TextEffect==DblOutline)
		{
			TRACE(_T("Double Outline Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Text Color:%d,%d,%d, Outline Color1:%d,%d,%d,%d, Outline Color2:%d,%d,%d,%d, Thick1:%d, Thick2:%d\n"),
				m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB(),
				m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB(),
				m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB(),
				m_nOutlineThickness1,
				m_nOutlineThickness2 );
			TRACE(szDbg);
			pOutlineText->TextDblOutline(
				Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				Color(m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB()),
				m_nOutlineThickness1,
				m_nOutlineThickness2);
		}
		else if(m_TextEffect==TextGlow)
		{
			TRACE(_T("Text Glow Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Text Color:%d,%d,%d, Outline Color:%d,%d,%d,%d, Thick:%d\n"),
				m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB(),
				m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB(),
				m_nOutlineThickness1 );
			TRACE(szDbg);

			pOutlineText->TextGlow(
				Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				m_nOutlineThickness1);
		}
		else if(m_TextEffect==GradOutline)
		{
			TRACE(_T("Gradient Outline Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Text Color:%d,%d,%d, Outline Color1:%d,%d,%d,%d, Outline Color2:%d,%d,%d,%d, Thick:%d\n"),
				m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB(),
				m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB(),
				m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB(),
				m_nOutlineThickness1 );
			TRACE(szDbg);
			pOutlineText->TextGradOutline(
				Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				Color(m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB()),
				m_nOutlineThickness1+m_nOutlineThickness2, TextDesigner::GradientType::Linear);
		}
		else if(m_TextEffect==NoOutline)
		{
			TRACE(_T("No Outline Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Text Color:%d,%d,%d\n"),
				m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB());
			TRACE(szDbg);
			pOutlineText->TextNoOutline(
				Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()));
		}
		else if(m_TextEffect==OnlyOutline)
		{
			TRACE(_T("Only Outline Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Outline Color:%d,%d,%d,%d, Thick:%d\n"),
				m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB(),
				m_nOutlineThickness1);
			TRACE(szDbg);
			pOutlineText->TextOnlyOutline(
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				m_nOutlineThickness1,
				false);
		}
		else if(m_TextEffect==DblGlow)
		{
			TRACE(_T("Double Glow Effect\n"));
			CString szDbg;
			szDbg.Format(_T("Text Color:%d,%d,%d, Outline Color1:%d,%d,%d,%d, Outline Color2:%d,%d,%d,%d, Thick1:%d, Thick2:%d\n"),
				m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB(),
				m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB(),
				m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB(),
				m_nOutlineThickness1,
				m_nOutlineThickness2 );
			TRACE(szDbg);
			pOutlineText->TextDblGlow(
				Color(m_clrText.GetR(),m_clrText.GetG(),m_clrText.GetB()),
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				Color(m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB()),
				m_nOutlineThickness1,
				m_nOutlineThickness2);
		}
	}
	else // with a pBrush
	{
		if(m_TextEffect==SingleOutline)
		{
			TRACE(_T("Single Outline Effect with brush\n"));
			pOutlineText->TextOutline(
				*pBrush,
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				m_nOutlineThickness1);
		}
		else if(m_TextEffect==DblOutline)
		{
			TRACE(_T("Double Outline Effect with brush\n"));
			pOutlineText->TextDblOutline(
				*pBrush,
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				Color(m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB()),
				m_nOutlineThickness1,
				m_nOutlineThickness2);
		}
		else if(m_TextEffect==TextGlow)
		{
			TRACE(_T("Text Glow Effect with brush\n"));
			pOutlineText->TextGlow(
				*pBrush,
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				m_nOutlineThickness1);
		}
		else if(m_TextEffect==GradOutline)
		{
			TRACE(_T("Gradient Outline Effect with brush\n"));
			pOutlineText->TextGradOutline(
				*pBrush,
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				Color(m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB()),
				m_nOutlineThickness1+m_nOutlineThickness2, TextDesigner::GradientType::Linear);
		}
		else if(m_TextEffect==NoOutline)
		{
			TRACE(_T("No Outline Effect with brush\n"));
			pOutlineText->TextNoOutline(*pBrush);
		}
		else if(m_TextEffect==OnlyOutline)
		{
			TRACE(_T("Only Outline Effect with brush\n"));
			pOutlineText->TextOnlyOutline(
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				m_nOutlineThickness1,
				false);
		}
		else if(m_TextEffect==DblGlow)
		{
			TRACE(_T("Double Glow Effect with brush\n"));
			pOutlineText->TextDblGlow(
				*pBrush,
				Color(m_nOutlineAlpha1, m_clrOutline1.GetR(),m_clrOutline1.GetG(),m_clrOutline1.GetB()),
				Color(m_nOutlineAlpha2, m_clrOutline2.GetR(),m_clrOutline2.GetG(),m_clrOutline2.GetB()),
				m_nOutlineThickness1,
				m_nOutlineThickness2);
		}
	}

	return true;
}

bool CMyScrollView::IsEnableReflection()
{
	return m_bEnableReflection;
}

void CMyScrollView::EnableReflection(bool bEnable)
{
	if(m_bEnableReflection == bEnable)
		return;

	m_bEnableReflection = bEnable;

	m_bDirty = true;

	Invalidate(FALSE);
}

void CMyScrollView::SetBegAlpha(float f) 
{ 
	if(m_fStartAlpha == f)
		return;

	m_fStartAlpha = f; 
	m_bDirty = true;
	Invalidate(FALSE);
}

void CMyScrollView::SetEndAlpha(float f) 
{ 
	if(m_fEndAlpha == f)
		return;

	m_fEndAlpha = f; 
	m_bDirty = true;
	Invalidate(FALSE);
}

void CMyScrollView::SetShown(float f) 
{ 
	if(m_fShown == f)
		return;

	m_fShown = f; 
	m_bDirty = true;
	Invalidate(FALSE);
}

void CMyScrollView::SetGap(int n) 
{ 
	if(m_nGap == n)
		return;

	m_nGap = n; 
	m_bDirty = true;
	Invalidate(FALSE);
}

float CMyScrollView::GetBegAlpha() 
{ 
	return m_fStartAlpha; 
}

float CMyScrollView::GetEndAlpha() 
{ 
	return m_fEndAlpha; 
}

float CMyScrollView::GetShown() 
{ 
	return m_fShown; 
}

int CMyScrollView::GetGap() 
{ 
	return m_nGap; 
}

bool CMyScrollView::SavePngImage(const CString& szFile)
{
	if(m_bPngImage)
		return m_PngOutlineText.SavePngFile(szFile);

	return false;
}

bool CMyScrollView::SaveRefPngImage(const CString& szFile)
{
	if(m_bPngImage&&m_bEnableReflection)
		return m_PngOutlineText.SaveReflectionPngFile(szFile);

	return false;
}
