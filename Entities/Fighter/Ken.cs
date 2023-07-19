using static Stage;

public class Ken : Fighter
{
    public Ken()
    {
        this.Image =  new Bitmap("./Assets/Sprites/Guilherme.png");
        this.Position = new Point(0, STAGE_FLOOR);
        this.Size = new Size(300, 300);
        this.Velocity = 75;
        this.AnimationTimer = 1;

        setFowardFrames();
    }

    public override void Draw(Graphics g)
    {
        var frame = Frames[AnimationName][AnimationFrame];
        g.DrawImage(
            this.Image,
            new RectangleF(
                this.Rectangle.X - frame.OriginPoint.X,
                this.Rectangle.Y - frame.OriginPoint.Y,
                this.Rectangle.Width,
                this.Rectangle.Height
            ),
            frame.ToRectangle(),
            GraphicsUnit.Pixel
        );

        this.DrawDebug(g);
    }

    public override void Update(Graphics g, TimeSpan t)
    {
        this.MoveX(t);
        if (Position.X > 1920 || Position.X < 0)
            this.Velocity = -this.Velocity;
        
        if (DateTime.Now - lastFrame > TimeSpan.FromMilliseconds(60))
        {
            AnimationFrame++;
            lastFrame = DateTime.Now;
        }

        if (AnimationFrame >= Frames[AnimationName].Count)
            AnimationFrame = 0;
    }

    public override void DrawDebug(Graphics g)
    {
        var frame = Frames[AnimationName][AnimationFrame];

        g.FillRectangle(
            Brushes.Red,
            new RectangleF(
                this.Rectangle.X - frame.OriginPoint.X - 5,
                this.Rectangle.Y - frame.OriginPoint.Y - 5,
                10,
                10
            )
        );
    }


    //! ANIMATION FRAMES AND SPRITES
    void setFowardFrames()
    {
        List<Frame> fowardFrames = new List<Frame>();

        fowardFrames.Add(
            new Frame(
                new Point(170, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 1, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 2, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 2, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 3, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 4, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 5, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );

        this.Frames.Add(AnimationName.Foward, fowardFrames);
    }
    void setBackwardFrames()
    {
        List<Frame> backwardFrames = new List<Frame>();

        for (int i = 0; i < 6; i++)
        {
            backwardFrames.Add(
                new Frame(
                    new Point(718 + 77 * i, 150),
                    new Size(77, 105),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(
            AnimationName.Backward,
            backwardFrames
        );
    }
}