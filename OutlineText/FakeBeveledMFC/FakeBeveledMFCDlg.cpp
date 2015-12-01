
// FakeBeveledMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "FakeBeveledMFC.h"
#include "FakeBeveledMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/Canvas.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CFakeBeveledMFCDlg dialog




CFakeBeveledMFCDlg::CFakeBeveledMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CFakeBeveledMFCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CFakeBeveledMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CFakeBeveledMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CFakeBeveledMFCDlg message handlers

BOOL CFakeBeveledMFCDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CFakeBeveledMFCDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		//CDialogEx::OnPaint();

		// Draw Faked Beveled effect

		using namespace Gdiplus;
		using namespace TextDesigner;
		CPaintDC dc(this);
		Graphics graphics(dc.GetSafeHdc());
		graphics.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

		CRect rect;
		GetClientRect(&rect);
		Bitmap* canvas = Canvas::GenImage(rect.Width(), rect.Height());

		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		// Load a font from its file into private collection, 
		// instead of from system font collection
		//=============================================================
		Gdiplus::PrivateFontCollection fontcollection;

		CString szFontFile = L"..\\CommonFonts\\Segoe Print.TTF";

		Gdiplus::Status nResults = fontcollection.AddFontFile(szFontFile);
		FontFamily fontFamily;
		int nNumFound=0;
		fontcollection.GetFamilies(1,&fontFamily,&nNumFound);

		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 38;

		context.pszText = L"Love Like Magic";
		context.ptDraw = Point(0, 0);

		// Draw the main outline
		//==========================================================
		auto mainOutline = Canvas::TextOutline(Color(235,10,230), Color(235,10,230), 4);
		Canvas::DrawTextImage(mainOutline, canvas, Point(4,4), context);

		// Draw the small bright outline shifted (-2, -2)
		//==========================================================
		auto mainBright = Canvas::TextOutline(Color(252,173,250), Color(252,173,250), 2);
		Canvas::DrawTextImage(mainBright, canvas, Point(2,2), context);

		// Draw the small dark outline shifted (+2, +2)
		//==========================================================
		auto mainDark = Canvas::TextOutline(Color(126,5,123), Color(126,5,123), 2);
		Canvas::DrawTextImage(mainDark, canvas, Point(6,6), context);

		// Draw the smallest outline (color same as main outline)
		//==========================================================
		auto mainInner = Canvas::TextOutline(Color(235,10,230), Color(235,10,230), 2);
		Canvas::DrawTextImage(mainInner, canvas, Point(4,4), context);

		// Finally blit the rendered canvas onto the window
		graphics.DrawImage(canvas, 0, 0, rect.Width(), rect.Height());

		// Release all the resources
		//============================
		delete canvas;
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CFakeBeveledMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

