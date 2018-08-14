
// InnerOutlineMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "InnerOutlineMFC.h"
#include "InnerOutlineMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/CanvasHelper.h"
#include "../TextDesigner/DrawGradient.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CInnerOutlineMFCDlg dialog



CInnerOutlineMFCDlg::CInnerOutlineMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_INNEROUTLINEMFC_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CInnerOutlineMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CInnerOutlineMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CInnerOutlineMFCDlg message handlers

BOOL CInnerOutlineMFCDlg::OnInitDialog()
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

void CInnerOutlineMFCDlg::OnPaint()
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

		// Create rainbow Text Effect in Aquarion EVOL anime
		using namespace Gdiplus;
		using namespace TextDesigner;
		CPaintDC dc(this);
		Graphics graphics(dc.GetSafeHdc());
		graphics.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);


		CRect rect;
		GetClientRect(&rect);
		auto canvas = CanvasHelper::GenImage(rect.Width(), rect.Height(), Color::White, 0);

		// Text context to store string and font info to be sent as parameter to CanvasHelper methods
		TextContext context;

		FontFamily fontFamily(L"Arial Black");
		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 54;

		context.pszText = L"VACATION";
		context.ptDraw = Point(0, 0);

		// Generate the mask image for measuring the size of the text image required
		//============================================================================
		auto strategyOutline2 = CanvasHelper::TextNoOutline(MaskColor::Blue());
		auto maskOutline2 = CanvasHelper::GenMask(strategyOutline2, rect.Width(), rect.Height(), Point(0, 0), context);

		UINT top = 0;
		UINT bottom = 0;
		UINT left = 0;
		UINT right = 0;
		CanvasHelper::MeasureMaskLength(maskOutline2, MaskColor::Blue(), top, left, bottom, right);
		right += 2;
		bottom += 2;

		Color light_purple(102, 159, 206);
		Color dark_purple(35, 68, 95);
		auto strategyOutline3 = CanvasHelper::TextGradOutline(light_purple, dark_purple, light_purple, 9, GradientType::Linear);
		CanvasHelper::DrawTextImage(strategyOutline3, canvas, Point(0, 0), context);

		Color light_yellow(255, 227, 85);
		Color dark_yellow(243, 163, 73);

		auto text = CanvasHelper::GenImage(rect.Width(), rect.Height(), dark_yellow, 0);
		auto strategyText2 = CanvasHelper::TextGradOutlineLast(light_yellow, dark_yellow, light_yellow, 9, GradientType::Sinusoid);
		CanvasHelper::DrawTextImage(strategyText2, text, Point(0, 0), context);
		CanvasHelper::ApplyImageToMask(text, maskOutline2, canvas, MaskColor::Blue(), true);

		// Finally blit the rendered canvas onto the window
		graphics.DrawImage(canvas.get(), 0, 0, rect.Width(), rect.Height());
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CInnerOutlineMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

