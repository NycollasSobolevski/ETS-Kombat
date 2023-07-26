public class FpsCounter : Entity
{
    public int Fps { get; private set; } = 0;
    public TimeSpan TimePerFrame { get; set; } = TimeSpan.FromSeconds(1);
    public int Ticks { get; set; }
    private Queue<DateTime> queue = new Queue<DateTime>();
    private DateTime oldDateTime;

    public void Update(Graphics g, TimeSpan t)
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
    }

    public void Draw(Graphics g)
        => g.DrawString(
            $"{Fps.ToString()}fps",
            new Font("arial", 10),
            Brushes.Black,
            new PointF(0, 0)
        );

    public void DrawDebug(Graphics g)
    {
        throw new NotImplementedException();
    }
}