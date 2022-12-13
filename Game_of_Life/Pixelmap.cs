namespace Game_of_Life;

public class Pixelmap
{
    private int height;
    private int width;
    private int[,] map;
    private int scalingFactor;
    private int[] midPoint;
    private int viewWidth;
    private int viewHeight;
    private int scalingAbsolute;
    private Color[] colors = {Color.Black, Color.White, Color.OrangeRed};

    public Pixelmap(int height, int width, int scalingFactor)
    {
        this.height = height;
        this.width = width;
        map = new int[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                map[w, h] = 0;
            }
        }
        
        this.scalingFactor = scalingFactor;
        this.midPoint = new int[] { width / 2, height / 2 };
        this.scalingAbsolute = 0;
    }

    public void setScalingFactor(int scalingFactor)
    {
        this.scalingFactor = scalingFactor;
    }

    public void setScalingAbsolute(int scalingAbsolute)
    {
        this.scalingAbsolute = scalingAbsolute;
    }

    public void clear()
    {
        map = new int[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                map[w, h] = 0;
            }
        }
    }

    public void setPixel(int x, int y, int val)
    {
        val = val >= 3 ? 0 : val;
        map[x, y] = val;
    }

    public void cyclePixel(int x, int y)
    {
        float aspectRatio = (float)viewHeight / (float)viewWidth;

        // set width and needed height of the border of the viewable area
        int borderWidth = width - scalingAbsolute;
        int borderHeight = (int)Math.Ceiling((float)borderWidth * aspectRatio);

        // scaling factors for both dimensions
        float scaleWidth = (float)viewWidth / (float)borderWidth;
        float scaleHeight = (float)viewHeight / (float)borderHeight;

        // set both left and right bound for the area being viewed
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

        float xTrans = (float)(x-scaleWidth) / scaleWidth;
        float yTrans = (float)(y-scaleHeight) / scaleHeight;
        int pixelX = (int)Math.Ceiling(xTrans + (float)leftBound);
        int pixelY = (int)Math.Ceiling(yTrans + (float)upperBound);
        int val = map[pixelX, pixelY] + 1;
        setPixel(pixelX, pixelY, val);
    }

    public Graphics paintOnGraphics(Graphics g, int viewWidth, int viewHeight)
    {
        g.Clear(Color.Black);

        Color penColor = Color.FromArgb(125, Color.Beige);
        Pen linePen = new Pen(penColor, 1);

        this.viewHeight = viewHeight;
        this.viewWidth = viewWidth;

        int[] viewMid = { viewWidth / 2, viewHeight / 2 };

        float aspectRatio = (float)viewHeight / (float)viewWidth;

        // set width and needed height of the border of the viewable area
        int borderWidth = width - scalingAbsolute;
        int borderHeight = (int)Math.Ceiling((float)borderWidth * aspectRatio);

        // scaling factors for both dimensions
        float scaleWidth = (float)viewWidth / (float)borderWidth;
        float scaleHeight = (float)viewHeight / (float)borderHeight;

        // ensure that pixel width and height are always at least 1px
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

        // set both left and right bound for the area being viewed
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

        // set both upper and lower bound for the area being viewed
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
                int deltaW = w - midPoint[0];
                int deltaH = h - midPoint[1];

                Color pixelColor = colors[map[w, h]];
                if (!pixelColor.Equals(Color.Black))
                {
                    int x = (int)(viewMid[0] + (deltaW * scaleWidth));
                    int y = (int)(viewMid[1] + (deltaH * scaleHeight));
                    int pixelWidth = (int)scalePixelWidth;
                    int pixelHeight = (int)scalePixelHeight;

                    if (x < 0)
                    {
                        int deltaX = 0 - x;
                        x = 0;
                        pixelWidth = scalePixelWidth - deltaX;
                    }
                    else if (x + (int)scalePixelWidth > viewWidth)
                    {
                        int deltaX = viewWidth - x;
                        pixelWidth = deltaX;
                    }

                    if (y < 0)
                    {
                        int deltaY = 0 - y;
                        y = 0;
                        pixelHeight = scalePixelHeight - deltaY;
                    }
                    else if (y + (int)scalePixelHeight > viewHeight)
                    {
                        int deltaY = viewHeight - y;
                        pixelWidth = deltaY;
                    }

                    Brush pixelBrush = new SolidBrush(pixelColor);
                    g.FillRectangle(pixelBrush,
                        new Rectangle(x, (int)(viewMid[1] + (deltaH * scaleHeight)), (int)scalePixelWidth, (int)scalePixelHeight));
                }
            }
        }
        return g;
    }
}