
// Fake3D2MFC.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CFake3D2MFCApp:
// See Fake3D2MFC.cpp for the implementation of this class
//

class CFake3D2MFCApp : public CWinApp
{
public:
	CFake3D2MFCApp();
	~CFake3D2MFCApp();
private:
	Gdiplus::GdiplusStartupInput m_gdiplusStartupInput;
	ULONG_PTR m_gdiplusToken;

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CFake3D2MFCApp theApp;