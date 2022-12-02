namespace Game_of_Life;

public class Pixelmap
{
    private int height;
    private int width;
    private Color[,] map;
    private int scalingFactor;
    private int[] midPoint; 

    public Pixelmap(int height, int width, int scalingFactor)
    {
        this.height = height;
        this.width = width;
        map = new Color[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                map[w, h] = Color.Black;
            }
        }
        
        this.scalingFactor = scalingFactor;
        this.midPoint = new int[] { (width / 2), (height / 2) };
    }

    public void setPixel(int x, int y, int val)
    {
        Color col = Color.Black;;
        if (val == 1)
        {
            col = Color.White;
        } else if (val == 2)
        {
            col = Color.Chocolate;
        }
        map[x, y] = col;
    }

    public Bitmap ToBitmap()
    {
        Bitmap b = new Bitmap(width, height);

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                b.SetPixel(w, h, map[w,h]);
            }
        }

        return b;
    }

    public Graphics paintOnGraphics(Graphics g)
    {
        Color penColor = Color.FromArgb(125, Color.Beige);
        Pen linePen = new Pen(penColor, 1);
        int borderWidth = (int)Math.Floor((double)width * ((double)10 / (double)scalingFactor));
        int borderHeight = (int)Math.Floor((double)height * ((double)10 / (double)scalingFactor));

        int leftBound = (int)Math.Floor(midPoint[0] - ((double)borderWidth / (double)2));
        if (leftBound < 0)
        {
            leftBound = 0;
        }
        int rightBound = (int)Math.Floor(midPoint[0] + ((double)borderWidth / (double)2));
        if (rightBound > width)
        {
            rightBound = width;
        }

        int upperBound = (int)Math.Floor(midPoint[1] - ((double)borderHeight / (double)2));
        if (upperBound < 0)
        {
            upperBound = 0;
        }
        int lowerBound = (int)Math.Floor(midPoint[1] + ((double)borderHeight / (double)2));
        if (lowerBound > height)
        {
            lowerBound = height;
        }

        for (int w = leftBound; w < rightBound; w++)
        {
            // g.DrawLine(linePen, w * scalingFactor, 0, w * scalingFactor, height * scalingFactor);

            for (int h = upperBound; h < lowerBound; h++)
            {
                // g.DrawLine(linePen, 0, h * scalingFactor, width * scalingFactor, h * scalingFactor);
                Color pixelColor = map[w, h];
                if (!pixelColor.Equals(Color.Black))
                {
                    Brush pixelBrush = new SolidBrush(pixelColor);
                    g.FillRectangle(pixelBrush, new Rectangle(((int)((double)w / ((double)1 / (double)scalingFactor))), ((int)((double)h / ((double)1 / (double)scalingFactor))), scalingFactor, scalingFactor));
                }
            }
        }
        return g;
    }
}