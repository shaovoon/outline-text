using System;
using System.Windows;

namespace TextDesignerWpf
{
    public interface ITextStrategy : IDisposable
    {
       	ITextStrategy Clone();

        bool DrawString(
            System.Windows.Media.DrawingContext graphics,
            System.Windows.Media.FontFamily fontFamily,
            System.Windows.FontStyle fontStyle,
            System.Windows.FontWeight fontWeight,
            double fontSize,
            string strText,
            System.Windows.Point ptDraw,
            System.Globalization.CultureInfo ci);

        Rect MeasureString(
            System.Windows.Media.DrawingContext graphics,
            System.Windows.Media.FontFamily fontFamily,
            System.Windows.FontStyle fontStyle,
            System.Windows.FontWeight fontWeight,
            double fontSize,
            string pszText,
            System.Windows.Point ptDraw,
            System.Globalization.CultureInfo ci,
            ref double fStartX,
            ref double fStartY,
            ref double fDestWidth,
            ref double fDestHeight);

    }
}
