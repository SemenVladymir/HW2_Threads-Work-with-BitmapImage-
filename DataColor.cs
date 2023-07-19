using System.Drawing;

namespace HW2_Threads__Work_with_BitmapImage_
{

    public class DataColor
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Color PosColor { get; set; }
        public LAB PosLAB { get; set; }
        public string BelongingToMainColor { get; set; }


        public DataColor(Color col, int x, int y)
        {
            PosX = x;
            PosY = y;
            PosColor = col;
            PosLAB = new LAB(new XYZ(col));
        }
        public DataColor(string colorName) 
        {
            BelongingToMainColor = colorName;
        }


        public override string ToString()
        {
            return $"Belonging to {BelongingToMainColor}\nPixel position: X-{PosX}, Y-{PosY}\nRGB color components: R-{PosColor.R}, G-{PosColor.G}, B-{PosColor.B};\nCIELAB color components: L-{PosLAB.L}, A-{PosLAB.a}, B-{PosLAB.b}";
        }

    }
}
