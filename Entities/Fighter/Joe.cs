using static Stage;

public class Joe : Fighter
{
    public Joe()
    {
        this.Image =  new Bitmap("./Assets/Sprites/Joe.png");            //! DOTNET RUN
        // this.Image =  new Bitmap("../../../Assets/Sprites/Joe.png");        //! DEBUG
        this.Position = new Point(800, 1080 - STAGE_FLOOR);
        this.Size = new Size(200, 236);
        this.AnimationTimer = 1;
        Direction = FighterDirection.LEFT;
            
    }

    public override void Update(Graphics g, DateTime t)
    {
        Move(t);

        Frame.HurtBox = new RectangleF(
            this.Rectangle.X,
            this.Rectangle.Y,
            Frame.HurtBox.Width,
            Frame.HurtBox.Height   
        );

        Frame.HitBox = new RectangleF(
            this.Rectangle.X + Frame.HitBoxInit.X,
            this.Rectangle.Y + Frame.HitBoxInit.Y,
            Frame.HitBoxInit.Width,
            Frame.HitBoxInit.Height   
        );

        this.UpdateAnimation(t);
        this.UpdateStageConstraints();
    }

    #region 
    protected override void setForwardFrames()
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

        Frames.Add(States.Forward, frames);
    }
protected override    void setBackwardFrames()
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

        Frames.Add(
            States.Backward,
            frames
        );
    }
protected override    void setIdleFrames()
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

        Frames.Add(
            States.Idle,
            frames
        );
    }
protected override    void setJumpingFrames()
    {
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
        Frames.Add(
            States.Jump,
            frames
        );
        setJumpingBFrames();   
        setJumpingFFrames();   
    }
    protected override void setJumpingFFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 4;

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

        Frames.Add(
            States.JumpForward,
            frames
        );
    }
    protected override void setJumpingBFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 4;

        for (int i = 4; i >= 0 ; i--)
        {
            frames.Add(
                new Frame(
                    new Point(360 + (100 * i), 179 + (118 * row)),
                    new Size(100, 118),
                    new Point(0, 0)
                )
            );
        }

        Frames.Add(
            States.JumpBackward,
            frames
        );
    }
protected override    void setCrouchFrames()
    {

        List<Frame> frames = new List<Frame>();
        int row = 6 ;

        frames.Add(new Frame(
            new Point(360 + 200, 179 + (118 * row)),
            new Size(100, 118),
            new Point(0, 0)
        ));

        Frames.Add(
            States.Crouch,
            frames
        );

        setCrouchDownFrames();
        setCrouchUpFrames();
    }
protected override    void setLightPunchFrames()
    {
        List<Frame> frames = new();
        int row = 3 ;

        for (int i = 0; i < 2; i++)
        {
            Frame frame = new Frame(
                new Point(360 + (120 * i), 179 + (118 * row)),
                new Size(120, 118),
                new Point(0, 0)
            );
            frames.Add(frame);
        }
        
        Frames.Add(
            States.LightPunch,
            frames
        );
    }    
protected override    void setMediumPunchFrames()
    {
        List<Frame> frames = new();
        int row = 3 ;

        for (int i = 0; i < 2; i++)
        {
            Frame frame = new Frame(
                new Point(660 + (120 * i), 179 + (118 * row)),
                new Size(120, 118),
                new Point(0, 0)
            );
            frames.Add(frame);
        }
        
        Frames.Add(
            States.MediumPunch,
            frames
        );
    }    
    protected override void setCrouchUpFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 6;

        for (int i = 1; i >= 0; i--)
        {
            frames.Add(
                new Frame(
                    new Point(360 + (100 * i), 179 + (118 * row)),
                    new Size(100, 118),
                    new Point(0, 0)
                )
            );
        }

        Frames.Add(
            States.CrouchUp,
            frames
        );
    }
    protected override void setCrouchDownFrames()
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

        Frames.Add(
            States.CrouchDown,
            frames
        );
    }
    
    #endregion 
}