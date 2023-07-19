using System.Drawing;

namespace HW2_Threads__Work_with_BitmapImage_
{
    public class XYZ
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public XYZ(Color col)
        { 
            double r = col.R * 1.0 / 255;
            double g = col.G * 1.0 / 255;
            double b = col.B * 1.0 / 255;

            if (r > 0.04045) r = Math.Pow((r + 0.055) / 1.055, 2.4);
            else r /= 12.92;
            if (g > 0.04045) g = Math.Pow((g + 0.055) / 1.055, 2.4);
            else g /= 12.92;
            if (b > 0.04045) b = Math.Pow((b + 0.055) / 1.055, 2.4);
            else b /= 12.92;

            r *= 100;
            g *= 100;
            b *= 100;

            X = (float)(r * 0.4124 + g * 0.3576 + b * 0.1805);
            Y = (float)(r * 0.2126 + g * 0.7152 + b * 0.0722);
            Z = (float)(r * 0.0193 + g * 0.1192 + b * 0.9505);
        }
    }
}
