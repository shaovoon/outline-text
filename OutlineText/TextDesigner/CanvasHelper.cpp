#include "StdAfx.h"
#include "CanvasHelper.h"
#include "MaskColor.h"
#include "PngOutlineText.h"
#include "DrawGradient.h"

using namespace TextDesigner;

std::shared_ptr<ITextStrategy> CanvasHelper::TextGlow(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextGlowStrategy> pStrat = std::make_shared<TextGlowStrategy>();
	pStrat->Init(clrText,clrOutline,nThickness);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextGlow(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextGlowStrategy> pStrat = std::make_shared<TextGlowStrategy>();
	pStrat->Init(&brushText,clrOutline,nThickness);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextOutline(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextOutlineStrategy> pStrat = std::make_shared<TextOutlineStrategy>();
	pStrat->Init(clrText,clrOutline,nThickness);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextOutline(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline, 
	int nThickness)
{
	std::shared_ptr<TextOutlineStrategy> pStrat = std::make_shared<TextOutlineStrategy>();
	pStrat->Init(&brushText,clrOutline,nThickness);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextGradOutline(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineStrategy> pStrat = std::make_shared<TextGradOutlineStrategy>();
	pStrat->Init(clrText,clrOutline1,clrOutline2,nThickness, gradType);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextGradOutline(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineStrategy> pStrat = std::make_shared<TextGradOutlineStrategy>();
	pStrat->Init(&brushText,clrOutline1,clrOutline2,nThickness, gradType);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextGradOutlineLast(
	Gdiplus::Color clrText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineLastStrategy> pStrat = std::make_shared<TextGradOutlineLastStrategy>();
	pStrat->Init(clrText,clrOutline1,clrOutline2,nThickness, gradType);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextGradOutlineLast(
	Gdiplus::Brush& brushText, 
	Gdiplus::Color clrOutline1, 
	Gdiplus::Color clrOutline2, 
	int nThickness,
	GradientType gradType)
{
	std::shared_ptr<TextGradOutlineLastStrategy> pStrat = std::make_shared<TextGradOutlineLastStrategy>();
	pStrat->Init(&brushText,clrOutline1,clrOutline2,nThickness, gradType);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextNoOutline(
	Gdiplus::Color clrText)
{
	std::shared_ptr<TextNoOutlineStrategy> pStrat = std::make_shared<TextNoOutlineStrategy>();
	pStrat->Init(clrText);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextNoOutline(
	Gdiplus::Brush& brushText)
{
	std::shared_ptr<TextNoOutlineStrategy> pStrat = std::make_shared<TextNoOutlineStrategy>();
	pStrat->Init(&brushText);

	return pStrat;
}

std::shared_ptr<ITextStrategy> CanvasHelper::TextOnlyOutline(
	Gdiplus::Color clrOutline, 
	int nThickness,
	bool bRoundedEdge)
{
	std::shared_ptr<TextOnlyOutlineStrategy> pStrat = std::make_shared<TextOnlyOutlineStrategy>();
	pStrat->Init(clrOutline,nThickness,bRoundedEdge);

	return pStrat;
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenImage(int width, int height)
{
	return std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenImage(int width, int height, std::vector<Gdiplus::Color>& vec, bool bHorizontal)
{
	std::shared_ptr<Gdiplus::Bitmap> bmp = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));

	DrawGradient::Draw(*bmp, vec, bHorizontal);

	return bmp;
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenImage(int width, int height, Gdiplus::Color clr)
{
	std::shared_ptr<Gdiplus::Bitmap> bmp = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));

	UINT* pixels = NULL;

	using namespace Gdiplus;

	BitmapData bitmapData;
	Rect rect(0, 0, bmp->GetWidth(), bmp->GetHeight() );

	bmp->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapData );

	pixels = (UINT*)bitmapData.Scan0;

	if( !pixels )
		return NULL;

	UINT col = 0;
	int stride = bitmapData.Stride >> 2;
	UINT color = clr.GetAlpha() << 24 | clr.GetRed() << 16 | clr.GetGreen() << 8 | clr.GetBlue();
	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		UINT total_row_len = row * stride;
		for(col = 0; col < bitmapData.Width; ++col)
		{
			UINT index = total_row_len + col;
			pixels[index] = color;
		}
	}

	bmp->UnlockBits(&bitmapData);

	return bmp;
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenImage(int width, int height, Gdiplus::Color clr, BYTE alpha)
{
	std::shared_ptr<Gdiplus::Bitmap> bmp = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));

	UINT* pixels = NULL;

	using namespace Gdiplus;

	BitmapData bitmapData;
	Rect rect(0, 0, bmp->GetWidth(), bmp->GetHeight() );

	bmp->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapData );

	pixels = (UINT*)bitmapData.Scan0;

	if( !pixels )
		return NULL;

	UINT col = 0;
	int stride = bitmapData.Stride >> 2;
	UINT color = alpha << 24 | clr.GetRed() << 16 | clr.GetGreen() << 8 | clr.GetBlue();
	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		UINT total_row_len = row * stride;
		for(col = 0; col < bitmapData.Width; ++col)
		{
			UINT index = total_row_len + col;
			pixels[index] = color;
		}
	}

	bmp->UnlockBits(&bitmapData);

	return bmp;
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenImage(int width, int height, Gdiplus::LinearGradientBrush& brush , BYTE alpha)
{
	std::shared_ptr<Gdiplus::Bitmap> bmp = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));
	Gdiplus::Graphics graph(bmp.get());
	graph.FillRectangle(&brush, Gdiplus::Rect(0, 0, width, height));

	UINT* pixels = NULL;

	using namespace Gdiplus;

	BitmapData bitmapData;
	Rect rect(0, 0, bmp->GetWidth(), bmp->GetHeight());

	bmp->LockBits(
		&rect,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapData);

	pixels = (UINT*)bitmapData.Scan0;

	if (!pixels)
		return NULL;

	UINT col = 0;
	int stride = bitmapData.Stride >> 2;
	for (UINT row = 0; row < bitmapData.Height; ++row)
	{
		UINT total_row_len = row * stride;
		for (col = 0; col < bitmapData.Width; ++col)
		{
			UINT index = total_row_len + col;

			UINT color = pixels[index] & 0xFFFFFF;

			pixels[index] = color;
		}
	}

	bmp->UnlockBits(&bitmapData);

	return bmp;
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenMask(
	std::shared_ptr<ITextStrategy>& pStrategy, 
	int width, 
	int height, 
	Gdiplus::Point offset,
	TextContext& textContext)
{
	std::shared_ptr<Gdiplus::Bitmap> pBmp = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));

	Gdiplus::Graphics graphics((Gdiplus::Image*)(pBmp.get()));
	graphics.SetSmoothingMode(Gdiplus::SmoothingModeAntiAlias);
	graphics.SetInterpolationMode(Gdiplus::InterpolationModeHighQualityBicubic);

	pStrategy->DrawString(&graphics,
		textContext.pFontFamily, 
		textContext.fontStyle, 
		textContext.nfontSize, 
		textContext.pszText, 
		Gdiplus::Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
		&(textContext.strFormat));

	return pBmp;
}

std::shared_ptr<Gdiplus::Bitmap> CanvasHelper::GenMask(
	std::shared_ptr<ITextStrategy>& pStrategy, 
	int width, 
	int height, 
	Gdiplus::Point offset,
	TextContext& textContext,
	Gdiplus::Matrix& mat)
{
	std::shared_ptr<Gdiplus::Bitmap> pBmp = std::shared_ptr<Gdiplus::Bitmap>(new Gdiplus::Bitmap(width, height, PixelFormat32bppARGB));

	Gdiplus::Graphics graphics((Gdiplus::Image*)(pBmp.get()));
	graphics.SetSmoothingMode(Gdiplus::SmoothingModeAntiAlias);
	graphics.SetInterpolationMode(Gdiplus::InterpolationModeHighQualityBicubic);
	graphics.SetTransform(&mat);

	pStrategy->DrawString(&graphics,
		textContext.pFontFamily, 
		textContext.fontStyle, 
		textContext.nfontSize, 
		textContext.pszText, 
		Gdiplus::Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
		&(textContext.strFormat));

	graphics.ResetTransform();

	return pBmp;
}

bool CanvasHelper::MeasureMaskLength(
	std::shared_ptr<Gdiplus::Bitmap>& pMask, 
	Gdiplus::Color maskColor,
	UINT& top,
	UINT& left,
	UINT& bottom,
	UINT& right)
{
	top = 30000;
	left = 30000;
	bottom = 0;
	right = 0;

	if(pMask==NULL)
		return false;

	UINT* pixelsMask = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataMask;
	Rect rect(0, 0, pMask->GetWidth(), pMask->GetHeight() );

	pMask->LockBits(
		&rect,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataMask );

	pixelsMask = (UINT*)bitmapDataMask.Scan0;

	if( !pixelsMask )
		return false;

	UINT col = 0;
	int stride = bitmapDataMask.Stride >> 2;
	for(UINT row = 0; row < bitmapDataMask.Height; ++row)
	{
		UINT total_row_len = row * stride;
		for(col = 0; col < bitmapDataMask.Width; ++col)
		{
			UINT index = total_row_len + col;
			BYTE nAlpha = 0;

			if(MaskColor::IsEqual(maskColor, MaskColor::Red()))
				nAlpha = (pixelsMask[index] & 0xff0000)>>16;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Green()))
				nAlpha = (pixelsMask[index] & 0xff00)>>8;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Blue()))
				nAlpha = pixelsMask[index] & 0xff;

			if(nAlpha>0)
			{
				if(col < left)
					left = col;
				if(row < top)
					top = row;
				if(col > right)
					right = col;
				if(row > bottom)
					bottom = row;

			}
		}
	}

	pMask->UnlockBits(&bitmapDataMask);

	return true;

}

bool CanvasHelper::ApplyImageToMask(
	std::shared_ptr<Gdiplus::Bitmap>& pImage, 
	std::shared_ptr<Gdiplus::Bitmap>& pMask, 
	std::shared_ptr<Gdiplus::Bitmap>& pCanvas, 
	Gdiplus::Color maskColor,
	bool NoAlphaAtBoundary)
{
	if(pImage==NULL||pMask==NULL||pCanvas==NULL)
		return false;

	UINT* pixelsImage = NULL;
	UINT* pixelsMask = NULL;
	UINT* pixelsCanvas = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataImage;
	BitmapData bitmapDataMask;
	BitmapData bitmapDataCanvas;
	Rect rectCanvas(0, 0, pCanvas->GetWidth(), pCanvas->GetHeight() );
	Rect rectMask(0, 0, pMask->GetWidth(), pMask->GetHeight() );
	Rect rectImage(0, 0, pImage->GetWidth(), pImage->GetHeight() );

	pImage->LockBits(
		&rectImage,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataImage );

	pMask->LockBits(
		&rectMask,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataMask );

	pCanvas->LockBits(
		&rectCanvas,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataCanvas );

	pixelsImage = (UINT*)bitmapDataImage.Scan0;
	pixelsMask = (UINT*)bitmapDataMask.Scan0;
	pixelsCanvas = (UINT*)bitmapDataCanvas.Scan0;

	if( !pixelsImage || !pixelsMask || !pixelsCanvas )
		return false;

	UINT col = 0;
	int stride = bitmapDataCanvas.Stride >> 2;
	for(UINT row = 0; row < bitmapDataCanvas.Height; ++row)
	{
		UINT total_row_len = row * stride;
		UINT total_row_mask_len = row * (bitmapDataMask.Stride >> 2);
		UINT total_row_image_len = row * (bitmapDataImage.Stride >> 2);
		for(col = 0; col < bitmapDataCanvas.Width; ++col)
		{
			if(row >= bitmapDataImage.Height || col >= bitmapDataImage.Width)
				continue;
			if(row >= bitmapDataMask.Height || col >= bitmapDataMask.Width)
				continue;

			UINT index = total_row_len + col;
			UINT indexMask = total_row_mask_len + col;
			UINT indexImage = total_row_image_len + col;

			BYTE mask = 0;
			
			if(MaskColor::IsEqual(maskColor, MaskColor::Red()))
				mask = (pixelsMask[indexMask] & 0xff0000)>>16;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Green()))
				mask = (pixelsMask[indexMask] & 0xff00)>>8;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Blue()))
				mask = pixelsMask[indexMask] & 0xff;

			if(mask>0)
			{
				if(NoAlphaAtBoundary)
				{
					pixelsCanvas[index] = AlphablendNoAlphaAtBoundary(pixelsCanvas[index], pixelsImage[indexImage], (BYTE)(pixelsMask[indexMask] >> 24), (BYTE)(pixelsMask[indexMask] >> 24));
				}
				else
				{
					pixelsCanvas[index] = Alphablend(pixelsCanvas[index], pixelsImage[indexImage], (BYTE)(pixelsMask[indexMask] >> 24), (BYTE)(pixelsMask[indexMask] >> 24));
				}
			}
		}
	}

	pCanvas->UnlockBits(&bitmapDataCanvas);
	pMask->UnlockBits(&bitmapDataMask);
	pImage->UnlockBits(&bitmapDataImage);

	return true;
}

bool CanvasHelper::ApplyColorToMask(
	Gdiplus::Color clr, 
	std::shared_ptr<Gdiplus::Bitmap>& pMask, 
	std::shared_ptr<Gdiplus::Bitmap>& pCanvas, 
	Gdiplus::Color maskColor)
{
	if(pMask==NULL||pCanvas==NULL)
		return false;

	UINT* pixelsMask = NULL;
	UINT* pixelsCanvas = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataMask;
	BitmapData bitmapDataCanvas;
	Rect rectCanvas(0, 0, pCanvas->GetWidth(), pCanvas->GetHeight() );
	Rect rectMask(0, 0, pMask->GetWidth(), pMask->GetHeight() );

	pMask->LockBits(
		&rectMask,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataMask );

	pCanvas->LockBits(
		&rectCanvas,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataCanvas );

	pixelsMask = (UINT*)bitmapDataMask.Scan0;
	pixelsCanvas = (UINT*)bitmapDataCanvas.Scan0;

	if( !pixelsMask || !pixelsCanvas )
		return false;

	UINT col = 0;
	int stride = bitmapDataCanvas.Stride >> 2;
	for(UINT row = 0; row < bitmapDataCanvas.Height; ++row)
	{
		UINT total_row_len = row * stride;
		UINT total_row_mask_len = row * (bitmapDataMask.Stride >> 2);
		for(col = 0; col < bitmapDataCanvas.Width; ++col)
		{
			if(row >= bitmapDataMask.Height || col >= bitmapDataMask.Width)
				continue;

			UINT index = total_row_len + col;
			UINT indexMask = total_row_mask_len + col;

			BYTE nAlpha = 0;

			if(MaskColor::IsEqual(maskColor, MaskColor::Red()))
				nAlpha = (pixelsMask[indexMask] & 0xff0000)>>16;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Green()))
				nAlpha = (pixelsMask[indexMask] & 0xff00)>>8;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Blue()))
				nAlpha = pixelsMask[indexMask] & 0xff;

			UINT color = 0xff << 24 | clr.GetRed() << 16 | clr.GetGreen() << 8 | clr.GetBlue() ;

			if(nAlpha>0)
				pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, nAlpha, nAlpha);
		}
	}

	pCanvas->UnlockBits(&bitmapDataCanvas);
	pMask->UnlockBits(&bitmapDataMask);

	return true;
}

bool CanvasHelper::ApplyColorToMask(
	Gdiplus::Color clr, 
	std::shared_ptr<Gdiplus::Bitmap>& pMask, 
	std::shared_ptr<Gdiplus::Bitmap>& pCanvas, 
	Gdiplus::Color maskColor,
	Gdiplus::Point offset)
{
	if(pMask==NULL||pCanvas==NULL)
		return false;

	UINT* pixelsMask = NULL;
	UINT* pixelsCanvas = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataMask;
	BitmapData bitmapDataCanvas;
	Rect rectCanvas(0, 0, pCanvas->GetWidth(), pCanvas->GetHeight() );
	Rect rectMask(0, 0, pMask->GetWidth(), pMask->GetHeight() );

	pMask->LockBits(
		&rectMask,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataMask );

	pCanvas->LockBits(
		&rectCanvas,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataCanvas );

	pixelsMask = (UINT*)bitmapDataMask.Scan0;
	pixelsCanvas = (UINT*)bitmapDataCanvas.Scan0;

	if( !pixelsMask || !pixelsCanvas )
		return false;

	UINT col = 0;
	int stride = bitmapDataCanvas.Stride >> 2;
	for(UINT row = 0; row < bitmapDataCanvas.Height; ++row)
	{
		UINT total_row_len = row * stride;
		UINT total_row_mask_len = (row - offset.Y) * (bitmapDataMask.Stride >> 2);
		for(col = 0; col < bitmapDataCanvas.Width; ++col)
		{
			if(row-offset.Y >= bitmapDataMask.Height || col-offset.X >= bitmapDataMask.Width||
				row-offset.Y < 0 || col-offset.X < 0)
				continue;

			UINT index = total_row_len + col;
			UINT indexMask = total_row_mask_len + (col-offset.X);

			BYTE nAlpha = 0;

			if(MaskColor::IsEqual(maskColor, MaskColor::Red()))
				nAlpha = (pixelsMask[indexMask] & 0xff0000)>>16;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Green()))
				nAlpha = (pixelsMask[indexMask] & 0xff00)>>8;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Blue()))
				nAlpha = pixelsMask[indexMask] & 0xff;

			UINT color = 0xff << 24 | clr.GetRed() << 16 | clr.GetGreen() << 8 | clr.GetBlue() ;

			if(nAlpha>0)
				pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, nAlpha, nAlpha);
		}
	}

	pCanvas->UnlockBits(&bitmapDataCanvas);
	pMask->UnlockBits(&bitmapDataMask);

	return true;
}

bool CanvasHelper::ApplyShadowToMask(
	Gdiplus::Color clrShadow, 
	std::shared_ptr<Gdiplus::Bitmap>& pMask, 
	std::shared_ptr<Gdiplus::Bitmap>& pCanvas, 
	Gdiplus::Color maskColor)
{
	if(pMask==NULL||pCanvas==NULL)
		return false;

	UINT* pixelsMask = NULL;
	UINT* pixelsCanvas = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataMask;
	BitmapData bitmapDataCanvas;
	Rect rectCanvas(0, 0, pCanvas->GetWidth(), pCanvas->GetHeight() );
	Rect rectMask(0, 0, pMask->GetWidth(), pMask->GetHeight() );

	pMask->LockBits(
		&rectMask,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataMask );

	pCanvas->LockBits(
		&rectCanvas,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataCanvas );

	pixelsMask = (UINT*)bitmapDataMask.Scan0;
	pixelsCanvas = (UINT*)bitmapDataCanvas.Scan0;

	if( !pixelsMask || !pixelsCanvas )
		return false;

	UINT col = 0;
	int stride = bitmapDataCanvas.Stride >> 2;
	for(UINT row = 0; row < bitmapDataCanvas.Height; ++row)
	{
		UINT total_row_len = row * stride;
		UINT total_row_mask_len = row * (bitmapDataMask.Stride >> 2);
		for(col = 0; col < bitmapDataCanvas.Width; ++col)
		{
			if(row >= bitmapDataMask.Height || col >= bitmapDataMask.Width)
				continue;

			UINT index = total_row_len + col;
			UINT indexMask = total_row_mask_len + col;

			BYTE nAlpha = 0;

			if(MaskColor::IsEqual(maskColor, MaskColor::Red()))
				nAlpha = (pixelsMask[indexMask] & 0xff0000)>>16;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Green()))
				nAlpha = (pixelsMask[indexMask] & 0xff00)>>8;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Blue()))
				nAlpha = pixelsMask[indexMask] & 0xff;

			UINT color = 0xff << 24 | clrShadow.GetRed() << 16 | clrShadow.GetGreen() << 8 | clrShadow.GetBlue() ;

			if(nAlpha>0)
			{
				UINT maskAlpha = (pixelsMask[indexMask] >> 24);

				pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, maskAlpha, maskAlpha * clrShadow.GetAlpha() / 255);
			}
		}
	}

	pCanvas->UnlockBits(&bitmapDataCanvas);
	pMask->UnlockBits(&bitmapDataMask);

	return true;
}

bool CanvasHelper::ApplyShadowToMask(
	Gdiplus::Color clrShadow, 
	std::shared_ptr<Gdiplus::Bitmap>& pMask, 
	std::shared_ptr<Gdiplus::Bitmap>& pCanvas, 
	Gdiplus::Color maskColor,
	Gdiplus::Point offset)
{
	if(pMask==NULL||pCanvas==NULL)
		return false;

	UINT* pixelsMask = NULL;
	UINT* pixelsCanvas = NULL;

	using namespace Gdiplus;

	BitmapData bitmapDataMask;
	BitmapData bitmapDataCanvas;
	Rect rectCanvas(0, 0, pCanvas->GetWidth(), pCanvas->GetHeight() );
	Rect rectMask(0, 0, pMask->GetWidth(), pMask->GetHeight() );

	pMask->LockBits(
		&rectMask,
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapDataMask );

	pCanvas->LockBits(
		&rectCanvas,
		ImageLockModeWrite,
		PixelFormat32bppARGB,
		&bitmapDataCanvas );

	pixelsMask = (UINT*)bitmapDataMask.Scan0;
	pixelsCanvas = (UINT*)bitmapDataCanvas.Scan0;

	if( !pixelsMask || !pixelsCanvas )
		return false;

	UINT col = 0;
	int stride = bitmapDataCanvas.Stride >> 2;
	for(UINT row = 0; row < bitmapDataCanvas.Height; ++row)
	{
		UINT total_row_len = row * stride;
		UINT total_row_mask_len = (row - offset.Y) * (bitmapDataMask.Stride >> 2);
		for(col = 0; col < bitmapDataCanvas.Width; ++col)
		{
			if(row-offset.Y >= bitmapDataMask.Height || col-offset.X >= bitmapDataMask.Width||
				row-offset.Y < 0 || col-offset.X < 0)
				continue;

			UINT index = total_row_len + col;
			UINT indexMask = total_row_mask_len + (col-offset.X);

			BYTE nAlpha = 0;

			if(MaskColor::IsEqual(maskColor, MaskColor::Red()))
				nAlpha = (pixelsMask[indexMask] & 0xff0000)>>16;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Green()))
				nAlpha = (pixelsMask[indexMask] & 0xff00)>>8;
			else if(MaskColor::IsEqual(maskColor, MaskColor::Blue()))
				nAlpha = pixelsMask[indexMask] & 0xff;

			UINT color = 0xff << 24 | clrShadow.GetRed() << 16 | clrShadow.GetGreen() << 8 | clrShadow.GetBlue() ;

			if(nAlpha>0)
			{
				UINT maskAlpha = (pixelsMask[indexMask] >> 24);

				pixelsCanvas[index] = Alphablend(pixelsCanvas[index], color, maskAlpha, maskAlpha * clrShadow.GetAlpha() / 255);
			}
		}
	}

	pCanvas->UnlockBits(&bitmapDataCanvas);
	pMask->UnlockBits(&bitmapDataMask);

	return true;
}

bool CanvasHelper::DrawTextImage(
	std::shared_ptr<ITextStrategy>& pStrategy, 
	std::shared_ptr<Gdiplus::Bitmap>& pImage, 
	Gdiplus::Point offset,
	TextContext& textContext)
{
	if(pImage==NULL)
		return false;

	Gdiplus::Graphics graphics((Gdiplus::Image*)(pImage.get()));
	graphics.SetSmoothingMode(Gdiplus::SmoothingModeAntiAlias);
	graphics.SetInterpolationMode(Gdiplus::InterpolationModeHighQualityBicubic);

	bool bRet = pStrategy->DrawString(&graphics,
		textContext.pFontFamily, 
		textContext.fontStyle, 
		textContext.nfontSize, 
		textContext.pszText, 
		Gdiplus::Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
		&(textContext.strFormat));

	return bRet;
}

bool CanvasHelper::DrawTextImage(
	std::shared_ptr<ITextStrategy>& pStrategy, 
	std::shared_ptr<Gdiplus::Bitmap>& pImage, 
	Gdiplus::Point offset,
	TextContext& textContext,
	Gdiplus::Matrix& mat)
{
	if(pImage==NULL)
		return false;

	Gdiplus::Graphics graphics((Gdiplus::Image*)(pImage.get()));
	graphics.SetSmoothingMode(Gdiplus::SmoothingModeAntiAlias);
	graphics.SetInterpolationMode(Gdiplus::InterpolationModeHighQualityBicubic);
	graphics.SetTransform(&mat);

	bool bRet = pStrategy->DrawString(&graphics,
		textContext.pFontFamily, 
		textContext.fontStyle, 
		textContext.nfontSize, 
		textContext.pszText, 
		Gdiplus::Point(textContext.ptDraw.X + offset.X, textContext.ptDraw.Y + offset.Y),
		&(textContext.strFormat));

	graphics.ResetTransform();

	return bRet;
}

inline UINT CanvasHelper::AddAlpha(UINT dest, UINT source, BYTE nAlpha)
{
	if( 0 == nAlpha )
		return dest;

	if( 255 == nAlpha )
		return source;

	BYTE nRed   = (source & 0xff0000) >> 16; 
	BYTE nGreen = (source & 0xff00) >> 8; 
	BYTE nBlue  = (source & 0xff); 

	return nAlpha << 24 | nRed << 16 | nGreen << 8 | nBlue;
}

inline UINT CanvasHelper::AlphablendNoAlphaAtBoundary(UINT dest, UINT source, BYTE nAlpha, BYTE nAlphaFinal)
{
	BYTE nInvAlpha = ~nAlpha;

	BYTE nSrcRed   = (source & 0xff0000) >> 16; 
	BYTE nSrcGreen = (source & 0xff00) >> 8; 
	BYTE nSrcBlue  = (source & 0xff); 

	BYTE nDestRed   = (dest & 0xff0000) >> 16; 
	BYTE nDestGreen = (dest & 0xff00) >> 8; 
	BYTE nDestBlue  = (dest & 0xff); 

	BYTE nRed  = ( nSrcRed   * nAlpha + nDestRed * nInvAlpha   )>>8;
	BYTE nGreen= ( nSrcGreen * nAlpha + nDestGreen * nInvAlpha )>>8;
	BYTE nBlue = ( nSrcBlue  * nAlpha + nDestBlue * nInvAlpha  )>>8;

	return 0xff << 24 | nRed << 16 | nGreen << 8 | nBlue;
}

inline UINT CanvasHelper::Alphablend(UINT dest, UINT source, BYTE nAlpha, BYTE nAlphaFinal)
{
	BYTE nInvAlpha = ~nAlpha;

	BYTE nSrcRed   = (source & 0xff0000) >> 16; 
	BYTE nSrcGreen = (source & 0xff00) >> 8; 
	BYTE nSrcBlue  = (source & 0xff); 

	BYTE nDestRed   = (dest & 0xff0000) >> 16; 
	BYTE nDestGreen = (dest & 0xff00) >> 8; 
	BYTE nDestBlue  = (dest & 0xff); 

	BYTE nRed  = ( nSrcRed   * nAlpha + nDestRed * nInvAlpha   )>>8;
	BYTE nGreen= ( nSrcGreen * nAlpha + nDestGreen * nInvAlpha )>>8;
	BYTE nBlue = ( nSrcBlue  * nAlpha + nDestBlue * nInvAlpha  )>>8;

	return nAlphaFinal << 24 | nRed << 16 | nGreen << 8 | nBlue;
}

inline UINT CanvasHelper::PreMultipliedAlphablend(UINT dest, UINT source)
{
	BYTE nAlpha = (source & 0xff000000) >> 24;
	BYTE nInvAlpha = 255 - nAlpha;

	BYTE nSrcRed   = (source & 0xff0000) >> 16; 
	BYTE nSrcGreen = (source & 0xff00) >> 8; 
	BYTE nSrcBlue  = (source & 0xff); 

	BYTE nDestRed   = (dest & 0xff0000) >> 16; 
	BYTE nDestGreen = (dest & 0xff00) >> 8; 
	BYTE nDestBlue  = (dest & 0xff); 

	BYTE nRed  = nSrcRed   + ((nDestRed * nInvAlpha   )>>8);
	BYTE nGreen= nSrcGreen + ((nDestGreen * nInvAlpha )>>8);
	BYTE nBlue = nSrcBlue  + ((nDestBlue * nInvAlpha  )>>8);

	return 0xff << 24 | nRed << 16 | nGreen << 8 | nBlue;
}
