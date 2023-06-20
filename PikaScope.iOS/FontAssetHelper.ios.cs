using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using PikaScope.iOS;
using SkiaSharp;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FontAssetHelper))]
namespace PikaScope.iOS
{
    public class FontAssetHelper : IFontAssetHelper
    {
        public SKTypeface GetSkiaTypefaceFromAssetFont(string fontName)
        {
            string fontFile = Path.Combine(NSBundle.MainBundle.BundlePath, fontName);
            SkiaSharp.SKTypeface typeFace;

            using (var asset = File.OpenRead(fontFile))
            {
                var fontStream = new MemoryStream();
                asset.CopyTo(fontStream);
                fontStream.Flush();
                fontStream.Position = 0;
                typeFace = SKTypeface.FromStream(fontStream);
            }

            return typeFace;
        }
    }
}