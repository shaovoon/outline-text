// TestOutlineTextDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TestOutlineText.h"
#include "TestOutlineTextDlg.h"
#include "ReflectSettingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CTestOutlineTextDlg dialog




CTestOutlineTextDlg::CTestOutlineTextDlg(CWnd* pParent /*=NULL*/)
	: 
CDialog(CTestOutlineTextDlg::IDD, pParent),
m_pScrollView(NULL)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CTestOutlineTextDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CBO_TEXT_PATH, m_cboTextPath);
	DDX_Control(pDX, IDC_CBO_TEXT_EFFECT, m_cboTextEffect);
	DDX_Control(pDX, IDC_EDT_TEXT, m_edtText);
	DDX_Control(pDX, IDC_BTN_FONT, m_btnFont);
	DDX_Control(pDX, IDC_EDT_POSX, m_edtPosX);
	DDX_Control(pDX, IDC_EDT_POSY, m_edtPosY);
	DDX_Control(pDX, IDC_STATIC_ANGLE, m_staticAngle);
	DDX_Control(pDX, IDC_EDT_ANGLE, m_edtAngle);
	DDX_Control(pDX, IDC_BTN_BKGD_COLOR, m_btnBkgdColor);
	DDX_Control(pDX, IDC_BTN_IMAGEBROWSE, m_btnImageBrowse);
	DDX_Control(pDX, IDC_BTN_OUTLINE_COLOR1, m_btnOutlineColor1);
	DDX_Control(pDX, IDC_EDT_THICK1, m_edtThickness1);
	DDX_Control(pDX, IDC_EDT_ALPHA1, m_edtOutlineAlpha1);
	DDX_Control(pDX, IDC_STATIC_OUTLINE_COLOR2, m_staticOutlineColor2);
	DDX_Control(pDX, IDC_BTN_OUTLINE_COLOR2, m_btnOutlineColor2);
	DDX_Control(pDX, IDC_STATIC_THICK2, m_staticThick2);
	DDX_Control(pDX, IDC_EDT_THICK2, m_edtThickness2);
	DDX_Control(pDX, IDC_STATIC_ALPHA2, m_staticAlpha2);
	DDX_Control(pDX, IDC_EDT_ALPHA2, m_edtOutlineAlpha2);
	DDX_Control(pDX, IDC_STATIC_ALPHATEXT2, m_staticAlphaText2);
	DDX_Control(pDX, IDC_BTN_TEXT_COLOR, m_btnTextColor);
	DDX_Control(pDX, IDC_EDT_FONT, m_edtFont);
	DDX_Control(pDX, IDC_STATIC_SHADOW_COLOR, m_staticShadowColor);
	DDX_Control(pDX, IDC_BTN_SHADOW_COLOR, m_btnShadowColor);
	DDX_Control(pDX, IDC_EDT_SHADOW_THICK, m_edtShadowThickness);
	DDX_Control(pDX, IDC_EDT_SHADOW_ALPHA, m_edtShadowAlpha);
	DDX_Control(pDX, IDC_EDT_SHADOW_OFFSETX, m_edtShadowOffsetX);
	DDX_Control(pDX, IDC_EDT_SHADOW_OFFSETY, m_edtShadowOffsetY);
	DDX_Control(pDX, IDC_CHK_ENABLE_SHADOW, m_chkEnableShadow);
	DDX_Control(pDX, IDC_CHK_ENABLE_PNG_RENDERING, m_chkPngRendering);
	DDX_Control(pDX, IDC_BTN_RENDER, m_btnRender);
	DDX_Control(pDX, IDC_CHK_DIFFUSED_SHADOW, m_chkDiffusedShadow);
	DDX_Control(pDX, IDC_CHK_EXTRUDED_TEXT, m_chkExtrudedText);
	DDX_Control(pDX, IDC_CHK_GRADIENT_TEXT, m_chkGradientText);
	DDX_Control(pDX, IDC_BTN_TEXT_COLOR3, m_btnTextColor2);
	DDX_Control(pDX, IDC_STATIC_TEXT_COLOR2, m_staticTextColor2);
	DDX_Control(pDX, IDC_STATIC_OUTLINE_COLOR1, m_staticOutlineColor1);
	DDX_Control(pDX, IDC_STATIC_ALPHA1, m_staticAlpha1);
	DDX_Control(pDX, IDC_STATIC_ALPHATEXT1, m_staticAlphaText1);
	DDX_Control(pDX, IDC_STATIC_THICK1, m_staticThick1);
	DDX_Control(pDX, IDC_CHK_REFLECT, m_chkEnableReflection);
	DDX_Control(pDX, IDC_BTN_RELECT_SETTINGS, m_btnReflectSettings);
	DDX_Control(pDX, IDC_BTN_SAVE_PNG, m_btnSavePng);
	DDX_Control(pDX, IDC_BTN_SAVE_REFLECTIVE_PNG, m_btnSaveRefPng);
}

BEGIN_MESSAGE_MAP(CTestOutlineTextDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_WM_MOUSEWHEEL()
	ON_WM_SIZE()
	ON_BN_CLICKED(IDC_BTN_BKGD_COLOR, &CTestOutlineTextDlg::OnBnClickedBtnBkgdColor)
	ON_BN_CLICKED(IDC_BTN_OUTLINE_COLOR1, &CTestOutlineTextDlg::OnBnClickedBtnOutlineColor1)
	ON_BN_CLICKED(IDC_BTN_OUTLINE_COLOR2, &CTestOutlineTextDlg::OnBnClickedBtnOutlineColor2)
	ON_BN_CLICKED(IDC_BTN_TEXT_COLOR, &CTestOutlineTextDlg::OnBnClickedBtnTextColor)
	ON_BN_CLICKED(IDC_BTN_FONT, &CTestOutlineTextDlg::OnBnClickedBtnFont)
	ON_CBN_SELCHANGE(IDC_CBO_TEXT_PATH, &CTestOutlineTextDlg::OnCbnSelchangeCboTextPath)
	ON_CBN_SELCHANGE(IDC_CBO_TEXT_EFFECT, &CTestOutlineTextDlg::OnCbnSelchangeCboTextEffect)
	ON_EN_CHANGE(IDC_EDT_TEXT, &CTestOutlineTextDlg::OnEnChangeEdtText)
	ON_EN_CHANGE(IDC_EDT_POSX, &CTestOutlineTextDlg::OnEnChangeEdtPosx)
	ON_EN_CHANGE(IDC_EDT_POSY, &CTestOutlineTextDlg::OnEnChangeEdtPosy)
	ON_EN_CHANGE(IDC_EDT_ANGLE, &CTestOutlineTextDlg::OnEnChangeEdtAngle)
	ON_EN_CHANGE(IDC_EDT_THICK1, &CTestOutlineTextDlg::OnEnChangeEdtThick1)
	ON_EN_CHANGE(IDC_EDT_ALPHA1, &CTestOutlineTextDlg::OnEnChangeEdtAlpha1)
	ON_EN_CHANGE(IDC_EDT_THICK2, &CTestOutlineTextDlg::OnEnChangeEdtThick2)
	ON_EN_CHANGE(IDC_EDT_ALPHA2, &CTestOutlineTextDlg::OnEnChangeEdtAlpha2)
	ON_BN_CLICKED(IDC_BTN_IMAGEBROWSE, &CTestOutlineTextDlg::OnBnClickedBtnImagebrowse)
	ON_WM_CLOSE()
	ON_BN_CLICKED(IDC_CHK_ENABLE_SHADOW, &CTestOutlineTextDlg::OnBnClickedChkEnableShadow)
	ON_EN_CHANGE(IDC_EDT_SHADOW_THICK, &CTestOutlineTextDlg::OnEnChangeEdtShadowThick)
	ON_EN_CHANGE(IDC_EDT_SHADOW_ALPHA, &CTestOutlineTextDlg::OnEnChangeEdtShadowAlpha)
	ON_EN_CHANGE(IDC_EDT_SHADOW_OFFSETX, &CTestOutlineTextDlg::OnEnChangeEdtShadowOffsetx)
	ON_EN_CHANGE(IDC_EDT_SHADOW_OFFSETY, &CTestOutlineTextDlg::OnEnChangeEdtShadowOffsety)
	ON_BN_CLICKED(IDC_BTN_SHADOW_COLOR, &CTestOutlineTextDlg::OnBnClickedBtnShadowColor)
	ON_BN_CLICKED(IDC_CHK_ENABLE_PNG_RENDERING, &CTestOutlineTextDlg::OnBnClickedChkEnablePngRendering)
	ON_BN_CLICKED(IDC_BTN_RENDER, &CTestOutlineTextDlg::OnBnClickedBtnRender)
	ON_BN_CLICKED(IDC_CHK_DIFFUSED_SHADOW, &CTestOutlineTextDlg::OnBnClickedChkDiffusedShadow)
	ON_BN_CLICKED(IDC_CHK_EXTRUDED_TEXT, &CTestOutlineTextDlg::OnBnClickedChkExtrudedText)
	ON_BN_CLICKED(IDC_CHK_GRADIENT_TEXT, &CTestOutlineTextDlg::OnBnClickedChkGradientText)
	ON_BN_CLICKED(IDC_BTN_TEXT_COLOR3, &CTestOutlineTextDlg::OnBnClickedBtnTextColor2)
	ON_BN_CLICKED(IDC_BTN_RELECT_SETTINGS, &CTestOutlineTextDlg::OnBnClickedBtnRelectSettings)
	ON_BN_CLICKED(IDC_CHK_REFLECT, &CTestOutlineTextDlg::OnBnClickedChkReflect)
	ON_BN_CLICKED(IDC_BTN_SAVE_PNG, &CTestOutlineTextDlg::OnBnClickedBtnSavePng)
	ON_BN_CLICKED(IDC_BTN_SAVE_REFLECTIVE_PNG, &CTestOutlineTextDlg::OnBnClickedBtnSaveReflectivePng)
END_MESSAGE_MAP()


// CTestOutlineTextDlg message handlers

BOOL CTestOutlineTextDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// Initialize the scroll view
	CRect rect;
	GetClientRect(&rect);
	CClientDC dc(this);
	int PosY = 190*dc.GetDeviceCaps(LOGPIXELSY)/96;
	int nMargin = 7*dc.GetDeviceCaps(LOGPIXELSY)/96;
	CRect rect2(nMargin,PosY,rect.Width()-nMargin*2,rect.Height()-nMargin-PosY);
	CRuntimeClass *pClass = RUNTIME_CLASS(CMyScrollView);
	m_pScrollView = (CMyScrollView*)pClass->CreateObject();
	m_pScrollView->Create(NULL, NULL, WS_CHILD | WS_VISIBLE, rect2, this, 10001);
	m_pScrollView->ModifyStyleEx(0, WS_EX_CLIENTEDGE);
	m_pScrollView->OnInitialUpdate();
	m_pScrollView->SetWindowPos(
		&m_staticShadowColor,
		nMargin,PosY,
		rect.Width()-nMargin*2,
		rect.Height()-nMargin-PosY,
		SWP_SHOWWINDOW);

	m_pScrollView->Render(false);

	// Initialize buttons
	COLORREF color= RGB(64,0,64);
	m_btnOutlineColor1.SetColor(color);
	m_pScrollView->SetOutlineColor1(Gdiplus::Color(GetRValue(color),GetGValue(color),GetBValue(color)));
	COLORREF color2= RGB(255,128,255);
	m_btnOutlineColor2.SetColor(color2);
	m_pScrollView->SetOutlineColor2(Gdiplus::Color(GetRValue(color2),GetGValue(color2),GetBValue(color2)));
	COLORREF colorText= RGB(255, 128, 64);
	m_btnTextColor.SetColor(colorText);
	m_pScrollView->SetTextColor1(Gdiplus::Color(GetRValue(colorText),GetGValue(colorText),GetBValue(colorText)));
	COLORREF colorText2= RGB(255,0,0);
	m_btnTextColor2.SetColor(colorText2);
	m_pScrollView->SetTextColor2(Gdiplus::Color(GetRValue(colorText2),GetGValue(colorText2),GetBValue(colorText2)));
	m_chkGradientText.SetCheck(1);
	m_pScrollView->EnableTextGradient(true);

	// initialize combo.
	m_cboTextEffect.AddString(_T("Single Outline"));
	m_cboTextEffect.AddString(_T("Double Outline"));
	m_cboTextEffect.AddString(_T("Text Glow"));
	m_cboTextEffect.AddString(_T("Gradient Outline"));
	m_cboTextEffect.AddString(_T("Just Outline"));
	m_cboTextEffect.AddString(_T("No Outline"));
	m_cboTextEffect.AddString(_T("Double Text Glow"));
	m_cboTextEffect.SetCurSel(3);
	m_pScrollView->SetTextEffect(GradOutline);
	EnableOutline2UI(TRUE, TRUE);
	m_cboTextPath.AddString(_T("GDI+"));
	m_cboTextPath.AddString(_T("GDI"));
	m_cboTextPath.SetCurSel(0);
	m_pScrollView->SetTextPath(GdiPlus);
	m_edtAngle.EnableWindow(FALSE);
	m_staticAngle.EnableWindow(FALSE);

	// Set the font
	//LOGFONT lf;
	memset(&m_lf, 0, sizeof(m_lf));
	m_lf.lfHeight = -48; //-MulDiv(48, dc.GetDeviceCaps(LOGPIXELSY), 72);
	m_lf.lfWeight = FW_NORMAL;
	//m_lf.lfItalic = TRUE;
	//m_lf.lfOrientation = 100;
	//m_lf.lfEscapement = 100;
	//m_lf.lfOutPrecision = OUT_TT_ONLY_PRECIS;
	m_lf.lfOutPrecision = OUT_DEFAULT_PRECIS;
	m_lf.lfClipPrecision = CLIP_DEFAULT_PRECIS;
	m_lf.lfQuality = PROOF_QUALITY;

	wcscpy_s(m_lf.lfFaceName, _T("Arial Black"));
	m_edtFont.SetWindowText(_T("Arial Black, 36 Regular"));
	m_pScrollView->SetLogFont(&m_lf);
	m_pScrollView->SetFontSize(48);

	// Set the Text
	CString str = _T("TEXT DESIGNER");
	m_edtText.SetWindowText(str);
	m_pScrollView->SetText(str);

	// Set the Alphas
	m_edtOutlineAlpha1.SetWindowText(_T("255"));
	m_pScrollView->SetOutlineAlpha1(255);
	m_edtOutlineAlpha2.SetWindowText(_T("255"));
	m_pScrollView->SetOutlineAlpha2(255);

	// Set the Thickness
	m_edtThickness1.SetWindowText(_T("5"));
	m_pScrollView->SetOutlineThickness1(5);
	m_edtThickness2.SetWindowText(_T("5"));
	m_pScrollView->SetOutlineThickness2(5);

	// Set Text Position
	m_edtPosX.SetWindowText(_T("10"));
	m_pScrollView->SetTextPosX(10);
	m_edtPosY.SetWindowText(_T("10"));
	m_pScrollView->SetTextPosY(10);
	m_edtAngle.SetWindowText(_T("0"));
	m_pScrollView->SetTextAngle(0);

	// Set Shadow Properties
	m_chkEnableShadow.SetCheck(1);
	//m_pScrollView->SetEnableShadow(true);
	OnBnClickedChkEnableShadow();
	m_edtShadowAlpha.SetWindowText(_T("128"));
	m_pScrollView->SetShadowAlpha(128);
	m_edtShadowOffsetX.SetWindowText(_T("4"));
	m_pScrollView->SetShadowOffsetX(4);
	m_edtShadowOffsetY.SetWindowText(_T("4"));
	m_pScrollView->SetShadowOffsetY(4);
	m_btnShadowColor.SetColor(RGB(0,0,0));
	m_pScrollView->SetShadowColor(Gdiplus::Color(0,0,0));
	m_edtShadowThickness.SetWindowText(_T("8"));
	m_pScrollView->SetShadowThickness(8);
	m_chkDiffusedShadow.SetCheck(0);
	OnBnClickedChkDiffusedShadow();

	// PNG rendering
	m_chkPngRendering.SetCheck(0);
	OnBnClickedChkEnablePngRendering();

	m_chkExtrudedText.SetCheck(0);
	OnBnClickedChkExtrudedText();

	m_chkEnableReflection.SetCheck(0);
	m_chkEnableReflection.EnableWindow(FALSE);
	m_pScrollView->EnableReflection(false);
	m_btnReflectSettings.EnableWindow(FALSE);
	m_btnSavePng.EnableWindow(FALSE);
	m_btnSaveRefPng.EnableWindow(FALSE);

	//m_btnFont.EnableWindow(FALSE);

	m_pScrollView->Render(true);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CTestOutlineTextDlg::OnPaint()
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
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CTestOutlineTextDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


BOOL CTestOutlineTextDlg::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt)
{
	m_pScrollView->OnMouseWheel(nFlags, zDelta, pt);

	return CDialog::OnMouseWheel(nFlags, zDelta, pt);
}

void CTestOutlineTextDlg::OnSize(UINT nType, int cx, int cy)
{
	CDialog::OnSize(nType, cx, cy);

	if(m_pScrollView && m_pScrollView->GetSafeHwnd())
	{
		CClientDC dc(this);
		int PosY = 190*dc.GetDeviceCaps(LOGPIXELSY)/96;
		int nMargin = 7*dc.GetDeviceCaps(LOGPIXELSY)/96;
		m_pScrollView->SetWindowPos(
			&m_staticShadowColor,
			nMargin,
			PosY,
			cx-nMargin*2,
			cy-nMargin-PosY,
			SWP_SHOWWINDOW);
	}
}

void CTestOutlineTextDlg::OnBnClickedBtnBkgdColor()
{
	CColorDialog dlg(m_btnBkgdColor.GetColor(), CC_FULLOPEN, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		m_btnBkgdColor.SetColor(dlg.GetColor());
		m_pScrollView->SetBkgdColor(Gdiplus::Color(GetRValue(dlg.GetColor()),GetGValue(dlg.GetColor()),GetBValue(dlg.GetColor())));
	}

}

void CTestOutlineTextDlg::OnBnClickedBtnOutlineColor1()
{
	CColorDialog dlg(m_btnOutlineColor1.GetColor(), CC_FULLOPEN, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		m_btnOutlineColor1.SetColor(dlg.GetColor());
		m_pScrollView->SetOutlineColor1(Gdiplus::Color(GetRValue(dlg.GetColor()),GetGValue(dlg.GetColor()),GetBValue(dlg.GetColor())));
	}
}

void CTestOutlineTextDlg::OnBnClickedBtnOutlineColor2()
{
	CColorDialog dlg(m_btnOutlineColor2.GetColor(), CC_FULLOPEN, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		m_btnOutlineColor2.SetColor(dlg.GetColor());
		m_pScrollView->SetOutlineColor2(Gdiplus::Color(GetRValue(dlg.GetColor()),GetGValue(dlg.GetColor()),GetBValue(dlg.GetColor())));
	}
}

void CTestOutlineTextDlg::OnBnClickedBtnTextColor()
{
	CColorDialog dlg(m_btnTextColor.GetColor(), CC_FULLOPEN, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		m_btnTextColor.SetColor(dlg.GetColor());
		m_pScrollView->SetTextColor1(Gdiplus::Color(GetRValue(dlg.GetColor()),GetGValue(dlg.GetColor()),GetBValue(dlg.GetColor())));
	}
}

void CTestOutlineTextDlg::EnableOutline2UI(BOOL bEnable1, BOOL bEnable2)
{
	m_staticOutlineColor1.EnableWindow(bEnable1);
	m_staticAlpha1.EnableWindow(bEnable1);
	m_staticAlphaText1.EnableWindow(bEnable1);
	m_staticThick1.EnableWindow(bEnable1);
	m_edtThickness1.EnableWindow(bEnable1);
	m_edtOutlineAlpha1.EnableWindow(bEnable1);
	m_btnOutlineColor1.EnableWindow(bEnable1);

	m_staticOutlineColor2.EnableWindow(bEnable2);
	m_staticAlpha2.EnableWindow(bEnable2);
	m_staticAlphaText2.EnableWindow(bEnable2);
	m_staticThick2.EnableWindow(bEnable2);
	m_edtThickness2.EnableWindow(bEnable2);
	m_edtOutlineAlpha2.EnableWindow(bEnable2);
	m_btnOutlineColor2.EnableWindow(bEnable2);
}

void CTestOutlineTextDlg::OnBnClickedBtnFont()
{
	CFontDialog dlg(&m_lf, CF_EFFECTS | CF_SCREENFONTS, NULL, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		dlg.GetCurrentFont(&m_lf);
		
		m_pScrollView->SetLogFont(&m_lf);
		CString str;
		CString szBold = m_lf.lfWeight == FW_BOLD ? _T(" Bold") : _T("");
		CString szItalic = m_lf.lfItalic ? _T(" Italic") : _T("");

		CClientDC dc(this);
		int nSize = m_lf.lfHeight < 0 ? -m_lf.lfHeight : m_lf.lfHeight;
		m_pScrollView->SetFontSize(nSize);
		//int nSize = -(int)((m_lf.lfHeight * 72.0f / dc.GetDeviceCaps(LOGPIXELSY))+0.5f);
		str.Format(_T("%s, %d"), 
			m_lf.lfFaceName, 
			dlg.GetSize()/10 );

		if(szBold.IsEmpty() && szItalic.IsEmpty())
			str += " Normal";
		if(szBold.IsEmpty()==FALSE)
			str += szBold;
		if(szItalic.IsEmpty()==FALSE)
			str += szItalic;

		m_edtFont.SetWindowText(str);
	}
}

void CTestOutlineTextDlg::OnCbnSelchangeCboTextPath()
{
	int nSel = m_cboTextPath.GetCurSel();
	if(nSel==0)
	{
		m_pScrollView->SetTextPath(GdiPlus);
		m_edtAngle.EnableWindow(FALSE);
		m_staticAngle.EnableWindow(FALSE);
	}
	else if(nSel==1)
	{
		m_pScrollView->SetTextPath(Gdi);
		m_edtAngle.EnableWindow(TRUE);
		m_staticAngle.EnableWindow(TRUE);
	}
}

void CTestOutlineTextDlg::OnCbnSelchangeCboTextEffect()
{
	int nSel = m_cboTextEffect.GetCurSel();
	if(nSel==0)
	{
		m_pScrollView->Render(false);

		m_pScrollView->SetTextEffect(SingleOutline);
		EnableOutline2UI(TRUE, FALSE);

		if(m_pScrollView->GetOutlineAlpha1()<255)
			m_edtOutlineAlpha1.SetWindowText(_T("255"));

		m_pScrollView->Render(true);
	}
	else if(nSel==1)
	{
		m_pScrollView->Render(false);

		m_pScrollView->SetTextEffect(DblOutline);
		EnableOutline2UI(TRUE, TRUE);

		if(m_pScrollView->GetOutlineAlpha1()<255)
			m_edtOutlineAlpha1.SetWindowText(_T("255"));
		if(m_pScrollView->GetOutlineAlpha2()<255)
			m_edtOutlineAlpha2.SetWindowText(_T("255"));

		m_pScrollView->Render(true);
	}
	else if(nSel==2)
	{
		m_pScrollView->Render(false);

		m_pScrollView->SetTextEffect(TextGlow);
		EnableOutline2UI(TRUE, FALSE);

		if(m_pScrollView->GetOutlineAlpha1()>84)
			m_edtOutlineAlpha1.SetWindowText(_T("64"));

		if(m_pScrollView->GetOutlineThickness1()<8)
			m_edtThickness1.SetWindowText(_T("12"));

		m_pScrollView->Render(true);
	}
	else if(nSel==3)
	{
		m_pScrollView->Render(false);

		m_pScrollView->SetTextEffect(GradOutline);
		EnableOutline2UI(TRUE, TRUE);

		if(m_pScrollView->GetOutlineAlpha1()<255)
			m_edtOutlineAlpha1.SetWindowText(_T("255"));
		if(m_pScrollView->GetOutlineAlpha2()<255)
			m_edtOutlineAlpha2.SetWindowText(_T("255"));

		m_pScrollView->Render(true);
	}
	else if(nSel==4)
	{
		m_pScrollView->Render(false);

		m_pScrollView->SetTextEffect(OnlyOutline);
		EnableOutline2UI(TRUE, FALSE);

		if(m_pScrollView->GetOutlineAlpha1()<255)
			m_edtOutlineAlpha1.SetWindowText(_T("255"));

		m_pScrollView->Render(true);
	}
	else if(nSel==5)
	{
		m_pScrollView->SetTextEffect(NoOutline);
		EnableOutline2UI(FALSE, FALSE);
	}
	else if(nSel==6)
	{
		m_pScrollView->Render(false);

		m_pScrollView->SetTextEffect(DblGlow);
		EnableOutline2UI(TRUE, TRUE);

		if(m_pScrollView->GetOutlineAlpha1()<255)
			m_edtOutlineAlpha1.SetWindowText(_T("255"));
		if(m_pScrollView->GetOutlineAlpha2()>84)
			m_edtOutlineAlpha2.SetWindowText(_T("64"));

		m_pScrollView->Render(true);
	}

}

void CTestOutlineTextDlg::OnEnChangeEdtText()
{
	CString str;
	m_edtText.GetWindowText(str);
	m_pScrollView->SetText(str);
}

void CTestOutlineTextDlg::OnEnChangeEdtPosx()
{
	CString str;
	m_edtPosX.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetTextPosX(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtPosy()
{
	CString str;
	m_edtPosY.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetTextPosY(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtAngle()
{
	CString str;
	m_edtAngle.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetTextAngle(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtThick1()
{
	CString str;
	m_edtThickness1.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetOutlineThickness1(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtAlpha1()
{
	CString str;
	m_edtOutlineAlpha1.GetWindowText(str);
	int n = _wtoi(str);
	if(n>255)
		n=255;
	if(n<0)
		n=0;
	m_pScrollView->SetOutlineAlpha1(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtThick2()
{
	CString str;
	m_edtThickness2.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetOutlineThickness2(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtAlpha2()
{
	CString str;
	m_edtOutlineAlpha2.GetWindowText(str);
	int n = _wtoi(str);
	if(n>255)
		n=255;
	if(n<0)
		n=0;
	m_pScrollView->SetOutlineAlpha2(n);
}

void CTestOutlineTextDlg::OnBnClickedBtnImagebrowse()
{
	TCHAR szFilter[] = _T("All Files (*.*)||");

	CFileDialog fdlg( 
		TRUE, NULL, NULL, 
		OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		szFilter, AfxGetMainWnd() );

	if( IDOK == fdlg.DoModal( ) )
	{
		CString szBuf = fdlg.GetPathName();

		m_pScrollView->SetImage(szBuf);
	}
}

void CTestOutlineTextDlg::OnClose()
{
	//int result = MessageBox(_T("Do you want to quit the application?"), _T("Closing"), MB_YESNO);

	//if(IDNO == result)
	//	return;

	CDialog::OnClose();
}

void CTestOutlineTextDlg::OnOK()
{
	//int result = MessageBox(_T("Do you want to quit the application?"), _T("Closing"), MB_YESNO);

	//if(IDNO == result)
	//	return;

	CDialog::OnOK();
}

void CTestOutlineTextDlg::OnBnClickedChkEnableShadow()
{
	if(m_chkEnableShadow.GetCheck())
	{
		m_staticShadowColor.EnableWindow();
		m_btnShadowColor.EnableWindow();
		m_edtShadowThickness.EnableWindow();
		m_edtShadowAlpha.EnableWindow();
		m_edtShadowOffsetX.EnableWindow();
		m_edtShadowOffsetY.EnableWindow();
		m_pScrollView->SetEnableShadow(true);
		m_chkDiffusedShadow.EnableWindow();
		m_chkExtrudedText.EnableWindow();
		if(m_chkExtrudedText.GetCheck())
			m_chkDiffusedShadow.EnableWindow(FALSE);
	}
	else
	{
		m_staticShadowColor.EnableWindow(FALSE);
		m_btnShadowColor.EnableWindow(FALSE);
		m_edtShadowThickness.EnableWindow(FALSE);
		m_edtShadowAlpha.EnableWindow(FALSE);
		m_edtShadowOffsetX.EnableWindow(FALSE);
		m_edtShadowOffsetY.EnableWindow(FALSE);
		m_pScrollView->SetEnableShadow(false);
		m_chkDiffusedShadow.EnableWindow(FALSE);
		m_chkExtrudedText.EnableWindow(FALSE);
	}
}

void CTestOutlineTextDlg::OnEnChangeEdtShadowThick()
{
	CString str;
	m_edtShadowThickness.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetShadowThickness(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtShadowAlpha()
{
	CString str;
	m_edtShadowAlpha.GetWindowText(str);
	int n = _wtoi(str);
	if(n>255)
		n=255;
	if(n<0)
		n=0;
	m_pScrollView->SetShadowAlpha(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtShadowOffsetx()
{
	CString str;
	m_edtShadowOffsetX.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetShadowOffsetX(n);
}

void CTestOutlineTextDlg::OnEnChangeEdtShadowOffsety()
{
	CString str;
	m_edtShadowOffsetY.GetWindowText(str);
	int n = _wtoi(str);
	m_pScrollView->SetShadowOffsetY(n);
}

void CTestOutlineTextDlg::OnBnClickedBtnShadowColor()
{
	CColorDialog dlg(m_btnShadowColor.GetColor(), CC_FULLOPEN, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		m_btnShadowColor.SetColor(dlg.GetColor());
		m_pScrollView->SetShadowColor(Gdiplus::Color(GetRValue(dlg.GetColor()),GetGValue(dlg.GetColor()),GetBValue(dlg.GetColor())));
	}
}

void CTestOutlineTextDlg::OnBnClickedChkEnablePngRendering()
{
	if(m_chkPngRendering.GetCheck())
	{
		m_chkEnableReflection.EnableWindow(TRUE);
		m_btnSavePng.EnableWindow(TRUE);
		if(m_chkEnableReflection.GetCheck())
		{
			m_btnReflectSettings.EnableWindow(TRUE);
			m_btnSaveRefPng.EnableWindow(TRUE);
		}
		else
		{
			m_btnReflectSettings.EnableWindow(FALSE);
			m_btnSaveRefPng.EnableWindow(FALSE);
		}
		m_pScrollView->EnablePngRendering(true);
	}
	else
	{
		m_chkEnableReflection.EnableWindow(FALSE);
		m_btnReflectSettings.EnableWindow(FALSE);
		m_btnSavePng.EnableWindow(FALSE);
		m_btnSaveRefPng.EnableWindow(FALSE);
		m_pScrollView->EnablePngRendering(false);
	}
}

void CTestOutlineTextDlg::OnBnClickedBtnRender()
{
}

void CTestOutlineTextDlg::OnBnClickedChkDiffusedShadow()
{
	if(m_chkDiffusedShadow.GetCheck())
	{
		m_pScrollView->Render(false);
		m_pScrollView->SetDiffusedShadow(true);

		if(m_pScrollView->GetShadowAlpha()>48)
			m_edtShadowAlpha.SetWindowText(_T("16"));

		m_pScrollView->Render(true);
	}
	else
	{
		m_pScrollView->Render(false);
		m_pScrollView->SetDiffusedShadow(false);

		if(m_pScrollView->GetShadowAlpha()<48)
			m_edtShadowAlpha.SetWindowText(_T("128"));

		m_pScrollView->Render(true);
	}
}

bool CTestOutlineTextDlg::CopyTextToClipboard(CString& str)
{
   LPTSTR  lptstrCopy; 
   HGLOBAL hglbCopy; 

   if(str.IsEmpty())
	   return false;

	// Open the clipboard, and empty it. 
	if (!OpenClipboard())
		return false; 
	
	EmptyClipboard(); 

	// Allocate a global memory object for the text. 
	hglbCopy = GlobalAlloc(GMEM_MOVEABLE, 
		(str.GetLength() + 1) * sizeof(TCHAR)); 
	if (hglbCopy == NULL) 
	{ 
		CloseClipboard(); 
		return FALSE; 
	} 

	// Lock the handle and copy the text to the buffer. 

	lptstrCopy = (LPTSTR)(GlobalLock(hglbCopy)); 
	memcpy(lptstrCopy, (void*)(str.GetBuffer(str.GetLength())), 
		str.GetLength() * sizeof(TCHAR)); 
	lptstrCopy[str.GetLength()] = (TCHAR) 0;    // null character 
	GlobalUnlock(hglbCopy); 

	// Place the handle on the clipboard. 

	SetClipboardData(CF_UNICODETEXT, hglbCopy); 

	CloseClipboard(); 

	return true; 
}

void CTestOutlineTextDlg::OnBnClickedChkExtrudedText()
{
	if(m_chkExtrudedText.GetCheck())
	{
		m_pScrollView->Render(false);

		m_chkDiffusedShadow.EnableWindow(FALSE);
		m_pScrollView->SetExtrudedText(true);

		if(m_pScrollView->GetShadowAlpha()<255)
			m_edtShadowAlpha.SetWindowText(_T("255"));

		m_pScrollView->Render(true);
	}
	else
	{
		m_chkDiffusedShadow.EnableWindow(TRUE);
		m_pScrollView->SetExtrudedText(false);
	}
}

void CTestOutlineTextDlg::OnBnClickedChkGradientText()
{
	if(m_chkGradientText.GetCheck())
	{
		m_btnTextColor2.EnableWindow(TRUE);
		m_staticTextColor2.EnableWindow(TRUE);
		m_pScrollView->EnableTextGradient(true);
	}
	else
	{
		m_btnTextColor2.EnableWindow(FALSE);
		m_staticTextColor2.EnableWindow(FALSE);
		m_pScrollView->EnableTextGradient(false);
	}
}

void CTestOutlineTextDlg::OnBnClickedBtnTextColor2()
{
	CColorDialog dlg(m_btnTextColor2.GetColor(), CC_FULLOPEN, this);
	INT_PTR result = dlg.DoModal();

	if(IDOK == result)
	{
		m_btnTextColor2.SetColor(dlg.GetColor());
		m_pScrollView->SetTextColor2(Gdiplus::Color(GetRValue(dlg.GetColor()),GetGValue(dlg.GetColor()),GetBValue(dlg.GetColor())));
	}
}


void CTestOutlineTextDlg::OnBnClickedBtnRelectSettings()
{
	CReflectSettingDlg dlg;
	dlg.SetBegAlpha(m_pScrollView->GetBegAlpha());
	dlg.SetEndAlpha(m_pScrollView->GetEndAlpha());
	dlg.SetShown(m_pScrollView->GetShown());
	dlg.SetGap(m_pScrollView->GetGap());

	if( IDOK == dlg.DoModal() )
	{
		m_pScrollView->Render(false);
		m_pScrollView->SetBegAlpha(dlg.GetBegAlpha());
		m_pScrollView->SetEndAlpha(dlg.GetEndAlpha());
		m_pScrollView->SetShown(dlg.GetShown());
		m_pScrollView->SetGap(dlg.GetGap());
		m_pScrollView->Render(true);
	}
}


void CTestOutlineTextDlg::OnBnClickedChkReflect()
{
	if(m_chkEnableReflection.GetCheck())
	{
		m_btnReflectSettings.EnableWindow(TRUE);
		m_btnSaveRefPng.EnableWindow(TRUE);
		m_pScrollView->EnableReflection(true);
	}
	else
	{
		m_btnReflectSettings.EnableWindow(FALSE);
		m_btnSaveRefPng.EnableWindow(FALSE);
		m_pScrollView->EnableReflection(false);
	}
}

void CTestOutlineTextDlg::OnBnClickedBtnSavePng()
{
	TCHAR szFilter[] = _T("PNG Image files (*.png)||");

	CFileDialog fdlg( 
		FALSE, NULL, NULL, 
		OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		szFilter, AfxGetMainWnd() );

	if( IDOK == fdlg.DoModal( ) )
	{
		CString szBuf = fdlg.GetPathName();

		if(szBuf.Find(_T(".png"))==-1 && szBuf.Find(_T(".PNG"))==-1 && szBuf.Find(_T(".Png"))==-1)
		{
			szBuf += _T(".png");
		}

		if(m_pScrollView->IsEnablePngRendering())
			m_pScrollView->SavePngImage(szBuf);
	}

}

void CTestOutlineTextDlg::OnBnClickedBtnSaveReflectivePng()
{
	TCHAR szFilter[] = _T("PNG Image files (*.png)||");

	CFileDialog fdlg( 
		FALSE, NULL, NULL, 
		OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		szFilter, AfxGetMainWnd() );

	if( IDOK == fdlg.DoModal( ) )
	{
		CString szBuf = fdlg.GetPathName();

		if(szBuf.Find(_T(".png"))==-1 && szBuf.Find(_T(".PNG"))==-1 && szBuf.Find(_T(".Png"))==-1)
		{
			szBuf += _T(".png");
		}

		if(m_pScrollView->IsEnablePngRendering()&&m_pScrollView->IsEnableReflection())
			m_pScrollView->SaveRefPngImage(szBuf);
	}
}
