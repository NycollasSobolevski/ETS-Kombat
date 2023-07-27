using static Stage;

public class Ruyviu : Fighter
{
    public Ruyviu(PointF initialPosition)
    {
        this.Image =  new Bitmap("./Assets/Sprites/Ruyviu.png");
        this.Position = new PointF(initialPosition.X, initialPosition.Y - STAGE_FLOOR);
        this.Size = new Size(200, 236);
        this.AnimationTimer = 1;
        Direction = FighterDirection.RIGHT;
    }

    #region Override SetFrames
    protected override void setForwardFrames()
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
    protected override void setBackwardFrames()
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
    protected override void setIdleFrames()
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
        for (int i = 3; i > 1; i--)
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
    protected override void setCrouchFrames()
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
    protected override void setCrouchUpFrames()
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

        this.Frames.Add(
            States.CrouchDown,
            frames
        );
    }
    protected override void setJumpingFrames()
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
    protected override void setJumpingFFrames()
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
    protected override void setJumpingBFrames()
    {
        List<Frame> frames = new List<Frame>();
        int row = 7;

        for (int i = 4; i > 0 ; i--)
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

    #endregion
}