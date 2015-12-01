
// Fake3DMFC.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CFake3DMFCApp:
// See Fake3DMFC.cpp for the implementation of this class
//

class CFake3DMFCApp : public CWinApp
{
public:
	CFake3DMFCApp();
	~CFake3DMFCApp();
private:
	Gdiplus::GdiplusStartupInput m_gdiplusStartupInput;
	ULONG_PTR m_gdiplusToken;

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CFake3DMFCApp theApp;