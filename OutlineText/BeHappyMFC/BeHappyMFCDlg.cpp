
// BeHappyMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "BeHappyMFC.h"
#include "BeHappyMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/Canvas.h"
#include "../TextDesigner/DrawGradient.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CBeHappyMFCDlg dialog




CBeHappyMFCDlg::CBeHappyMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CBeHappyMFCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBeHappyMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CBeHappyMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CBeHappyMFCDlg message handlers

BOOL CBeHappyMFCDlg::OnInitDialog()
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

void CBeHappyMFCDlg::OnPaint()
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
		
		// Draw the BE HAPPY effect from the BE HAPPY soap opera

		using namespace Gdiplus;
		using namespace TextDesigner;
		CPaintDC dc(this);
		Graphics graphics(dc.GetSafeHdc());
		graphics.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

		CRect rect;
		GetClientRect(&rect);
		// Create canvas to be rendered
		Bitmap* canvas = Canvas::GenImage(rect.Width(), rect.Height());
		// Create canvas for the green outermost outline
		Bitmap* canvasOuter = Canvas::GenImage(rect.Width(), rect.Height());
		// Create canvas for the white inner outline
		Bitmap* canvasInner = Canvas::GenImage(rect.Width(), rect.Height());

		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		// Load a font from its file into private collection, 
		// instead of from system font collection
		//=============================================================
		Gdiplus::PrivateFontCollection fontcollection;

		CString szFontFile = L"..\\CommonFonts\\ALBA____.TTF";

		Gdiplus::Status nResults = fontcollection.AddFontFile(szFontFile);
		FontFamily fontFamily;
		int nNumFound=0;
		fontcollection.GetFamilies(1,&fontFamily,&nNumFound);

		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 48;

		context.pszText = L"bE";
		context.ptDraw = Point(55, 0);

		// Create the outer strategy
		auto strategyOutline2 = Canvas::TextOutline(Color::LightSeaGreen, Color::LightSeaGreen, 16);
		// Draw the bE text (outer green outline)
		Canvas::DrawTextImage(strategyOutline2, canvasOuter, Point(0,0), context);
		context.pszText = L"Happy";
		context.ptDraw = Point(0, 48);
		// Draw the Happy text (outer green outline)
		Canvas::DrawTextImage(strategyOutline2, canvasOuter, Point(0,0), context);

		// blit the canvasOuter all the way down (5 pixels down)
		//========================================================
		Graphics graphicsCanvas(canvas);
		graphicsCanvas.SetSmoothingMode(SmoothingModeAntiAlias);
		graphicsCanvas.SetInterpolationMode(InterpolationModeHighQualityBicubic);
		graphicsCanvas.DrawImage(canvasOuter, 0, 0, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasOuter, 0, 1, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasOuter, 0, 2, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasOuter, 0, 3, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasOuter, 0, 4, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasOuter, 0, 5, rect.Width(), rect.Height());
		graphics.DrawImage(canvas, 0, 0, rect.Width(), rect.Height());

		context.pszText = L"bE";
		context.ptDraw = Point(55, 0);

		// Create the inner white strategy
		auto strategyOutline1 = Canvas::TextOutline(Color::White, Color::White, 8);
		// Draw the bE text (inner white outline)
		Canvas::DrawTextImage(strategyOutline1, canvasInner, Point(0,0), context);
		context.pszText = L"Happy";
		context.ptDraw = Point(0, 48);
		// Draw the Happy text (inner white outline)
		Canvas::DrawTextImage(strategyOutline1, canvasInner, Point(0,0), context);

		// blit the canvasInner all the way down (5 pixels down)
		//========================================================
		graphicsCanvas.DrawImage(canvasInner, 0, 0, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasInner, 0, 1, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasInner, 0, 2, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasInner, 0, 3, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasInner, 0, 4, rect.Width(), rect.Height());
		graphicsCanvas.DrawImage(canvasInner, 0, 5, rect.Width(), rect.Height());
		graphics.DrawImage(canvas, 0, 0, rect.Width(), rect.Height());

		// Create the strategy for green text body
		auto strategyOutline = Canvas::TextOutline(Color::LightSeaGreen, Color::LightSeaGreen, 1);

		context.pszText = L"bE";
		context.ptDraw = Point(55, 0);
		// Draw the bE text (text body)
		Canvas::DrawTextImage(strategyOutline, canvas, Point(0,0), context);

		context.pszText = L"Happy";
		context.ptDraw = Point(0, 48);
		// Draw the Happy text (text body)
		Canvas::DrawTextImage(strategyOutline, canvas, Point(0,0), context);

		// Finally blit the rendered canvas onto the window
		graphics.DrawImage(canvas, 0, 0, rect.Width(), rect.Height());

		// Release all the resources
		//============================
		delete canvasOuter;
		delete canvasInner;
		delete canvas;
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CBeHappyMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

