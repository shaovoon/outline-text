using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextDesignerCSLibrary
{
    public class DrawGradient
    {
        public static bool Draw(Bitmap bmp, List<Color> colors, bool bHorizontal)
        {
	        if(colors.Count==0)
		        return false;

	        if(colors.Count==1)
	        {
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    using (SolidBrush brush = new SolidBrush(colors[0]))
                    {
                        graph.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                    }
                }
	        }
            else if (bHorizontal)
            {
                using (Graphics graph = Graphics.FromImage(bmp))
                {

                    int gradRectNum = colors.Count - 1;
                    int gradWidth = bmp.Width / gradRectNum;
                    int remainder = bmp.Width % gradRectNum;

                    int TotalWidthRendered = 0;
                    int WidthToBeRendered = 0;

                    for (int i = 0; i < gradRectNum; ++i)
                    {
                        int addRemainder = 0;
                        if (i < remainder)
                            addRemainder = 1;
                        WidthToBeRendered = gradWidth + addRemainder;
                        Rectangle rect = new Rectangle(TotalWidthRendered - 1, 0, WidthToBeRendered + 1, bmp.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, colors[i], colors[i + 1], LinearGradientMode.Horizontal))
                        {
                            graph.FillRectangle(brush, TotalWidthRendered, 0, WidthToBeRendered, bmp.Height);
                        }
                        TotalWidthRendered += WidthToBeRendered;
                    }

                }

            }
            else
            {
                using (Graphics graph = Graphics.FromImage(bmp))
                {

                    int gradRectNum = colors.Count - 1;
                    int gradHeight = bmp.Height / gradRectNum;
                    int remainder = bmp.Height % gradRectNum;

                    int TotalHeightRendered = 0;
                    int HeightToBeRendered = 0;
                    for (int i = 0; i < gradRectNum; ++i)
                    {
                        int addRemainder = 0;
                        if (i < remainder)
                            addRemainder = 1;
                        HeightToBeRendered = gradHeight + addRemainder;
                        Rectangle rect = new Rectangle(0, TotalHeightRendered - 1, bmp.Width, HeightToBeRendered + 1);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, colors[i], colors[i + 1], LinearGradientMode.Vertical))
                        {
                            graph.FillRectangle(brush, 0, TotalHeightRendered, bmp.Width, HeightToBeRendered);
                        }
                        TotalHeightRendered += HeightToBeRendered;
                    }

                }

            }
            return true;
        }
    }
}