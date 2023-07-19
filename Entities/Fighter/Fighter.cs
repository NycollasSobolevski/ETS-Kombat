#pragma warning disable


public abstract class Fighter : Entity
{
    // Basic Functions of an entity
    protected DateTime lastFrame = DateTime.Now;
    protected int AnimationTimer { get; set; }
    public PointF Position { get; set; }
    public Size Size { get; set; }
    public Dictionary<AnimationName, List<Frame>> Frames { get; set; } = new Dictionary<AnimationName, List<Frame>>();
    public AnimationName AnimationName { get; set; } = AnimationName.Foward;
    public int AnimationFrame { get; set; } = 0;
    public RectangleF Rectangle {
        get {
            return new RectangleF(Position.X, Position.Y, Size.Width, Size.Height);
        }
        private set{ }
    }
    public Bitmap Image { get; set; }
    public int Velocity { get; set; }
    public abstract void Update(Graphics g, TimeSpan t);
    public abstract void DrawDebug(Graphics g);
    public abstract void Draw(Graphics g);

    public void MoveX(TimeSpan t)
    {
        this.Position = new PointF(
            (float)(this.Position.X + (this.Velocity * t.TotalSeconds)),
            this.Position.Y
        ); 
    }

}