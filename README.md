# Outline-Text

This is GDI/GDI+ library for drawing text with outline in C++ MFC and C# Windows Form.

[Version 1 Tutorial](https://www.codeproject.com/Articles/42529/Outline-Text)

When using C++ library, you need to startup/shutdown GDI+, preferably in main class constructor and destructor.

```
// class declaration
class CTestOutlineTextApp
{
// ...
private:
    // data members
    Gdiplus::GdiplusStartupInput m_gdiplusStartupInput;
    ULONG_PTR m_gdiplusToken;
};

// default constructor
CTestOutlineTextApp::CTestOutlineTextApp()
{
    Gdiplus::GdiplusStartup(&m_gdiplusToken, &m_gdiplusStartupInput, NULL);
}

// destructor
CTestOutlineTextApp::~CTestOutlineTextApp()
{
    Gdiplus::GdiplusShutdown(m_gdiplusToken);
}
```

# Version 1 Example

![GitHub Logo](/images/singleoutline.png)

C++ code for doing the above outline outline, using GDI+

```
#include "TextDesigner/OutlineText.h"

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial Black");
    StringFormat strformat;
    wchar_t pszbuf[] = L"Text Designer";

    TextDesigner::OutlineText text;
    text.TextOutline(Color(255,128,64),Color(200,0,0),8);
    text.EnableShadow(true);
    CRect rect;
    GetClientRect(&rect);
    text.SetShadowBkgd(Color(255,255,0),rect.Width(),rect.Height());
    text.Shadow(Color(128,0,0,0), 4, Point(4,8));
    text.DrawString(&graphics,&fontFamily,FontStyleItalic, 
        48, pszbuf, Gdiplus::Point(10,10), &strformat);
}
```

C# code for doing the above outline, using GDI+

```
using TextDesignerCSLibrary;

private void Form1_Paint(object sender, PaintEventArgs e)
{

	e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
	e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

	FontFamily fontFamily = new FontFamily("Arial Black");
	StringFormat strformat = new StringFormat();
	string szbuf = "Text Designer";

	OutlineText text = new OutlineText();
	text.TextOutline(Color.FromArgb(255, 128, 64), Color.FromArgb(200, 0, 0), 8);
	text.EnableShadow(true);
	text.SetShadowBkgd(Color.FromArgb(255, 255, 0), this.Size.Width, this.Size.Height);
	text.Shadow(Color.FromArgb(128, 0, 0, 0), 4, new Point(4, 8));
	text.DrawString(e.Graphics, fontFamily,
		FontStyle.Italic, 48, szbuf, new Point(10, 10), strformat);

	fontFamily.Dispose();
	e.Graphics.Dispose();
}
```


[Version 2 Tutorial](https://www.codeproject.com/Articles/865246/Outline-Text-Part)
