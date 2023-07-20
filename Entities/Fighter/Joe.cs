using static Stage;

public class Joe : Fighter
{
    public Joe()
    {
        this.Image =  new Bitmap("./Assets/Sprites/Joe.png");
        this.Position = new Point(500, 1080 - STAGE_FLOOR);
        this.Size = new Size(200, 236);
        this.AnimationTimer = 1;
        Direction = FighterDirection.LEFT;

        setForwardFrames();
        setBackwardFrames();
        setIdleFrames();
        setJumpingFrames();
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
        List<Frame> frames = new List<Frame>();
        int row = 1;

        for (int i = 0; i < 5; i++)
        {
            frames.Add( 
                new Frame(
                    new Point(360 + (100 * i), 179 + (118 * row)),
                    new Size(100, 118),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(States.Forward, frames);
    }
    void setBackwardFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 2;

        for (int i = 0; i < 5; i++)
        {
            frames.Add(
                new Frame(
                    new Point(360 + (100 * i), 179 + (118 * row)),
                    new Size(100, 118),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(
            States.Backward,
            frames
        );
    }
    void setIdleFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 0;

        for (int i = 0; i < 5; i++)
        {
            frames.Add(
                new Frame(
                    new Point(360 + (100 * i), 179 + (118 * row)),
                    new Size(100, 118),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(
            States.Idle,
            frames
        );
    }
    void setJumpingFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 4;

        for (int i = 0; i < 5; i++)
        {
            frames.Add(
                new Frame(
                    new Point(360 + (100 * i), 179 + (118 * row)),
                    new Size(100, 118),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(
            States.Jump,
            frames
        );
    }
}