
// Fake3D2MFCDlg.h : header file
//

#pragma once


// CFake3D2MFCDlg dialog
class CFake3D2MFCDlg : public CDialogEx
{
// Construction
public:
	CFake3D2MFCDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_FAKE3D2MFC_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
	void DrawChar( int x_offset, CRect &rect, TextDesigner::TextContext& context, Gdiplus::Graphics &graphics, Gdiplus::Matrix& mat );


	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();


	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
};
