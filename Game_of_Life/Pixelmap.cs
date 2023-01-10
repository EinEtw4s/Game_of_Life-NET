namespace Game_of_Life;

public class Pixelmap
{
    private int height;
    private int width;
    private int[,] map;
    private int scalingFactor;
    public int[] midPoint;
    private int viewWidth;
    private int viewHeight;
    private int scalingAbsolute;
    private Color[] colors = {Color.Black, Color.White, Color.OrangeRed};

    // gameWindow = [leftBound, rightBound, upperBound, lowerBound]
    public int[] gameWindow = { 0, 0, 0, 0 };

    public Pixelmap(int height, int width, int scalingFactor)
    {
        this.height = height;
        this.width = width;
        map = new int[width, height];
        gameWindow = new int[] { width, 0, height, 0 };

        this.scalingFactor = scalingFactor;
        this.midPoint = new int[] { width / 2, height / 2 };
        this.scalingAbsolute = 0;
    }

    public void SetScalingFactor(int scalingFactor)
    {
        this.scalingFactor = scalingFactor;
    }

    public void SetScalingAbsolute(int scalingAbsolute)
    {
        this.scalingAbsolute = scalingAbsolute;
    }

    public void Clear()
    {
        map = new int[width, height];
        gameWindow = new int[] { width, 0, height, 0 };
    }

    private void SetBounds(int x, int y)
    {
        x = x == 0 ? 2 : x;
        y = y == 0 ? 2 : y;

        x = x == width ? width - 2 : x;
        y = y == height ? height - 2 : y;

        if (x < gameWindow[0])
        {
            gameWindow[0] = x;
        } 
        
        if (x > gameWindow[1])
        {
            gameWindow[1] = x;
        }

        if (y < gameWindow[2])
        {
            gameWindow[2] = y;
        }
        
        if (y > gameWindow[3])
        {
            gameWindow[3] = y;
        }
    }

    public void SetPixel(int x, int y, int val)
    {
        val = val >= 3 ? 0 : val;
        map[x, y] = val;

        SetBounds(x, y);
    }


    public void CyclePixel(int x, int y)
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

        float xTrans = (float)(x- scaleWidth - 0.5 * scaleWidth) / scaleWidth;
        float yTrans = (float)(y- scaleHeight + 0.5 * scaleHeight) / scaleHeight;
        int pixelX = (int)Math.Ceiling(xTrans + (float)leftBound);
        int pixelY = (int)Math.Ceiling(yTrans + (float)upperBound);
        int val = map[pixelX, pixelY] + 1;
        SetPixel(pixelX, pixelY, val);
    }

    public Graphics paintOnGraphics(Graphics g, int viewportWidth, int viewportHeight)
    {
        g.Clear(Color.Black);

        Color penColor = Color.FromArgb(125, Color.Beige);
        Pen linePen = new Pen(penColor, 1);

        viewHeight = viewportHeight;
        viewWidth = viewportWidth;

        int[] viewMid = { viewportWidth / 2, viewportHeight / 2 };

        float aspectRatio = viewportHeight / (float)viewportWidth;

        // set width and needed height of the border of the viewable area
        int borderWidth = width - scalingAbsolute;
        int borderHeight = (int)Math.Ceiling(borderWidth * aspectRatio);

        // scaling factors for both dimensions
        float scaleWidth = viewportWidth / (float)borderWidth;
        float scaleHeight = viewportHeight / (float)borderHeight;

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
        int leftBound = (int)Math.Ceiling(midPoint[0] - (borderWidth / (float)2));
        if (leftBound < 0)
        {
            leftBound = 0;
        }
        int rightBound = (int)Math.Ceiling(midPoint[0] + (borderWidth / (float)2));
        if (rightBound > width)
        {
            rightBound = width;
        }

        // set both upper and lower bound for the area being viewed
        int upperBound = (int)Math.Ceiling(midPoint[1] - (borderHeight / (float)2));
        if (upperBound < 1)
        {
            upperBound = 1;
        }
        int lowerBound = (int)Math.Ceiling(midPoint[1] + (borderHeight / (float)2));
        if (lowerBound > height)
        {
            lowerBound = height;
        }

        for (int w = leftBound; w < rightBound; w++)
        {
            for (int h = (upperBound - 1); h < lowerBound; h++)
            {
                int deltaW = w - midPoint[0];
                int deltaH = h - midPoint[1];

                Color pixelColor = colors[map[w, h]];
                if (!pixelColor.Equals(Color.Black))
                {
                    int x = (int)(viewMid[0] + (deltaW * scaleWidth) + 0.5 * scaleWidth);
                    int y = (int)(viewMid[1] + (deltaH * scaleHeight) + 0.5 * scaleHeight);
                    int pixelWidth = scalePixelWidth;
                    int pixelHeight = scalePixelHeight;

                    if (x < 0)
                    {
                        int deltaX = 0 - x;
                        x = 0;
                        pixelWidth = scalePixelWidth - deltaX;
                    }
                    else if (x + scalePixelWidth > viewportWidth)
                    {
                        int deltaX = viewportWidth - x;
                        pixelWidth = deltaX;
                    }

                    if (y < 0)
                    {
                        int deltaY = 0 - y;
                        y = 0;
                        pixelHeight = scalePixelHeight - deltaY;
                    }
                    else if (y + scalePixelHeight > viewportHeight)
                    {
                        int deltaY = viewportHeight - y;
                        pixelWidth = deltaY;
                    }

                    Brush pixelBrush = new SolidBrush(pixelColor);
                    g.FillRectangle(pixelBrush,
                        new Rectangle(x, (int)(viewMid[1] + (deltaH * scaleHeight)), scalePixelWidth, scalePixelHeight));
                }
            }
        }
        return g;
    }

    public Graphics paintGameArea(Graphics g, int viewportWidth, int viewportHeight)
    {
        g.Clear(Color.Black);

        Color penColor = Color.FromArgb(125, Color.Beige);
        Pen linePen = new Pen(penColor, 1);

        viewHeight = viewportHeight;
        viewWidth = viewportWidth;

        int[] viewMid = { viewportWidth / 2, viewportHeight / 2 };

        float aspectRatio = viewportHeight / (float)viewportWidth;

        // set width and needed height of the border of the viewable area
        int borderWidth = width - scalingAbsolute;
        int borderHeight = (int)Math.Ceiling(borderWidth * aspectRatio);

        // scaling factors for both dimensions
        float scaleWidth = viewportWidth / (float)borderWidth;
        float scaleHeight = viewportHeight / (float)borderHeight;

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
        int leftBound = (int)Math.Ceiling(midPoint[0] - (borderWidth / (float)2));
        if (leftBound < 0)
        {
            leftBound = 0;
        }
        int rightBound = (int)Math.Ceiling(midPoint[0] + (borderWidth / (float)2));
        if (rightBound > width)
        {
            rightBound = width;
        }

        // set both upper and lower bound for the area being viewed
        int upperBound = (int)Math.Ceiling(midPoint[1] - (borderHeight / (float)2));
        if (upperBound < 1)
        {
            upperBound = 1;
        }
        int lowerBound = (int)Math.Ceiling(midPoint[1] + (borderHeight / (float)2));
        if (lowerBound > height)
        {
            lowerBound = height;
        }

        int[,] gameMap = new int[width, height];

        for (int w2 = gameWindow[0] - 1; w2 <= gameWindow[1] + 1; w2++)
        //for (int w = 1; w < width - 1; w++)
        {
            for (int h2 = gameWindow[2] - 1; h2 <= gameWindow[3] + 1; h2++)
            { 
                gameMap[w2, h2] = 2;
            }
        }

        for (int w = leftBound; w < rightBound; w++)
        {
            for (int h = (upperBound - 1); h < lowerBound; h++)
            {
                int deltaW = w - midPoint[0];
                int deltaH = h - midPoint[1];

                Color pixelColor = colors[gameMap[w,h]];
                if (!pixelColor.Equals(Color.Black))
                {
                    int x = (int)(viewMid[0] + (deltaW * scaleWidth) + 0.5 * scaleWidth);
                    int y = (int)(viewMid[1] + (deltaH * scaleHeight) + 0.5 * scaleHeight);
                    int pixelWidth = scalePixelWidth;
                    int pixelHeight = scalePixelHeight;

                    if (x < 0)
                    {
                        int deltaX = 0 - x;
                        x = 0;
                        pixelWidth = scalePixelWidth - deltaX;
                    }
                    else if (x + scalePixelWidth > viewportWidth)
                    {
                        int deltaX = viewportWidth - x;
                        pixelWidth = deltaX;
                    }

                    if (y < 0)
                    {
                        int deltaY = 0 - y;
                        y = 0;
                        pixelHeight = scalePixelHeight - deltaY;
                    }
                    else if (y + scalePixelHeight > viewportHeight)
                    {
                        int deltaY = viewportHeight - y;
                        pixelWidth = deltaY;
                    }

                    Brush pixelBrush = new SolidBrush(pixelColor);
                    g.FillRectangle(pixelBrush,
                        new Rectangle(x, (int)(viewMid[1] + (deltaH * scaleHeight)), scalePixelWidth, scalePixelHeight));
                }
            }
        }
        return g;
    }

    public void Tick()
    {
        int[,]? mapBuffer = new int[width, height];

        for (int w = gameWindow[0] - 2; w < gameWindow[1] + 2; w++)
        {
            for (int h = gameWindow[2] - 2; h < gameWindow[3] + 2; h++)
            {
                if (w < 0)
                {
                    continue;
                }
                else if (w >= width)
                {
                    continue;
                }

                if (h < 0)
                {
                    continue;
                }
                else if (h >= height)
                {
                    continue;
                }

                int neighbors = 0;
                var rand = new Random();
                int numOptions = colors.Length;

                // iterate through all neighbors
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        int mx = x + w;
                        int my = y + h;
                        if (mx < 0)
                        {
                            break;
                        } else if (mx >= width)
                        {
                            break;
                        }

                        if (my < 0)
                        {
                            break;
                        } else if (my >= height)
                        {
                            break;
                        }

                        int cell = 0;

                        try
                        {
                            cell = map[x + w, y + h];
                        } catch (Exception e)
                        {
                            break;
                        }
                        if (cell > 0)
                        {
                            neighbors += 1;
                        }
                    }
                }

                int currCell = 0;

                try
                {
                    currCell = map[w, h];
                } catch (Exception e) { continue; }

                if (map[w, h] == 0)
                {
                    if (neighbors == 3)
                    {
                        SetBounds(w, h);
                        mapBuffer[w, h] = 1;
                    }
                }
                else
                {
                    // since the current entity is also being counted...
                    neighbors--;

                    switch (neighbors)
                    {
                        // dies
                        case < 2:
                            mapBuffer[w, h] = 0;
                            break;

                        // lives
                        case 2:
                            mapBuffer[w, h] = 1;
                            SetBounds(w, h);
                            break;

                        // lives
                        case 3:
                            mapBuffer[w, h] = 1;
                            SetBounds(w, h);
                            break;

                        // dies
                        case > 3:
                            mapBuffer[w, h] = 0;
                            break;
                    }
                }
            }
        }

        map = mapBuffer;
        mapBuffer = null;
    }
}