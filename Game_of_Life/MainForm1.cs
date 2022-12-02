using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game_of_Life;

public partial class MainForm1 : Form
{
    private int scalingFactor = 10;
    Pixelmap pixelmap;

    public MainForm1()
    {
        InitializeComponent();

        int height = 1000;
        int width = 2000;

        pixelmap = new Pixelmap(height, width, scalingFactor);
        Random rand = new Random();

        for (int i = 0; i < 2000; i++)
        {
            pixelmap.setPixel(rand.Next(width), rand.Next(height), rand.Next(3));
        }

        label1.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)1 / (double)scalingFactor)} - Dimensions: {Math.Floor(pictureBox1.Size.Height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(pictureBox1.Size.Width * ((double)10 / (double)scalingFactor)).ToString()}";
    }

    private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
        int height = pictureBox1.Size.Height;
        int width = pictureBox1.Size.Width;

        label1.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)1 / (double)scalingFactor)} - Dimensions: {Math.Floor(height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(width * ((double)10 / (double)scalingFactor)).ToString()}";

        Graphics graphics = e.Graphics;

        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;
        pixelmap.paintOnGraphics(graphics);
        // Thread.Sleep(1000);
    }

    private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
    {
        scalingFactor += e.Delta/64;
        if (scalingFactor < 10)
        {
            scalingFactor = 10;
        }
        label1.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)1 / (double)scalingFactor)} - Dimensions: {Math.Floor(pictureBox1.Size.Height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(pictureBox1.Size.Width * ((double)10 / (double)scalingFactor)).ToString()}";
        pictureBox1.Refresh();
    }
}