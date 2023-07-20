using static Stage;

public class Ken : Fighter
{
    public Ken()
    {
        this.Image =  new Bitmap("./Assets/Sprites/Joe Sprites.png");
        this.Position = new Point(500, STAGE_FLOOR);
        this.Size = new Size(300, 300);
        this.AnimationTimer = 1;
        Direction = FighterDirection.LEFT;

        setForwardFrames();
        setBackwardFrames();
        setIdleFrames();
    }

    public override void Draw(Graphics g)
    {
        var container = g.BeginContainer();

        Frame = Frames[CurrentState][AnimationFrame];

        ChangeState(CurrentState);

        g.DrawImage(
            this.Image,
            this.Rectangle,
            Frame.ToRectangle(),
            GraphicsUnit.Pixel
        );

        g.EndContainer(container);
        this.DrawDebug(g);
    }

    public override void Update(Graphics g, TimeSpan t)
    {
        this.Move(t);
        this.UpdateStageConstraints();
        
        if (DateTime.Now - lastFrame > TimeSpan.FromMilliseconds(60))
        {
            AnimationFrame++;
            lastFrame = DateTime.Now;
        }

        if (AnimationFrame >= Frames[CurrentState].Count)
            AnimationFrame = 0;
    }

    public override void DrawDebug(Graphics g)
    {
        g.FillRectangle(
            Brushes.Red,
            new RectangleF(
                Rectangle.X + Frame.OriginPoint.X,
                Rectangle.Y + Frame.OriginPoint.Y,
                5,
                5
            )
        );
    }


    //! ANIMATION FRAMES AND SPRITES
    void setForwardFrames()
    {
        List<Frame> forward = new List<Frame>();

        for (int i = 0; i < 6; i++)
        {
            forward.Add(
                new Frame(
                    new Point(360 + (100 * i), 279),
                    new Size(100, 100),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(States.Forward, forward);
    }
    void setBackwardFrames()
    {
        List<Frame> backwardFrames = new List<Frame>();

        for (int i = 0; i < 6; i++)
        {
            backwardFrames.Add(
                new Frame(
                    new Point(360 + (100 * i), 379),
                    new Size(100, 100),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(
            States.Backward,
            backwardFrames
        );
    }

    void setIdleFrames()
    {
        List<Frame> idle = new List<Frame>();

        for (int i = 0; i < 6; i++)
        {
            idle.Add(
                new Frame(
                    new Point(360 + (100 * i), 179),
                    new Size(100, 100),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(States.Idle, idle);
    }
}