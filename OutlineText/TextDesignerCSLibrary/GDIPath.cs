using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TextDesignerCSLibrary
{
	public enum FontWeight : int
	{
		FW_DONTCARE = 0,
		FW_THIN = 100,
		FW_EXTRALIGHT = 200,
		FW_LIGHT = 300,
		FW_NORMAL = 400,
		FW_MEDIUM = 500,
		FW_SEMIBOLD = 600,
		FW_BOLD = 700,
		FW_EXTRABOLD = 800,
		FW_HEAVY = 900,
	}
	public enum FontCharSet : byte
	{
		ANSI_CHARSET = 0,
		DEFAULT_CHARSET = 1,
		SYMBOL_CHARSET = 2,
		SHIFTJIS_CHARSET = 128,
		HANGEUL_CHARSET = 129,
		HANGUL_CHARSET = 129,
		GB2312_CHARSET = 134,
		CHINESEBIG5_CHARSET = 136,
		OEM_CHARSET = 255,
		JOHAB_CHARSET = 130,
		HEBREW_CHARSET = 177,
		ARABIC_CHARSET = 178,
		GREEK_CHARSET = 161,
		TURKISH_CHARSET = 162,
		VIETNAMESE_CHARSET = 163,
		THAI_CHARSET = 222,
		EASTEUROPE_CHARSET = 238,
		RUSSIAN_CHARSET = 204,
		MAC_CHARSET = 77,
		BALTIC_CHARSET = 186,
	}
	public enum FontPrecision : byte
	{
		OUT_DEFAULT_PRECIS = 0,
		OUT_STRING_PRECIS = 1,
		OUT_CHARACTER_PRECIS = 2,
		OUT_STROKE_PRECIS = 3,
		OUT_TT_PRECIS = 4,
		OUT_DEVICE_PRECIS = 5,
		OUT_RASTER_PRECIS = 6,
		OUT_TT_ONLY_PRECIS = 7,
		OUT_OUTLINE_PRECIS = 8,
		OUT_SCREEN_OUTLINE_PRECIS = 9,
		OUT_PS_ONLY_PRECIS = 10,
	}
	public enum FontClipPrecision : byte
	{
		CLIP_DEFAULT_PRECIS = 0,
		CLIP_CHARACTER_PRECIS = 1,
		CLIP_STROKE_PRECIS = 2,
		CLIP_MASK = 0xf,
		CLIP_LH_ANGLES = (1 << 4),
		CLIP_TT_ALWAYS = (2 << 4),
		CLIP_DFA_DISABLE = (4 << 4),
		CLIP_EMBEDDED = (8 << 4),
	}
	public enum FontQuality : byte
	{
		DEFAULT_QUALITY = 0,
		DRAFT_QUALITY = 1,
		PROOF_QUALITY = 2,
		NONANTIALIASED_QUALITY = 3,
		ANTIALIASED_QUALITY = 4,
		CLEARTYPE_QUALITY = 5,
		CLEARTYPE_NATURAL_QUALITY = 6,
	}
	[Flags]
	public enum FontPitchAndFamily : byte
	{
		DEFAULT_PITCH = 0,
		FIXED_PITCH = 1,
		VARIABLE_PITCH = 2,
		FF_DONTCARE = (0 << 4),
		FF_ROMAN = (1 << 4),
		FF_SWISS = (2 << 4),
		FF_MODERN = (3 << 4),
		FF_SCRIPT = (4 << 4),
		FF_DECORATIVE = (5 << 4),
	}
	// if we specify CharSet.Auto instead of CharSet.Ansi, then the string will be unreadable
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class LOGFONT
	{
		public int lfHeight;
		public int lfWidth;
		public int lfEscapement;
		public int lfOrientation;
		public FontWeight lfWeight;
		[MarshalAs(UnmanagedType.U1)]
		public bool lfItalic;
		[MarshalAs(UnmanagedType.U1)]
		public bool lfUnderline;
		[MarshalAs(UnmanagedType.U1)]
		public bool lfStrikeOut;
		public FontCharSet lfCharSet;
		public FontPrecision lfOutPrecision;
		public FontClipPrecision lfClipPrecision;
		public FontQuality lfQuality;
		public FontPitchAndFamily lfPitchAndFamily;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string lfFaceName;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("LOGFONT\n");
			sb.AppendFormat("   lfHeight: {0}\n", lfHeight);
			sb.AppendFormat("   lfWidth: {0}\n", lfWidth);
			sb.AppendFormat("   lfEscapement: {0}\n", lfEscapement);
			sb.AppendFormat("   lfOrientation: {0}\n", lfOrientation);
			sb.AppendFormat("   lfWeight: {0}\n", lfWeight);
			sb.AppendFormat("   lfItalic: {0}\n", lfItalic);
			sb.AppendFormat("   lfUnderline: {0}\n", lfUnderline);
			sb.AppendFormat("   lfStrikeOut: {0}\n", lfStrikeOut);
			sb.AppendFormat("   lfCharSet: {0}\n", lfCharSet);
			sb.AppendFormat("   lfOutPrecision: {0}\n", lfOutPrecision);
			sb.AppendFormat("   lfClipPrecision: {0}\n", lfClipPrecision);
			sb.AppendFormat("   lfQuality: {0}\n", lfQuality);
			sb.AppendFormat("   lfPitchAndFamily: {0}\n", lfPitchAndFamily);
			sb.AppendFormat("   lfFaceName: {0}\n", lfFaceName);

			return sb.ToString();
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int X;
		public int Y;

		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

		public static implicit operator System.Drawing.Point(POINT p)
		{
			return new System.Drawing.Point(p.X, p.Y);
		}

		public static implicit operator POINT(System.Drawing.Point p)
		{
			return new POINT(p.X, p.Y);
		}
	}

    // For GDI, not GDIPlus
    public class NonSystemFontLoader: IDisposable
    {
        [DllImport("gdi32.dll")]
        static extern int AddFontResourceEx(string lpszFilename, uint fl, IntPtr pdv);
        [DllImport("gdi32.dll")]
        static extern int RemoveFontResourceEx(string lpFileName, uint fl, IntPtr pdv);

        public NonSystemFontLoader(string font_file)
        {
            m_FontFile = font_file;
            AddFontResourceEx(font_file, 0x10, IntPtr.Zero);
            disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed==false)
            {
                disposed = true;
                RemoveFontResourceEx(m_FontFile, 0x10, IntPtr.Zero);
            }
        }
        private string m_FontFile;
        bool disposed;
    }

	public class GDIPath
	{

	const int TRANSPARENT = 1;
	const int OPAQUE = 2;
    [DllImport("gdi32.dll", SetLastError = true)]
	static extern int SetBkMode(IntPtr hdc, int iBkMode);
    //[DllImport("gdi32.dll", SetLastError = true)]
	//static extern IntPtr CreateFontIndirect([In] ref LOGFONT lplf);
    [DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern IntPtr CreateFontIndirect(
      [In, MarshalAs(UnmanagedType.LPStruct)]
        LOGFONT lplf   // characteristics
      );
	[DllImport("gdi32.dll", SetLastError = true, EntryPoint = "SelectObject")]
	static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);
    [DllImport("gdi32.dll", SetLastError = true)]
	static extern bool BeginPath(IntPtr hdc);
	[DllImport("gdi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart,  string lpString, int cbString);
    [DllImport("gdi32.dll", SetLastError = true)]
	static extern bool EndPath(IntPtr hdc);
    [DllImport("gdi32.dll", SetLastError = true)]
	static extern int GetPath(IntPtr hdc, [Out] POINT[] lpPoints, [Out] byte[] lpTypes, int nSize);
    [DllImport("gdi32.dll", SetLastError = true)]
	static extern bool PolyDraw(IntPtr hdc, POINT[] lppt, byte[] lpbTypes, int cCount);
    [DllImport("gdi32.dll", SetLastError = true, EntryPoint = "DeleteObject")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DeleteObject([In] IntPtr hObject);

	public struct PathPointType
	{
		public const byte Start = 0;
		public const byte Line = 1;
		public const byte Bezier = 3;
		public const byte PathTypeMask = 0x7;
		public const byte PathDashMode = 0x10;
		public const byte PathMarker = 0x20;
		public const byte CloseSubpath = 0x80;
		public const byte Bezier3 = 3;
	}


	static public bool GetStringPath(
		System.Drawing.Graphics pGraphics,
		System.Drawing.Drawing2D.GraphicsPath ppPath,
		string pszText,
		LOGFONT plf,
		System.Drawing.Point ptDraw)
	{
		IntPtr hDC = pGraphics.GetHdc();

		int nPrevMode = SetBkMode(hDC, TRANSPARENT);

		// create and select it
		IntPtr hFont = CreateFontIndirect(plf);
		if (null == hFont)
			return false;
		IntPtr hOldFont = (IntPtr)SelectObject(hDC, hFont);

		// use a path to record how the text was drawn
		BeginPath(hDC);
		TextOut(hDC, ptDraw.X, ptDraw.Y, pszText, pszText.Length);
		EndPath(hDC);

		// Find out how many points are in the path. Note that
		// for long strings or complex fonts, this number might be
		// gigantic!
        // Allocate memory to hold points and stroke types from
        // the path.
        int nNumPts = GetPath(hDC, null, null, 0);
        if (nNumPts == 0)
        {
			return false;
        }

        // Allocate memory to hold points and stroke types from
        // the path.
        POINT[] lpPoints = new POINT[nNumPts];
        byte[] lpTypes = new byte[nNumPts];

		// Now that we have the memory, really get the path data.
		nNumPts = GetPath(hDC, lpPoints, lpTypes, nNumPts);

		// If it worked, draw the lines. Win95 and Win98 don't support
		// the PolyDraw API, so we use our own member function to do
		// similar work. If you're targeting only Windows NT, you can
		// use the PolyDraw() API and avoid the COutlineView::PolyDraw()
		// member function.

		if (nNumPts != -1)
			PolyDraw(ppPath, lpPoints, lpTypes, nNumPts);

		// Put back the old font
		SelectObject(hDC, hOldFont);
		DeleteObject(hFont);
		SetBkMode(hDC, nPrevMode);

		pGraphics.ReleaseHdc(hDC);

		return true;

	}
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }
    [DllImport("user32.dll")]
    static extern int DrawText(IntPtr hDC, string lpString, int nCount, ref RECT lpRect, uint uFormat);

    static public bool GetStringPath(
        System.Drawing.Graphics pGraphics,
        System.Drawing.Drawing2D.GraphicsPath ppPath,
        string pszText,
        LOGFONT plf,
        System.Drawing.Rectangle rtDraw)
    {
        IntPtr hDC = pGraphics.GetHdc();

        int nPrevMode = SetBkMode(hDC, TRANSPARENT);

        // create and select it
        IntPtr hFont = CreateFontIndirect(plf);
        if (null == hFont)
            return false;
        IntPtr hOldFont = (IntPtr)SelectObject(hDC, hFont);

        RECT rect = new RECT(rtDraw);

        // use a path to record how the text was drawn
       	const uint DT_CENTER = 0x00000001;

        BeginPath(hDC);
        DrawText(hDC, pszText, pszText.Length, ref rect, DT_CENTER);
        EndPath(hDC);

        // Find out how many points are in the path. Note that
        // for long strings or complex fonts, this number might be
        // gigantic!
        int nNumPts = GetPath(hDC, null, null, 0);
        if (nNumPts == 0)
            return false;

        // Allocate memory to hold points and stroke types from
        // the path.
        POINT[] lpPoints = new POINT[nNumPts];
        byte[] lpTypes = new byte[nNumPts];

        // Now that we have the memory, really get the path data.
        nNumPts = GetPath(hDC, lpPoints, lpTypes, nNumPts);

        // If it worked, draw the lines. Win95 and Win98 don't support
        // the PolyDraw API, so we use our own member function to do
        // similar work. If you're targeting only Windows NT, you can
        // use the PolyDraw() API and avoid the COutlineView::PolyDraw()
        // member function.

        if (nNumPts != -1)
            PolyDraw(ppPath, lpPoints, lpTypes, nNumPts);

        // Put back the old font
        SelectObject(hDC, hOldFont);
        DeleteObject(hFont);
        SetBkMode(hDC, nPrevMode);

        pGraphics.ReleaseHdc(hDC);

        return true;

    }
    class stStart
	{
		public stStart()
		{
			fPoint = new PointF();
			nCount = 0;
			bDrawn = false;
		}

		public PointF fPoint;
		public int nCount;
		public bool bDrawn;
	}

	class stBezier
	{
		public stBezier()
		{
			fPoints = new PointF[4];
			nCount = 0;
		}
		public PointF[] fPoints;
		public int nCount;
	}

	static public bool DrawGraphicsPath(
		System.Drawing.Graphics pGraphics,
		System.Drawing.Drawing2D.GraphicsPath pGraphicsPath,
		System.Drawing.Color clrPen,
		float fPenWidth)
	{
		if (pGraphics == null || pGraphicsPath == null)
			return false;

		System.Drawing.Drawing2D.PathData pathData = pGraphicsPath.PathData;

		if (pathData.Points.Length <= 0)
			return false;

		System.Drawing.Pen pen = new Pen(clrPen, fPenWidth);
		pen.LineJoin = LineJoin.Round;


		stBezier bezier = new stBezier();
		stStart start = new stStart();
		PointF prevPoint = new PointF();

		for (int i = 0; i < pathData.Types.Length; ++i)
		{
			byte maskedByte = pathData.Types[i];
			if (pathData.Types[i] == (byte)PathPointType.PathTypeMask)
			{
				maskedByte = (byte)(pathData.Types[i] & 0x3);
			}

			switch (maskedByte)
			{
				case PathPointType.Start:
				case PathPointType.Start | PathPointType.PathMarker:
					start.fPoint = pathData.Points[i];
					start.nCount = 1;
					start.bDrawn = false;
					bezier.nCount = 0;
					break;

				case PathPointType.Line:
				case PathPointType.Line | PathPointType.PathMarker:
					pGraphics.DrawLine(pen, prevPoint, pathData.Points[i]);
					break;

				case PathPointType.Line | PathPointType.CloseSubpath:
				case PathPointType.Line | PathPointType.PathMarker | PathPointType.CloseSubpath:
					pGraphics.DrawLine(pen, prevPoint, pathData.Points[i]);
					pGraphics.DrawLine(pen, pathData.Points[i], start.fPoint);
					start.nCount = 0;
					break;

				case PathPointType.Bezier:
				case PathPointType.Bezier | PathPointType.PathMarker:
					bezier.fPoints[bezier.nCount] = pathData.Points[i];
					bezier.nCount++;
					if (bezier.nCount == 1)
						pGraphics.DrawLine(pen, prevPoint, pathData.Points[i]);
					if (bezier.nCount >= 4)
					{
						pGraphics.DrawBezier(pen,
							bezier.fPoints[0], bezier.fPoints[1],
							bezier.fPoints[2], bezier.fPoints[3]);
						bezier.nCount = 0;
					}
					break;

				case PathPointType.Bezier | PathPointType.CloseSubpath:
				case PathPointType.Bezier | PathPointType.PathMarker | PathPointType.CloseSubpath:
					bezier.fPoints[bezier.nCount] = pathData.Points[i];
					bezier.nCount++;
					if (bezier.nCount == 1)
						pGraphics.DrawLine(pen, prevPoint, pathData.Points[i]);
					if (bezier.nCount >= 4)
					{
						pGraphics.DrawBezier(pen,
							bezier.fPoints[0], bezier.fPoints[1],
							bezier.fPoints[2], bezier.fPoints[3]);
						bezier.nCount = 0;
						if (start.nCount == 1)
						{
							pGraphics.DrawLine(pen, pathData.Points[i], start.fPoint);
							start.nCount = 0;
						}
					}
					else if (start.nCount == 1)
					{
						pGraphics.DrawLine(pen, pathData.Points[i], start.fPoint);
						start.nCount = 0;
						start.bDrawn = true;
					}
					break;
				default:
					{
						//wchar_t buf[200];
						//memset(buf,0, sizeof(buf));
						//wsprintf(buf,_T("maskedByte: 0x%X\n"), maskedByte);
						//OutputDebugStringW(buf);
					}
					break;
			}
			prevPoint = pathData.Points[i];
		}

		return true;
	}

	const int PT_CLOSEFIGURE = 0x01;
	const int PT_LINETO = 0x02;
	const int PT_BEZIERTO = 0x04;
	const int PT_MOVETO = 0x06;


    static public void PolyDraw(
        System.Drawing.Drawing2D.GraphicsPath pPath,
        POINT[] lppt,
        byte[] lpbTypes,
        int cCount)
    {
        int nIndex;
        POINT pptLastMoveTo = new POINT();
        POINT pptPrev = new POINT();

        bool bLastMoveToNull = true;

        // for each of the points we have...
        for (nIndex = 0; nIndex < cCount; nIndex++)
        {
            switch (lpbTypes[nIndex])
            {
                case PT_MOVETO:
                    if (bLastMoveToNull == false && nIndex > 0)
                    {
                        pPath.CloseFigure();
                    }
                    pptLastMoveTo = lppt[nIndex];
                    bLastMoveToNull = false;
                    pptPrev = lppt[nIndex];
                    break;

                case PT_LINETO | PT_CLOSEFIGURE:
                    pPath.AddLine(pptPrev.X, pptPrev.Y, lppt[nIndex].X, lppt[nIndex].Y);
                    pptPrev = lppt[nIndex];
                    if (bLastMoveToNull == false)
                    {
                        pPath.CloseFigure();
                        pptPrev = pptLastMoveTo;
                    }
                    bLastMoveToNull = true;
                    break;

                case PT_LINETO:
                    pPath.AddLine(pptPrev.X, pptPrev.Y, lppt[nIndex].X, lppt[nIndex].Y);
                    pptPrev = lppt[nIndex];
                    break;

                case PT_BEZIERTO | PT_CLOSEFIGURE:
                    //ASSERT(nIndex + 2 <= cCount);
                    pPath.AddBezier(
                        pptPrev.X, pptPrev.Y,
                        lppt[nIndex].X, lppt[nIndex].Y,
                        lppt[nIndex + 1].X, lppt[nIndex + 1].Y,
                        lppt[nIndex + 2].X, lppt[nIndex + 2].Y);
                    nIndex += 2;
                    pptPrev = lppt[nIndex];
                    if (bLastMoveToNull == false)
                    {
                        pPath.CloseFigure();
                        pptPrev = pptLastMoveTo;
                    }
                    bLastMoveToNull = true;
                    break;

                case PT_BEZIERTO:
                    //ASSERT(nIndex + 2 <= cCount);
                    pPath.AddBezier(
                        pptPrev.X, pptPrev.Y,
                        lppt[nIndex].X, lppt[nIndex].Y,
                        lppt[nIndex + 1].X, lppt[nIndex + 1].Y,
                        lppt[nIndex + 2].X, lppt[nIndex + 2].Y);
                    nIndex += 2;
                    pptPrev = lppt[nIndex];
                    break;
            }
        }

        // If the figure was never closed and should be,
        // close it now.
        if (bLastMoveToNull == false && nIndex > 1)
        {
            pPath.AddLine(pptPrev.X, pptPrev.Y, pptLastMoveTo.X, pptLastMoveTo.Y);
            //pPath->CloseFigure();
        }
    }



		static public bool MeasureGraphicsPath(
			System.Drawing.Graphics graphics,
			System.Drawing.Drawing2D.GraphicsPath graphicsPath,
			ref float fStartX,
			ref float fStartY,
			ref float fPixelsWidth,
			ref float fPixelsHeight)
		{
			if(graphicsPath.PathData.Points.Length<=0)
				return false;

			float fHighestX = graphicsPath.PathData.Points[0].X;
			float fHighestY = graphicsPath.PathData.Points[0].Y;
			float fLowestX = graphicsPath.PathData.Points[0].X;
			float fLowestY = graphicsPath.PathData.Points[0].Y;
			int length = graphicsPath.PathData.Points.Length;
			PointF[] points = graphicsPath.PathData.Points;
			for(int i=1; i<length; ++i)
			{
				if (points[i].X < fLowestX)
					fLowestX = points[i].X;
				if (points[i].Y < fLowestY)
					fLowestY = points[i].Y;
				if (points[i].X > fHighestX)
					fHighestX = points[i].X;
				if (points[i].Y > fHighestY)
					fHighestY = points[i].Y;
			}

			// Hack!
			if (fLowestX < 0.0f)
			{
				fStartX = fLowestX;
				fLowestX = -fLowestX;
			}
			else
			{
				fStartX = fLowestX;
				fLowestX = 0.0f;
			}

			if (fLowestY < 0.0f)
			{
				fStartY = fLowestY;
				fLowestY = -fLowestY;
			}
			else
			{
				fStartY = fLowestY;
				fLowestY = 0.0f;
			}

			bool b = ConvertToPixels(
				graphics,
				fLowestX + fHighestX - fPixelsWidth,
				fLowestY + fHighestY - fPixelsHeight,
				ref fStartX,
				ref fStartY,
				ref fPixelsWidth,
				ref fPixelsHeight );

			return b;
		}

		static public bool MeasureGraphicsPathRealHeight(
			System.Drawing.Graphics graphics,
			System.Drawing.Drawing2D.GraphicsPath graphicsPath,
			ref float fStartX,
			ref float fStartY,
			ref float fPixelsWidth,
			ref float fPixelsHeight)
		{
			if (graphicsPath.PathData.Points.Length <= 0)
				return false;

			float fHighestX = graphicsPath.PathData.Points[0].X;
			float fHighestY = graphicsPath.PathData.Points[0].Y;
			float fLowestX = graphicsPath.PathData.Points[0].X;
			float fLowestY = graphicsPath.PathData.Points[0].Y;
			int length = graphicsPath.PathData.Points.Length;
			PointF[] points = graphicsPath.PathData.Points;
			for (int i = 1; i < length; ++i)
			{
				if (points[i].X < fLowestX)
					fLowestX = points[i].X;
				if (points[i].Y < fLowestY)
					fLowestY = points[i].Y;
				if (points[i].X > fHighestX)
					fHighestX = points[i].X;
				if (points[i].Y > fHighestY)
					fHighestY = points[i].Y;
			}

			fStartX = fLowestX;
			fStartY = fLowestY;

			bool b = ConvertToPixels(
				graphics,
				fLowestX + fHighestX - fPixelsWidth,
				fLowestY + fHighestY - fPixelsHeight,
				ref fStartX,
				ref fStartY,
				ref fPixelsWidth,
				ref fPixelsHeight);

			return b;
		}

		static public bool ConvertToPixels(
			System.Drawing.Graphics graphics,
			float fSrcWidth,
			float fSrcHeight,
			ref float fStartX,
			ref float fStartY,
			ref float fDestWidth,
			ref float fDestHeight)
		{
			GraphicsUnit unit = graphics.PageUnit;
			float fDpiX = graphics.DpiX;
			float fDpiY = graphics.DpiY;

			if(unit==GraphicsUnit.World)
				return false; // dunno how to convert

			if(unit==GraphicsUnit.Display||unit==GraphicsUnit.Pixel)
			{
				fDestWidth = fSrcWidth;
				fDestHeight = fSrcHeight;
				return true;
			}

			if(unit==GraphicsUnit.Point)
			{
				fStartX = 1.0f / 72.0f * fDpiX * fStartX;
				fStartY = 1.0f / 72.0f * fDpiY * fStartY;
				fDestWidth = 1.0f / 72.0f * fDpiX * fSrcWidth;
				fDestHeight = 1.0f / 72.0f * fDpiY * fSrcHeight;
				return true;
			}

			if(unit==GraphicsUnit.Inch)
			{
				fStartX = fDpiX * fStartX;
				fStartY = fDpiY * fStartY;
				fDestWidth = fDpiX * fSrcWidth;
				fDestHeight = fDpiY * fSrcHeight;
				return true;
			}

			if(unit==GraphicsUnit.Document)
			{
				fStartX = 1.0f / 300.0f * fDpiX * fStartX;
				fStartY = 1.0f / 300.0f * fDpiY * fStartY;
				fDestWidth = 1.0f / 300.0f * fDpiX * fSrcWidth;
				fDestHeight = 1.0f / 300.0f * fDpiY * fSrcHeight;
				return true;
			}

			if(unit==GraphicsUnit.Millimeter)
			{
				fStartX = 1.0f / 25.4f * fDpiX * fStartX;
				fStartY = 1.0f / 25.4f * fDpiY * fStartY;
				fDestWidth = 1.0f / 25.4f * fDpiX * fSrcWidth;
				fDestHeight = 1.0f / 25.4f * fDpiY * fSrcHeight;
				return true;
			}

			return false;
		}
	}
}
