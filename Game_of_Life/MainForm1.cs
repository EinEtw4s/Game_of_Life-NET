using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game_of_Life;

public partial class MainForm1 : Form
{
    private int scalingFactor = 10;
    private int scalingAbsolute = 0;
    private int scalingMax;
    Pixelmap pixelmap;
    private int fieldHeight;
    private int fieldWidth;

    public MainForm1()
    {
        InitializeComponent();

        fieldHeight = 1800;
        fieldWidth = 3200;
        if (fieldWidth % 2 == 0)
        {
            scalingMax = fieldWidth - 2;
        }
        else
        {
            scalingMax = fieldWidth - 1;
        }
        

        scalingSlider.Minimum = 0;
        scalingSlider.Maximum = scalingMax;

        pixelmap = new Pixelmap(fieldHeight, fieldWidth, scalingFactor);
        Random rand = new Random();

        for (int i = 0; i < 4000; i++)
        {
            pixelmap.setPixel(rand.Next(fieldWidth), rand.Next(fieldHeight), rand.Next(3));
        }
    }

    private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
        int height = pictureBox.Size.Height;
        int width = pictureBox.Size.Width;

        Graphics graphics = e.Graphics;

        pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
        pictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
        pixelmap.paintOnGraphics(graphics, width, height);
        // Thread.Sleep(1000);
    }

    private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
    {
        scalingFactor += e.Delta/16;
        scalingAbsolute += e.Delta / 16;

        if (scalingFactor < 10)
        {
            scalingFactor = 10;
        } else if (scalingFactor > 500)
        {
            scalingFactor = 500;
        }

        if (scalingAbsolute < 0)
        {
            scalingAbsolute = 0;
        }

        if (scalingAbsolute > scalingMax)
        {
            scalingAbsolute = scalingMax;
        }

        if (scalingAbsolute % 2 != 0)
        {
            scalingAbsolute += 1;
            scalingSlider.Value = scalingAbsolute;
        }
        if (scalingAbsolute > (fieldWidth - 10))
        {
            scalingAbsolute = (fieldWidth - 12);
            if (scalingAbsolute % 2 != 0)
            {
                scalingAbsolute += 1;
                scalingSlider.Value = scalingAbsolute;
            }
        }

        scalingSlider.Value = scalingAbsolute;
        pixelmap.setScalingFactor(scalingFactor);
        pixelmap.setScalingAbsolute(scalingAbsolute);
        pictureBox.Refresh();
    }

    private void scalingSlider_Changed(object sender, EventArgs e)
    {
        scalingAbsolute = scalingSlider.Value;
        if (scalingAbsolute % 2 != 0)
        {
            scalingAbsolute += 1;
            scalingSlider.Value = scalingAbsolute;
        }
        pixelmap.setScalingAbsolute(scalingAbsolute);
        pictureBox.Refresh();
    }

    private void buttonRepopulate_Click(object sender, EventArgs e)
    {
        pixelmap.clear();
        Random rand = new Random();

        for (int i = 0; i < 4000; i++)
        {
            pixelmap.setPixel(rand.Next(fieldWidth), rand.Next(fieldHeight), rand.Next(3));
        }
        pictureBox.Refresh();
    }

    private void pictureBox_Click(object sender, EventArgs e)
    {
        pixelmap.cyclePixel(MousePosition.X, MousePosition.Y);
        pictureBox.Refresh();
    }

    private void zoomInButton_Click(object sender, EventArgs e)
    {
        scalingAbsolute += 4;
        while (scalingAbsolute % 4 != 0)
        {
            scalingAbsolute += 1;
            scalingSlider.Value = scalingAbsolute;
        }
        if (scalingAbsolute < 0)
        {
            scalingAbsolute = 0;
        }
        if (scalingAbsolute > (fieldWidth - 10))
        {
            scalingAbsolute = (fieldWidth - 12);
            while (scalingAbsolute % 4 != 0)
            {
                scalingAbsolute += 1;
                scalingSlider.Value = scalingAbsolute;
            }
        }

        scalingSlider.Value = scalingAbsolute;
        pixelmap.setScalingAbsolute(scalingAbsolute);
    }

    private void zoomOutButton_Click(object sender, EventArgs e)
    {
        scalingAbsolute -= 4;
        while (scalingAbsolute % 4 != 0)
        {
            scalingAbsolute += 1;
            scalingSlider.Value = scalingAbsolute;
        }
        if (scalingAbsolute < 0)
        {
            scalingAbsolute = 0;
        }
        if (scalingAbsolute > (fieldWidth - 10))
        {
            scalingAbsolute = (fieldWidth - 12);
            while (scalingAbsolute % 4 != 0)
            {
                scalingAbsolute += 1;
                scalingSlider.Value = scalingAbsolute;
            }
        }

        scalingSlider.Value = scalingAbsolute;
        pixelmap.setScalingAbsolute(scalingAbsolute);
    }
}
