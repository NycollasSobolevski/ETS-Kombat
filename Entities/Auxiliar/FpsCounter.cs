public class FpsCounter : Entity
{
    public int Fps { get; private set; } = 0;
    public TimeSpan TimePerFrame { get; set; } = TimeSpan.FromSeconds(1);
    public int Ticks { get; set; }
    private Queue<DateTime> queue = new Queue<DateTime>();
    private DateTime oldDateTime;

    protected bool toggleDraw = false;
    protected bool show = false;

    public void Update(Graphics g, DateTime t)
    {
        queue.Enqueue(DateTime.Now);

        if (queue.Count > Ticks)
        {
            oldDateTime = queue.Dequeue();

            TimeSpan fps = DateTime.Now - oldDateTime;

            var totalMilliseconds = fps.TotalMilliseconds / 19;
            TimePerFrame = TimeSpan.FromMilliseconds(totalMilliseconds);

            Fps = (int)(1000 / totalMilliseconds);
        }

        
        if ( Control.KeyMapping.Map[Keys.OemMinus] && !show)
            show = true;

        if (!Control.KeyMapping.Map[Keys.OemMinus]  && show)
        {
            toggleDraw = !toggleDraw;
            show = false;
        }
    }

    public void Draw(Graphics g)
    {
        if (toggleDraw)
            g.DrawString(
            $"{Fps.ToString()}fps",
            new Font("arial", 10),
            Brushes.Black,
            new PointF(0, 0)
        );
    }

    public void DrawDebug(Graphics g)
    {
        throw new NotImplementedException();
    }
}