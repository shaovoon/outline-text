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

namespace TextDesignerWpf
{
    /// <summary>
    /// Simple helper class to use mask color
    /// </summary>
    public class MaskColor
    {
        /// <summary>
        /// Method to return a primary red color to be used as mask
        /// </summary>
        public static System.Windows.Media.Color Red
        {
            get
            {
                return System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0, 0);
            }
        }
        /// <summary>
        /// Method to return a primary green color to be used as mask
        /// </summary>
        public static System.Windows.Media.Color Green
        {
            get
            {
                return System.Windows.Media.Color.FromArgb(0xFF, 0, 0xFF, 0);
            }
        }
        /// <summary>
        /// Method to return a primary blue color to be used as mask
        /// </summary>
        public static System.Windows.Media.Color Blue
        {
            get
            {
                return System.Windows.Media.Color.FromArgb(0xFF, 0, 0, 0xFF);
            }
        }
        /// <summary>
        /// Method to compare 2 GDI+ color
        /// </summary>
        /// <param name="clr1">is 1st color</param>
        /// <param name="clr2">is 2nd color</param>
        /// <returns>true if equal</returns>
        public static bool IsEqual(System.Windows.Media.Color clr1, System.Windows.Media.Color clr2)
        {
            if (clr1.R == clr2.R && clr1.G == clr2.G && clr1.B == clr2.B)
                return true;

            return false;
        }
    }
}
