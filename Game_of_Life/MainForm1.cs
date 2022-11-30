using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game_of_Life;

public partial class MainForm1 : Form
{
    private int scalingFactor = 10;
    public MainForm1()
    {
        InitializeComponent();

        label1.Text = "Scaling Factor: " + scalingFactor.ToString() + $"\nDimensions: {pictureBox1.Size.Height.ToString()}x{pictureBox1.Size.Width.ToString()}";


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

    private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
        Pixelmap pixelmap;
        
        int height = pictureBox1.Size.Height;
        int width = pictureBox1.Size.Width;

        Graphics graphics = e.Graphics;


        pixelmap = new Pixelmap(height, width, scalingFactor);
        Random rand = new Random();

        for (int i = 0; i < 20; i++)
        {
            pixelmap.setPixel(rand.Next(width / scalingFactor), rand.Next(height / scalingFactor), rand.Next(3));
        }

        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;
        pixelmap.paintOnGraphics(graphics);
        // Thread.Sleep(1000);
    }

    private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
    {
        scalingFactor += e.Delta/32;
        if (scalingFactor < 1)
        {
            scalingFactor = 1;
        }
        label1.Text = "Scaling Factor: " + scalingFactor.ToString() + $"\nDimensions: {pictureBox1.Size.Height.ToString()}x{pictureBox1.Size.Width.ToString()}";
        pictureBox1.Refresh();
    }
}