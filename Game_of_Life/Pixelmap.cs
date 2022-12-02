namespace Game_of_Life;

public class Pixelmap
{
    private int height;
    private int width;
    private Color[,] map;
    private int scalingFactor;
    private int[] midPoint;
    private int viewWidth;
    private int viewHeight;

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

    public void setScalingFactor(int scalingFactor)
    {
        this.scalingFactor = scalingFactor;
    }

    public void clear()
    {
        map = new Color[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                map[w, h] = Color.Black;
            }
        }
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

    public void cyclePixel(int x, int y)
    {
        float zoomFactor = ((float)10 / (float)scalingFactor);
        float aspectRatio = (float)viewHeight / (float)viewWidth;

        int borderWidth = (int)Math.Ceiling((float)width * zoomFactor);
        int borderHeight = (int)Math.Ceiling((float)width * zoomFactor * aspectRatio);

        float scaleWidth = (float)viewWidth / (float)borderWidth;
        float scaleHeight = (float)viewHeight / (float)borderHeight;


        int leftBound = (int)Math.Floor(midPoint[0] - ((float)borderWidth / (float)2));
        if (leftBound < 0)
        {
            leftBound = 0;
        }

        int upperBound = (int)Math.Floor(midPoint[1] - ((float)borderHeight / (float)2));
        if (upperBound < 0)
        {
            upperBound = 0;
        }

        int scalePixelWidth;
        if (scaleWidth < 1)
        {
            scalePixelWidth = 1;
        }
        else
        {
            scalePixelWidth = (int)scaleWidth;
        }

        int scalePixelHeight;
        if (scaleHeight < 1)
        {
            scalePixelHeight = 1;
        }
        else
        {
            scalePixelHeight = (int)scaleHeight;
        }

        float xTrans = (float)x / scaleWidth;
        float yTrans = (float)y / scaleHeight;

        setPixel((int)(xTrans + leftBound), (int)(yTrans + upperBound), 1);
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

    public Graphics paintOnGraphics(Graphics g, int viewWidth, int viewHeight)
    {
        Color penColor = Color.FromArgb(125, Color.Beige);
        Pen linePen = new Pen(penColor, 1);

        this.viewHeight = viewHeight;
        this.viewWidth = viewWidth;

        float zoomFactor = ((float)10 / (float)scalingFactor);
        float aspectRatio = (float)viewHeight / (float)viewWidth;

        int borderWidth = (int)Math.Ceiling((float)width * zoomFactor);
        int borderHeight = (int)Math.Ceiling((float)width * zoomFactor * aspectRatio);

        float scaleWidth = (float)viewWidth / (float)borderWidth;
        float scaleHeight = (float)viewHeight / (float)borderHeight;

        int scalePixelWidth;
        if (scaleWidth < 1)
        {
            scalePixelWidth = 1;
        }
        else
        {
            scalePixelWidth = (int)Math.Ceiling(scaleWidth);
        }

        int scalePixelHeight;
        if (scaleHeight < 1)
        {
            scalePixelHeight = 1;
        }
        else
        {
            scalePixelHeight = (int)Math.Ceiling(scaleHeight);
        }


        int leftBound = (int)Math.Ceiling(midPoint[0] - ((float)borderWidth / (float)2));
        if (leftBound < 0)
        {
            leftBound = 0;
        }
        int rightBound = (int)Math.Ceiling(midPoint[0] + ((float)borderWidth / (float)2));
        if (rightBound > width)
        {
            rightBound = width;
        }

        int upperBound = (int)Math.Ceiling(midPoint[1] - ((float)borderHeight / (float)2));
        if (upperBound < 0)
        {
            upperBound = 0;
        }
        int lowerBound = (int)Math.Ceiling(midPoint[1] + ((float)borderHeight / (float)2));
        if (lowerBound > height)
        {
            lowerBound = height;
        }

        for (int w = leftBound; w < rightBound; w++)
        {
            for (int h = upperBound; h < lowerBound; h++)
            {
                Color pixelColor = map[w, h];
                if (!pixelColor.Equals(Color.Black))
                {
                    Brush pixelBrush = new SolidBrush(pixelColor);
                    g.FillRectangle(pixelBrush,
                        new Rectangle((int)(((float)w - (float)leftBound) * scaleHeight), (int)(((float)h - (float)upperBound) * scaleHeight), (int)scalePixelWidth, (int)scalePixelHeight));
                }
            }
        }
        return g;
    }
}