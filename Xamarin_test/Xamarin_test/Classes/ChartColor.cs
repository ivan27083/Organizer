using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_test.Classes
{
    public class ChartColor
    {
        SKColor startcolor;
        SKColor endcolor;
        public SKColor color;

        public ChartColor(double percent)
        {
            startcolor = new SKColor(166,4,0);
            endcolor = new SKColor(255, 192, 64);
            int r1 = startcolor.Red;
            int r2 = endcolor.Red;
            int g1 = startcolor.Green;
            int g2 = endcolor.Green;
            int b1 = startcolor.Blue;
            int b2 = endcolor.Blue;
            byte r = (byte)Math.Ceiling(r1 + (r2-r1)/100 * percent);
            byte g = (byte)Math.Ceiling(g1 + (g2 - g1) / 100 * percent);
            byte b = (byte)Math.Ceiling(b1 + (b2 - b1) / 100 * percent);
            color = new SKColor(r, g, b);
        }

        public string ToString(double p1)
        {
            string s = "#" + Convert.ToString(color.Red, 16) + Convert.ToString(color.Green, 16) + Convert.ToString(color.Blue, 16);
            return s;
        }
    }
}
