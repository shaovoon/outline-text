// MyButton.cpp : implementation file
//

#include "stdafx.h"
#include "MyButton.h"


// CMyButton

IMPLEMENT_DYNAMIC(CMyButton, CButton)

CMyButton::CMyButton()
{

}

CMyButton::~CMyButton()
{
}


BEGIN_MESSAGE_MAP(CMyButton, CButton)
END_MESSAGE_MAP()

void CMyButton::PreSubclassWindow() 
{
	CButton::PreSubclassWindow();

	ModifyStyle(0, BS_OWNERDRAW);	// make the button owner drawn

}

// CMyButton message handlers
void CMyButton::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct) 
{
	CDC* pDC   = CDC::FromHandle(lpDrawItemStruct->hDC);
	CRect rect = lpDrawItemStruct->rcItem;
	UINT state = lpDrawItemStruct->itemState;

	CString strText;
	GetWindowText(strText);

	// draw the control edges (DrawFrameControl is handy!)

	if (state & ODS_SELECTED)
		pDC->DrawFrameControl(rect, DFC_BUTTON, DFCS_BUTTONPUSH | DFCS_PUSHED);
	else
		pDC->DrawFrameControl(rect, DFC_BUTTON, DFCS_BUTTONPUSH);

	// Deflate the drawing rect by the size of the button's edges

	rect.DeflateRect( CSize(GetSystemMetrics(SM_CXEDGE), GetSystemMetrics(SM_CYEDGE)));

	// Draw the text
	if (!strText.IsEmpty())
	{
		CSize Extent = pDC->GetTextExtent(strText);
		CPoint pt( rect.CenterPoint().x - Extent.cx/2, 
			rect.CenterPoint().y - Extent.cy/2 );

		if (state & ODS_SELECTED) 
			pt.Offset(1,1);

		int nMode = pDC->SetBkMode(TRANSPARENT);

		if (state & ODS_DISABLED)
			pDC->DrawState(pt, Extent, strText, DSS_DISABLED, TRUE, 0, (HBRUSH)NULL);
		else
			pDC->TextOut(pt.x, pt.y, strText);

		pDC->SetBkMode(nMode);
	}
}


