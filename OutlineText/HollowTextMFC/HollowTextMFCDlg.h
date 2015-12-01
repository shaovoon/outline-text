
// HollowTextMFCDlg.h : header file
//

#pragma once


// CHollowTextMFCDlg dialog
class CHollowTextMFCDlg : public CDialogEx
{
// Construction
public:
	CHollowTextMFCDlg(CWnd* pParent = NULL);	// standard constructor

	int m_nTimerLoop;
// Dialog Data
	enum { IDD = IDD_HOLLOWTEXTMFC_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnTimer(UINT_PTR nIDEvent);
};
