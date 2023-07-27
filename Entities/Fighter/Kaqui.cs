using static Stage;


public class Kaqui : Fighter
{

    public Kaqui ( PointF inititalPosition) : base(inititalPosition)
    {
        this.Image = new Bitmap ("./Assets/Sprites/Kaqui.png");
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
        setLightKickFrames();
        setMediumKickFrames();
    }
    #region Override SetFrames
    private void setIdleFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 1;
        
        for (int i = 0; i < 4; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.Idle, frames);
    }
    private void setBackwardFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 1;
        
        for (int i = 0; i < 5; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.Backward, frames);
    }
    private void setForwardFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 1;
        
        for (int i = 4; i >= 0; i--)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.Forward, frames);
    }
    private void setJumpingFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 4;
        
        for (int i = 0; i < 3; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.Jump, frames);

        frames = new List<Frame>();
        row = 5;
        
        for (int i = 0; i < 5; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.JumpForward, frames);

        frames = new List<Frame>();
        row = 5;
        
        for (int i = 4; i >= 0; i--)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.JumpBackward, frames);
    }
    private void setCrouchFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 2;
        
        for (int i = 0; i < 3; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.CrouchDown, frames);
        frames = new List<Frame>();
        
        for (int i = 2; i >= 0; i--)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.CrouchUp, frames);

        frames = new List<Frame>();
        frames.Add(new Frame(
                new Point(0 + (110 * 2), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));

        this.Frames.Add(States.Crouch, frames);
    }
    private void setLightPunchFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 3;
        
        frames.Add(new Frame(
            new Point(0 + (110), 0 + (row  * 230)),
            new Size(130, 230),
            new Point(0,0)
        ));
        frames.Add(new Frame(
            new Point(0 + (130), 0 + (row  * 230)),
            new Size(120, 230),
            new Point(0,0)
        ));
        frames.Add(new Frame(
            new Point(0 + (250), 0 + (row  * 230)),
            new Size(120, 230),
            new Point(0,0)
        ));

        this.Frames.Add(States.LightPunch, frames);
    }
    private void setMediumPunchFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 3;
        
        for (int i = 0; i < 5; i++)
        {
            frames.Add(new Frame(
                new Point(440 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }

        this.Frames.Add(States.MediumPunch, frames);
    }
    private void setLightKickFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 6;
        
        for (int i = 0; i < 2; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }
        frames.Add(new Frame(
            new Point((110 * 2), 0 + (row  * 230)),
            new Size(220, 230),
            new Point(0,0)
        ));
        frames.Add(new Frame(
            new Point((110 * 4), 0 + (row  * 230)),
            new Size(110, 230),
            new Point(0,0)
        ));
        this.Frames.Add(States.LightKick, frames);
    }
    private void setMediumKickFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 7;
        
        for (int i = 0; i < 3; i++)
        {
            frames.Add(new Frame(
                new Point(0 + (110 * i), 0 + (row  * 230)),
                new Size(110, 230),
                new Point(0,0)
            ));
        }
        frames.Add(new Frame(
            new Point((110 * 3), 0 + (row  * 230)),
            new Size(170, 230),
            new Point(0,0)
        ));
        frames.Add(new Frame(
            new Point(500, 0 + (row  * 230)),
            new Size(110, 230),
            new Point(0,0)
        ));
        frames.Add(new Frame(
            new Point(620, 0 + (row  * 230)),
            new Size(110, 230),
            new Point(0,0)
        ));
        frames.Add(new Frame(
            new Point(740, 0 + (row  * 230)),
            new Size(110, 230),
            new Point(0,0)
        ));
        this.Frames.Add(States.MediumKick, frames);
    }
    #endregion

    #region Override SetHitboxes
    //TODO: ========================================================================================================================
    protected override void setHitboxes()
    {
        for (int i = 0; i < Frames[States.Idle].Count; i++)
            Frames[States.Idle][i].HurtBox = 
                new RectangleF( this.Position.X, this.Position.Y, 51, 117 );
        for (int i = 0; i < Frames[States.Forward].Count; i++)
            Frames[States.Forward][i].HurtBox = 
                new RectangleF( this.Position.X, this.Position.Y, 51, 117 );
        for (int i = 0; i < Frames[States.Backward].Count; i++)
            Frames[States.Backward][i].HurtBox = 
                new RectangleF( this.Position.X, this.Position.Y, 51, 117 );
    }

    #endregion
}