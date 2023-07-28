public class Background : Entity
{
    private string path { get; set; }
    private Bitmap image { get; set; }
    private SizeF size { get; set; }
    public Background(string path, SizeF screenSize)
    {
        this.path = path;
        this.image = new Bitmap(path);
        this.size = screenSize;
    }

    public void Draw(Graphics g)
    {
        var screenWidth = size.Width;
        var screenHeight = size.Height;

        g.DrawImage(
            this.image,
            new RectangleF(0, 0, screenWidth, screenHeight )
        );
    }

    public void DrawDebug(Graphics g)
    {
        throw new NotImplementedException();
    }

    public void Update(Graphics g, DateTime t)
    {
        return;
    }
}