using HW2_Threads__Work_with_BitmapImage_;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;


partial class Program
{
    static Bitmap bitmap;
    static int imgWidth = 30;               //The width of the bitmap image
    static int imgHeight = 30;              //The height of the bitmap image
    static int checkDist = 100;             //Distance to the main color (the smaller the more accurate)


    static LAB redLAB = new LAB(new XYZ(Color.FromArgb(255, 255, 0, 0)));
    static LAB greenLAB = new LAB(new XYZ(Color.FromArgb(255, 0, 255, 0)));
    static LAB blueLAB = new LAB(new XYZ(Color.FromArgb(255, 0, 0, 255)));

    static List<DataColor> BelongToRed = new List<DataColor>();
    static List<DataColor> BelongToGreen = new List<DataColor>();
    static List<DataColor> BelongToBlue = new List<DataColor>();
    static List<DataColor> EqualColors = new List<DataColor>();


    static void CreateBitmapFile()
    {
            bitmap = new Bitmap(imgWidth, imgHeight);
            Random rnd = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int k = 0; k < bitmap.Height; k++)
                {
                    Color color = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    bitmap.SetPixel(i, k, color);
                }
            }
            bitmap.Save("myfile.png", ImageFormat.Png);   
    }

    static void ColorIsRed(object? obj)
    {
        if (obj is DataColor color)
        {
            float dist = color.PosLAB.ColorsDelta(redLAB);
            if (dist <= checkDist)
            {
                color.BelongingToMainColor = "Red";
                BelongToRed.Add(color);
            }
        }
    }

    static void ColorIsGreen(object? obj)
    {
        if (obj is DataColor color)
        {
            float dist = color.PosLAB.ColorsDelta(greenLAB);
            if (dist <= checkDist)
            {
                color.BelongingToMainColor = "Green";
                BelongToGreen.Add(color);
            }
        }
    }

    static void ColorIsBlue(object? obj)
    {
        if (obj is DataColor color)
        {
            float dist = color.PosLAB.ColorsDelta(blueLAB);
            if (dist <= checkDist)
            {
                color.BelongingToMainColor = "Blue";
                BelongToBlue.Add(color);
            }
        }
    }


    static void AnalysisBitmap(Bitmap? bitmap)
    {
        for (int i = 0; i < bitmap.Width; i++)
        {
            for (int k = 0; k < bitmap.Height; k++)
            {
                Thread thred = new Thread(new ParameterizedThreadStart(ColorIsRed));
                thred.Start(new DataColor(bitmap.GetPixel(i, k), i, k));

                Thread thgreen = new Thread(new ParameterizedThreadStart(ColorIsGreen));
                thgreen.Start(new DataColor(bitmap.GetPixel(i, k), i, k));

                Thread thblue = new Thread(new ParameterizedThreadStart(ColorIsBlue));
                thblue.Start(new DataColor(bitmap.GetPixel(i, k), i, k));

                Console.WriteLine($"Y - {i}, X - {k}");
            }
        }
        EqualColors = BelongToRed.Concat(BelongToGreen.Concat(BelongToBlue)).OrderBy(y => y.PosY).OrderBy(x => x.PosX).ToList();
    }


    static void Main(string[] args)
    {
        //First the bitmap-file creates
        CreateBitmapFile();

        //Then read the data from the bitmap-file
        bitmap = new Bitmap("myfile.png", true);


        //During the analysis used three threads to analysis three colors separated
        AnalysisBitmap(bitmap);             

        //The analysis of bitmap colors saves to a json-file
        string str = JsonSerializer.Serialize<List<DataColor>>(EqualColors);
        File.WriteAllText("AnalysisBitmapColors.json", str);
    }
}