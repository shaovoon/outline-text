
// Fake3D2MFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Fake3D2MFC.h"
#include "Fake3D2MFCDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CFake3D2MFCDlg dialog




CFake3D2MFCDlg::CFake3D2MFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CFake3D2MFCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CFake3D2MFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CFake3D2MFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CFake3D2MFCDlg message handlers

BOOL CFake3D2MFCDlg::OnInitDialog()
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

void CFake3D2MFCDlg::OnPaint()
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

		// Load a font from its file into private collection, 
		// instead of from system font collection
		//=============================================================
		Gdiplus::PrivateFontCollection fontcollection;

		CString szFontFile = L"..\\CommonFonts\\Airbus Special.TTF";

		Gdiplus::Status nResults = fontcollection.AddFontFile(szFontFile);
		FontFamily fontFamily;
		int nNumFound=0;
		fontcollection.GetFamilies(1,&fontFamily,&nNumFound);

		CRect rect;
		GetClientRect(&rect);

		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 40;

		context.ptDraw = Point(0, 5);

		CString text = L"PENTHOUSE";
		int x_offset = 0;

		for(int i=0; i<text.GetLength(); ++i)
		{
			CString str = L"";
			str += text.GetAt(i);
			context.pszText = str.GetBuffer(1);

			Gdiplus::Matrix mat;
			mat.Rotate(-10.0f, Gdiplus::MatrixOrderAppend);
			mat.Scale(0.75f, 1.0f, Gdiplus::MatrixOrderAppend);
			DrawChar(x_offset, rect, context, graphics, mat);

			if(i==2)
				x_offset += 42;
			else if(i>=4&&i<=6)
				x_offset += 42;
			else if(i==7)
				x_offset += 37;
			else
				x_offset += 39;

			x_offset -= 8;
			str.ReleaseBuffer();
		}


	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CFake3D2MFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CFake3D2MFCDlg::DrawChar( int x_offset, CRect &rect, TextDesigner::TextContext& context, Gdiplus::Graphics &graphics, Gdiplus::Matrix& mat )
{
	using namespace Gdiplus;
	using namespace TextDesigner;

	Bitmap* canvas = Canvas::GenImage(rect.Width(), rect.Height(), Color::White, 0);

	// Create the outline strategy which is going to shift blit diagonally
	auto strategyOutline = Canvas::TextOutline(MaskColor::Blue(), MaskColor::Blue(), 4);

	// the single mask outline
	Bitmap* maskOutline = Canvas::GenMask(strategyOutline, rect.Width(), rect.Height(), Point(0,0), &context, mat);
	// the mask to store all the single mask blitted diagonally
	Bitmap* maskOutlineAll = Canvas::GenImage(rect.Width()+10, rect.Height()+10);

	Graphics graphMaskAll(maskOutlineAll);

	// blit diagonally
	for(int i=0; i<8; ++i)
		graphMaskAll.DrawImage(maskOutline, -i, -i, rect.Width(), rect.Height());

	// Measure the dimension of the big mask in order to generate the correct sized gradient image
	//=============================================================================================
	UINT top = 0;
	UINT bottom = 0;
	UINT left = 0;
	UINT right = 0;
	Canvas::MeasureMaskLength(maskOutlineAll, MaskColor::Blue(), top, left, bottom, right);
	right += 2;
	bottom += 2;

	// Generate the gradient image for the diagonal outline
	//=======================================================
	Bitmap* gradImage = Canvas::GenImage(right-left, bottom-top);

	std::vector<Color> vecColors;
	vecColors.push_back(Color::Purple);
	vecColors.push_back(Color::MediumPurple);
	DrawGradient::Draw(*gradImage, vecColors, false);

	// Because Canvas::ApplyImageToMask requires all image to have same dimensions,
	// we have to blit our small gradient image onto a temp image as big as the canvas
	//===================================================================================
	Bitmap* gradBlitted = Canvas::GenImage(rect.Width(), rect.Height());

	Graphics graphgradBlitted(gradBlitted);

	graphgradBlitted.DrawImage(gradImage, (int)left, (int)top, (int)(gradImage->GetWidth()), (int)(gradImage->GetHeight()));

	Canvas::ApplyImageToMask(gradBlitted, maskOutlineAll, canvas, MaskColor::Blue(), false);

	// Create strategy and mask image for the text body
	//===================================================
	auto strategyText = Canvas::TextNoOutline(MaskColor::Blue());
	Bitmap* maskText = Canvas::GenMask(strategyText, rect.Width(), rect.Height(), Point(0,0), &context, mat);

	// Measure the dimension required for text body using the mask
	//=============================================================
	top = 0;
	bottom = 0;
	left = 0;
	right = 0;
	Canvas::MeasureMaskLength(maskText, MaskColor::Blue(), top, left, bottom, right);
	top -= 2;
	left -= 2;

	right += 2;
	bottom += 2;

	// Create the gradient brush for the text body
	LinearGradientBrush gradTextbrush(Gdiplus::Rect((int)left, (int)top, (int)right, (int)bottom), Color::DeepPink, Color::LightPink, 90.0f);

	// Create the actual strategy for the text body used for rendering, with the gradient brush
	auto strategyText2 = Canvas::TextNoOutline(&gradTextbrush);

	// Draw the newly created strategy onto the canvas
	Canvas::DrawTextImage(strategyText2, canvas, Point(0,0), &context, mat);

	// Finally blit the rendered canvas onto the window
	graphics.DrawImage(canvas, x_offset, 0, rect.Width(), rect.Height());

	delete gradImage;
	delete gradBlitted;

	delete maskText;

	delete strategyText;
	delete strategyText2;

	delete maskOutline;
	delete maskOutlineAll;

	delete strategyOutline;

	delete canvas;
}
