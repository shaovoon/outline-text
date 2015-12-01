//#define WIN32_LEAN_AND_MEAN

//The main windows include file
#include <windows.h>
#include <gdiplus.h>
#include <gl/gl.h>
#include <gl/glu.h>
//#include <gl/glaux.h>
#include <string>
#include <cstdlib>
#include <ctime>

#include <string.h> //for strstr(..)
#include <process.h>

#include "../TextDesigner/PngOutlineText.h"

using namespace TextDesigner;

//function pointer typdefs
typedef void (APIENTRY
			  *PFNWGLEXTSWAPCONTROLPROC) (int);
typedef int (*PFNWGLEXTGETSWAPINTERVALPROC) (void);

//declare functions
PFNWGLEXTSWAPCONTROLPROC wglSwapIntervalEXT = NULL;
PFNWGLEXTGETSWAPINTERVALPROC wglGetSwapIntervalEXT = NULL;

//init VSync func
void InitVSync();
bool IsVSyncEnabled();
void SetVSyncState(bool enable);


#define MAX_STAR_COL 8
#define MAX_STAR_ROW 5

#define MAX_RECT_COL 4
#define MAX_RECT_ROW 3

struct BmpInfo
{
	// default constructor
	BmpInfo() : pPixels1(NULL), nWidth(0), nHeight(0), nTexLen(0) {}
	UINT* pPixels1;
	UINT nWidth;
	UINT nHeight;

	UINT nTexLen;
};

struct FPOINT
{
	FPOINT() : x(0.0f), y(0.0f) {}
	float x;
	float y;
};

FPOINT g_ptArrStar[MAX_STAR_COL][MAX_STAR_ROW];
float g_fAngleOffset[MAX_STAR_COL][MAX_STAR_ROW];

FPOINT g_ptRect[MAX_RECT_COL][MAX_RECT_ROW];

float g_fRectWidth;
float g_fRectHeight;

// OpenGL helper functions
void SetupPixelFormat( HDC hDC );
bool DrawPic1();
bool DrawPic2();
void Render( void );

bool LoadBitmaps();
bool LoadOneBitmap( BmpInfo& bmpInfo, const std::wstring& szExePath, const std::wstring& szFile);
bool LoadOneTextBitmapFromFile( BmpInfo& bmpInfo, const std::wstring& szExePath, const std::wstring& szFile);

bool LoadPhotos();
bool LoadOnePhoto( BmpInfo& bmpInfo, const std::wstring& szExePath, const std::wstring& szFile);

bool LoadTextures();
bool LoadOneTexture( BmpInfo& bmpInfo, UINT& texture );
bool LoadOneTextTexture( BmpInfo& bmpInfo, UINT& texture );
void CleanUp();
bool ValidatePath( const std::wstring& szPath );

void DrawBackground1();
void DrawBackground2();

void DrawStar(float angle, float fx, float fy);
void DrawStarShard(float angle, float fx, float fy);

void DrawInvertedStar(float angle, float fx, float fy);
void DrawInvertedStarShard(float angle, float fx, float fy);


void DrawRectangle1(float fx, float fy);
void DrawRectangle2(float fx, float fy);

void DrawPhoto1(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fRotateX, 
	float fRotateY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight );

void DrawPhoto2(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fRotateX, 
	float fRotateY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight );

void DrawText1(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight );

void DrawText2(
   UINT texture, 
   float fTexX, 
   float fTexY, 
   float fTransX, 
   float fTransY,
   float fRectWidth, 
   float fRectHeight );

int GetEncoderClsid(const WCHAR* format, CLSID* pClsid);
bool CaptureScreenShot(
	int nWidth, 
	int nHeight, 
	const std::wstring& szDestFile,
	const std::wstring& szEncoderString);

bool GenerateTextBitmap(
	Gdiplus::Graphics* pGraphics,
	const std::wstring& szText, 
	Gdiplus::Bitmap** pbmp, 
	bool bPurple);

bool LoadOneTextBitmap( 
	BmpInfo& bmpInfo, 
	Gdiplus::Bitmap* pbmp);


// Global Variables
HDC g_hDC;
HWND g_hWnd;
UINT g_texture1=0; // star shard
UINT g_texture3=0; // rect

UINT g_texture4=0; // inverted star shard
UINT g_texture6=0; // rect 2

UINT g_texture7=0; // Photo
UINT g_texture8=0; // Photo2 for the next page
UINT g_texture9=0; // Photo
UINT g_texture10=0; // Photo2 for the next page

UINT g_textureText1=0; // Text Png
UINT g_textureText2=0; // Text Png
UINT g_textureText3=0; // Text Png
UINT g_textureText4=0; // Text Png

BmpInfo g_bmpInfo1; // star shard
BmpInfo g_bmpInfo3; // rect

BmpInfo g_bmpInfo4; // inverted star shard
BmpInfo g_bmpInfo6; // rect 2

BmpInfo g_bmpInfo7; // Photo
BmpInfo g_bmpInfo8; // Photo

BmpInfo g_bmpInfo9; // Photo
BmpInfo g_bmpInfo10; // Photo

BmpInfo g_bmpTextInfo1; // Photo
BmpInfo g_bmpTextInfo2; // Photo
BmpInfo g_bmpTextInfo3; // Photo
BmpInfo g_bmpTextInfo4; // Photo

bool g_reset;
bool g_bFrontShown;
bool g_NoOpenGL;

void TextThread(void* p);

struct tagTextStruct
{
	std::wstring szText;
	Gdiplus::Bitmap* pbmp;
	bool bPurple;
};

bool GenerateTextBitmapThread(
							  const std::wstring& szText, 
							  Gdiplus::Bitmap** pbmp, 
							  bool bPurple);



// The Window procedure to handle events
long CALLBACK WndProc(HWND hWnd, UINT uMessage, WPARAM wParam, LPARAM lParam)
{
//	PAINTSTRUCT PaintStruct;//Structure used during windows painting
	static HDC hDC;
	static HGLRC hRC;
	int height, width;

	// Switch the windows message to figure out what it is
	switch(uMessage)
	{
		case WM_CREATE://THe CreateWindows was just called
		{
			hDC = GetDC( hWnd );
			g_hDC = hDC;

			SetupPixelFormat( hDC );

			
			hRC = wglCreateContext( hDC );
			wglMakeCurrent( hDC, hRC );

			LPCSTR psz = (LPCSTR)glGetString( GL_VERSION );

			std::string strVers = psz;

			if(strVers.length() >= 3)
			{
				if(strVers[0] == '1' && strVers[2] >= '4')
					g_NoOpenGL = false;
				else if(strVers[0] >= '2')
					g_NoOpenGL = false;
				else
					g_NoOpenGL = true;
			}
			else
				g_NoOpenGL = true;

			return 0;
		}
/*		
		case WM_PAINT://The window needs to be redrawn
		{
			//Tell windows we want to start updating the window
			hDC=BeginPaint(hWnd,&PaintStruct);
			
			//Do any drawing with GDI here
			
			//Tell Windows we have finished updating the window
			EndPaint(hWnd, &PaintStruct);
			return 0;
		}
*/			
		case WM_SIZE:
		{
			//height = HIWORD(lParam);
			//width = LOWORD(lParam);

			RECT rect;
			GetClientRect( hWnd, &rect );

			height = rect.bottom;
			width = rect.right;

			if( height == 0 )
				height = 1;

			// Reset the viewport to new dimensions
			glViewport( 0, 0, width, height );
			glMatrixMode( GL_PROJECTION );
			glLoadIdentity();

			// Calculate the aspect ratio of window
			//gluPerspective( 54.0f, (GLfloat)height/(GLfloat)width, 1.0f, 200.0f );
			gluPerspective( 45.0f, (GLfloat)width/(GLfloat)height, 5.0f, 100.0f );

			glMatrixMode( GL_MODELVIEW );
			glLoadIdentity();

			return 0;
		}	

		case WM_DESTROY:
		{
			//Our main window is closing.
			// This means we want our app to exit
			
			wglMakeCurrent( hDC, NULL );
			wglDeleteContext( hRC );

			
			
			//Tell windows to put a WM_QUIT message in our message queue
			PostQuitMessage(0);
			return 0;
		}	
			
		//case WM_LBUTTONDBLCLK:
		//{
		//	wchar_t FullPath[MAX_PATH];
		//	memset( FullPath, 0, sizeof(FullPath) );
		//	std::wstring szExePath;
		//	if (::GetModuleFileNameW( NULL, FullPath, sizeof(wchar_t)*MAX_PATH))
		//	{
		//		szExePath = FullPath;
		//		
		//		int pos = szExePath.rfind( L'\\' );
		//		
		//		if( -1 != pos )
		//		{
		//			szExePath = szExePath.substr(0,pos+1);
		//		}
		//	}

		//	std::wstring szDestFile = szExePath;
		//	szDestFile += _T("Screenshot.jpg");

		//	RECT rect;
		//	memset(&rect,0,sizeof(rect));
		//	GetClientRect(g_hWnd,&rect);
		//	
		//	CaptureScreenShot(
		//		rect.right, 
		//		rect.bottom, 
		//		szDestFile,
		//		_T("image/jpeg");

		//	return 0;
		//}

		default:
		{
			//Let windows handle this message
			return DefWindowProc(hWnd, uMessage, wParam, lParam);	
			
		}	
	}	
}

//The windows entry point, The application will start executing here
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
					PSTR pstrCmdLine, int iCmdShow)
{
	HWND hWnd;//The handle to our main window
	MSG msg;//The message windows is sending us
	WNDCLASSEX wc; //The window class used to create our window
	g_NoOpenGL = false;	

	// The name of our class and also the title to our window
	static wchar_t strAppName[]=L"Demo";

	//Fill in the windows class
	wc.cbSize=sizeof(WNDCLASSEX);
	//Style of windows
	wc.style=CS_HREDRAW | CS_VREDRAW | CS_OWNDC | CS_DBLCLKS;
	//Useless info, just set to 0
	wc.cbClsExtra=0;
	//Useless info, just set to 0
	wc.cbWndExtra=0;
	// The name of our event handler
	wc.lpfnWndProc = WndProc;
	// A handler to the application instance
	wc.hInstance=hInstance;
	// The handle of the background brush
	wc.hbrBackground=(HBRUSH)GetStockObject( DKGRAY_BRUSH );
	// A handle to the icon
	wc.hIcon=LoadIcon(NULL, IDI_APPLICATION);
	// A handle to the small icon
	wc.hIconSm=LoadIcon(NULL, IDI_APPLICATION);
	//A handler o the cursor
	wc.hCursor=LoadCursor(NULL,IDC_CROSS);
	// A handler to the resource to use as menu
	wc.lpszMenuName = NULL;
	// The Human readable name for this class
	wc.lpszClassName=strAppName;
	
	//Register the class with windows
	RegisterClassEx(&wc);
	
	//Create a windows based on the previous class
	hWnd=CreateWindowEx(NULL,//Advanced style settings
						strAppName,//class name
						strAppName,//window caption
						WS_OVERLAPPEDWINDOW,//Windows style
						CW_USEDEFAULT,//initial x position
						CW_USEDEFAULT,//initial y position
						960,735,//Initial width and height
						NULL,//Handle to the parent windows
						NULL,//Handle to the menu
						hInstance,//Handle to the app instance
						NULL);//Advanced context
	//Display the windows
	ShowWindow(hWnd,iCmdShow);
	//Draw the window contents for the first time
	UpdateWindow(hWnd);

	g_hWnd = hWnd;

	if(g_NoOpenGL)
	{
		MessageBoxA(hWnd, "Your PC does not have minimum OpenGL version 1.4\nDemo exit. Sorry.\nPlease update your graphics driver and try again.", "OpenGL Version", MB_ICONERROR | MB_OK);
		CleanUp();
		return 1;
	}

	srand( (unsigned int) time(NULL) );

	using namespace std;
	g_fRectWidth = 12.0f;
	g_fRectHeight = 2.0f;
	for( int x=0; x<MAX_RECT_COL; ++x )
		for( int y=0; y<MAX_RECT_ROW; ++y )
		{
			g_ptRect[x][y].x = x * (g_fRectWidth+4.0f);
			g_ptRect[x][y].y = y * 6.0f;
		}

		for( int x=0; x<MAX_STAR_COL; ++x )
	{
		for( int y=0; y<MAX_STAR_ROW; ++y )
		{
			g_ptArrStar[x][y].x = x*4.5f;
			g_ptArrStar[x][y].y = y*4.5f+((rand()%50)/100.0f)*4.5f;

			g_fAngleOffset[x][y] = (float)(rand()%360);
			
		}
	}
	

	glEnable(GL_TEXTURE_2D);							// Enable Texture Mapping ( NEW )
	glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
	glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background
	glClearDepth(1.0f);									// Depth Buffer Setup
	glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
	glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations

	// Blend
	GLfloat LightAmbient[]=		{ 0.5f, 0.5f, 0.5f, 1.0f };
	GLfloat LightDiffuse[]=		{ 1.0f, 1.0f, 1.0f, 1.0f };
	GLfloat LightPosition[]=	{ 0.0f, 0.0f, 2.0f, 1.0f };

	glLightfv(GL_LIGHT1, GL_AMBIENT, LightAmbient);		// Setup The Ambient Light
	glLightfv(GL_LIGHT1, GL_DIFFUSE, LightDiffuse);		// Setup The Diffuse Light
	glLightfv(GL_LIGHT1, GL_POSITION,LightPosition);	// Position The Light
	glEnable(GL_LIGHT1);								// Enable Light One

	//glColor4f(1.0f,1.0f,1.0f,0.5f);			// Full Brightness, 50% Alpha ( NEW )
	glBlendFunc(GL_SRC_ALPHA,GL_ONE_MINUS_SRC_ALPHA);	
	glEnable(GL_BLEND); 
	glAlphaFunc(GL_GREATER,0.2f);                        // Set Alpha Testing     (disable blending)
	glEnable(GL_ALPHA_TEST);                        // Enable Alpha Testing  (disable blending)

	InitVSync();
	if(!IsVSyncEnabled())
		SetVSyncState(1);

	if( false ==  LoadBitmaps() )
	{
		MessageBoxA(NULL, "LoadBitmaps failed", "error", MB_OK );
		return 0;
	}

	if( false ==  LoadPhotos() )
	{
		MessageBoxA(NULL, "LoadPhotos failed", "error", MB_OK );
		return 0;
	}

	if( false ==  LoadTextures() )
	{
		MessageBoxA(NULL, "LoadTextures failed", "error", MB_OK );
		return 0;
	}

	//Start the message loop
	while(TRUE)
	{
		//Check if a message is waiting 
		if( PeekMessage(&msg, NULL, 0, 0, PM_REMOVE) )
		{
			if(msg.message == WM_QUIT )
				break;//Exit message loop
				
			Render();

			//Change the format of certain messages
			TranslateMessage(&msg);
			// Pass the message to WndProc() for processing
			DispatchMessage(&msg);
		}
		else
		{
			// idle code
			Render();
		}
	}

	CleanUp();

	return msg.wParam;
}



// OpenGL helper functions
void SetupPixelFormat( HDC hDC )
{
	int nPixelFormat;

	static PIXELFORMATDESCRIPTOR pfd = 
		{
		sizeof( PIXELFORMATDESCRIPTOR ),
		1,
		PFD_DRAW_TO_WINDOW |
		PFD_SUPPORT_OPENGL |
		PFD_DOUBLEBUFFER   |
		PFD_TYPE_RGBA,
		32,
		0,0,0,0,0,
		0,
		0,
		0,
		0,0,0,0,
		16,
		0,
		0,
		PFD_MAIN_PLANE,
		0,
		0,0,0 };

	nPixelFormat = ChoosePixelFormat( hDC, &pfd );

	SetPixelFormat( hDC, nPixelFormat, &pfd );

} // SetupPixelFormat

void Render( void )
{
	//Put idle code here
	glClear( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT );
	glLoadIdentity();

	gluLookAt( 
		0.0f, 0.0f, 1.0f,
		0.0f, 0.0f, 2.0f,
		0.0f, 1.0f, 0.0f );

	static bool bDrawPic2 = false;
	if( bDrawPic2 )
		bDrawPic2 = DrawPic2()? false : true;
	else
		bDrawPic2 = DrawPic1();
	
	glMatrixMode( GL_PROJECTION );
//	glLoadIdentity();

	glFrustum( -1.0, 1.0, -1.0, 1.0, 0.0, 30.0 );
	
	SwapBuffers( g_hDC );
	
	glMatrixMode( GL_MODELVIEW );
	glLoadIdentity();

} // Render

// true means change to next pic.
bool DrawPic1()
{
	static float angle = -0.5f;
	static int donecnt = 100;
	static bool bFirstShown = true;

	if( donecnt <= 0 )
	{
		angle = -0.5f;
		donecnt = 100;

		bFirstShown = bFirstShown ? false : true;
	}

	const float angle_step = 1.0f;
	angle += angle_step;

	if( angle >= 360.0f )
	{
		angle = 0.0f;
	}

	static float sfy = 0.0f;
	sfy += 0.05f;
	if( sfy > 10000.0f )
		sfy -= 10000.0f;

	DrawBackground1();

	for( int x=0; x<MAX_RECT_COL; ++x )
		for( int y=0; y<MAX_RECT_ROW; ++y )
		{
			//g_ptRect[x][y].y = y * 5.0f;
			float tempx = g_ptRect[x][y].x + (sfy/3.0f);
			while( tempx > 48.0f )
				tempx = tempx - 48.0f;

			DrawRectangle1(tempx-22.0f, g_ptRect[x][y].y-6.0f);
		}	

	for( int x=0; x<MAX_STAR_COL; ++x )
		for( int y=0; y<MAX_STAR_ROW; ++y )
		{
			float tempangle = angle + g_fAngleOffset[x][y];
			if( tempangle > 360.0f )
				tempangle -= 360.0f;
			float tempy = sfy+g_ptArrStar[x][y].y;
			while( tempy > 24.0f )
				tempy = tempy - 24.0f;

			DrawStar( tempangle, g_ptArrStar[x][y].x-11.0f, tempy-12.0f );
		}

	float RotateX = 20.0f;
	float RotateY = -40.0f;

	RotateX -= (angle*0.1f);
	RotateY += (angle*0.1f)*2;
	static float PrevRotateX = 20.0f;
	static float PrevRotateY = -40.0f;
	
	bool bNoRotationX = false;
	bool bNoRotationY = false;
	if( RotateX < 0.1f)
	{
		RotateX = PrevRotateX;
		bNoRotationX = true;
	}
	else
		PrevRotateX = RotateX;

	if( RotateY > -0.1f)
	{
		RotateY = PrevRotateY;
		bNoRotationY = true;
	}
	else
		PrevRotateY = RotateY;

	UINT texture = g_texture7;
	UINT textureText = g_textureText1;
	BmpInfo* pbmpInfo = &g_bmpInfo7;
	BmpInfo* pbmpTextInfo = &g_bmpTextInfo1;
	if( false == bFirstShown )
	{
		texture = g_texture9;
		pbmpInfo = &g_bmpInfo9;
		textureText = g_textureText3;
		pbmpTextInfo = &g_bmpTextInfo3;
	}
	
	
	DrawPhoto1( 
		texture, 
		(float)pbmpInfo->nWidth/pbmpInfo->nTexLen,
		(float)pbmpInfo->nHeight/pbmpInfo->nTexLen,
		RotateX,
		RotateY,
		-5.0f,
		-5.0f,
		(float)pbmpInfo->nWidth*0.03f,
		(float)pbmpInfo->nHeight*0.03f );

	DrawText1( 
		textureText, 
		(float)pbmpTextInfo->nWidth/pbmpTextInfo->nTexLen,
		(float)pbmpTextInfo->nHeight/pbmpTextInfo->nTexLen,
		-3.0f,
		-3.0f,
		(float)pbmpTextInfo->nWidth*0.03f,
		(float)pbmpTextInfo->nHeight*0.03f );

	if(	bNoRotationX && bNoRotationY && donecnt <= 1 )
	{
		--donecnt;
		return true;
	}
	else if( bNoRotationX && bNoRotationY )
		--donecnt;

	return false;

} // DrawPic1

bool DrawPic2()
{
	static float angle = -0.5f;
	static int donecnt = 100;
	static bool bFirstShown = true;

	if( donecnt <= 0 )
	{
		angle = -0.5f;
		donecnt = 100;

		bFirstShown = bFirstShown ? false : true;
	}


	const float angle_step = 1.0f;
	angle += angle_step;

	if( angle >= 360.0f )
	{
		angle = 0.0f;
	}

	static float sfy = 0.0f;
	sfy += 0.05f;
	if( sfy > 10000.0f )
		sfy -= 10000.0f;

	DrawBackground2();

	for( int x=0; x<MAX_RECT_COL; ++x )
		for( int y=0; y<MAX_RECT_ROW; ++y )
		{
			//g_ptRect[x][y].y = y * 5.0f;
			float tempx = g_ptRect[x][y].x + (sfy/3.0f);
			while( tempx > 48.0f )
				tempx = tempx - 48.0f;

			DrawRectangle2(tempx-22.0f, g_ptRect[x][y].y-6.0f);
		}	

	for( int x=0; x<MAX_STAR_COL; ++x )
		for( int y=0; y<MAX_STAR_ROW; ++y )
		{
			float tempangle = angle + g_fAngleOffset[x][y];
			if( tempangle > 360.0f )
				tempangle -= 360.0f;
			float tempy = sfy+g_ptArrStar[x][y].y;
			while( tempy > 24.0f )
				tempy = tempy - 24.0f;

			DrawInvertedStar( tempangle, g_ptArrStar[x][y].x-11.0f, tempy-12.0f );
		}

	float RotateX = -45.0f;
	float RotateY = 20.0f;

	RotateX += (angle*0.1f)*2;
	RotateY -= (angle*0.1f);
	static float PrevRotateX = -45.0f;
	static float PrevRotateY = 20.0f;
	
	bool bNoRotationX = false;
	bool bNoRotationY = false;
	if( RotateX > -0.1f)
	{
		RotateX = PrevRotateX;
		bNoRotationX = true;
	}
	else
		PrevRotateX = RotateX;

	if( RotateY < 0.1f)
	{
		RotateY = PrevRotateY;
		bNoRotationY = true;
	}
	else
		PrevRotateY = RotateY;
	
	UINT texture = g_texture8;
	UINT textureText = g_textureText2;
	BmpInfo* pbmpInfo = &g_bmpInfo8;
	BmpInfo* pbmpTextInfo = &g_bmpTextInfo2;
	if( false == bFirstShown )
	{
		texture = g_texture10;
		pbmpInfo = &g_bmpInfo10;
		textureText = g_textureText4;
		pbmpTextInfo = &g_bmpTextInfo4;
	}

	DrawPhoto2( 
		texture, 
		(float)pbmpInfo->nWidth/pbmpInfo->nTexLen,
		(float)pbmpInfo->nHeight/pbmpInfo->nTexLen,
		RotateX,
		RotateY,
		0.0f,
		-5.0f,
		(float)pbmpInfo->nWidth*0.03f,
		(float)pbmpInfo->nHeight*0.03f );

	DrawText2( 
		textureText, 
		(float)pbmpTextInfo->nWidth/pbmpTextInfo->nTexLen,
		(float)pbmpTextInfo->nHeight/pbmpTextInfo->nTexLen,
		-3.0f,
		-3.0f,
		(float)pbmpTextInfo->nWidth*0.03f,
		(float)pbmpTextInfo->nHeight*0.03f );

	if(	bNoRotationX && bNoRotationY && donecnt <= 1 )
	{
		--donecnt;
		return true;
	}
	else if( bNoRotationX && bNoRotationY )
		--donecnt;

	return false;
} // DrawPic2

void DrawBackground1()
{
	glClearColor(155.0f/255.0f, 1.0f, 185.0f/255.0f, 1.0f);

} // DrawBackground1

void DrawBackground2()
{
	glClearColor(1.0f, 155.0f/255.0f, 84.0f/255.0f, 1.0f);
} // DrawBackground2

void DrawStar(float angle, float fx, float fy)
{
	DrawStarShard( 0.0f+angle, fx, fy );
	DrawStarShard( 72.0f+angle, fx, fy  );
	DrawStarShard( 144.0f+angle, fx, fy  );
	DrawStarShard( 216.0f+angle, fx, fy  );
	DrawStarShard( 288.0f+angle, fx, fy  );
} // DrawStar

void DrawStarShard(float angle, float fx, float fy)
{
	glPushMatrix();

	glRotatef(180.0f,0.0f,0.0f,1.0f);
	glTranslatef(fx,fy,25.0f);

	glScalef( 0.6f, 0.6f, 0.6f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );
	glRotatef( angle, 0.0f, 0.0f, 1.0f );

	glBindTexture(GL_TEXTURE_2D, g_texture1);
	glBegin( GL_QUADS );
		glTexCoord2f(0.05f, 0.05f); glVertex3f(0.0f,-1.0f-0.688f,0.0f); 
		glTexCoord2f(0.9f, 0.05f); glVertex3f(-0.5f,-0.688f,0.0f); // Front face
		glTexCoord2f(0.9f, 0.9f); glVertex3f(0.0f,0.0f,0.0f); 
		glTexCoord2f(0.05f, 0.9f); glVertex3f(0.5f,-0.688f,0.0f); 
	glEnd();

	glPopMatrix();

} // DrawStarShard

void DrawInvertedStar(float angle, float fx, float fy)
{
	DrawInvertedStarShard( 0.0f+angle, fx, fy );
	DrawInvertedStarShard( 72.0f+angle, fx, fy  );
	DrawInvertedStarShard( 144.0f+angle, fx, fy  );
	DrawInvertedStarShard( 216.0f+angle, fx, fy  );
	DrawInvertedStarShard( 288.0f+angle, fx, fy  );
} // DrawInvertedStar

void DrawInvertedStarShard(float angle, float fx, float fy)
{
	glPushMatrix();

	glRotatef(180.0f,0.0f,0.0f,1.0f);
	glTranslatef(fx,fy,25.0f);

	glScalef( 0.6f, 0.6f, 0.6f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );
	glRotatef( angle, 0.0f, 0.0f, 1.0f );

	glBindTexture(GL_TEXTURE_2D, g_texture4);
	glBegin( GL_QUADS );
		glTexCoord2f(0.0f, 0.0f); glVertex3f(0.5f,-1.5f-0.688f,0.0f); 
		glTexCoord2f(0.98f, 0.0f); glVertex3f(0.0f,-1.5f+0.2f,0.0f); // Front face
		glTexCoord2f(0.98f, 0.98f); glVertex3f(0.0f,0.0f,0.0f); 
		glTexCoord2f(0.0f, 0.98f); glVertex3f(0.5f,-0.688f,0.0f); 

		
		glTexCoord2f(0.0f, 0.0f); glVertex3f(-0.5f,-1.5f-0.688f,0.0f); 
		glTexCoord2f(0.98f, 0.0f); glVertex3f(-0.5f,-0.688f,0.0f); // Front face
		glTexCoord2f(0.98f, 0.98f); glVertex3f(0.0f,0.0f,0.0f); 
		glTexCoord2f(0.0f, 0.98f); glVertex3f(0.0f,-1.5f+0.2f,0.0f); 
	glEnd();

	glPopMatrix();

} // DrawInvertedStarShard

void DrawPhoto1(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fRotateX, 
	float fRotateY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight )
{
	glPushMatrix();

	glTranslatef(fTransX,fTransY+3.0f,18.0f);
	glRotatef(fRotateY,0.0f,1.0f,0.0f); // rotate y
	glRotatef(fRotateX,1.0f,0.0f,0.0f); // rotate x
	glRotatef(180.0f,0.0f,0.0f,1.0f); // rotate z
	glTranslatef(fRectWidth/-2.0f,fRectHeight/-2.0f,0.0f);

	glScalef( 0.7f, 0.7f, 0.7f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );

	glBindTexture(GL_TEXTURE_2D, texture);
	glBegin( GL_QUADS );
		//glColor4f(0.3f,0.3f,0.3f,0.7f);			// Full Brightness, 50% Alpha ( NEW )
		//glColor4f(1.0f,1.0f,1.0f,0.7f);			// Full Brightness, 50% Alpha ( NEW )
		glTexCoord2f(fTexX, 0.0f); glVertex3f(fRectWidth,0.0f,-0.00001f); 
		glTexCoord2f(fTexX, fTexY); glVertex3f(fRectWidth,fRectHeight,-0.00001f); 
		glTexCoord2f(0.0f, fTexY); glVertex3f(0.0f,fRectHeight,-0.00001f); // Front face
		glTexCoord2f(0.0f, 0.0f); glVertex3f(0.0f,0.0f,-0.00001f); 
		//glColor4f(1.0f,1.0f,1.0f,0.3f);			// Full Brightness, 50% Alpha ( NEW )
	glEnd();

	glPopMatrix();

} // DrawPhoto1

void DrawPhoto2(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fRotateX, 
	float fRotateY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight )
{
	glPushMatrix();

	glTranslatef(fTransX,fTransY+3.0f,18.0f);
	glRotatef(fRotateX,1.0f,0.0f,0.0f); // rotate x
	glRotatef(fRotateY,0.0f,1.0f,0.0f); // rotate y
	glRotatef(180.0f,0.0f,0.0f,1.0f); // rotate z
	glTranslatef(fRectWidth/-2.0f,fRectHeight/-2.0f,0.0f);

	glScalef( 0.7f, 0.7f, 0.7f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );

	glBindTexture(GL_TEXTURE_2D, texture);
	glBegin( GL_QUADS );
		glTexCoord2f(fTexX, 0.0f); glVertex3f(fRectWidth,0.0f,-0.00001f); 
		glTexCoord2f(fTexX, fTexY); glVertex3f(fRectWidth,fRectHeight,-0.00001f); 
		glTexCoord2f(0.0f, fTexY); glVertex3f(0.0f,fRectHeight,-0.00001f); // Front face
		glTexCoord2f(0.0f, 0.0f); glVertex3f(0.0f,0.0f,-0.00001f); 
	glEnd();

	glPopMatrix();

} // DrawPhoto2

void DrawText1(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight )
{
	glPushMatrix();

	glTranslatef(fTransX+4.0f,fTransY-2.0f,18.0f);
	glRotatef(180.0f,0.0f,0.0f,1.0f); // rotate z
	glTranslatef(fRectWidth/-2.0f,fRectHeight/-2.0f,0.0f);

	glScalef( 0.7f, 0.7f, 0.7f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );

	glBindTexture(GL_TEXTURE_2D, texture);
	glBegin( GL_QUADS );
	glTexCoord2f(fTexX, 0.0f); glVertex3f(fRectWidth,0.0f,-0.00001f); 
	glTexCoord2f(fTexX, fTexY); glVertex3f(fRectWidth,fRectHeight,-0.00001f); 
	glTexCoord2f(0.0f, fTexY); glVertex3f(0.0f,fRectHeight,-0.00001f); // Front face
	glTexCoord2f(0.0f, 0.0f); glVertex3f(0.0f,0.0f,-0.00001f); 
	glEnd();

	glPopMatrix();

} // DrawText1

void DrawText2(
	UINT texture, 
	float fTexX, 
	float fTexY, 
	float fTransX, 
	float fTransY,
	float fRectWidth, 
	float fRectHeight )
{
	glPushMatrix();

	glTranslatef(fTransX-2.0f,fTransY-2.0f,18.0f);
	glRotatef(180.0f,0.0f,0.0f,1.0f); // rotate z
	glTranslatef(fRectWidth/-2.0f,fRectHeight/-2.0f,0.0f);

	glScalef( 0.7f, 0.7f, 0.7f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );

	glBindTexture(GL_TEXTURE_2D, texture);
	glBegin( GL_QUADS );
	glTexCoord2f(fTexX, 0.0f); glVertex3f(fRectWidth,0.0f,-0.00001f); 
	glTexCoord2f(fTexX, fTexY); glVertex3f(fRectWidth,fRectHeight,-0.00001f); 
	glTexCoord2f(0.0f, fTexY); glVertex3f(0.0f,fRectHeight,-0.00001f); // Front face
	glTexCoord2f(0.0f, 0.0f); glVertex3f(0.0f,0.0f,-0.00001f); 
	glEnd();

	glPopMatrix();

} // DrawText2

void DrawRectangle1(float fx, float fy)
{
	glPushMatrix();

	glRotatef(180.0f,0.0f,0.0f,1.0f);
	glTranslatef(fx,fy,26.0f);

	glScalef( 0.6f, 0.6f, 0.6f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );

	glBindTexture(GL_TEXTURE_2D, g_texture3);
	glBegin( GL_QUADS );
		glTexCoord2f(1.0f, 0.0f); glVertex3f(g_fRectWidth,0.0f,-0.00001f); 
		glTexCoord2f(1.0f, 1.0f); glVertex3f(g_fRectWidth,g_fRectHeight,-0.00001f); 
		glTexCoord2f(0.0f, 1.0f); glVertex3f(0.0f,g_fRectHeight,-0.00001f); // Front face
		glTexCoord2f(0.0f, 0.0f); glVertex3f(0.0f,0.0f,-0.00001f); 
	glEnd();

	glPopMatrix();

} // DrawRectangle1

void DrawRectangle2(float fx, float fy)
{
	glPushMatrix();

	glRotatef(180.0f,0.0f,0.0f,1.0f);
	glTranslatef(fx,fy,26.0f);

	glScalef( 0.6f, 0.6f, 0.6f );
	//glTranslatef( (fx+0.25f)*1.0f, (fy+0.25f)*0.75f, 0.0f );

	glBindTexture(GL_TEXTURE_2D, g_texture6);
	glBegin( GL_QUADS );
		glTexCoord2f(1.0f, 0.0f); glVertex3f(g_fRectWidth,0.0f,-0.00001f); 
		glTexCoord2f(1.0f, 1.0f); glVertex3f(g_fRectWidth,g_fRectHeight,-0.00001f); 
		glTexCoord2f(0.0f, 1.0f); glVertex3f(0.0f,g_fRectHeight,-0.00001f); // Front face
		glTexCoord2f(0.0f, 0.0f); glVertex3f(0.0f,0.0f,-0.00001f); 
	glEnd();

	glPopMatrix();

} // DrawRectangle2

bool LoadBitmaps()
{
	wchar_t FullPath[MAX_PATH];
	memset( FullPath, 0, sizeof(FullPath) );
	std::wstring szExePath;
	if (::GetModuleFileNameW( NULL, FullPath, sizeof(wchar_t)*MAX_PATH))
	{
		szExePath = FullPath;
		
		int pos = szExePath.rfind( L'\\' );
		
		if( -1 != pos )
		{
			szExePath = szExePath.substr(0,pos+1);
		}
	}

	using namespace Gdiplus;
	// Initialize GDI+
	GdiplusStartupInput gdiplusStartupInput;
	ULONG_PTR gdiplusToken;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	bool bRet = LoadOneBitmap( g_bmpInfo1, szExePath, L"bitmaps\\starquad.bmp" );
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}

	bRet = LoadOneBitmap( g_bmpInfo3, szExePath, L"bitmaps\\GreenRect.bmp");
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}

	bRet = LoadOneBitmap( g_bmpInfo4, szExePath, L"bitmaps\\invstarquad.bmp");
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}

	bRet = LoadOneBitmap( g_bmpInfo6, szExePath, L"bitmaps\\OrangeRect.bmp" );
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}


	//bRet = LoadOneTextBitmapFromFile( g_bmpTextInfo1, szExePath, _T("TextPngs\\FiguringItOutText.png" );
	//if( false == bRet )
	//{
	//	GdiplusShutdown(gdiplusToken);
	//	return false;
	//}

	//bRet = LoadOneTextBitmapFromFile( g_bmpTextInfo2, szExePath, _T("TextPngs\\ThinkingOfYouText.png" );
	//if( false == bRet )
	//{
	//	GdiplusShutdown(gdiplusToken);
	//	return false;
	//}

	//bRet = LoadOneTextBitmapFromFile( g_bmpTextInfo3, szExePath, _T("TextPngs\\SmilingAtYouText.png" );
	//if( false == bRet )
	//{
	//	GdiplusShutdown(gdiplusToken);
	//	return false;
	//}

	//bRet = LoadOneTextBitmapFromFile( g_bmpTextInfo4, szExePath, _T("TextPngs\\HappyTimesText.png" );
	//if( false == bRet )
	//{
	//	GdiplusShutdown(gdiplusToken);
	//	return false;
	//}


	Gdiplus::Bitmap* pText1=NULL;
	tagTextStruct st1;
	st1.szText = L"Figuring It Out";
	st1.pbmp = pText1;
	st1.bPurple = false;
	HANDLE hThread1 = (HANDLE)_beginthread(TextThread, 0, (void*)(&st1));
	Gdiplus::Bitmap* pText2=NULL;
	tagTextStruct st2;
	st2.szText = L"Thinking Of You";
	st2.pbmp = pText2;
	st2.bPurple = true;
	HANDLE hThread2 = (HANDLE)_beginthread(TextThread, 0, (void*)(&st2));
	Gdiplus::Bitmap* pText3=NULL;
	tagTextStruct st3;
	st3.szText = L"Smiling At You";
	st3.pbmp = pText3;
	st3.bPurple = false;
	HANDLE hThread3 = (HANDLE)_beginthread(TextThread, 0, (void*)(&st3));
	Gdiplus::Bitmap* pText4=NULL;
	tagTextStruct st4;
	st4.szText = L"Happy Times";
	st4.pbmp = pText4;
	st4.bPurple = true;
	HANDLE hThread4 = (HANDLE)_beginthread(TextThread, 0, (void*)(&st4));


	HANDLE handles[4];
	handles[0] = hThread1;
	handles[1] = hThread2;
	handles[2] = hThread3;
	handles[3] = hThread4;
	WaitForMultipleObjects(4, handles, TRUE, INFINITE);

	bRet = LoadOneTextBitmap( g_bmpTextInfo1, st1.pbmp );
	if(pText1)
	{
		delete pText1;
		pText1 = NULL;
	}

	bRet = LoadOneTextBitmap( g_bmpTextInfo2, st2.pbmp );
	if(pText2)
	{
		delete pText2;
		pText2 = NULL;
	}

	bRet = LoadOneTextBitmap( g_bmpTextInfo3, st3.pbmp );
	if(pText3)
	{
		delete pText3;
		pText3 = NULL;
	}

	bRet = LoadOneTextBitmap( g_bmpTextInfo4, st4.pbmp );
	if(pText4)
	{
		delete pText4;
		pText4 = NULL;
	}

	// Shutdown GDI+
   GdiplusShutdown(gdiplusToken);

   return true;
}

bool LoadOneBitmap( BmpInfo& bmpInfo, const std::wstring& szExePath, const std::wstring& szFile)
{
	std::wstring szJpg1 = szExePath + szFile;
	
	if( false == ValidatePath(szJpg1) )
	{
		std::wstring str = szFile;
		str += L" do not exists";
		MessageBoxW( NULL, str.c_str(), L"Error", MB_OK );
		return false;
	}

	using namespace Gdiplus;
	Bitmap m_Bmp1( szJpg1.c_str(), FALSE );

	Rect rect1(0, 0, m_Bmp1.GetWidth(), m_Bmp1.GetHeight());

	// First Bitmap
	BitmapData bitmapData;
	memset( &bitmapData, 0, sizeof(bitmapData));
	m_Bmp1.LockBits( 
		&rect1, 
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapData );

	int nStride1 = bitmapData.Stride;
	if( nStride1 < 0 )
		nStride1 = -nStride1;

	bmpInfo.pPixels1 = new UINT[bitmapData.Width * bitmapData.Height];
	bmpInfo.nWidth = bitmapData.Width;
	bmpInfo.nHeight = bitmapData.Height;

	UINT* pixels = (UINT*)bitmapData.Scan0;

	if( !pixels )
		return false;

	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		for(UINT col = 0; col < bitmapData.Width; ++col)
		{
			bmpInfo.pPixels1[row*bitmapData.Width + col] = 
				pixels[row * nStride1 / 4 + col];
		}
	}

	m_Bmp1.UnlockBits( 
		&bitmapData );

	return true;

}

bool LoadPhotos()
{
	wchar_t FullPath[MAX_PATH];
	memset( FullPath, 0, sizeof(FullPath) );
	std::wstring szExePath;
	if (::GetModuleFileNameW( NULL, FullPath, sizeof(wchar_t)*MAX_PATH))
	{
		szExePath = FullPath;
		
		int pos = szExePath.rfind( L'\\' );
		
		if( -1 != pos )
		{
			szExePath = szExePath.substr(0,pos+1);
		}
	}

	szExePath += L"Images\\";

	using namespace Gdiplus;
	// Initialize GDI+
	GdiplusStartupInput gdiplusStartupInput;
	ULONG_PTR gdiplusToken;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	bool bRet = LoadOnePhoto( g_bmpInfo7, szExePath, L"9.jpg" );
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}

	bRet = LoadOnePhoto( g_bmpInfo8, szExePath, L"10.JPG" );
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}
	
	bRet = LoadOnePhoto( g_bmpInfo9, szExePath, L"11.jpg" );
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}

	bRet = LoadOnePhoto( g_bmpInfo10, szExePath, L"3.JPG" );
	if( false == bRet )
	{
		GdiplusShutdown(gdiplusToken);
		return false;
	}

	// Shutdown GDI+
   GdiplusShutdown(gdiplusToken);

   return true;
}

bool LoadOnePhoto( BmpInfo& bmpInfo, const std::wstring& szExePath, const std::wstring& szFile)
{
	std::wstring szJpg1 = szExePath + szFile;
	
	if( false == ValidatePath(szJpg1) )
	{
		std::wstring str = szFile;
		str += L" do not exists";
		MessageBoxW( NULL, str.c_str(), L"Error", MB_OK );
		return false;
	}

	using namespace Gdiplus;
	Bitmap m_Bmp1( szJpg1.c_str(), FALSE );

	Rect rect1(0, 0, m_Bmp1.GetWidth(), m_Bmp1.GetHeight());

	// First Bitmap
	BitmapData bitmapData;
	memset( &bitmapData, 0, sizeof(bitmapData));
	m_Bmp1.LockBits( 
		&rect1, 
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapData );

	int nStride1 = bitmapData.Stride;
	if( nStride1 < 0 )
		nStride1 = -nStride1;

	const UINT MARGIN_RATIO = 4;
	UINT margin = 0;
	// calculate border's margins
	if( bitmapData.Width >= bitmapData.Height )
	{
		margin = MARGIN_RATIO * bitmapData.Width / 100;
	}
	else if( bitmapData.Width < bitmapData.Height )
	{
		margin = MARGIN_RATIO * bitmapData.Height / 100;
	}
	bitmapData.Width += margin*2; 
	bitmapData.Height += margin*2; 

	bmpInfo.pPixels1 = new UINT[bitmapData.Width * bitmapData.Height];
	bmpInfo.nWidth = bitmapData.Width;
	bmpInfo.nHeight = bitmapData.Height;

	UINT* pixels = (UINT*)bitmapData.Scan0;

	if( !pixels )
		return false;

	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		for(UINT col = 0; col < bitmapData.Width; ++col)
		{
			bmpInfo.pPixels1[row*bitmapData.Width + col] = 0xffffffff;
		}
	}

	for(UINT row = margin; row < bitmapData.Height-margin; ++row)
	{
		for(UINT col = margin; col < bitmapData.Width-margin; ++col)
		{
			bmpInfo.pPixels1[row*bitmapData.Width + col] = 
				pixels[(row-margin) * nStride1 / 4 + (col-margin)];

			// add transparency
			//bmpInfo.pPixels1[row*bitmapData.Width + col] &= 0x00ffffff;
			//bmpInfo.pPixels1[row*bitmapData.Width + col] |= 0x88000000;
		}
	}

	m_Bmp1.UnlockBits( 
		&bitmapData );

	return true;

}

bool LoadOneTextBitmapFromFile( BmpInfo& bmpInfo, const std::wstring& szExePath, const std::wstring& szFile)
{
	std::wstring szJpg1 = szExePath + szFile;

	if( false == ValidatePath(szJpg1) )
	{
		std::wstring str = szFile;
		str += L" do not exists";
		MessageBoxW( NULL, str.c_str(), L"Error", MB_OK );
		return false;
	}

	using namespace Gdiplus;
	Bitmap m_Bmp1( szJpg1.c_str(), FALSE );

	Rect rect1(0, 0, m_Bmp1.GetWidth(), m_Bmp1.GetHeight());

	// First Bitmap
	BitmapData bitmapData;
	memset( &bitmapData, 0, sizeof(bitmapData));
	m_Bmp1.LockBits( 
		&rect1, 
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapData );

	int nStride1 = bitmapData.Stride;
	if( nStride1 < 0 )
		nStride1 = -nStride1;

	bmpInfo.pPixels1 = new UINT[bitmapData.Width * bitmapData.Height];
	bmpInfo.nWidth = bitmapData.Width;
	bmpInfo.nHeight = bitmapData.Height;

	UINT* pixels = (UINT*)bitmapData.Scan0;

	if( !pixels )
		return false;

	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		for(UINT col = 0; col < bitmapData.Width; ++col)
		{
			bmpInfo.pPixels1[row*bitmapData.Width + col] = 
				pixels[row * nStride1 / 4 + col];

			// add transparency
			//bmpInfo.pPixels1[row*bitmapData.Width + col] &= 0x00ffffff;
			//bmpInfo.pPixels1[row*bitmapData.Width + col] |= 0x88000000;
		}
	}

	m_Bmp1.UnlockBits( 
		&bitmapData );

	return true;

}

bool LoadTextures()
{
	bool bRet = LoadOneTexture( g_bmpInfo1, g_texture1 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo3, g_texture3 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo4, g_texture4 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo6, g_texture6 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo7, g_texture7 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo8, g_texture8 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo9, g_texture9 );
	if( false == bRet )
		return false;

	bRet = LoadOneTexture( g_bmpInfo10, g_texture10 );
	if( false == bRet )
		return false;

	bRet = LoadOneTextTexture( g_bmpTextInfo1, g_textureText1 );
	if( false == bRet )
		return false;

	bRet = LoadOneTextTexture( g_bmpTextInfo2, g_textureText2 );
	if( false == bRet )
		return false;

	bRet = LoadOneTextTexture( g_bmpTextInfo3, g_textureText3 );
	if( false == bRet )
		return false;

	bRet = LoadOneTextTexture( g_bmpTextInfo4, g_textureText4 );
	if( false == bRet )
		return false;
	return true;
}

bool LoadOneTexture( BmpInfo& bmpInfo, UINT& texture )
{
	UINT* pixels = NULL;
	UINT nLen = 32;
	if( bmpInfo.nWidth >= bmpInfo.nHeight )
	{
		if( bmpInfo.nWidth > 1024 && bmpInfo.nWidth <= 2048 )
			nLen = 2048;
		else if( bmpInfo.nWidth > 512 && bmpInfo.nWidth <= 1024 )
			nLen = 1024;
		else if( bmpInfo.nWidth > 256 && bmpInfo.nWidth <= 512 )
			nLen = 512;
		else if( bmpInfo.nWidth > 128 && bmpInfo.nWidth <= 256 )
			nLen = 256;
		else if( bmpInfo.nWidth > 64 && bmpInfo.nWidth <= 128 )
			nLen = 128;
		else if( bmpInfo.nWidth > 32 && bmpInfo.nWidth <= 64 )
			nLen = 64;
	}
	else if( bmpInfo.nHeight > bmpInfo.nWidth )
	{
		if( bmpInfo.nHeight > 1024 && bmpInfo.nHeight <= 2048 )
			nLen = 2048;
		else if( bmpInfo.nHeight > 512 && bmpInfo.nHeight <= 1024 )
			nLen = 1024;
		else if( bmpInfo.nHeight > 256 && bmpInfo.nHeight <= 512 )
			nLen = 512;
		else if( bmpInfo.nHeight > 128 && bmpInfo.nHeight <= 256 )
			nLen = 256;
		else if( bmpInfo.nHeight > 64 && bmpInfo.nHeight <= 128 )
			nLen = 128;
		else if( bmpInfo.nHeight > 32 && bmpInfo.nHeight <= 64 )
			nLen = 64;
	}

	{
	pixels = new UINT[nLen * nLen];
	if( 0 == pixels )
		return false;

	bmpInfo.nTexLen = nLen;

	for(UINT row = 0; row < nLen; ++row)
	{
		for(UINT col = 0; col < nLen; ++col)
		{
			pixels[row * nLen + col] = 0xffffffff; // put all as white!
		}
	}

	for(UINT row = 0; row < nLen; ++row)
	{
		for(UINT col = 0; col < nLen; ++col)
		{
			if( row < bmpInfo.nHeight && col < bmpInfo.nWidth )
			{
				UINT pixel = bmpInfo.pPixels1[row*bmpInfo.nWidth + col];

				BYTE nAlpha   = (pixel & 0xff000000) >> 24; 
				BYTE nRed   = (pixel & 0xff0000) >> 16; 
				BYTE nGreen = (pixel & 0xff00) >> 8; 
				BYTE nBlue  = (pixel & 0xff); 

				//pixel = 0xff000000 | nBlue << 16 | nGreen << 8 | nRed;
				pixel = nAlpha << 24 | nBlue << 16 | nGreen << 8 | nRed;

				pixels[row * nLen + col] = pixel;
			}
		}
	}

	delete [] bmpInfo.pPixels1;

	bmpInfo.pPixels1 = pixels;

	glGenTextures(1, &texture);					// Create The Texture
	glBindTexture(GL_TEXTURE_2D, texture);

	glTexImage2D(
		GL_TEXTURE_2D, 
		0, 
		GL_RGBA, 
		bmpInfo.nTexLen, 
		bmpInfo.nTexLen, 
		0, 
		GL_RGBA, 
		GL_UNSIGNED_BYTE, 
		bmpInfo.pPixels1 );

	glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MIN_FILTER,GL_LINEAR);	// Linear Filtering
	glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MAG_FILTER,GL_LINEAR);	// Linear Filtering

	delete [] bmpInfo.pPixels1;
	bmpInfo.pPixels1 = NULL;
	}
	return true;
}

bool LoadOneTextTexture( BmpInfo& bmpInfo, UINT& texture )
{
	UINT* pixels = NULL;
	UINT nLen = 32;
	if( bmpInfo.nWidth >= bmpInfo.nHeight )
	{
		if( bmpInfo.nWidth > 1024 && bmpInfo.nWidth <= 2048 )
			nLen = 2048;
		else if( bmpInfo.nWidth > 512 && bmpInfo.nWidth <= 1024 )
			nLen = 1024;
		else if( bmpInfo.nWidth > 256 && bmpInfo.nWidth <= 512 )
			nLen = 512;
		else if( bmpInfo.nWidth > 128 && bmpInfo.nWidth <= 256 )
			nLen = 256;
		else if( bmpInfo.nWidth > 64 && bmpInfo.nWidth <= 128 )
			nLen = 128;
		else if( bmpInfo.nWidth > 32 && bmpInfo.nWidth <= 64 )
			nLen = 64;
	}
	else if( bmpInfo.nHeight > bmpInfo.nWidth )
	{
		if( bmpInfo.nHeight > 1024 && bmpInfo.nHeight <= 2048 )
			nLen = 2048;
		else if( bmpInfo.nHeight > 512 && bmpInfo.nHeight <= 1024 )
			nLen = 1024;
		else if( bmpInfo.nHeight > 256 && bmpInfo.nHeight <= 512 )
			nLen = 512;
		else if( bmpInfo.nHeight > 128 && bmpInfo.nHeight <= 256 )
			nLen = 256;
		else if( bmpInfo.nHeight > 64 && bmpInfo.nHeight <= 128 )
			nLen = 128;
		else if( bmpInfo.nHeight > 32 && bmpInfo.nHeight <= 64 )
			nLen = 64;
	}

	{
		pixels = new UINT[nLen * nLen];
		if( 0 == pixels )
			return false;

		bmpInfo.nTexLen = nLen;

		for(UINT row = 0; row < nLen; ++row)
		{
			for(UINT col = 0; col < nLen; ++col)
			{
				pixels[row * nLen + col] = 0x00ffffff; // put all as transparent white!
			}
		}

		for(UINT row = 0; row < nLen; ++row)
		{
			for(UINT col = 0; col < nLen; ++col)
			{
				if( row < bmpInfo.nHeight && col < bmpInfo.nWidth )
				{
					UINT pixel = bmpInfo.pPixels1[row*bmpInfo.nWidth + col];

					BYTE nAlpha   = (pixel & 0xff000000) >> 24; 
					BYTE nRed   = (pixel & 0xff0000) >> 16; 
					BYTE nGreen = (pixel & 0xff00) >> 8; 
					BYTE nBlue  = (pixel & 0xff); 

					//pixel = 0xff000000 | nBlue << 16 | nGreen << 8 | nRed;
					pixel = nAlpha << 24 | nBlue << 16 | nGreen << 8 | nRed;

					pixels[row * nLen + col] = pixel;
				}
			}
		}

		delete [] bmpInfo.pPixels1;

		bmpInfo.pPixels1 = pixels;

		glGenTextures(1, &texture);					// Create The Texture
		glBindTexture(GL_TEXTURE_2D, texture);

		glTexImage2D(
			GL_TEXTURE_2D, 
			0, 
			GL_RGBA, 
			bmpInfo.nTexLen, 
			bmpInfo.nTexLen, 
			0, 
			GL_RGBA, 
			GL_UNSIGNED_BYTE, 
			bmpInfo.pPixels1 );

		glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MIN_FILTER,GL_LINEAR);	// Linear Filtering
		glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MAG_FILTER,GL_LINEAR);	// Linear Filtering

		delete [] bmpInfo.pPixels1;
		bmpInfo.pPixels1 = NULL;
	}
	return true;
}

void CleanUp()
{
	if( g_bmpInfo1.pPixels1 )
	{
		delete [] g_bmpInfo1.pPixels1;
		g_bmpInfo1.pPixels1 = 0;
	}
	if( g_bmpInfo3.pPixels1 )
	{
		delete [] g_bmpInfo3.pPixels1;
		g_bmpInfo3.pPixels1 = 0;
	}
	if( g_bmpInfo4.pPixels1 )
	{
		delete [] g_bmpInfo4.pPixels1;
		g_bmpInfo4.pPixels1 = 0;
	}
	if( g_bmpInfo6.pPixels1 )
	{
		delete [] g_bmpInfo6.pPixels1;
		g_bmpInfo6.pPixels1 = 0;
	}
	if( g_bmpInfo7.pPixels1 )
	{
		delete [] g_bmpInfo7.pPixels1;
		g_bmpInfo7.pPixels1 = 0;
	}
	if( g_bmpInfo8.pPixels1 )
	{
		delete [] g_bmpInfo8.pPixels1;
		g_bmpInfo8.pPixels1 = 0;
	}
	if( g_bmpInfo9.pPixels1 )
	{
		delete [] g_bmpInfo9.pPixels1;
		g_bmpInfo9.pPixels1 = 0;
	}
	if( g_bmpInfo10.pPixels1 )
	{
		delete [] g_bmpInfo10.pPixels1;
		g_bmpInfo10.pPixels1 = 0;
	}
	if( g_bmpTextInfo1.pPixels1 )
	{
		delete [] g_bmpTextInfo1.pPixels1;
		g_bmpTextInfo1.pPixels1 = 0;
	}
	if( g_bmpTextInfo2.pPixels1 )
	{
		delete [] g_bmpTextInfo2.pPixels1;
		g_bmpTextInfo2.pPixels1 = 0;
	}
	if( g_bmpTextInfo3.pPixels1 )
	{
		delete [] g_bmpTextInfo3.pPixels1;
		g_bmpTextInfo3.pPixels1 = 0;
	}
	if( g_bmpTextInfo4.pPixels1 )
	{
		delete [] g_bmpTextInfo4.pPixels1;
		g_bmpTextInfo4.pPixels1 = 0;
	}
}

bool ValidatePath( const std::wstring& szPath )
{
	bool bValid = true;
	
	DWORD dwResult = ::GetFileAttributesW( szPath.c_str() );

	if( INVALID_FILE_ATTRIBUTES == dwResult )
	{
		bValid = false;
	}

	return bValid;
}

using namespace Gdiplus;
int GetEncoderClsid(const WCHAR* format, CLSID* pClsid)
{
	
	UINT  num = 0;          // number of image encoders
	UINT  size = 0;         // size of the image encoder array in bytes

   ImageCodecInfo* pImageCodecInfo = NULL;

   GetImageEncodersSize(&num, &size);
   if(size == 0)
      return -1;  // Failure

   pImageCodecInfo = (ImageCodecInfo*)(malloc(size));
   if(pImageCodecInfo == NULL)
      return -1;  // Failure

   GetImageEncoders(num, size, pImageCodecInfo);

   for(UINT j = 0; j < num; ++j)
   {
      if( wcscmp(pImageCodecInfo[j].MimeType, format) == 0 )
      {
         *pClsid = pImageCodecInfo[j].Clsid;
         free(pImageCodecInfo);
         return j;  // Success
      }    
   }

   free(pImageCodecInfo);
   return -1;  // Failure
}

bool CaptureScreenShot(
	int nWidth, 
	int nHeight, 
	const std::wstring& szDestFile,
	const std::wstring& szEncoderString)
{
	UINT *pixels=new UINT[nWidth * nHeight];
	memset(pixels, 0, sizeof(UINT)*nWidth*nHeight);

	glFlush(); glFinish();

	glReadPixels(0,0,nWidth,nHeight,GL_BGRA_EXT,GL_UNSIGNED_BYTE,pixels);

	if(NULL==pixels)
		return false;

	// Initialize GDI+
	GdiplusStartupInput gdiplusStartupInput;
	ULONG_PTR gdiplusToken;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	{
	// Create the dest image
	Bitmap DestBmp(nWidth,nHeight,PixelFormat32bppARGB);

	Rect rect1(0, 0, nWidth, nHeight);

	BitmapData bitmapData;
	memset( &bitmapData, 0, sizeof(bitmapData));
	DestBmp.LockBits( 
		&rect1, 
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapData );

	int nStride1 = bitmapData.Stride;
	if( nStride1 < 0 )
		nStride1 = -nStride1;

	UINT* DestPixels = (UINT*)bitmapData.Scan0;

	if( !DestPixels )
	{
		delete [] pixels;
		return false;
	}

	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		for(UINT col = 0; col < bitmapData.Width; ++col)
		{
			 DestPixels[row * nStride1 / 4 + col] = pixels[row * nWidth + col];
		}
	}

	DestBmp.UnlockBits( 
		&bitmapData );

	delete [] pixels;
	pixels = NULL;

	DestBmp.RotateFlip( RotateNoneFlipY );

	CLSID Clsid;
	int result = GetEncoderClsid(szEncoderString.c_str(), &Clsid);

	if( result < 0 )
		return false;

	Status status = DestBmp.Save( szDestFile.c_str(), &Clsid );
	}
	// Shutdown GDI+
	GdiplusShutdown(gdiplusToken);

	return true;
}

//init VSync func
void InitVSync()
{
	//get extensions of graphics card
	char* extensions = (char*)glGetString(GL_EXTENSIONS);

	//is WGL_EXT_swap_control in the string? VSync switch possible?
	if (strstr(extensions,"WGL_EXT_swap_control"))
	{
		//get address's of both functions and save them
		wglSwapIntervalEXT = (PFNWGLEXTSWAPCONTROLPROC)
			wglGetProcAddress("wglSwapIntervalEXT");
		wglGetSwapIntervalEXT = (PFNWGLEXTGETSWAPINTERVALPROC)
			wglGetProcAddress("wglGetSwapIntervalEXT");
	}
}

bool IsVSyncEnabled()
{
	//if interval is positif, it is not 0 so enabled ;)
	if(wglGetSwapIntervalEXT)
		return (wglGetSwapIntervalEXT() > 0);

	return false;
}

void SetVSyncState(bool enable)
{
	if (enable)
	{
		if(wglSwapIntervalEXT)
			wglSwapIntervalEXT(1); //set interval to 1 -&gt; enable
	}
	else
	{
		if(wglSwapIntervalEXT)
			wglSwapIntervalEXT(0); //disable
	}
}

bool GenerateTextBitmap(
	Gdiplus::Graphics* pGraphics,
	const std::wstring& szText, 
	Gdiplus::Bitmap** pbmp, 
	bool bPurple)
{    
	using namespace Gdiplus;

	if(!pGraphics)
		return false;

	FontFamily fontFamily(L"Arial");
	StringFormat strformat;

	PngOutlineText text;
	if(bPurple)
		text.TextOutline(Color(177,100,255),Color(128,0,255),8);
	else
		text.TextOutline(Color(125,190,255),Color(0,121,242),8); // blue outline

	text.EnableShadow(false);
	float fWidth=0.0f;
	float fHeight=0.0f;
	text.MeasureString(pGraphics,&fontFamily,FontStyleBold, 
		72, szText.c_str(), Gdiplus::Point(0,0), &strformat,
		NULL, NULL, &fWidth, &fHeight);
	*pbmp = new Bitmap(fWidth+8.0f, fHeight+8.0f, PixelFormat32bppARGB);

	if(!*pbmp)
		return false;

	text.SetPngImage(*pbmp);
	text.DrawString(pGraphics,&fontFamily,FontStyleBold, 
		72, szText.c_str(), Gdiplus::Point(0,0), &strformat);

	return true;
}

void TextThread(void* p)
{
	tagTextStruct* st = (tagTextStruct*)(p);
	GenerateTextBitmapThread(st->szText, &(st->pbmp), st->bPurple);
}

bool GenerateTextBitmapThread(
	const std::wstring& szText, 
	Gdiplus::Bitmap** pbmp, 
	bool bPurple)
{    
	using namespace Gdiplus;
	Gdiplus::Bitmap* pGraphbmp = new Gdiplus::Bitmap(600,600,PixelFormat32bppARGB);
	Graphics* pGraphics = new Graphics(pGraphbmp);
	if( !pGraphics )
	{
		if(pGraphbmp) delete pGraphbmp;

		return false;
	}

	pGraphics->SetSmoothingMode(SmoothingModeAntiAlias);
	pGraphics->SetInterpolationMode(InterpolationModeHighQualityBicubic);
	pGraphics->SetPageUnit(UnitPixel);

	if(!pGraphics)
		return false;

	FontFamily fontFamily(L"Arial");
	StringFormat strformat;

	PngOutlineText text;
	if(bPurple)
		text.TextOutline(Color(177,100,255),Color(128,0,255),8);
	else
		text.TextOutline(Color(125,190,255),Color(0,121,242),8); // blue outline

	text.EnableShadow(false);
	float fWidth=0.0f;
	float fHeight=0.0f;
	text.MeasureString(pGraphics,&fontFamily,FontStyleBold, 
		72, szText.c_str(), Gdiplus::Point(0,0), &strformat,
		NULL, NULL, &fWidth, &fHeight);
	*pbmp = new Bitmap(fWidth+8.0f, fHeight+8.0f, PixelFormat32bppARGB);

	if(!*pbmp)
		return false;

	text.SetPngImage(*pbmp);
	text.DrawString(pGraphics,&fontFamily,FontStyleBold, 
		72, szText.c_str(), Gdiplus::Point(0,0), &strformat);

	if(pGraphics)
	{
		delete pGraphics;
		pGraphics = NULL;
	}
	if(pGraphbmp)
	{
		delete pGraphbmp;
		pGraphbmp = NULL;
	}

	return true;
}

bool LoadOneTextBitmap( BmpInfo& bmpInfo, Gdiplus::Bitmap* pbmp)
{
	if(!pbmp)
		return false;
	
	Rect rect1(0, 0, pbmp->GetWidth(), pbmp->GetHeight());

	// First Bitmap
	BitmapData bitmapData;
	memset( &bitmapData, 0, sizeof(bitmapData));
	pbmp->LockBits( 
		&rect1, 
		ImageLockModeRead,
		PixelFormat32bppARGB,
		&bitmapData );

	int nStride1 = bitmapData.Stride;
	if( nStride1 < 0 )
		nStride1 = -nStride1;

	bmpInfo.pPixels1 = new UINT[bitmapData.Width * bitmapData.Height];
	bmpInfo.nWidth = bitmapData.Width;
	bmpInfo.nHeight = bitmapData.Height;

	UINT* pixels = (UINT*)bitmapData.Scan0;

	if( !pixels )
		return false;

	for(UINT row = 0; row < bitmapData.Height; ++row)
	{
		for(UINT col = 0; col < bitmapData.Width; ++col)
		{
			bmpInfo.pPixels1[row*bitmapData.Width + col] = 
				pixels[row * nStride1 / 4 + col];

			// add transparency
			//bmpInfo.pPixels1[row*bitmapData.Width + col] &= 0x00ffffff;
			//bmpInfo.pPixels1[row*bitmapData.Width + col] |= 0x88000000;
		}
	}

	pbmp->UnlockBits( 
		&bitmapData );

	return true;

}
