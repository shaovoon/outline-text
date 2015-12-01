
// AquarionMFC.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CAquarionMFCApp:
// See AquarionMFC.cpp for the implementation of this class
//

class CAquarionMFCApp : public CWinApp
{
public:
	CAquarionMFCApp();
	~CAquarionMFCApp();
private:
	Gdiplus::GdiplusStartupInput m_gdiplusStartupInput;
	ULONG_PTR m_gdiplusToken;

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CAquarionMFCApp theApp;