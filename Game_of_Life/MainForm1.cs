using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game_of_Life;

public partial class MainForm1 : Form
{
    private int scalingFactor = 10;
    Pixelmap pixelmap;
    private int height;
    private int width;

    public MainForm1()
    {
        InitializeComponent();

        height = 1800;
        width = 3200;

        pixelmap = new Pixelmap(height, width, scalingFactor);
        Random rand = new Random();

        for (int i = 0; i < 4000; i++)
        {
            pixelmap.setPixel(rand.Next(width), rand.Next(height), rand.Next(3));
        }

        debugLabel.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)10 / (double)scalingFactor)} - Dimensions: {Math.Floor(pictureBox.Size.Height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(pictureBox.Size.Width * ((double)10 / (double)scalingFactor)).ToString()}";
    }

    private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
        int height = pictureBox.Size.Height;
        int width = pictureBox.Size.Width;

        debugLabel.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)10 / (double)scalingFactor)} - Dimensions: {Math.Floor(height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(width * ((double)10 / (double)scalingFactor)).ToString()}";

        Graphics graphics = e.Graphics;

        pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
        pictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
        pixelmap.paintOnGraphics(graphics, width, height);
        // Thread.Sleep(1000);
    }

    private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
    {
        scalingFactor += e.Delta/16;
        if (scalingFactor < 10)
        {
            scalingFactor = 10;
        } else if (scalingFactor > 500)
        {
            scalingFactor = 500;
        }

        scalingSlider.Value = scalingFactor;
        pixelmap.setScalingFactor(scalingFactor);
        debugLabel.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)10 / (double)scalingFactor)} - Dimensions: {Math.Floor(pictureBox.Size.Height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(pictureBox.Size.Width * ((double)10 / (double)scalingFactor)).ToString()}";
        pictureBox.Refresh();
    }

    private void scalingSlider_Changed(object sender, EventArgs e)
    {
        scalingFactor = scalingSlider.Value;
        pixelmap.setScalingFactor(scalingFactor);
        debugLabel.Text = $"Scaling Factor: {scalingFactor.ToString()}\n{((double)10 / (double)scalingFactor)} - Dimensions: {Math.Floor(pictureBox.Size.Height * ((double)10 / (double)scalingFactor)).ToString()}x{Math.Floor(pictureBox.Size.Width * ((double)10 / (double)scalingFactor)).ToString()}";
        pictureBox.Refresh();
    }

    private void buttonRepopulate_Click(object sender, EventArgs e)
    {
        pixelmap.clear();
        Random rand = new Random();

        for (int i = 0; i < 4000; i++)
        {
            pixelmap.setPixel(rand.Next(width), rand.Next(height), rand.Next(3));
        }
        pictureBox.Refresh();
    }

    private void pictureBox_Click(object sender, EventArgs e)
    {
        pixelmap.cyclePixel(MousePosition.X, MousePosition.Y);
        pictureBox.Refresh();
    }
}
