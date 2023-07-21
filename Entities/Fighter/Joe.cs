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
        

        setIdleFrames();
        this.Frame = Frames[States.Idle][0];
        setIdleHurtboxes();
        setForwardFrames();
        setBackwardFrames();
        setJumpingFrames();
        setCrouchFrames();
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

        this.Frame.HurtBox = new RectangleF(
            this.Rectangle.X + 30,
            this.Rectangle.Y + 30,
            this.Frame.HurtBox.Width,
            this.Frame.HurtBox.Height   
        );
        
        if (DateTime.Now - lastFrame > TimeSpan.FromMilliseconds(60))
        {
            AnimationFrame++;
            lastFrame = DateTime.Now;
        }

        if (AnimationFrame >= Frames[CurrentState].Count)
            AnimationFrame = 0;
        
        
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

    //TODO :: new frame is here!!!
    void setJumpingFrames()
    {
        RectangleF[] hurtboxes = new RectangleF[5];
        // Rectangle hitboxes = new Rectangle();
        // Rectangle pushboxes = new Rectangle();
        // Rectangle throwboxes = new Rectangle();
        
        List<Frame> frames = new List<Frame>();
        int row = 4;

        for (int i = 0; i < 5; i++)
        {
            Frame frame = new Frame(
                new Point(360 + (100 * i), 179 + (118 * row)),
                new Size(100, 118),
                new Point(0, 0)
            );
            frames.Add(frame);
        }


        this.Frames.Add(
            States.Jump,
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

        for (int i = 1; i > 0; i--)
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

    void setIdleHurtboxes()
    {
        var hurtboxes = new RectangleF[] {
            new RectangleF(0, 0, 72, 115),        
            new RectangleF(0, 0, 61, 105),
            new RectangleF(0, 0, 61, 80),
            new RectangleF(0, 0, 61, 105),
            new RectangleF(0, 0, 72, 115),
        };

        for (int i = 0; i < Frames[States.Idle].Count; i++)
            Frames[States.Idle][i].HurtBox = hurtboxes[i];
    }
}