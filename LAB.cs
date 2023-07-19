namespace HW2_Threads__Work_with_BitmapImage_
{
    public class LAB
    {
        public float L { get; set; }
        public float a { get; set; }
        public float b { get; set; }

        public LAB(XYZ xyz)
        {
            double x = xyz.X / 94.811;
            double y = xyz.Y / 100;
            double z = xyz.Z / 107.304;

            if (x > 0.008856) x = Math.Pow(x, 1.0 / 3);
            else x = (7.787 * x) + (16 / 116);
            if (y > 0.008856) y = Math.Pow(y, 1.0 / 3);
            else y = (7.787 * y) + (16 / 116);
            if (z > 0.008856) z = Math.Pow(z, 1.0 / 3);
            else z = (7.787 * z) + (16 / 116);

            L = (float)((116 * y) - 16);
            a = (float)(500 * (x - y));
            b = (float)(200 * (y - z));
        }

        public float ColorsDelta (LAB col)
        {
            return (float)Math.Sqrt(Math.Pow(L-col.L, 2) + Math.Pow(a - col.a, 2) + Math.Pow(b - col.b, 2));
        }

    }
}
