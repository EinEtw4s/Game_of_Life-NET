namespace Game_of_Life;

public class Pixelmap
{
    private int height;
    private int width;
    private Color[,] map;
    private int scalingFactor;

    public Pixelmap(int height, int width, int scalingFactor)
    {
        this.height = height / scalingFactor;
        this.width = width / scalingFactor;
        map = new Color[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                map[w, h] = Color.Black;
            }
        }

        this.scalingFactor = scalingFactor;
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
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                Color pixelColor = map[w, h];
                if (!pixelColor.Equals(Color.Black))
                {
                    Brush pixelBrush = new SolidBrush(pixelColor);
                    g.FillRectangle(pixelBrush, new Rectangle((w*scalingFactor), (h * scalingFactor), scalingFactor, scalingFactor));
                }
            }
        }
        return g;
    }
}