public class Life
{

    public Size ScreenSize { get; set; }
    public int Player { get; set; }

    private PointF position;
    private Size size;
    private bool debug = false;

    public Life(Size screenSize, int player)
    {
        this.ScreenSize = screenSize;
        this.Player = player;

        if (this.Player == 1)
            this.position = new PointF(100, 0);
        else
            this.position = new PointF(ScreenSize.Width / 2 + 100, 0);
        
        this.size = new Size(ScreenSize.Width / 3, 50);
    }
    public void Draw(Graphics g, int health)
    {
        if (debug)
        {
            if (Player == 1)
            g.DrawString(
                $"Life: {health}",
                new Font("Arial", 12),
                Brushes.Black,
                new PointF(
                    size.Width / 2 - 100,
                    size.Height
                )
            );

            else
            g.DrawString(
                $"Life: {health}",
                new Font("Arial", 12),
                Brushes.Black,
                new PointF(
                    size.Width / 2 + 100,
                    size.Height
                )
            );

            return;
        }


        g.DrawRectangle(
            Pens.Black,
            new RectangleF(
                position.X, position.Y,
                size.Width,
                size.Height
            )
        );
        
        if (Player == 1)
            g.FillRectangle(
                Brushes.DarkBlue,
                new RectangleF(
                    position.X + (size.Width - (health * size.Width / 1000)),
                    position.Y + 1,
                    health * size.Width / 1000 - 1,
                    size.Height
                )
            );
        else
            g.FillRectangle(
                Brushes.DarkBlue,
                new RectangleF(
                    position.X,
                    position.Y + 1,
                    health * size.Width / 1000 - 1,
                    size.Height
                )
            );
    }
    
}