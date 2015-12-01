// ColorButton.cpp : implementation file
//

#include "stdafx.h"
#include "ColorButton.h"


// CColorButton

IMPLEMENT_DYNAMIC(CColorButton, CButton)

CColorButton::CColorButton()
:
m_color(RGB(255,255,255))
{

}

CColorButton::~CColorButton()
{
}


BEGIN_MESSAGE_MAP(CColorButton, CButton)
END_MESSAGE_MAP()



// CColorButton message handlers

void CColorButton::PreSubclassWindow() 
{
	CButton::PreSubclassWindow();

	ModifyStyle(0, BS_OWNERDRAW);	// make the button owner drawn
}


void CColorButton::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct) 
{
	// TODO: Add your message handler code here and/or call default
	CDC* pDC   = CDC::FromHandle(lpDrawItemStruct->hDC);
	CRect rect = lpDrawItemStruct->rcItem;
	UINT state = lpDrawItemStruct->itemState;

	// draw the control edges (DrawFrameControl is handy!)
	if (state & ODS_SELECTED)
		pDC->DrawFrameControl(rect, DFC_BUTTON, DFCS_BUTTONPUSH | DFCS_PUSHED);
	else
		pDC->DrawFrameControl(rect, DFC_BUTTON, DFCS_BUTTONPUSH);

	// Deflate the drawing rect by the size of the button's edges

	rect.DeflateRect( CSize(GetSystemMetrics(SM_CXEDGE), GetSystemMetrics(SM_CYEDGE)));

	CSize Extent = pDC->GetTextExtent(_T(""));
	CPoint pt( rect.CenterPoint().x - Extent.cx/2, 
		rect.CenterPoint().y - Extent.cy/2 );

	if (state & ODS_SELECTED) 
		pt.Offset(1,1);

	int nMode = pDC->SetBkMode(TRANSPARENT);

	if (state & ODS_DISABLED)
		pDC->DrawState(pt, Extent, _T(""), DSS_DISABLED, TRUE, 0, (HBRUSH)NULL);

	COLORREF clrPen;
	COLORREF clrBrush;
	if(state & ODS_DISABLED)
	{
		clrPen = RGB(150,150,150);
		const int nInc = 20;
		int nRed = GetRValue(m_color)+nInc;
		int nGreen = GetGValue(m_color)+nInc;
		int nBlue = GetBValue(m_color)+nInc;
		if(nRed>255) nRed = 255;
		if(nGreen>255) nGreen = 255;
		if(nBlue>255) nBlue = 255;

		clrBrush = RGB(nRed,nGreen,nBlue);
	}
	else
	{
		clrPen = RGB(0,0,0);
		clrBrush = m_color;
	}
	CPen penBlack(PS_SOLID, 1, clrPen);
	CPen *pOldPen = pDC->SelectObject(&penBlack);
	CBrush brush(clrBrush);
	CBrush* pOldBrush = pDC->SelectObject(&brush);
	const int nMargin = 4;
	CRect rectDraw(rect.left+nMargin,rect.top+nMargin,rect.right-nMargin,rect.bottom-nMargin);
	pDC->Rectangle(&rectDraw);
	//pDC->FillSolidRect(rect, RGB(255, 255, 0)); // yellow
	pDC->SelectObject(pOldBrush);
	pDC->SelectObject(pOldPen);

	pDC->SetBkMode(nMode);
}
