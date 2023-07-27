using static Stage;

public class Joe : Fighter
{
    public Joe(PointF initialPosition) : base(initialPosition)
    {
        this.Image =  new Bitmap("./Assets/Sprites/Joe.png");
    }
    protected override void setFrames()
    {
        setIdleFrames();
        setForwardFrames();
        setBackwardFrames();
        setJumpingFrames();
        setCrouchFrames();
        setLightPunchFrames();
        setMediumPunchFrames();
    }

    #region Override SetFrames
    private void setForwardFrames()
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
    private void setBackwardFrames()
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
    private void setIdleFrames()
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
    private void setJumpingFrames()
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
    private void setJumpingFFrames()
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
    private void setJumpingBFrames()
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
    private void setCrouchFrames()
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
    private void setLightPunchFrames()
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
    private void setMediumPunchFrames()
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
    private void setCrouchUpFrames()
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

        Frames.Add(
            States.CrouchDown,
            frames
        );
    }
    #endregion 
}