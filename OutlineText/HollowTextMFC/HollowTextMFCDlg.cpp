
// HollowTextMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "HollowTextMFC.h"
#include "HollowTextMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/Canvas.h"
#include "../TextDesigner/DrawGradient.h"

//#ifdef _DEBUG
//#define new DEBUG_NEW
//#endif


// CHollowTextMFCDlg dialog




CHollowTextMFCDlg::CHollowTextMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CHollowTextMFCDlg::IDD, pParent)
	, m_nTimerLoop(0)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CHollowTextMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CHollowTextMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CHollowTextMFCDlg message handlers

BOOL CHollowTextMFCDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	SetTimer(0, 500, NULL);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CHollowTextMFCDlg::OnPaint()
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

		// Generating the hollow text effect where the text looks like cut out of canvas
		using namespace Gdiplus;
		using namespace TextDesigner;
		CPaintDC dc(this);
		Graphics graphics(dc.GetSafeHdc());
		graphics.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

		// Generating the outline strategy for displaying inside the hollow
		auto strategyOutline = Canvas::TextGradOutline(Color(255,255,255), Color(230,230,230), Color(100,100,100), 9);

		CRect rect;
		GetClientRect(&rect);
		auto canvas = Canvas::GenImage(rect.Width(), rect.Height());
		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		FontFamily fontFamily(L"Arial Black");

		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleBold;
		context.nfontSize = 56;

		context.pszText = L"CUTOUT";
		context.ptDraw = Point(0, 0);

		auto hollowImage = Canvas::GenImage(rect.Width(), rect.Height());
		// Algorithm to shift the shadow outline in and then out continuous
		int shift=0;
		if(m_nTimerLoop>=0&&m_nTimerLoop<=2)
			shift = m_nTimerLoop;
		else
			shift = 2 - (m_nTimerLoop - 2);

		// Draw the hollow (shadow) outline by shifting accordingly
		Canvas::DrawTextImage(strategyOutline, hollowImage, Point(2+shift,2+shift), context);

		// Generate the green mask for the cutout holes in the text
		//============================================================
		auto maskImage = Canvas::GenImage(rect.Width(), rect.Height());
		auto strategyMask = Canvas::TextOutline(MaskColor::Green(), MaskColor::Green(), 0);
		Canvas::DrawTextImage(strategyMask, maskImage, Point(0,0), context);

		// Apply the hollowed image against the green mask on the canvas
		Canvas::ApplyImageToMask(hollowImage, maskImage, canvas, MaskColor::Green(), false);

		// Create a double-buffer to blit onto the window for non-flickering, instead of clearing the window
		auto backBuffer = Canvas::GenImage(rect.Width(), rect.Height(), Color(0,0,0), 255);

		// Create a black outline only strategy and blit it onto the canvas to cover 
		// the unnatural outline from the gradient shadow
		//=============================================================================
		auto strategyOutlineOnly = Canvas::TextOnlyOutline(Color(0,0,0), 2, false);
		Canvas::DrawTextImage(strategyOutlineOnly, canvas, Point(0,0), context);

		// Draw the transparent canvas onto the back buffer
		//===================================================
		Graphics graphics2(backBuffer.get());
		graphics2.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics2.SetInterpolationMode(InterpolationModeHighQualityBicubic);
		graphics2.DrawImage(canvas.get(), 0, 0, rect.Width(), rect.Height());

		// Finally blit the rendered image onto the window
		graphics.DrawImage(backBuffer.get(), 0, 0, rect.Width(), rect.Height());
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CHollowTextMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CHollowTextMFCDlg::OnTimer(UINT_PTR nIDEvent)
{
	if(nIDEvent==0)
	{
		++m_nTimerLoop;

		if(m_nTimerLoop>4)
			m_nTimerLoop=0;

		Invalidate(FALSE);
	}

	CDialogEx::OnTimer(nIDEvent);
}
