public class Background : Entity
{
    private string path { get; set; }
    private Bitmap image { get; set; }
    public Background(string path)
    {
        this.path = path;
        this.image = new Bitmap(path);
    }

    public void Draw(Graphics g)
    { }
    public void Draw(Graphics g, float screenWidth, float screenHeight)
    {
        g.DrawImage(
            this.image,
            new RectangleF( 0, 0, screenWidth, screenHeight )
        );
    }

    public void DrawDebug(Graphics g)
    {
        throw new NotImplementedException();
    }

    public void Update(Graphics g, DateTime t)
    {
        throw new NotImplementedException();
    }
}