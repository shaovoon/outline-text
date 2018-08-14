using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextDesignerCSLibrary;
using System.Drawing.Text;

namespace Fake3D2WinForm
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

		    // Text context to store string and font info to be sent as parameter to Canvas methods
		    TextContext context = new TextContext();

		    context.fontStyle = FontStyle.Regular;
		    context.nfontSize = 40;

		    context.ptDraw = new Point(0, 5);

            string szFontFile = "..\\..\\..\\CommonFonts\\Airbus Special.TTF";

            PrivateFontCollection fontcollection = new PrivateFontCollection();

            fontcollection.AddFontFile(szFontFile);
            if (fontcollection.Families.Count() > 0)
                context.fontFamily = fontcollection.Families[0];
        
            string text = "PENTHOUSE";
		    int x_offset = 0;

		    for(int i=0; i<text.Length; ++i)
		    {
			    string str = "";
			    str += text[i];
			    context.pszText = str;

                Matrix mat = new Matrix();
                mat.Rotate(-10.0f, MatrixOrder.Append);
                mat.Scale(0.75f, 1.0f, MatrixOrder.Append);
                DrawChar(x_offset, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), context, e.Graphics, mat);

			    if(i==2)
				    x_offset += 42;
			    else if(i>=4&&i<=6)
				    x_offset += 42;
			    else if(i==7)
				    x_offset += 37;
			    else
				    x_offset += 39;

                x_offset -= 8;
		    }
	    }

		void DrawChar( int x_offset, Rectangle rect, TextContext context, Graphics graphics, Matrix mat )
		{
            Bitmap canvas = CanvasHelper.GenImage(ClientSize.Width, ClientSize.Height, Color.White, 0);
            
            // Create the outline strategy which is going to shift blit diagonally
			var strategyOutline = CanvasHelper.TextOutline(MaskColor.Blue, MaskColor.Blue, 4);

			// the single mask outline
			Bitmap maskOutline = CanvasHelper.GenMask(strategyOutline, rect.Width, rect.Height, new Point(0,0), context, mat);
			// the mask to store all the single mask blitted diagonally
			Bitmap maskOutlineAll = CanvasHelper.GenImage(rect.Width+10, rect.Height+10);

			Graphics graphMaskAll = Graphics.FromImage(maskOutlineAll);

			// blit diagonally
			for(int i=0; i<8; ++i)
				graphMaskAll.DrawImage(maskOutline, -i, -i, rect.Width, rect.Height);

			// Measure the dimension of the big mask in order to generate the correct sized gradient image
			//=============================================================================================
			UInt32 top = 0;
			UInt32 bottom = 0;
			UInt32 left = 0;
			UInt32 right = 0;
			CanvasHelper.MeasureMaskLength(maskOutlineAll, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
			right += 2;
			bottom += 2;

			// Generate the gradient image for the diagonal outline
			//=======================================================
			Bitmap gradImage = CanvasHelper.GenImage((int)(right-left), (int)(bottom-top));

			List<Color> listColors = new List<Color>();
			listColors.Add(Color.Purple);
			listColors.Add(Color.MediumPurple);
			DrawGradient.Draw(gradImage, listColors, false);

			// Because Canvas::ApplyImageToMask requires all image to have same dimensions,
			// we have to blit our small gradient image onto a temp image as big as the canvas
			//===================================================================================
			Bitmap gradBlitted = CanvasHelper.GenImage(rect.Width, rect.Height);

			Graphics graphgradBlitted = Graphics.FromImage(gradBlitted);

			graphgradBlitted.DrawImage(gradImage, (int)left, (int)top, (int)(gradImage.Width), (int)(gradImage.Height));

			CanvasHelper.ApplyImageToMask(gradBlitted, maskOutlineAll, canvas, MaskColor.Blue, false);

			// Create strategy and mask image for the text body
			//===================================================
			var strategyText = CanvasHelper.TextNoOutline(MaskColor.Blue);
			Bitmap maskText = CanvasHelper.GenMask(strategyText, rect.Width, rect.Height, new Point(0,0), context, mat);

			// Measure the dimension required for text body using the mask
			//=============================================================
			top = 0;
			bottom = 0;
			left = 0;
			right = 0;
			CanvasHelper.MeasureMaskLength(maskText, MaskColor.Blue, ref top, ref left, ref bottom, ref right);
			top -= 2;
			left -= 2;

			right += 2;
			bottom += 2;

			// Create the gradient brush for the text body
			LinearGradientBrush gradTextbrush = new LinearGradientBrush(new Rectangle((int)left, (int)top, (int)right, (int)bottom), Color.DeepPink, Color.LightPink, 90.0f);

			// Create the actual strategy for the text body used for rendering, with the gradient brush
			var strategyText2 = CanvasHelper.TextNoOutline(gradTextbrush);

			// Draw the newly created strategy onto the canvas
			CanvasHelper.DrawTextImage(strategyText2, canvas, new Point(0,0), context, mat);

			// Finally blit the rendered canvas onto the window
			graphics.DrawImage(canvas, x_offset, 0, rect.Width, rect.Height);

			gradImage.Dispose();
			gradBlitted.Dispose();

			maskText.Dispose();

			strategyText.Dispose();
			strategyText2.Dispose();

			maskOutline.Dispose();
			maskOutlineAll.Dispose();

			strategyOutline.Dispose();

            canvas.Dispose();
		}
	}
}
