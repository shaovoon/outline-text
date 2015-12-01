// ScratchPadDlg.h : header file
//

#pragma once


// CScratchPadDlg dialog
class CScratchPadDlg : public CDialog
{
// Construction
public:
	CScratchPadDlg(CWnd* pParent = NULL);	// standard constructor
	~CScratchPadDlg();

// Dialog Data
	enum { IDD = IDD_SCRATCHPAD_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

private:
	void LoadSrcImage();

	Gdiplus::Bitmap* m_pSrcBitmap;

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
};
