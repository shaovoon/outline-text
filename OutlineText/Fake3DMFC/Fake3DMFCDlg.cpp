
// Fake3DMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Fake3DMFC.h"
#include "Fake3DMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/Canvas.h"
#include "../TextDesigner/DrawGradient.h"
#include <vector>

//#ifdef _DEBUG
//#define new DEBUG_NEW
//#endif


// CFake3DMFCDlg dialog




CFake3DMFCDlg::CFake3DMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CFake3DMFCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CFake3DMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CFake3DMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CFake3DMFCDlg message handlers

BOOL CFake3DMFCDlg::OnInitDialog()
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

void CFake3DMFCDlg::OnPaint()
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

		// Create the extruded text effect
		using namespace Gdiplus;
		using namespace TextDesigner;
		CPaintDC dc(this);
		Graphics graphics(dc.GetSafeHdc());
		graphics.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

		// Create the outline strategy which is going to shift blit diagonally
		auto strategyOutline = Canvas::TextOutline(MaskColor::Blue(), MaskColor::Blue(), 4);

		CRect rect;
		GetClientRect(&rect);
		auto canvas = Canvas::GenImage(rect.Width(), rect.Height(), Color::White, 0);

		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		FontFamily fontFamily(L"Arial Black");

		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 40;

		context.pszText = L"CODING MONKEY";
		context.ptDraw = Point(0, 0);

		// the single mask outline
		auto maskOutline = Canvas::GenMask(strategyOutline, rect.Width(), rect.Height(), Point(0,0), context);
		// the mask to store all the single mask blitted diagonally
		auto maskOutlineAll = Canvas::GenImage(rect.Width()+10, rect.Height()+10);

		Graphics graphMaskAll(maskOutlineAll.get());

		// blit diagonally
		for(int i=0; i<7; ++i)
			graphMaskAll.DrawImage(maskOutline.get(), i, i, rect.Width(), rect.Height());

		// Measure the dimension of the big mask in order to generate the correct sized gradient image
		//=============================================================================================
		UINT top = 0;
		UINT bottom = 0;
		UINT left = 0;
		UINT right = 0;
		Canvas::MeasureMaskLength(maskOutlineAll.get(), MaskColor::Blue(), top, left, bottom, right);
		right += 2;
		bottom += 2;

		// Generate the gradient image for the diagonal outline
		//=======================================================
		auto gradImage = Canvas::GenImage(right-left, bottom-top);

		std::vector<Color> vecColors;
		vecColors.push_back(Color::DarkGreen);
		vecColors.push_back(Color::YellowGreen);
		DrawGradient::Draw(*gradImage, vecColors, false);

		// Because Canvas::ApplyImageToMask requires all image to have same dimensions,
		// we have to blit our small gradient image onto a temp image as big as the canvas
		//===================================================================================
		auto gradBlitted = Canvas::GenImage(rect.Width(), rect.Height());

		Graphics graphgradBlitted(gradBlitted.get());

		graphgradBlitted.DrawImage(gradImage.get(), (int)left, (int)top, (int)(gradImage->GetWidth()), (int)(gradImage->GetHeight()));

		Canvas::ApplyImageToMask(gradBlitted.get(), maskOutlineAll.get(), canvas.get(), MaskColor::Blue(), false);

		// Create strategy and mask image for the text body
		//===================================================
		auto strategyText = Canvas::TextNoOutline(MaskColor::Blue());
		auto maskText = Canvas::GenMask(strategyText, rect.Width(), rect.Height(), Point(0,0), context);

		// Measure the dimension required for text body using the mask
		//=============================================================
		top = 0;
		bottom = 0;
		left = 0;
		right = 0;
		Canvas::MeasureMaskLength(maskText.get(), MaskColor::Blue(), top, left, bottom, right);
		top -= 2;
		left -= 2;

		right += 2;
		bottom += 2;

		// Create the gradient brush for the text body
		LinearGradientBrush gradTextbrush(Gdiplus::Rect((int)left, (int)top, (int)right, (int)bottom), Color::Orange, Color::OrangeRed, 90.0f);

		// Create the actual strategy for the text body used for rendering, with the gradient brush
		auto strategyText2 = Canvas::TextNoOutline(&gradTextbrush);

		// Draw the newly created strategy onto the canvas
		Canvas::DrawTextImage(strategyText2, canvas.get(), Point(0,0), context);

		// Finally blit the rendered canvas onto the window
		graphics.DrawImage(canvas.get(), 0, 0, rect.Width(), rect.Height());
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CFake3DMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

