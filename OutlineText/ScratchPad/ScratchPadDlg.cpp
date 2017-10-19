// ScratchPadDlg.cpp : implementation file
//

#include "stdafx.h"
#include "ScratchPad.h"
#include "ScratchPadDlg.h"
#include "../TextDesigner/OutlineText.h"
#include "../TextDesigner/PngOutlineText.h"
#include "../TextDesigner/DrawGradient.h"
#include "../TextDesigner/BmpOutlineText.h"
//#ifdef _DEBUG
//#define new DEBUG_NEW
//#endif

CScratchPadDlg::CScratchPadDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CScratchPadDlg::IDD, pParent)
	, m_pSrcBitmap(NULL)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

CScratchPadDlg::~CScratchPadDlg()
{
	delete m_pSrcBitmap;
	m_pSrcBitmap = NULL;
}

void CScratchPadDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CScratchPadDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CScratchPadDlg message handlers

BOOL CScratchPadDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	LoadSrcImage();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CScratchPadDlg::OnPaint()
{
	//CDialog::OnPaint();

	using namespace Gdiplus;
	using namespace TextDesigner;
	CPaintDC dc(this);
	Graphics graphics(dc.GetSafeHdc());
	graphics.SetSmoothingMode(SmoothingModeAntiAlias);
	graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

	graphics.DrawImage(m_pSrcBitmap, 0, 0, m_pSrcBitmap->GetWidth(), m_pSrcBitmap->GetHeight());

	//Drawing the back ground color
	CRect rect;
	GetClientRect(&rect);
	Color m_clrBkgd(255, 255, 255);
	SolidBrush brushBkgnd(m_clrBkgd);
	//graphics.FillRectangle(&brushBkgnd,0,0,rect.Width(),rect.Height());

	PngOutlineText m_PngOutlineText;
	m_PngOutlineText.TextGradOutline(
		Color(255,128,64), 
		Color(255,64,0,64), 
		Color(255,255,128,255), 
		10, false);
	m_PngOutlineText.EnableReflection(false);

	m_PngOutlineText.EnableShadow(true);
	m_PngOutlineText.Shadow(
		Gdiplus::Color(90,0,0,0), 8, 
		Gdiplus::Point(8,8));
	LOGFONTW m_LogFont;
	memset(&m_LogFont, 0, sizeof(m_LogFont));
	wcscpy_s(m_LogFont.lfFaceName, L"Arial Black");
	m_LogFont.lfHeight = -48;

	m_LogFont.lfOrientation = 0;
	m_LogFont.lfEscapement = 0;
	m_LogFont.lfItalic = 0;
	float fStartX = 0.0f;
	float fStartY = 0.0f;
	float fDestWidth = 0.0f;
	float fDestHeight = 0.0f;
	m_PngOutlineText.GdiMeasureString(
		&graphics,
		&m_LogFont,
		L"TEXT DESIGNER",
		Gdiplus::Point(10, 10),
		&fStartX,
		&fStartY,
		&fDestWidth,
		&fDestHeight);
	fDestWidth += 10;
	fDestHeight += 10;
	m_PngOutlineText.SetShadowBkgd(m_clrBkgd,(int)fDestWidth,(int)fDestHeight);

	bool m_bDirty = true;
	std::shared_ptr<Bitmap> m_pPngImage;
	if(m_bDirty == false&&m_pPngImage)
	{
		graphics.DrawImage(m_pPngImage.get(),10,10,m_pPngImage->GetWidth(),m_pPngImage->GetHeight());
		return;
	}

	m_pPngImage = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(fDestWidth,fDestHeight,PixelFormat32bppARGB));
	m_PngOutlineText.SetPngImage(m_pPngImage);
	LinearGradientBrush gradientBrush(Gdiplus::Rect(fStartX, fStartY, fDestWidth - (fStartX + 10), fDestHeight - (fStartY + 10)),
		Color(255, 128, 64), Color(255, 0, 0), LinearGradientModeVertical );
	m_PngOutlineText.TextGradOutline(
		gradientBrush, 
		Color(255,64,0,64), 
		Color(255,255,128,255), 
		10, false);
	m_PngOutlineText.GdiDrawString(&graphics, &m_LogFont, L"TEXT DESIGNER", 
		Gdiplus::Point(10, 10));
	graphics.DrawImage(m_pPngImage.get(), 50, 200, m_pPngImage->GetWidth(), m_pPngImage->GetHeight());

}
// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CScratchPadDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CScratchPadDlg::OnSize(UINT nType, int cx, int cy)
{
	CDialog::OnSize(nType, cx, cy);

	Invalidate(TRUE);
}


void CScratchPadDlg::LoadSrcImage()
{
	HINSTANCE hResInstance = AfxGetResourceHandle( );

	HRSRC res = FindResource(hResInstance,
		MAKEINTRESOURCE(IDB_BITMAP1),L"BINARY");
	if (res) 
	{
		HGLOBAL mem = LoadResource(hResInstance, res);
		void *data = LockResource(mem);
		size_t len = SizeofResource(hResInstance, res);

		HGLOBAL hGlobal = GlobalAlloc(GMEM_MOVEABLE, len);
		LPVOID pvData = GlobalLock( hGlobal );
		memcpy(pvData,data,len);
		GlobalUnlock(hGlobal);

		LPSTREAM pStream = NULL;
		HRESULT hr = CreateStreamOnHGlobal( hGlobal, TRUE, &pStream );

		using namespace Gdiplus;
		m_pSrcBitmap = new Bitmap(pStream,false);

		pStream->Release();
	}
}