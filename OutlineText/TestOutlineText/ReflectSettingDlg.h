#pragma once
#include "afxwin.h"


// CReflectSettingDlg dialog

class CReflectSettingDlg : public CDialog
{
	DECLARE_DYNAMIC(CReflectSettingDlg)

public:
	CReflectSettingDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CReflectSettingDlg();

	void SetBegAlpha(float f) { m_fStartAlpha = f; }
	void SetEndAlpha(float f) { m_fEndAlpha = f; }
	void SetShown(float f) { m_fShown = f; }
	void SetGap(int n) { m_nGap = n; }

	float GetBegAlpha() { return m_fStartAlpha; }
	float GetEndAlpha() { return m_fEndAlpha; }
	float GetShown() { return m_fShown; }
	int GetGap() { return m_nGap; }

// Dialog Data
	enum { IDD = IDD_DLG_REFLECT_SETTINGS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CEdit m_edtStartAlpha;
	CEdit m_edtEndAlpha;
	CEdit m_edtShown;
	CEdit m_edtGap;
	float m_fStartAlpha;
	float m_fEndAlpha;
	float m_fShown;
	int m_nGap;

	afx_msg void OnBnClickedOk();
	virtual BOOL OnInitDialog();
};
