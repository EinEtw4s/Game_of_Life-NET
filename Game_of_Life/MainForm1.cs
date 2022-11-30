using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game_of_Life;

public partial class MainForm1 : Form
{
    public MainForm1()
    {
        Pixelmap pixelmap;
        int scalingFactor = 10;

        InitializeComponent();

        int height = pictureBox1.Size.Height;
        int width = pictureBox1.Size.Width;

        Graphics graphics = pictureBox1.CreateGraphics();


        pixelmap = new Pixelmap(height, width, scalingFactor);
        Random rand = new Random();

        for (int i = 0; i < 20; i++)
        {
            pixelmap.setPixel(rand.Next(width), rand.Next(height), rand.Next(3));
        }

        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;
        pixelmap.paintOnGraphics(graphics);

        /* this.Shown += (s, e) =>
        {
            while (true)
            {
                for (int i = 0; i < 20; i++)
                {
                    pixelmap.setPixel(rand.Next(50), rand.Next(20), rand.Next(3));
                }

                pictureBox1.Image = pixelmap.ToBitmap();
                Thread.Sleep(120);
            }
        };*/
    }
}