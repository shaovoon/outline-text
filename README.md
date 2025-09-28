# Outline Text in C++ and CSharp

![century_gothic_3d1.png](/images/century_gothic_3d1.png)

![century_gothic_text_glow1.png](/images/century_gothic_text_glow1.png)

![comic_sans_bold1.png](/images/comic_sans_bold1.png)

![engravers_double_outline1.png](/images/engravers_double_outline1.png)

![impact_italic1.png](/images/impact_italic1.png)

![matisse_double_outline1.png](/images/matisse_double_outline1.png)

## Table of Contents

* Introduction
* Initializing and Uninitializing GDI+
* Drawing Single Outline Text with Generic GDI+
* Drawing Single Outline Text with Gradient Color
* Drawing Double Outline Text with Generic GDI+
* Drawing Text Glow with Generic GDI+
* Postscript OpenType Fonts
* Drawing Outline Text using DirectWrite
* What about Drawing Shadows?
* Drawing Single Outline using OutlineText
* Drawing Single Outline Text with Gradient Color using OutlineText
* Drawing Double Outline using OutlineText
* Drawing Text Glow using OutlineText
* Fake 3D Text
* Real 3D Text (Orthogonal)
* Rotated Italic Text
* Diffused Shadow and Sample Code
* PngOutlineText Class
* MeasureString and GdiMeasureString
* OpenGL Demo
* Codeplex
* References
* Changelog of Source Code
* History

![samplescreenshot.jpg](/images/samplescreenshot.jpg)

## Introduction

I am an avid fan of animes (Japanese animations). As I do not understand the Japanese language, the animes which I watched have English subtitles. These fan-subbed animes have the most beautiful fonts and text. Below is a screenshot of the "Sora no Manimani" which is an anime about an Astronomy club in high school.

![animescreenshot.jpg](/images/animescreenshot.jpg)

I was fascinated by the outline text. I searched on the web for an outline text library which allows me to do outline text. Sadly I found none. Those that I found, are too difficult for me to retrofit to my general purpose and I do not fully understand their sparsely commented codes. I decided to roll up my sleeves to write my own outline text library. In my previous article, [How to Use a Font Without Installing it](http://www.codeproject.com/KB/GDI/NoInstalledFonts.aspx), a reader, knoami, commented and requested about using C# to do the same thing. Now this time taking C# users into account, every C++ code example in this article is accompanied by a C# code example. Without further ado, let us begin now!



## Initializing and Uninitializing GDI+

Before we use GDI+ in C++ application, we need to initialize it. Below is an example on how to initialize GDI+ in the constructor and uninitialize GDI+ in the destructor.

```Cpp
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

For .NET Windows Form application, GDI+ initialization and uninitialization is handled automatically for the developers.

## Drawing Single Outline Text with Generic GDI+

![singleoutline1.png](/images/singleoutline1.png)

For this tutorial, I mostly stick to the Arial font, because Arial font comes with every Windows Operating Systems, so the sample code will work out of the box for you. To draw outline text, we have to add the string to the `GraphicsPath` object, using its `AddString` method, so that we can have its path to draw its outline. We must draw the text&#39;s outline first. To do that, we use `Graphics` class&#39;s `DrawPath` method. Lastly, we draw the text body with `Graphics` class&#39;s `FillPath` method. Simple, right?

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial");
    StringFormat strformat;
    wchar_t pszbuf[] = L"Text Designer";

    GraphicsPath path;
    path.AddString(pszbuf, wcslen(pszbuf), &fontFamily, 
	FontStyleRegular, 48, Gdiplus::Point(10,10), &strformat );
    Pen pen(Color(234,137,6), 6);
    graphics.DrawPath(&pen, &path);
    SolidBrush brush(Color(128,0,255));
    graphics.FillPath(&brush, &path);
}
```

This is the equivalent C# code, using GDI+&#39;s `System::Drawing` classes to draw outline text. You will notice that it is quite similar to the C++ code above.

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    SolidBrush brushWhite = new SolidBrush(Color.White);
    e.Graphics.FillRectangle(brushWhite, 0, 0, 
	this.ClientSize.Width, this.ClientSize.Height);

    FontFamily fontFamily = new FontFamily("Arial");
    StringFormat strformat = new StringFormat();
    string szbuf = "Text Designer";

    GraphicsPath path = new GraphicsPath();
    path.AddString(szbuf, fontFamily, 
	(int)FontStyle.Regular, 48.0f, new Point(10, 10), strformat);
    Pen pen = new Pen(Color.FromArgb(234, 137, 6), 6);
    e.Graphics.DrawPath(pen, path);
    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 255));
    e.Graphics.FillPath(brush, path);
	
    brushWhite.Dispose();
    fontFamily.Dispose();
    path.Dispose();
    pen.Dispose();
    brush.Dispose();
    e.Graphics.Dispose();
}
```

![singleoutline1problematic.png](/images/singleoutline1problematic.png)

We have a problem with the above text, specifically "A"; there is a sharp pointer at the top of "A". This problem exists if there are sharp edges or corners in the font glyphs and the outline is quite thick or thicker than the text body. The sharp pointer above came from the outline of the inner triangle of "A". Below is the C++ code&nbsp;followed by the C# code to reproduce the problem.

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial");
    StringFormat strformat;
    wchar_t pszbuf[] = L"ABC";

    GraphicsPath path;
    path.AddString(pszbuf, wcslen(pszbuf), &fontFamily, 
	FontStyleRegular, 48, Gdiplus::Point(10,10), &strformat );
    Pen pen(Color(234,137,6), 6);
    graphics.DrawPath(&pen, &path);
    SolidBrush brush(Color(128,0,255));
    graphics.FillPath(&brush, &path);
}
```

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    SolidBrush brushWhite = new SolidBrush(Color.White);
    e.Graphics.FillRectangle(brushWhite, 0, 0, 
	this.ClientSize.Width, this.ClientSize.Height);

    FontFamily fontFamily = new FontFamily("Arial");
    StringFormat strformat = new StringFormat();
    string szbuf = "ABC";

    GraphicsPath path = new GraphicsPath();
    path.AddString(szbuf, fontFamily, 
	(int)FontStyle.Regular, 48.0f, new Point(10, 10), strformat);
    Pen pen = new Pen(Color.FromArgb(234, 137, 6), 6);
    e.Graphics.DrawPath(pen, path);
    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 255));
    e.Graphics.FillPath(brush, path);
	
    brushWhite.Dispose();
    fontFamily.Dispose();
    path.Dispose();
    pen.Dispose();
    brush.Dispose();
    e.Graphics.Dispose();
}
```

Fortunately, I have a workaround for this problem. We can set the `LineJoin` property of GDI+ pen to `LineJoinRound` to avoid sharp edges and corners. The downside is every edge will be rounded, instead of crisp sharp as the font. Below is the C++ code to call the `SetLineJoin` method.

![singleoutline1solved.png](/images/singleoutline1solved.png)

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial");
    StringFormat strformat;
    wchar_t pszbuf[] = L"ABC";

    GraphicsPath path;
    path.AddString(pszbuf, wcslen(pszbuf), &fontFamily, 
	FontStyleRegular, 48, Gdiplus::Point(10,10), &strformat );
    Pen pen(Color(234,137,6), 6);
    pen.SetLineJoin(LineJoinRound);
    graphics.DrawPath(&pen, &path);
    SolidBrush brush(Color(128,0,255));
    graphics.FillPath(&brush, &path);
}
```

This is the equivalent C# code to solve the problem by setting the `LineJoin `property to `LineJoin.Round`.

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    SolidBrush brushWhite = new SolidBrush(Color.White);
    e.Graphics.FillRectangle(brushWhite, 0, 0, 
	this.ClientSize.Width, this.ClientSize.Height);

    FontFamily fontFamily = new FontFamily("Arial");
    StringFormat strformat = new StringFormat();
    string szbuf = "ABC";

    GraphicsPath path = new GraphicsPath();
    path.AddString(szbuf, fontFamily, 
	(int)FontStyle.Regular, 48.0f, new Point(10, 10), strformat);
    Pen pen = new Pen(Color.FromArgb(234, 137, 6), 6);
    pen.LineJoin = LineJoin.Round;
    e.Graphics.DrawPath(pen, path);
    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 255));
    e.Graphics.FillPath(brush, path);

    brushWhite.Dispose();
    fontFamily.Dispose();
    path.Dispose();
    pen.Dispose();
    brush.Dispose();
    e.Graphics.Dispose();
}
```

## Drawing Single Outline Text with Gradient Color

![GenericCppGradient.png](/images/GenericCppGradient.png)

We can select a gradient or texture brush, instead of a solid brush, for the text color. Below is a C++ example that shows how to do it.

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial");
    StringFormat strformat;
    wchar_t pszbuf[] = L"Text Designer";

    GraphicsPath path;
    path.AddString(pszbuf, wcslen(pszbuf), &fontFamily, 
    FontStyleBold, 48, Gdiplus::Point(10,10), &strformat );
    Pen pen(Color(0,0,160), 5);
    pen.SetLineJoin(LineJoinRound);
    graphics.DrawPath(&pen, &path);
    LinearGradientBrush brush(Gdiplus::Rect(10, 10, 30, 60), 
        Color(132,200,251), Color(0,0,160), LinearGradientModeVertical);
    graphics.FillPath(&brush, &path);
}
```

Below is C# example that shows how to select a gradient brush.

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    SolidBrush brushWhite = new SolidBrush(Color.White);
    e.Graphics.FillRectangle(brushWhite, 0, 0,
    this.ClientSize.Width, this.ClientSize.Height);

    FontFamily fontFamily = new FontFamily("Arial");
    StringFormat strformat = new StringFormat();
    string szbuf = "Text Designer";

    GraphicsPath path = new GraphicsPath();
    path.AddString(szbuf, fontFamily,
        (int)FontStyle.Bold, 48.0f, new Point(10, 10), strformat);
    Pen pen = new Pen(Color.FromArgb( 0, 0, 160), 5);
    pen.LineJoin = LineJoin.Round;
    e.Graphics.DrawPath(pen, path);
    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(10,10,30,70), 
        Color.FromArgb(132,200,251), 
        Color.FromArgb(0,0,160), LinearGradientMode.Vertical);
    e.Graphics.FillPath(brush, path);

    brushWhite.Dispose();
    fontFamily.Dispose();
    path.Dispose();
    pen.Dispose();
    brush.Dispose();
    e.Graphics.Dispose();
}
```



## Drawing Double Outline Text with Generic GDI+

![doubleoutline1.png](/images/doubleoutline1.png)

To achieve double outline text, you have to render the outer outline first, then the inner outline, using `DrawPath`, followed by the `FillPath` call to draw the text body. Below is the C++ code to achieve that:

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial");
    StringFormat strformat;
    wchar_t pszbuf[] = L"Text Designer";

    GraphicsPath path;
    path.AddString(pszbuf, wcslen(pszbuf), 
	&fontFamily, FontStyleRegular, 48, Gdiplus::Point(10,10), &strformat );
	
    Pen penOut(Color(32, 117, 81), 12);
    penOut.SetLineJoin(LineJoinRound);
    graphics.DrawPath(&penOut, &path);

    Pen pen(Color(234,137,6), 6);
    pen.SetLineJoin(LineJoinRound);
    graphics.DrawPath(&pen, &path);
    SolidBrush brush(Color(128,0,255));
    graphics.FillPath(&brush, &path);
}
```

This is the equivalent C# code to draw double outline text:

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    SolidBrush brushWhite = new SolidBrush(Color.White);
    e.Graphics.FillRectangle(brushWhite, 0, 0, 
	this.ClientSize.Width, this.ClientSize.Height);

    FontFamily fontFamily = new FontFamily("Arial");
    StringFormat strformat = new StringFormat();
    string szbuf = "Text Designer";

    GraphicsPath path = new GraphicsPath();
    path.AddString(szbuf, fontFamily, 
	(int)FontStyle.Regular, 48.0f, new Point(10, 10), strformat);
    
    Pen penOut = new Pen(Color.FromArgb(32, 117, 81), 12);
    penOut.LineJoin = LineJoin.Round;
    e.Graphics.DrawPath(penOut, path);
    
    Pen pen = new Pen(Color.FromArgb(234, 137, 6), 6);
    pen.LineJoin = LineJoin.Round;
    e.Graphics.DrawPath(pen, path);
    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 255));
    e.Graphics.FillPath(brush, path);
	
    brushWhite.Dispose();
    fontFamily.Dispose();
    path.Dispose();
    penOut.Dispose();
    pen.Dispose();
    brush.Dispose();
    e.Graphics.Dispose();
}
```

## Drawing Text Glow with Generic GDI+

![textglow1.png](/images/textglow1.png)

To draw text glow, you have to start with a thin pen with low alpha values between 24 to 64 and draw the outline, repeatedly draw the outline with a thicker pen. Then finally draw the text body with `FillPath` method.

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial");
    StringFormat strformat;
    wchar_t pszbuf[] = L"Text Designer";

    GraphicsPath path;
    path.AddString(pszbuf, wcslen(pszbuf), &fontFamily, 
	FontStyleRegular, 48, Gdiplus::Point(10,10), &strformat );
	
    for(int i=1; i<8; ++i)
    {
        Pen pen(Color(32, 0, 128, 192), i);
        pen.SetLineJoin(LineJoinRound);
        graphics.DrawPath(&pen, &path);
    }
	
    SolidBrush brush(Color(255,255,255));
    graphics.FillPath(&brush, &path);
}
```

This is the equivalent C# code to draw text glow:

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    SolidBrush brushWhite = new SolidBrush(Color.White);
    e.Graphics.FillRectangle(brushWhite, 0, 0, 
	this.ClientSize.Width, this.ClientSize.Height);

    FontFamily fontFamily = new FontFamily("Arial");
    StringFormat strformat = new StringFormat();
    string szbuf = "Text Designer";

    GraphicsPath path = new GraphicsPath();
    path.AddString(szbuf, fontFamily, 
	(int)FontStyle.Regular, 48.0f, new Point(10, 10), strformat);
    
    for(int i=1; i<8; ++i)
    {
        Pen pen = new Pen(Color.FromArgb(32, 0, 128, 192), i);
        pen.LineJoin = LineJoin.Round;
        e.Graphics.DrawPath(pen, path);
        pen.Dispose();
    }

    SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
    e.Graphics.FillPath(brush, path);
	
    brushWhite.Dispose();
    fontFamily.Dispose();
    path.Dispose();
    brush.Dispose();
    e.Graphics.Dispose();
}
```

## Postscript OpenType Fonts

Before you rush to make your own outline library, I have to tell you one pitfall of GDI+. GDI+ cannot handle Postscript OpenType fonts; GDI+ can only handle TrueType fonts. I have searched for a solution and found Sjaak Priester&#39;s [Make GDI+ Less Finicky About Fonts](http://www.codeguru.com/cpp/g-m/gdi/gdi/article.php/c10621). His approach is to parse the font file for its glyphs and draw its outline. Sadly, I cannot use his code as his library is using the restrictive GNU license as I want to make my code free for all to use. __Note__: This is the reason why Winform developers use the `[TextRenderer](http://msdn.microsoft.com/en-us/library/system.windows.forms.textrenderer.aspx)` class, to display the text, not GDI+ classes. I racked my brains for a solution. Since GDI (not GDI+) can display Postscript OpenType fonts and GDI supports path extraction through [BeginPath](http://msdn.microsoft.com/en-us/library/dd183363%28VS.85%29.aspx)/[EndPath](http://msdn.microsoft.com/en-us/library/dd162599%28VS.85%29.aspx)/[GetPath](http://msdn.microsoft.com/en-us/library/dd144908%28VS.85%29.aspx), I decided to use just that to get my path into GDI+. Below is the comparison of the GDI+ path and GDI path. __Note__: Both are rendered by GDI+, it is just that their path extraction is different; one is using GDI+ while the other is using GDI to get the text path.

![gdiandgdiplus.png](/images/gdiandgdiplus.png)

The top one is using GDI+ path and the bottom one is using GDI path. Looks like GDI path text is bigger and a bit inaccurate (not obvious here because it depends on the font). (__Note:__ I realised that if you use `Graphics::DrawString `to draw the text, they are roughly the same size as the GDI path text; it is the GDI+ path text which is smaller!) However, GDI paths can do rotated italic text trick, like below, which GDI+ cannot do because GDI `GraphicsPath`s `AddString `takes in a `FontFamily `object, not a `Font `object. My `OutlineText `class provides the `GdiDrawString` method if you have the need to use PostScript OpenType fonts. The effect below is a Franklin Gothic Demi font, size 36, Italic text rotated 10 degrees anti-clockwise.

![rotatedtext2.png](/images/rotatedtext2.png)

## Drawing Outline Text using DirectWrite

As you all know, [Direct2D](http://msdn.microsoft.com/en-us/library/dd370990%28VS.85%29.aspx) and [DirectWrite](http://msdn.microsoft.com/en-us/library/dd368038%28VS.85%29.aspx) are the next graphics and text APIs for Vista and Windows 7. I have emailed Tom Mulcahy (Microsoft&#39;s developer of Direct2D for Windows 7). Below is Tom Mulcahy&#39;s email reply to me.

<blockquote>
_(Courtesy of Tom Mulcahy) The way to do this is to get the text contents as an `ID2D1GeometrySink` (See `IDwriteFontFace::GetGlyphRunOutline`). You can then call `ID2D1RenderTarget::DrawGeometry` to draw the outline of the text (specifying any color and width you want). Next call `ID2D1RenderTarget::FillGeometry` to fill the text (again you can specify any color you want)._
</blockquote>

__Note:__ Text Designer Outline Text Library which is mentioned in the latter part of the article, will be updated with `DirectWrite `when Windows 7 is out.

## What about Drawing Shadows?

To tell you the truth, text shadow is drawn using the single outline text code. There is one problem: shadow is translucent. If we use the first code example to render shadows, it will turn out to be like the image below. It is because some area of the font body and font outline overlaps, so they are rendered twice, therefore it is darker.

![shadowcombined1.png](/images/shadowcombined1.png)

My solution is to render the shadow text body and shadow text outline separately like below and combine them, with the pixels with shadow text body taking precedence; Only where the shadow text body is not rendered, the shadow text outline is rendered. Shadow rendering is more involved and complicated, the 0.1.0 version of _OutlineText.cpp_ without shadow implementation is only 3KB in file size and has 164 lines of code while the 0.2.0 version of _OutlineText.cpp_ with shadow implementation is 23KB in file size and has 865 lines of code! Therefore, I will not show its code here, you can download and read the&nbsp;source code if you are interested.

![shadowtextbody1.png](/images/shadowtextbody1.png)

![shadowtextoutline1.png](/images/shadowtextoutline1.png)

![shadowproper.png](/images/shadowproper.png)

## Drawing Single Outline using OutlineText

![singleoutline.png](/images/singleoutline.png)

This is the C++ code to use the `OutlineText` class to display the single outline text, using the `TextOutline `and `DrawString `methods:

```Cpp
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

    OutlineText text;
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

This is the equivalent C# code to use the `OutlineText` class to display the single outline text, using the `TextOutline `and `DrawString `methods:

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
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



## Drawing Single Outline Text with Gradient Color using OutlineText

![CppGradient.png](/images/CppGradient.png)

We can select a gradient or texture brush, instead of a solid brush, for the text color. We can use the `MeasureString` method to calculate the width and height of the text will take and this returned width and height will be the width and height of the gradient brush. However, we must call `TextOutline` method again to set the brush. The reason `TextOutline` needs to be called twice is because `MeasureString` needs the `TextOutline` information before the string can be measured correctly. Do not worry: `TextOutline` is pretty lightweight, it just sets some information. Below is a C++ example that shows how to do it.

```Cpp
#include <Gdiplus.h>

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    using namespace Gdiplus;
    using namespace TextDesigner;
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    FontFamily fontFamily(L"Arial Black");
    StringFormat strformat;
    wchar_t pszbuf[] = L"Text Designer";

    OutlineText text;

    text.EnableShadow(true);
    CRect rect;
    GetClientRect(&rect);
    text.SetShadowBkgd(Color(255,255,0),rect.Width(),rect.Height());
    text.Shadow(Color(128,0,0,0), 4, Point(4,8));
    text.TextOutline(Color(0,0,0), Color(0,0,160),5);
    text.MeasureString(
        &graphics,
        &fontFamily,
        FontStyleItalic,
        48,
        pszbuf,
        Gdiplus::Point(10,10),
        &strformat,
        &fDestWidth,
        &fDestHeight);

    float fDestWidth = 0.0f;
    float fDestHeight = 0.0f;
    LinearGradientBrush brush(Gdiplus::Rect(10, 10, fDestWidth, fDestHeight), 
        Color(132,200,251), Color(0,0,160), LinearGradientModeVertical);
    text.TextOutline(&brush, Color(0,0,160),5);
    text.DrawString(&graphics,&fontFamily,FontStyleItalic, 
        48, pszbuf, Gdiplus::Point(10,10), &strformat);
}
```

Below is a C# example that shows how to select a gradient brush with `OutlineText`.

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    OutlineText outlineText = new OutlineText();
    outlineText.TextOutline(
        Color.FromArgb(255, 128, 192),
        Color.FromArgb(255, 0, 0, 160),
        4);

    outlineText.EnableShadow(true);
    //Rem to SetNullShadow() to release memory if a previous shadow has been set.
    outlineText.SetNullShadow();
    outlineText.Shadow(Color.FromArgb(128, 0, 0, 0), 4, new Point(4, 8));

    Color m_clrBkgd = Color.FromArgb(255, 255, 255);
    outlineText.SetShadowBkgd(m_clrBkgd, this.ClientSize.Width, this.ClientSize.Height);
    FontFamily fontFamily = new FontFamily("Arial Black");

    StringFormat strFormat = new StringFormat();

    float fDestWidth = 0.0f;
    float fDestHeight = 0.0f;

    outlineText.MeasureString(
        e.Graphics,
        fontFamily,
        FontStyle.Italic,
        48,
        "Text Designer",
        new Point(10, 10),
        strFormat,
        ref fDestWidth,
        ref fDestHeight);

    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(10, 10, 
				(int)fDestWidth, (int)fDestHeight), 
        Color.FromArgb(132,200,251), Color.FromArgb(0,0,160), 
		System.Drawing.Drawing2D.LinearGradientMode.Vertical);

    outlineText.TextOutline(
        brush,
        Color.FromArgb(255, 0, 0, 160),
        4);

    outlineText.DrawString(e.Graphics, fontFamily,
        FontStyle.Italic, 48, "Text Designer",
        new Point(10, 10), strFormat);

    e.Graphics.Dispose();
}
```

These are&nbsp;the settings in `TestOutlineText `application to display the above. I list out the settings here because sometimes even I was a bit lost on how to use `TestOutlineText `to display certain outline text effects. By listing the settings here, I hope the readers will get familiar with this application so that they can try out the outline effects they want. Please note if you enable shadow, the scrolling and resizing of the `TestOutlineText `application will be jerky because shadow rendering is computation intensive operation. I have written a `PngOutlineText` class to work around this problem, which I talk about it later towards the end of the article.

![singleoutlinesettings.png](/images/singleoutlinesettings.png)

## Drawing Double Outline using OutlineText

![doubleoutline.png](/images/doubleoutline.png)

To achieve double outline text, you have to specify the outer outline and the inner outline. This is the C++ code to display the double outline text, using the `TextDblOutline `and `DrawString `methods:

```Cpp
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

    OutlineText text;
    text.TextDblOutline(Color(255,255,255),Color(0,128,128),Color(0,255,0),4,4);
    text.EnableShadow(true);
    CRect rect;
    GetClientRect(&rect);
    text.SetShadowBkgd(Color(255,128,192),rect.Width(),rect.Height());
    text.Shadow(Color(128,0,0,0), 4, Point(4,8));
    text.DrawString(&graphics,&fontFamily,FontStyleRegular, 
        48, pszbuf, Gdiplus::Point(10,10), &strformat);
}
```

This is the C# code to display the double outline text, using the `TextDblOutline `and `DrawString `methods:

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    FontFamily fontFamily = new FontFamily("Arial Black");
    StringFormat strformat = new StringFormat();
    string szbuf = "Text Designer";

    OutlineText text = new OutlineText();
    text.TextDblOutline(Color.FromArgb(255, 255, 255),
        Color.FromArgb(0, 128, 128), Color.FromArgb(0, 255, 0), 4, 4);
    text.EnableShadow(true);
    text.SetShadowBkgd(Color.FromArgb(255, 128, 192), this.Size.Width, this.Size.Height);
    text.Shadow(Color.FromArgb(128, 0, 0, 0), 4, new Point(4, 8));
    text.DrawString(e.Graphics, fontFamily,
        FontStyle.Bold, 48, szbuf, new Point(10, 10), strformat);

    fontFamily.Dispose();
    e.Graphics.Dispose();
}
```

These are the settings to display the double outline text:

![doubleoutlinesettings.png](/images/doubleoutlinesettings.png)

## Drawing Text Glow using OutlineText

![textglow.png](/images/textglow.png)

This is the C++ code to display the text glow using the `TextGlow `and `DrawString `methods. Text glow is usually not displayed with a shadow because shadow interferes with the glow effect, so I disabled the shadow and did not set any shadow settings.

```Cpp
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

    OutlineText text;
    text.TextGlow(Color(191,255,255),Color(24,0,128,128),14);
    text.EnableShadow(false);
    text.DrawString(&graphics,&fontFamily,FontStyleRegular, 
        48, pszbuf, Gdiplus::Point(10,10), &strformat);
}
```

This is the similar C# code to display the text glow using the `TextGlow `and `DrawString `methods:

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    FontFamily fontFamily = new FontFamily("Arial Black");
    StringFormat strformat = new StringFormat();
    string szbuf = "Text Designer";

    OutlineText text = new OutlineText();
    text.TextGlow(Color.FromArgb(191, 255, 255), Color.FromArgb(24, 0, 128, 128), 14);
    text.EnableShadow(false);
    text.DrawString(e.Graphics, fontFamily, FontStyle.Bold,
        48, szbuf, new Point(10, 10), strformat);

    fontFamily.Dispose();
    e.Graphics.Dispose();
}
```

These are the settings to display the text glow:

![textglowsettings.png](/images/textglowsettings.png)

This is text glow with shadow if you are curious:

![textglowwithshadow.png](/images/textglowwithshadow.png)

## Fake 3D Text

You can achieve simulated 3D text by using a bigger and opaque shadow which has the same colour as the outline colour. Of course, if you look closely enough, you know it is not looking like 3D at all.

![fake3dtextsettings.png](/images/fake3dtextsettings.png)

<a name="real3dtext"></a>

## Real 3D Text (Orthogonal)

![Real3DText.PNG](/images/Real3DText.PNG)

It&#39;s easy to do real 3D text with `PngOutlineText` class. The extruded part is achieved by rendering the same colored text repeatedly and diagonally. By rendering diagonally, I mean render the text by offsetting the starting draw point by 1 pixel in x and y direction. Finally, we will render the real text at its original point. The sample code below achieves this by using the same `PngOutlineText` object. It sets new `TextOutline` parameters for the final text in `DrawActualText`. You will notice that in `DrawDiagonal` and `DrawActualText` methods, I blit the PNG image in `graphics` object which is created out of a ARGB `Bitmap` object, so that the resultant 3D text will be &#39;saved&#39; in the ARGB `Bitmap` object. Then in the `OnPaint` method, I just blit that ARGB `Bitmap` object without using `PngOutlineText` anymore. To draw outline text, `PngOutlineText` is the way to go; `OutlineText` is just too slow as it has to recalculate and redraw the text each time the client area is invalidated for repainting. By the way, the sample code below is modified from sample code which is pasted from the clipboard, which in turn is copied into the clipboard from the WYSIWYG "Copy C++ Code" button. Talk about eating your own dog food!

```Cpp
#include "../TextDesigner/PngOutlineText.h"

Gdiplus::Bitmap m_bitmap(420,100,PixelFormat32bppARGB);

BOOL CScratchPadDlg::OnInitDialog()
{
    CDialog::OnInitDialog();

    SetIcon(m_hIcon, TRUE);			// Set big icon
    SetIcon(m_hIcon, FALSE);		// Set small icon

    using namespace Gdiplus;
    Graphics graphics(&m_bitmap);
    PngOutlineText pngOutlineText;
    DrawDiagonal(graphics, pngOutlineText, 6);
    DrawActualText(graphics, pngOutlineText);

    return TRUE;
}

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    using namespace Gdiplus;
    CPaintDC dc(this);
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    // Fill background with white colour.
    CRect rect;
    GetClientRect(&rect);
    SolidBrush brushWhite(Color(255,255,255));
    graphics.FillRectangle(&brushWhite, 0, 0, rect.Width(), rect.Height());

    graphics.DrawImage(&m_bitmap, 10, 10, m_bitmap.GetWidth(), m_bitmap.GetHeight());
}

void CScratchPadDlg::DrawDiagonal(Gdiplus::Graphics& graphics, 
	PngOutlineText& pngOutlineText, int nDiagonal)
{
    using namespace Gdiplus;
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    pngOutlineText.TextOutline(
        Color(0,0,0), 
        Color(255,0,0,0), 
        4);

    pngOutlineText.EnableShadow(false);
    FontFamily fontFamily(L"Arial Black");

    StringFormat strFormat;

    Bitmap* pPngImage = new Gdiplus::Bitmap(m_bitmap.GetWidth(),
			m_bitmap.GetHeight(),PixelFormat32bppARGB);
    pngOutlineText.SetPngImage(pPngImage);
    pngOutlineText.DrawString(&graphics,&fontFamily, 
        FontStyleRegular, 48, L"Text Designer", 
        Gdiplus::Point(10,10), &strFormat);

    for(int i=0; i<nDiagonal; ++i)
        graphics.DrawImage(pPngImage, i, i, pPngImage->GetWidth(), 
					pPngImage->GetHeight());

    if(pPngImage)
        delete pPngImage;

    pPngImage = NULL;
}

void CScratchPadDlg::DrawActualText(Gdiplus::Graphics& graphics, 
				PngOutlineText& pngOutlineText)
{
    using namespace Gdiplus;
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    pngOutlineText.TextOutline(
        Color(178,0,255), 
        Color(255,0,0,0), 
        4);

    pngOutlineText.EnableShadow(false);
    FontFamily fontFamily(L"Arial Black");

    StringFormat strFormat;

    Bitmap* pPngImage = new Gdiplus::Bitmap(m_bitmap.GetWidth(),
			m_bitmap.GetHeight(),PixelFormat32bppARGB);
    pngOutlineText.SetPngImage(pPngImage);
    pngOutlineText.DrawString(&graphics,&fontFamily, 
        FontStyleRegular, 48, L"Text Designer", 
        Gdiplus::Point(10,10), &strFormat);

    graphics.DrawImage(pPngImage, 0, 0, pPngImage->GetWidth(), pPngImage->GetHeight());
	
    if(pPngImage)
        delete pPngImage;

    pPngImage = NULL;
}
```

For the above C++ code sample, I did not supply a C# equivalent code sample, because it was impossible to do blitting, using the previous version of .NET API because the .NET API makes a native copy of every managed object passed in. For example, if you supply a `Graphics` object with an internal `Bitmap` object, the `PngOutlineText` does not use that `Graphics` object to draw, instead it uses a native copy of that `Graphics` object to draw. As a result, the internal `Bitmap` will not get rendered. Since then, I have added `GetCopyOfInternalPng` method to `PngOutlineText` class to enable to do blitting with a copy of rendered PNG, using GDI+.

I have also added a method, called `Extrude` to do 3D text easily but this method is still not as fast as the previous "PNG blitting multiple times" method because the `ExtrudeStrategy `class is generic and it does not know whether `OutlineText` or `PngOutlineText` class is using it, so it cannot do any optimizations. __Note__: To `Extrude`, you have to `EnableShadow` because `Extrude` is treated like a type of shadow. Please note to achieve the 3D text effect, the shadow color has to be fully opaque (meaning 255) and the 3D extrude effect looks best when the absolute values of x and y offset are equal (For example, x=4,y=4 or x-4, y=4). Below is the C++ and C# sample code from the WYSIWYG clipboard code copy feature.

![Extrude.PNG](/images/Extrude.PNG)

```Cpp
#include "TextDesigner/OutlineText.h"

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();

    using namespace Gdiplus;
    using namespace TextDesign;
    CPaintDC dc(this);
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);
    OutlineText m_OutlineText;
    m_OutlineText.TextOutline(
        Color(255,128,192), 
        Color(255,128,0,0), 
        4);

    m_OutlineText.EnableShadow(true);
    //Rem to SetNullShadow() to release memory if a previous shadow has been set.
    m_OutlineText.SetNullShadow();
    m_OutlineText.Extrude(
        Gdiplus::Color(255,128,0,0), 
        4, 
        Gdiplus::Point(8,8));

    CRect rect;
    this->GetClientRect(&rect);
    Color m_clrBkgd(255, 255, 255);
    m_OutlineText.SetShadowBkgd(m_clrBkgd,rect.Width(),rect.Height());
    FontFamily fontFamily(L"Arial Black");

    StringFormat strFormat;
    m_OutlineText.DrawString(&graphics,&fontFamily, 
        FontStyleRegular, 48, L"Text Designer", 
        Gdiplus::Point(10, 10), &strFormat);
}
```

Here is the equivalent C# code to call `Extrude` achieve 3D extruded text.

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    OutlineText outlineText = new OutlineText();
    outlineText.TextOutline(
        Color.FromArgb(255, 128, 192),
        Color.FromArgb(255, 128, 0, 0),
        4);

    outlineText.EnableShadow(true);
    //Rem to SetNullShadow() to release memory if a previous shadow has been set.
    outlineText.SetNullShadow();
    outlineText.Extrude(
        Color.FromArgb(255, 128, 0, 0),
        4,
        new Point(8, 8));

    Color m_clrBkgd = Color.FromArgb(255, 255, 255);
    outlineText.SetShadowBkgd(m_clrBkgd, this.ClientSize.Width, this.ClientSize.Height);
    FontFamily fontFamily = new FontFamily("Arial Black");

    StringFormat strFormat = new StringFormat();
    outlineText.DrawString(e.Graphics, fontFamily,
        FontStyle.Regular, 48, "Text Designer",
        new Point(10, 10), strFormat);

    e.Graphics.Dispose();
}
```

Here is the settings to achieve 3D extruded text. You must enable the `Extrude Text` checkbox: (See the red rectangle!)

![ExtrudeSettings.PNG](/images/ExtrudeSettings.PNG)

## Rotated Italic Text

![rotatedtext.png](/images/rotatedtext.png)

We have to use `GdiDrawString` method to display the rotated italic text because `GdiDrawString `takes in a `LOGFONT` structure which allows us to specify rotational angle through the `lfEscapement` and `lfOrientation`. While `DrawString` method uses `AddString` method, of `GraphicsPath`, which takes in a font family object instead of a font object, we do not have the ability to specify the rotational angle.

```Cpp
#include "TextDesigner/OutlineText.h"

void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();
    CPaintDC dc(this);
    using namespace Gdiplus;
    
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    wchar_t pszbuf[] = L"Text Designer";

    LOGFONTW logfont;
    memset(&logfont, 0, sizeof(logfont));
    wcscpy_s(logfont.lfFaceName, L"Arial Black");
    logfont.lfHeight = -MulDiv(48, dc.GetDeviceCaps(LOGPIXELSY), 72);
    logfont.lfEscapement = 100;
    logfont.lfOrientation = 100;
    logfont.lfItalic = 1;

    OutlineText text;
    text.TextOutline(Color(64,193,255),Color(0,0,0),8);
    text.EnableShadow(false);
    CRect rect;
    GetClientRect(&rect);
    text.EnableShadow(true);
    text.SetShadowBkgd(Color(255,255,255),rect.Width(),rect.Height());
    text.Shadow(Color(128,0,0,0), 8, Point(4,4));
    text.GdiDrawString(&graphics, &logfont, pszbuf, Gdiplus::Point(10,100));
}
```

The C# code to display the rotated text is removed, as the new C# library has not implemented this yet as this will involve the pinvoke data types which I am afraid may have portablilty issues.

These are the settings to display the rotated text:

![rotatedtextsettings.png](/images/rotatedtextsettings.png)

## Diffused Shadow and Sample Code

I have added diffused shadow to the outline text library. Click the checkbox as indicated by the green rectangle to enable diffuse shadow. __Note:__ You have to tweak the shadow alpha values(ranged from 12 to 32) and shadow thickness (ranged from 8 to 12) to achieve the diffused shadow effect. Diffused shadow is implemented using the text glow effect, so the shadow thickness indicates how many times the shadow color will be rendered. So as a rule of thumb, the higher the shadow thickness, the lower shadow alpha value. I have also implemented WYSIWYG sample code generation. Click the "Copy C++ Code" and "Copy C# Code" buttons, indicated by the red rectangle, to copy the code to clipboard and paste it to your code editor! You may still have to edit the code in your editor to make it suit your requirement, for example, changing a local object to member object of your class. In the event of the sample code crashes, try changing the bitmap sizes to be the same and also please report this crash to me and the steps to reproduce it. __Note:__ The crash, if there is any, is due to the sample code being wrong, not because something is wrong with the Text Designer Outline Library.

![DiffusedShadowSetting2.JPG](/images/DiffusedShadowSetting2.JPG)

## PngOutlineText Class

I have written a `PngOutlineText` class which renders the text and shadow to a `Bitmap` object with an alpha channel (`PixelFormat32bppARGB` format), so that you need not re-generate the text whenever you need to render the text again because outline text generation typically takes a long time, especially for text with shadow. Using `PngOutlineText`, you must call the `SetPngImage` method to set the `PixelFormat32bppARGB` format image for the `PngOutlineText` to render to. After the first `DrawString` or `GdiDrawString`, you need just to blit this image to your graphics object, instead of generating the same outline text through `DrawString` or `GdiDrawString` again. I create `PngOutlineText` class for use in video rendering which is typically about 30fps or 60fps. If you use a big image background in the&nbsp;`TestOutlineText `application, you will find resizing the application and scrolling the image is not smooth. If you check "Enable PNG Rendering" checkbox (See the highlighted red rectangle below), resizing and scrolling becomes smooth because the `TestOutlineText `application detects if the settings have not been modified, it will just blit the transparent text image instead. You can use the `SavePngImage` method to save the image to PNG image. If you open the image in any image editor, like [Paint.Net](www.getpaint.net/) or [Adobe Photoshop](http://www.adobe.com/products/photoshop/compare/), you will see the checkered boxes which is the transparent part of the PNG image.

![enablepngsettings3_small.jpg](/images/enablepngsettings3_small.jpg)

![pngineditor.png](/images/pngineditor.png)

Version 2 Preview 6 has added GDI path to C# library. Below is the C++ and C# code using GDI (instead of GDI+) to do the gradient text. The sample code is put here because the auto C# code generation for GDI is not correct.

![gdi_textdesigner.png](/images/gdi_textdesigner.png)

```Cpp
void CScratchPadDlg::OnPaint()
{
    //CDialog::OnPaint();

    using namespace Gdiplus;
    using namespace TextDesigner;
    CPaintDC dc(this);
    Graphics graphics(dc.GetSafeHdc());
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);
    OutlineText m_OutlineText;
    m_OutlineText.TextGradOutline(
        Color(255,128,64), 
        Color(255,64,0,64), 
        Color(255,255,128,255), 
        10);

    m_OutlineText.EnableShadow(true);
    //Rem to SetNullShadow() to release memory if a previous shadow has been set.
    m_OutlineText.SetNullShadow();
    m_OutlineText.Shadow(
        Gdiplus::Color(128,0,0,0), 8, 
        Gdiplus::Point(4,4));
    FontFamily fontFamily(L"Arial Black");

    StringFormat strFormat;
    float fStartX = 0.0f;
    float fStartY = 0.0f;
    float fDestWidth = 0.0f;
    float fDestHeight = 0.0f;
    m_OutlineText.MeasureString(
        &graphics,
        &fontFamily,
        FontStyleRegular,
        48,
        L"TEXT DESIGNER",
        Gdiplus::Point(10, 10),
        &strFormat,
        &fStartX,
        &fStartY,
        &fDestWidth,
        &fDestHeight);

    CRect rect;
    this->GetClientRect(&rect);
    Color m_clrBkgd(255, 255, 255);
    m_OutlineText.SetShadowBkgd(m_clrBkgd,fDestWidth,fDestHeight);
    FontFamily fontFamily(L"Arial Black");

    StringFormat strFormat;
    LinearGradientBrush gradientBrush(Gdiplus::Rect(fStartX, fStartY, fDestWidth-(fStartX-10), fDestHeight-(fStartY-10)),
        Color(255, 128, 64), Color(255, 0, 0), LinearGradientModeVertical );
    m_OutlineText.TextGradOutline(
        &gradientBrush, 
        Color(255,64,0,64), 
        Color(255,255,128,255), 
        10);
    m_OutlineText.DrawString(&graphics,&fontFamily, 
        FontStyleRegular, 48, L"TEXT DESIGNER", 
        Gdiplus::Point(10, 10), &strFormat);
}

```

Here follows the C# code.

```CSharp
private void OnPaint(object sender, PaintEventArgs e)
{
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    //Drawing the back ground color
    Color m_clrBkgd = Color.FromArgb(255, 255, 255);
    SolidBrush brushBkgnd = new SolidBrush(m_clrBkgd);
    e.Graphics.FillRectangle(brushBkgnd, 0, 0, this.ClientSize.Width, this.ClientSize.Width);

    PngOutlineText m_PngOutlineText = new PngOutlineText();
    m_PngOutlineText.SetPngImage(new Bitmap(ClientSize.Width, ClientSize.Height));
    m_PngOutlineText.TextGradOutline(
        Color.FromArgb(255, 128, 64),
        Color.FromArgb(255, 64, 0, 64),
        Color.FromArgb(255, 255, 128, 255),
        10);
    m_PngOutlineText.EnableReflection(false);

    m_PngOutlineText.EnableShadow(true);
    //Rem to SetNullShadow() to release memory if a previous shadow has been set.
    m_PngOutlineText.SetNullShadow();
    m_PngOutlineText.Shadow(
        Color.FromArgb(128, 0, 0, 0), 8,
        new Point(4, 4));
    LOGFONT m_LogFont = new LOGFONT();
    m_LogFont.lfFaceName = "Arial Black";
    m_LogFont.lfHeight = -48;

    m_LogFont.lfOrientation = 0;
    m_LogFont.lfEscapement = 0;
    m_LogFont.lfItalic = false;
    float fStartX = 0.0f;
    float fStartY = 0.0f;
    float fDestWidth = 0.0f;
    float fDestHeight = 0.0f;
    m_PngOutlineText.GdiMeasureString(
        e.Graphics,
        m_LogFont,
        "TEXT DESIGNER",
        new Point(10, 10),
        ref fStartX,
        ref fStartY,
        ref fDestWidth,
        ref fDestHeight);
    m_PngOutlineText.SetShadowBkgd(m_clrBkgd, (int)fDestWidth+10, (int)fDestHeight+10);
    LinearGradientBrush gradientBrush = new LinearGradientBrush(new RectangleF(fStartX, fStartY, fDestWidth - (fStartX - 10), fDestHeight - (fStartY - 10)),
            Color.FromArgb(255, 128, 64), Color.FromArgb(255, 0, 0), LinearGradientMode.Vertical);
    m_PngOutlineText.TextGradOutline(
        gradientBrush,
        Color.FromArgb(255, 64, 0, 64),
        Color.FromArgb(255, 255, 128, 255),
        10);

    m_PngOutlineText.GdiDrawString(
        e.Graphics,
        m_LogFont,
        "TEXT DESIGNER",
        new Point(10, 10));

    e.Graphics.DrawImage(m_PngOutlineText.GetPngImage(), new Point(0, 0));

    brushBkgnd.Dispose();
    e.Graphics.Dispose();
}

```

## MeasureString and GdiMeasureString

I have implemented `MeasureString` and `GdiMeasureString` method for `PngOutlineText`. Please do not use `Graphics::MeasureString`, as `Graphics::MeasureString` is for `Graphics::DrawString` method. After you use `MeasureString` and `GdiMeasureString` to get the minimum width and height required, you should add some space to width and height, like 5 pixels. `MeasureString` and `GdiMeasureString` parameters are similar to `DrawString` and `GdiDrawString`, respectively, except for the additional 2 parameters to get the width and height. This is how `MeasureString` family methods are used. First call `MeasureString` to get the width and height, then allocate a `PixelFormat32bppARGB` format `Bitmap` which is slightly larger. Make your shadow background the same size as this `PixelFormat32bppARGB` `Bitmap`. Any bitmap will do as a shadow background as this does not affect rendering for `PngOutlineText`, however for `OutlineText`, you need to crop out the part of background for the shadow background. After setting up the outline text attributes, `DrawString` or `GdiDrawString` at point (0,0) position, then `Graphics::DrawImage` the `PixelFormat32bppARGB` `Bitmap` at the position you want the text to appear.

The above method is fine for outline text without shadow or the shadow is at the bottom right, meaning positive x and y offset. If one or both of the offsets is negative, it would not work out so nicely. Imagine you draw the text at point (0,0), the shadow offset is at point (-4,-4), so part of the shadow may not be seen. So if the shadow offset is at point (-4,-4), you `DrawString` the text at point (4,4) and `Graphics::DrawImage` the final `PixelFormat32bppARGB` `Bitmap` at original position subtracted by point (4,4). So in this way, whether your shadow is offset to the top or bottom or left or right, the text would always appear at the same position. Below is the code to accomplish this. Please note that the code below does not appear in the sample code because I do not want to confuse the beginners on how to use the `PngOutlineText` class as `PngOutlineText` sample code is already the longest.

```Cpp
float fWidth=0.0f;
float fHeight=0.0f;
m_PngOutlineText.MeasureString(&graphics,&fontFamily,FontStyleBold, 
    72, m_szText, Gdiplus::Point(0,0), &strFormat,
    &fWidth, &fHeight);
	
m_pPngImage = new Bitmap(fWidth+5.0f, fHeight+5.0f, PixelFormat32bppARGB);

if(!m_pPngImage)
    return;

m_PngOutlineText.SetPngImage(m_pPngImage);
m_PngOutlineText.SetNullShadow();
m_PngOutlineText.SetShadowBkgd(
Gdiplus::Color(GetRValue(m_clrBkgd),GetGValue(m_clrBkgd),GetBValue(m_clrBkgd)),
    m_pPngImage->GetWidth(), m_pPngImage->GetHeight());

if(!m_bEnableShadow)
{
    m_PngOutlineText.DrawString(
        &graphics,&fontFamily,fontStyle,m_nFontSize,
	m_szText,Gdiplus::Point(0,0), &strFormat);
    graphics.DrawImage(m_pPngImage, (float)m_nTextPosX, (float)m_nTextPosY, 
        (float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());
}
else
{
    int nShadowOffsetX = 0;
    if(m_nShadowOffsetX<0)
        nShadowOffsetX = -m_nShadowOffsetX;
    int nShadowOffsetY = 0;
    if(m_nShadowOffsetY<0)
        nShadowOffsetY = -m_nShadowOffsetY;
    m_PngOutlineText.DrawString(&graphics,&fontFamily,
        fontStyle,m_nFontSize,m_szText,Gdiplus::Point
	(nShadowOffsetX,nShadowOffsetY), &strFormat);
    graphics.DrawImage(m_pPngImage, (float)
	(m_nTextPosX-nShadowOffsetX), (float)(m_nTextPosY-nShadowOffsetY), 
        (float)m_pPngImage->GetWidth(), (float)m_pPngImage->GetHeight());
}
```

Please note that the above method doesn&#39;t work for `GdiDrawString`&#39;s rotated italic text effect. For information on how to make rotated italic text effect work, search for `GdiMeasureStringRealHeight `method call, in _MyScrollView.cpp_ of `TestOutlineText `project, which is a quick hack to make it work. I do not put it here because it will complicate the code example.

## OpenGL Demo

I have made an OpenGL demo which shows how to use PNG images with alpha channel as textures. Please note that the text images are generated on the fly. I used to work in [muvee](www.muvee.com), a company, which specializes in making automatic video editing software, uses OpenGL to do their video special effects with the photos. I find their text captions, used in their automatic edited video, quite bland. I hope they will adopt my Text Designer Outline Text library in their next version of muvee software.

![openglscreenshot1.jpg](/images/openglscreenshot1.jpg)

![openglscreenshot2.jpg](/images/openglscreenshot2.jpg)

## References

* [Make GDI+ Less Finicky About Fonts: There&#39;s More in the World Than TrueType](http://www.codeguru.com/cpp/g-m/gdi/gdi/article.php/c10621) by Sjaak Priester
* [VC++ Example: GetPath, BeginPath, EndPath, True Type Font, Draw Outline of text](http://www.ucancode.net/Visual_C_Control/GetPath-BeginPath-EndPath-draw-ourline-text.htm)

## Changelog of Source Code


* Version 0.3.6 (15<sup>th</sup> minor release)
    * Version 2 preview. Version 2 only has 2 class, namely Canvas and MaskColor. Added WPF support for version 2.
	* Added 3 demos (Aquarion, Be Happy and Dirty) for C++ MFC, C# Winform and C# WPF
* Version 0.3.0 (9<sup>th</sup> minor release)
	* Added the ability to select brush, such as gradient brush or texture brush for the text body
	* Added CSharp library, `TextDesignerCSLibrary`. Managed C++ `dotNetOutline `library is obsolete and will be removed soon.
* Version 0.2.9 (8<sup>th</sup> minor release)
	* Added 3D Text to `OutlineText`, `PngOutlineText`, `dotNetOutlineText` and `dotNetPngOutlineText`
	* Add C++ classes to `TextDesign` namespace
	* Added WYSIWYG code sample for `Extrude `(3D Text feature)
* Version 0.2.8 (7<sup>th</sup> minor release)
	* Solved the bug of incorrect `DrawImage `position for the C++ and C# sample code for the `PngOutlineText`
* Version 0.2.7 (6<sup>th</sup> minor release)
	* Changed the `PngOutlineText `that user must call `Graphics::DrawImage `method now. `DrawString `and `GdiDrawString `methods don&#39;t render now
	* Changed the `TestOutlineText `to use `MeasureString `and `GdiMeasureString `for `PngOutlineText `code to use a small rectangle for rendering than the whole client rectangle
	* Added a `GdiMeasureStringRealHeight `method as a hack for the rotated italic text for `PngOutlineText`
* Version 0.2.6 (5<sup>th</sup> minor release)
	* Added C# sample code generation by which the C# code is copied to the clipboard(Beta)
* Version 0.2.5
	* Added C++ sample code generation by which the C++ code is copied to the clipboard&nbsp;(Beta)
* Version 0.2.4 (4<sup>th</sup> minor release)
	* OpenGL demo to generate the text PNG images dynamically, instead of reading from pre-rendered PNG image files. Noticeable pause at the start of the demo (grey window) because of text PNG images being generated in memory
* Version 0.2.3
	* Added `GdiMeasureString `and `MeasureString `methods to .NET classes
	* Added diffused shadow methods to .NET classes
	* Made `dotNetOutlineText `class and `dotNetPngOutlineText `class inherited from the interface class
* Version 0.2.2 (3<sup>rd</sup> minor release)
	* Added diffused shadow
* Version 0.2.1 (2<sup>nd</sup> minor release)
	* Fixed path memory leaks in `GdiDrawString `methods
	* Added `GdiMeasureString `and `MeasureString`
	* Made `OutlineText `class and `PngOutlineText `class inherited from the `abstract `class
* Version 0.2.0
	* First public release

## History

* 14<sup>th</sup> August, 2018
	* __UWP Win2D Version 0.5.0 Beta__ support only `DirectXPixelFormat.B8G8R8A8UIntNormalized`.
	* __GDI/GDI+ Version 2.1.1 Beta__ with minor optimization of moving part of array index computation out from inner loop.
* 4<sup>th</sup> October, 2015
	* Added GDI (not GDI+) path code example for C#
	* Updated the code download to Version 2 Preview 6
* 17<sup>th</sup> February, 2010
	* Added a section on how to initialize GDI+
	* Replaced all the obsolete C++/CLI `dotNetOutlineText `examples with C# `TextDesignerCSLibrary `code`
	* Added a generic section on how to select a gradient brush
	* Added a section on how to select a gradient brush
* 19<sup>th</sup> October, 2009
	* Expanded the section on how to do real 3D extruded text
* 13<sup>th</sup> October, 2009
	* Added a section on how to do real 3D text
* 7<sup>th</sup> October, 2009
	* Explained how to use `MeasureString `and `GdiMeasureString `methods for `PngOutlineText `objects`
	* Updated some C# code to dispose the objects at the end of `OnPaint `method
	* Updated some explanation
	* Added Diffused Shadow and C++ and C# sample code to copy to clipboard to achieve WYSIWYG
* 22<sup>nd</sup> September, 2009
	* First release on CodeProject
	* Text Designer Outline Text Library version 0.2.0
* 4<sup>th</sup> October, 2015	* Added GDI (not GDI+) path code example for C#	* Updated the code download to Version 2 Preview 6* 17<sup>th</sup> February, 2010	* Added a section on how to initialize GDI+	* Replaced all the obsolete C++/CLI `dotNetOutlineText `examples with C# `TextDesignerCSLibrary `code	* Added a generic section on how to select a gradient brush	* Added a section on how to select a gradient brush* 19<sup>th</sup> October, 2009	* Explained how to use `MeasureString` and `GdiMeasureString` methods for `PngOutlineText` objects
	* Updated some C# code to dispose the objects at the end of `OnPaint' method
	* Updated some explanation
	* Added Diffused Shadow and C++ and C# sample code to copy to clipboard to achieve WYSIWYG
* 22<sup>nd</sup> September, 2009
	* First release on CodeProject
	* Text Designer Outline Text Library version 0.2.0
