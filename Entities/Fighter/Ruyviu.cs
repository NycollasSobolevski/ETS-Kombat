using static Stage;

public class Ruyviu : Fighter
{
    public Ruyviu()
    {
        this.Image =  new Bitmap("./Assets/Sprites/Ruyviu.png");
        this.Position = new Point(500, 1080 - STAGE_FLOOR);
        this.Size = new Size(200, 236);
        this.AnimationTimer = 1;
        Direction = FighterDirection.RIGHT;

        setForwardFrames();
        setBackwardFrames();
        setIdleFrames();
        setJumpingFrames();
        setCrouchFrames();
    }

    public override void Draw(Graphics g)
    {
        var container = g.BeginContainer();

        Frame = Frames[CurrentState][AnimationFrame];

        ChangeState(CurrentState);
        this.ChangeSpriteDirectionX(g);

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
        var container = g.BeginContainer();

        this.Move(t);
        this.UpdateStageConstraints();
        
        if (DateTime.Now - lastFrame > TimeSpan.FromMilliseconds(60))
        {
            AnimationFrame++;
            lastFrame = DateTime.Now;
        }

        if (AnimationFrame >= Frames[CurrentState].Count)
            AnimationFrame = 0;

        g.EndContainer(container);
    }

    //! ANIMATION FRAMES AND SPRITES
    void setForwardFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 1;

        for (int i = 0; i < 6; i++)
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

        for (int i = 0; i < 6; i++)
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

        for (int i = 0; i < 4; i++)
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
    void setCrouchFrames()
    {
        setCrouchDownFrames();
        setCrouchUpFrames();

        List<Frame> frames = new List<Frame>();
        int row = 6 ;

        frames.Add(new Frame(
            new Point(360 + 200, 179 + (118 * row)),
            new Size(100, 118),
            new Point(0, 0)
        ));

        this.Frames.Add(
            States.Crouch,
            frames
        );
    }
    private void setCrouchUpFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 6;

        for (int i = 2; i > 0; i--)
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
            States.CrouchUp,
            frames
        );
    }
    private void setCrouchDownFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 6;

        for (int i = 0; i < 2; i++)
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
            States.CrouchDown,
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

        this.setJumpingFFrames();
        this.setJumpingBFrames();
    }
    private void setJumpingFFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 7;

        for (int i = 0; i < 5 ; i++)
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
            States.JumpForward,
            frames
        );
    }
    private void setJumpingBFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 7;

        for (int i = 5; i >= 0 ; i--)
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
            States.JumpBackward,
            frames
        );
    }
}