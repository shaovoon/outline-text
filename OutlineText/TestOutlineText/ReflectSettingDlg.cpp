// ReflectSettingDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TestOutlineText.h"
#include "ReflectSettingDlg.h"
//#include "afxdialog.h"


// CReflectSettingDlg dialog

IMPLEMENT_DYNAMIC(CReflectSettingDlg, CDialog)

CReflectSettingDlg::CReflectSettingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CReflectSettingDlg::IDD, pParent)
{
	m_fStartAlpha = 0.9f;
	m_fEndAlpha = 0.7f;
	m_fShown = 0.5f;
	m_nGap = 2;
}

CReflectSettingDlg::~CReflectSettingDlg()
{
}

void CReflectSettingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDT_START_ALPHA, m_edtStartAlpha);
	DDX_Control(pDX, IDC_EDT_END_ALPHA, m_edtEndAlpha);
	DDX_Control(pDX, IDC_EDT_SHOWN, m_edtShown);
	DDX_Control(pDX, IDC_EDT_GAP, m_edtGap);
}


BEGIN_MESSAGE_MAP(CReflectSettingDlg, CDialog)
	ON_BN_CLICKED(IDOK, &CReflectSettingDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CReflectSettingDlg message handlers
BOOL CReflectSettingDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	CString str;
	str.Format(_T("%.2f"), m_fStartAlpha);
	m_edtStartAlpha.SetWindowTextW(str);
	str.Format(_T("%.2f"), m_fEndAlpha);
	m_edtEndAlpha.SetWindowTextW(str);
	str.Format(_T("%.2f"), m_fShown);
	m_edtShown.SetWindowTextW(str);
	str.Format(_T("%d"), m_nGap);
	m_edtGap.SetWindowTextW(str);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CReflectSettingDlg::OnBnClickedOk()
{
	CString szTemp;
	m_edtStartAlpha.GetWindowTextW(szTemp);
	m_fStartAlpha = _wtof(szTemp);

	if(m_fStartAlpha < 0.0f || m_fStartAlpha > 1.0f)
	{
		MessageBox(_T("Starting transparency is lesser than 0.0 or greater than 1.0"), _T("Error"), MB_OK);
		return;
	}

	m_edtEndAlpha.GetWindowTextW(szTemp);
	m_fEndAlpha = _wtof(szTemp);

	if(m_fEndAlpha < 0.0f || m_fEndAlpha > 1.0f)
	{
		MessageBox(_T("Ending transparency is lesser than 0.0 or greater than 1.0"), _T("Error"), MB_OK);
		return;
	}

	m_edtShown.GetWindowTextW(szTemp);
	m_fShown = _wtof(szTemp);

	if(m_fShown < 0.0f || m_fShown > 1.0f)
	{
		MessageBox(_T("Image shown is lesser than 0.0 or greater than 1.0"), _T("Error"), MB_OK);
		return;
	}

	m_edtGap.GetWindowTextW(szTemp);
	m_nGap = _wtoi(szTemp);

	CDialog::OnOK();
}


