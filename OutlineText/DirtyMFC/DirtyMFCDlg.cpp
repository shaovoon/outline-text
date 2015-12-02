
// DirtyMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "DirtyMFC.h"
#include "DirtyMFCDlg.h"
#include "afxdialogex.h"
#include "../TextDesigner/MaskColor.h"
#include "../TextDesigner/Canvas.h"
#include "../TextDesigner/DrawGradient.h"

//#ifdef _DEBUG
//#define new DEBUG_NEW
//#endif


// CDirtyMFCDlg dialog




CDirtyMFCDlg::CDirtyMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CDirtyMFCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDirtyMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CDirtyMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CDirtyMFCDlg message handlers

BOOL CDirtyMFCDlg::OnInitDialog()
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

void CDirtyMFCDlg::OnPaint()
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

		// Create dirty text effect

		using namespace Gdiplus;
		using namespace TextDesigner;
		CPaintDC dc(this);
		Graphics graphics(dc.GetSafeHdc());
		graphics.SetSmoothingMode(SmoothingModeAntiAlias);
		graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

		CRect rect;
		GetClientRect(&rect);
		auto canvas = Canvas::GenImage(rect.Width(), rect.Height());
		// Load the dirty image from file
		auto canvasDirty = std::shared_ptr<Bitmap>(new Bitmap(L"..\\CommonImages\\dirty-texture.png"));

		// Text context to store string and font info to be sent as parameter to Canvas methods
		TextContext context;

		FontFamily fontFamily(L"Arial Black");
		context.pFontFamily = &fontFamily;
		context.fontStyle = FontStyleRegular;
		context.nfontSize = 48;

		context.pszText = L"DIRTY";
		context.ptDraw = Point(5, 70);

		// Load the texture image from file
		auto texture = std::shared_ptr<Bitmap>(new Bitmap(L"..\\CommonImages\\texture_blue.jpg"));

		auto texture2 = Canvas::GenImage(rect.Width(), rect.Height());
		// Draw the texture against the red dirty mask onto the 2nd texture
		Canvas::ApplyImageToMask(texture, canvasDirty, texture2, MaskColor::Red(), false );
		TextureBrush textureBrush(texture2.get());

		auto textureShadow = Canvas::GenImage(rect.Width(), rect.Height());
		// Draw the gray color against the red dirty mask onto the shadow texture
		Canvas::ApplyColorToMask(Color(0xaa, 0xcc, 0xcc, 0xcc), canvasDirty, textureShadow, MaskColor::Red() );
		// Create texture brush for the shadow
		TextureBrush shadowBrush(textureShadow.get());

		// Create strategy for the shadow with the shadow brush
		auto strategyShadow = Canvas::TextNoOutline(shadowBrush);

		auto canvasTemp = Canvas::GenImage(rect.Width(), rect.Height());
		// Draw the shadow image first onto the temp canvas
		Canvas::DrawTextImage(strategyShadow, canvasTemp, Point(0, 0), context);

		// Create strategy for the text body
		auto strategy = Canvas::TextNoOutline(textureBrush);

		// Create image brush for the shadow
		//=======================================
		Canvas::DrawTextImage(strategy, canvas, Point(0,0), context);

		// Draw the shadow image (canvasTemp) shifted -3, -3
		graphics.DrawImage(canvasTemp.get(), 3, 3, rect.Width()-3, rect.Height()-3);
		// Then draw the rendered image onto window
		graphics.DrawImage(canvas.get(), 0, 0, rect.Width(), rect.Height());
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CDirtyMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

