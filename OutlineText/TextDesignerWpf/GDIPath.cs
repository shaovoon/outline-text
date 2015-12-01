using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.IO;

namespace TextDesignerWpf
{
    public class GDIPath
    {
        static public Geometry CreateTextGeometry(string text, FontFamily font, FontStyle fontStyle, FontWeight fontWeight, double fontSize, Point point, CultureInfo ci)
        {
            FormattedText formattedText = new FormattedText(
                text,
                ci,
                FlowDirection.LeftToRight,
                new Typeface(
                    font,
                    fontStyle,
                    fontWeight,
                    FontStretches.Normal),
                fontSize,
                System.Windows.Media.Brushes.Black // This brush does not matter since we use the geometry of the text. 
                );

            // Build the geometry object that represents the text.
            Geometry textGeometry = formattedText.BuildGeometry(point);

            return textGeometry;
        }



    }
}
