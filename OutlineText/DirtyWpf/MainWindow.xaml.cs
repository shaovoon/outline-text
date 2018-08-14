using System;
using System.Collections.Generic;
using System.Linq;
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
using TextDesignerWpf;

namespace DirtyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Create dirty text effect
        public MainWindow()
        {
            InitializeComponent();

            // Load the dirty image from file
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("..\\..\\..\\CommonImages\\dirty-texture.png", UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();

            WriteableBitmap canvasDirty = new WriteableBitmap(src);

            // Text context to store string and font info to be sent as parameter to Canvas methods
            TextContext context = new TextContext();

            FontFamily fontFamily = new FontFamily("Arial Black");
            context.fontFamily = fontFamily;
            context.fontWeight = FontWeights.Normal;
            context.fontStyle = FontStyles.Normal;
            context.nfontSize = 45;

            context.pszText = "DIRTY";
            context.ptDraw = new Point(5, 90);

            // Load the texture image from file
            BitmapImage src2 = new BitmapImage();
            src2.BeginInit();
            src2.UriSource = new Uri("..\\..\\..\\CommonImages\\texture_blue.jpg", UriKind.Relative);
            src2.CacheOption = BitmapCacheOption.OnLoad;
            src2.EndInit();

            WriteableBitmap texture = new WriteableBitmap(src2);

            WriteableBitmap mask2 = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            // Draw the texture against the red dirty mask onto the 2nd texture
            TextDesignerWpf.CanvasHelper.ApplyImageToMask(texture, canvasDirty, mask2, MaskColor.Red, false);

            WriteableBitmap canvas = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            WriteableBitmap maskShadow = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            // Draw the gray color against the red dirty mask onto the shadow texture
            TextDesignerWpf.CanvasHelper.ApplyColorToMask(Color.FromArgb(0xaa, 0xcc, 0xcc, 0xcc), canvasDirty, maskShadow, MaskColor.Red, new Point(3, 3));

            WriteableBitmap texture2 = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width), (int)(image1.Height));
            TextDesignerWpf.CanvasHelper.ApplyImageToMask(texture, mask2, texture2, MaskColor.Red, false);
            byte[] pixels = new byte[texture2.PixelHeight * texture2.PixelWidth * texture2.Format.BitsPerPixel / 8];
            texture2.CopyPixels(new Int32Rect((int)5, (int)80, (int)(image1.Width - 5), (int)(image1.Height - 80)), pixels, texture2.BackBufferStride, 0);
            WriteableBitmap texture2Cropped = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width-5), (int)(image1.Height-80));
            texture2Cropped.WritePixels(new Int32Rect((int)0, (int)0, (int)(image1.Width - 5), (int)(image1.Height - 80)), pixels, texture2.BackBufferStride, 0);

            // Create image brush for the texture
            //=======================================
            ImageBrush textureBrush = new ImageBrush();
            textureBrush.ImageSource = texture2Cropped;
            textureBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            textureBrush.Stretch = Stretch.None;
            textureBrush.TileMode = TileMode.None;
            textureBrush.AlignmentX = AlignmentX.Left;
            textureBrush.AlignmentY = AlignmentY.Top;
            textureBrush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;

            byte[] pixelsShadow = new byte[maskShadow.PixelHeight * maskShadow.PixelWidth * maskShadow.Format.BitsPerPixel / 8];
            maskShadow.CopyPixels(new Int32Rect((int)5+3, (int)80+3, (int)(image1.Width - 5-3), (int)(image1.Height - 80-3)), pixelsShadow, maskShadow.BackBufferStride, 0);
            WriteableBitmap textureShadowCropped = TextDesignerWpf.CanvasHelper.GenImage((int)(image1.Width - 5-3), (int)(image1.Height - 80-3));
            textureShadowCropped.WritePixels(new Int32Rect((int)0, (int)0, (int)(image1.Width - 5-3), (int)(image1.Height - 80-3)), pixelsShadow, maskShadow.BackBufferStride, 0);

            // Create image brush for the shadow
            //=======================================
            ImageBrush shadowBrush = new ImageBrush();
            shadowBrush.ImageSource = textureShadowCropped;
            shadowBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            shadowBrush.Stretch = Stretch.None;
            shadowBrush.TileMode = TileMode.None;
            shadowBrush.AlignmentX = AlignmentX.Left;
            shadowBrush.AlignmentY = AlignmentY.Top;
            shadowBrush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;

            // Create strategy for the shadow with the shadow brush
            var strategyShadow = TextDesignerWpf.CanvasHelper.TextNoOutline(shadowBrush);

            // Draw the shadow image shifted -3, -3
            TextDesignerWpf.CanvasHelper.DrawTextImage(strategyShadow, ref canvas, new Point(3, 3), context);

            // Create strategy for the text body
            var strategy = TextDesignerWpf.CanvasHelper.TextNoOutline(textureBrush);

            TextDesignerWpf.CanvasHelper.DrawTextImage(strategy, ref canvas, new Point(0, 0), context);

            // Then draw the rendered image onto window by assigning it to the image control
            image1.Source = canvas;

        }
    }
}
