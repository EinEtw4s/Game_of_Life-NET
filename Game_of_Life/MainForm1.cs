using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Game_of_Life;

public partial class MainForm1 : Form
{
    private int scalingFactor = 10;
    private int scalingAbsolute;
    private int scalingMax;
    Pixelmap pixelmap;
    private int fieldHeight;
    private int fieldWidth;
    private List<Thread> renderThreads = new List<Thread>();
    private decimal renderSleep = 0;
    private bool showGameArea = false;

    public MainForm1()
    {
        InitializeComponent();

        fieldHeight = 160;
        fieldWidth = 300;
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
        
        /*
        Random rand = new Random();
        
        for (int i = 0; i < 4000; i++)
        {
            pixelmap.SetPixel(rand.Next(fieldWidth), rand.Next(fieldHeight), rand.Next(3));
        }
        */
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        int height = pictureBox.Size.Height;
        int width = pictureBox.Size.Width;

        Graphics graphics = e.Graphics;

        pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
        pictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
        debugLabel.Text = $"{renderSleep}";
        if (!showGameArea)
        {
            pixelmap.paintOnGraphics(graphics, width, height);
        } else
        {
            pixelmap.paintGameArea(graphics, width, height);
            showGameArea = false;
        }
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
        pixelmap.SetScalingFactor(scalingFactor);
        pixelmap.SetScalingAbsolute(scalingAbsolute);
        pictureBox.Invalidate();
    }

    private void scalingSlider_Changed(object sender, EventArgs e)
    {
        scalingAbsolute = scalingSlider.Value;
        if (scalingAbsolute % 2 != 0)
        {
            scalingAbsolute += 1;
            scalingSlider.Value = scalingAbsolute;
        }
        pixelmap.SetScalingAbsolute(scalingAbsolute);
        pictureBox.Invalidate();
    }

    private void renderSleep_Changed(object sender, EventArgs e)
    {
        renderSleep = numericRenderSleep.Value;
    }

    private void buttonRepopulate_Click(object sender, EventArgs e)
    {
        pixelmap.Clear();
        Random rand = new Random();

        for (int i = 0; i < 4000; i++)
        {
            pixelmap.SetPixel(rand.Next(fieldWidth), rand.Next(fieldHeight), rand.Next(3));
        }
        pictureBox.Invalidate();
    }

    private void pictureBox_Click(object sender, EventArgs e)
    {
        var cursorLocation = PointToClient(Cursor.Position);
        pixelmap.CyclePixel(cursorLocation.X, cursorLocation.Y);
        pictureBox.Invalidate();
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
        pixelmap.SetScalingAbsolute(scalingAbsolute);
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
        pixelmap.SetScalingAbsolute(scalingAbsolute);
    }

    delegate void SetRefreshPictureBoxCallback();

    private void RefreshPictureBox()
    {
        try
        {
            if (pictureBox.InvokeRequired)
            {
                SetRefreshPictureBoxCallback d = RefreshPictureBox;
                Invoke(d, new object[] { });
            }
            else
            {
                pictureBox.Invalidate();
            }
        }
        catch (ThreadInterruptedException)
        {
            throw new Exception("Thread interrupted");
        } catch(Exception e)
        {
            throw new Exception("");
        }
    }

    private void RenderTick()
    {
        try
        {
            while (true)
            {
                var stopwatch = Stopwatch.StartNew();

                pixelmap.Tick();
                RefreshPictureBox();

                while ((decimal)stopwatch.Elapsed.Milliseconds <= renderSleep)
                {
                    Thread.Sleep(1);
                }
            }
        }
        catch (Exception)
        {
            RefreshPictureBox();
            // ignored; can exit without much consequences
        }
    }

    private void buttonTick_Click(object sender, EventArgs e)
    {
        if (renderThreads.Count == 0)
        {
            var rThread = new Thread(RenderTick);
            rThread.Start();
            renderThreads.Add(rThread);
            buttonTick.Text = "⏸PAUSE";
        } else
        {
            renderThreads.ForEach(r => { r.Interrupt(); });
            renderThreads.Clear();
            buttonTick.Text = "⏯PLAY";
        }
    }
    
    private void buttonTickOnce_Click(object sender, EventArgs e)
    {
        pixelmap.Tick();
        pictureBox.Invalidate();
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        pixelmap.Clear();
        var mp = pixelmap.midPoint;
        pixelmap.SetPixel(mp[0], mp[1], 1);
        pixelmap.SetPixel((int)mp[0], (int)mp[1] - 1, 1);
        pixelmap.SetPixel((int)mp[0], (int)mp[1] - 2, 1);
        pixelmap.SetPixel((int)mp[0] - 1, (int)mp[1] - 1, 1);
        pixelmap.SetPixel((int)mp[0] + 1, (int)mp[1] - 2, 1);
        pictureBox.Invalidate();
    }

    private void buttonGameArea_Click(object sender, EventArgs e)
    {
        showGameArea = true;
        pictureBox.Invalidate();
    }
}
