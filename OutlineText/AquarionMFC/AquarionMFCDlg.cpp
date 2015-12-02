
// AquarionMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "AquarionMFC.h"
#include "AquarionMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/Canvas.h"
#include "../TextDesigner/DrawGradient.h"
#include <vector>

//#ifdef _DEBUG
//#define new DEBUG_NEW
//#endif


// CAquarionMFCDlg dialog




CAquarionMFCDlg::CAquarionMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CAquarionMFCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CAquarionMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAquarionMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CAquarionMFCDlg message handlers

BOOL CAquarionMFCDlg::OnInitDialog()
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

void CAquarionMFCDlg::OnPaint()
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

		// Create the outline strategy which is used later on for measuring 
		// the size of text in order to generate a correct sized gradient image
		auto strategyOutline2 = Canvas::TextOutline(MaskColor::Blue(), MaskColor::Blue(), 8);

		CRect rect;
		GetClientRect(&rect);
		auto canvas = Canvas::GenImage(rect.Width(), rect.Height(), Color::White, 0);

		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		// Load a font from its file into private collection, 
		// instead of from system font collection
		//=============================================================
		Gdiplus::PrivateFontCollection fontcollection;

		CString szFontFile = L"..\\CommonFonts\\Ruzicka TypeK.ttf";

		Gdiplus::Status nResults = fontcollection.AddFontFile(szFontFile);
		FontFamily fontFamily;
		int nNumFound=0;
		fontcollection.GetFamilies(1,&fontFamily,&nNumFound);

		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 36;

		context.pszText = L"I cross over the deep blue void";
		context.ptDraw = Point(0, 0);

		// Generate the mask image for measuring the size of the text image required
		//============================================================================
		auto maskOutline2 = Canvas::GenMask(strategyOutline2, rect.Width(), rect.Height(), Point(0,0), context);

		UINT top = 0;
		UINT bottom = 0;
		UINT left = 0;
		UINT right = 0;
		Canvas::MeasureMaskLength(maskOutline2, MaskColor::Blue(), top, left, bottom, right);
		right += 2;
		bottom += 2;

		// Generate the gradient image
		//=============================
		DrawGradient grad;
		auto bmpGrad = std::shared_ptr<Bitmap>(new Bitmap(right - left, bottom - top, PixelFormat32bppARGB));
		using namespace std;
		vector<Color> vec;
		vec.push_back(Color(255,0,0)); // Red
		vec.push_back(Color(0,0,255)); // Blue
		vec.push_back(Color(0,255,0)); // Green
		grad.Draw(*bmpGrad, vec, true);

		// Because Canvas::ApplyImageToMask requires the all images to have equal dimension,
		// we need to blit our new gradient image onto a larger image to be same size as canvas image
		//==============================================================================================
		auto bmpGrad2 = std::shared_ptr<Bitmap>(new Bitmap(rect.Width(), rect.Height(), PixelFormat32bppARGB));
		Graphics graphGrad(bmpGrad2.get());
		graphGrad.SetSmoothingMode(SmoothingModeAntiAlias);
		graphGrad.SetInterpolationMode(InterpolationModeHighQualityBicubic);
		graphGrad.DrawImage(bmpGrad.get(), (int)left, (int)top, (int)(right - left), (int)(bottom - top));

		// Apply the rainbow text against the blue mask onto the canvas
		Canvas::ApplyImageToMask(bmpGrad2, maskOutline2, canvas, MaskColor::Blue(), false);

		// Draw the (white body and black outline) text onto the canvas
		//==============================================================
		auto strategyOutline1 = Canvas::TextOutline(Color(255,255,255), Color(0,0,0), 4);
		Canvas::DrawTextImage(strategyOutline1, canvas, Point(0,0), context);

		// Finally blit the rendered canvas onto the window
		graphics.DrawImage(canvas.get(), 0, 0, rect.Width(), rect.Height());
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CAquarionMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

