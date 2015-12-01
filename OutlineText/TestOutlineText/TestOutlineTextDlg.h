// TestOutlineTextDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "ColorButton.h"
#include "MyButton.h"
#include "MyScrollView.h"

// CTestOutlineTextDlg dialog
class CTestOutlineTextDlg : public CDialog
{
// Construction
public:
	CTestOutlineTextDlg(CWnd* pParent = NULL);	// standard constructor
protected:
	void OnOK();
private:
	CMyScrollView* m_pScrollView;
private:
	void EnableOutline2UI(BOOL bEnable1, BOOL bEnable2);


// Dialog Data
	enum { IDD = IDD_TESTOUTLINETEXT_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	bool CopyTextToClipboard(CString& str);


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CComboBox m_cboTextPath;
	CComboBox m_cboTextEffect;
	CEdit m_edtText;
	CMyButton m_btnFont;
	CEdit m_edtPosX;
	CEdit m_edtPosY;
	CStatic m_staticAngle;
	CEdit m_edtAngle;
	CColorButton m_btnBkgdColor;
	CMyButton m_btnImageBrowse;
	CColorButton m_btnOutlineColor1;
	CEdit m_edtThickness1;
	CEdit m_edtOutlineAlpha1;
	CStatic m_staticOutlineColor2;
	CColorButton m_btnOutlineColor2;
	CStatic m_staticThick2;
	CEdit m_edtThickness2;
	CStatic m_staticAlpha2;
	CEdit m_edtOutlineAlpha2;
	CStatic m_staticAlphaText2;
	CColorButton m_btnTextColor;
	CEdit m_edtFont;
	CStatic m_staticShadowColor;
	CColorButton m_btnShadowColor;
	CEdit m_edtShadowThickness;
	CEdit m_edtShadowAlpha;
	CEdit m_edtShadowOffsetX;
	CEdit m_edtShadowOffsetY;
	CButton m_chkEnableShadow;
	CButton m_chkPngRendering;
	CMyButton m_btnRender;
	CButton m_chkDiffusedShadow;
	LOGFONT m_lf;
	CButton m_chkExtrudedText;
	CButton m_chkGradientText;
	CColorButton m_btnTextColor2;
	CStatic m_staticTextColor2;
	CStatic m_staticOutlineColor1;
	CStatic m_staticAlpha1;
	CStatic m_staticAlphaText1;
	CStatic m_staticThick1;
	CButton m_chkEnableReflection;
	CMyButton m_btnReflectSettings;
	CMyButton m_btnSavePng;
	CMyButton m_btnSaveRefPng;
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint pt);
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnBnClickedBtnBkgdColor();
	afx_msg void OnBnClickedBtnOutlineColor1();
	afx_msg void OnBnClickedBtnOutlineColor2();
	afx_msg void OnBnClickedBtnTextColor();
	afx_msg void OnBnClickedBtnFont();
	afx_msg void OnCbnSelchangeCboTextPath();
	afx_msg void OnCbnSelchangeCboTextEffect();
	afx_msg void OnEnChangeEdtText();
	afx_msg void OnEnChangeEdtPosx();
	afx_msg void OnEnChangeEdtPosy();
	afx_msg void OnEnChangeEdtAngle();
	afx_msg void OnEnChangeEdtThick1();
	afx_msg void OnEnChangeEdtAlpha1();
	afx_msg void OnEnChangeEdtThick2();
	afx_msg void OnEnChangeEdtAlpha2();
	afx_msg void OnBnClickedBtnImagebrowse();
	afx_msg void OnClose();
	afx_msg void OnBnClickedChkEnableShadow();
	afx_msg void OnEnChangeEdtShadowThick();
	afx_msg void OnEnChangeEdtShadowAlpha();
	afx_msg void OnEnChangeEdtShadowOffsetx();
	afx_msg void OnEnChangeEdtShadowOffsety();
	afx_msg void OnBnClickedBtnShadowColor();
	afx_msg void OnBnClickedChkEnablePngRendering();
	afx_msg void OnBnClickedBtnRender();
	afx_msg void OnBnClickedChkDiffusedShadow();
	afx_msg void OnBnClickedChkExtrudedText();
	afx_msg void OnBnClickedChkGradientText();
	afx_msg void OnBnClickedBtnTextColor2();
	afx_msg void OnBnClickedBtnRelectSettings();
	afx_msg void OnBnClickedChkReflect();
	afx_msg void OnBnClickedBtnSavePng();
	afx_msg void OnBnClickedBtnSaveReflectivePng();
};
