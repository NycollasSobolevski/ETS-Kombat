#pragma warning disable


public abstract class Fighter : Entity
{
    // *Basic Props
    public Dictionary<States, List<Frame>> Frames { get; set; } = new Dictionary<States, List<Frame>>();
    public States CurrentState { get; set; } = States.Idle;
    public VelocityClass Velocity { get; set; } = new VelocityClass(0 ,0);
    public FighterDirection Direction { get; set; } = FighterDirection.LEFT;
    public int AnimationFrame { get; set; } = 0;
    public Size ScreenSize;
    public Bitmap Image { get; set; }
    public PointF Position { get; set; }
    public Size Size { get; set; }
    public int Gravity { get; set; } = 0;
    public Frame Frame { get; set; }
    public RectangleF Rectangle {
        get {
            return new RectangleF(
                Position.X + Frame.OriginPoint.X,
                Position.Y - Frame.OriginPoint.Y,
                Size.Width,
                Size.Height
            );
        }
        private set{ }
    }
    protected DateTime lastFrame = DateTime.Now;
    protected int AnimationTimer { get; set; }
    protected bool isJumping = false;
    protected bool isCrouching = false;
    
    
    // !FUNCTIONS
    public abstract void Update(Graphics g, TimeSpan t);
    public abstract void Draw(Graphics g);
    public virtual void DrawDebug(Graphics g)
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

        if (this.Frame.HitBox != null)
            g.DrawRectangle(
                Pens.Red,
                new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Frame.HitBox.Width,
                    (int)Frame.HitBox.Height
                )
            );
        
        if (this.Frame.HurtBox != null)
            g.DrawRectangle(
                Pens.Blue,
                new Rectangle(
                    (int)Frame.HurtBox.X,
                    (int)Frame.HurtBox.Y,
                    (int)Frame.HurtBox.Width,
                    (int)Frame.HurtBox.Height
                )
            );
        
        if (this.Frame.PushBox != null)
                g.DrawRectangle(
                    Pens.Green,
                    new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Frame.PushBox.Width,
                    (int)Frame.PushBox.Height
                )
                );

        if (this.Frame.ThrowBox != null)
                g.DrawRectangle(
                    Pens.Black,
                    new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Frame.ThrowBox.Width,
                    (int)Frame.ThrowBox.Height
                )
                );
        
        
    }
    public void Move(TimeSpan t)
    {
        this.Velocity.Y += (float)(this.Gravity * t.TotalSeconds);
        this.Position = new PointF(
            (float)(this.Position.X + (this.Velocity.X * t.TotalSeconds)),
            (float)(this.Position.Y + this.Velocity.Y * t.TotalSeconds)
        ); 
    }
    public void UpdateStageConstraints()
    {
        if (this.Position.X > ScreenSize.Width - Stage.FIGHTER_WIDTH - this.Size.Width)
            this.Position = new PointF(ScreenSize.Width - Stage.FIGHTER_WIDTH - this.Size.Width, this.Position.Y);
    
        if (this.Position.X < 0 + Stage.FIGHTER_WIDTH)
            this.Position = new PointF(Stage.FIGHTER_WIDTH, this.Position.Y);
        
        if (this.Position.Y > this.ScreenSize.Height - Stage.STAGE_FLOOR)
        {
            this.Position = new Point((int)this.Position.X, this.ScreenSize.Height - Stage.STAGE_FLOOR);
            this.CurrentState = States.Idle;
        }
            
    }
    public void ChangeState(States state)
    {
        switch(state)
        {
            case States.Backward:
                handleWalkingLeft();
                break;
            
            case States.Forward:
                handleWalkingRight();
                break;
            
            case States.Idle:
                handleIdle();
                break;
            
            case States.Jump:
                handleJump();
                break;
            
            case States.CrouchDown:
                handleCrouchDown();
                break;
            
            case States.Crouch:
                handleCrouch();
                break;
            
            case States.CrouchUp:
                handleCrouchUp();
                break;
        }
    }

    // ?Basic Movement Functions
    public void handleWalkingLeft()
    {
        this.Velocity.X = -250;
        this.CurrentState = States.Backward;
    }
    public void handleWalkingRight()
    {
        this.Velocity.X = 250;
        this.CurrentState = States.Forward;
    }
    public void handleIdle()
    {
        this.Velocity.X = 0;
        this.Velocity.Y = 0;
        this.Gravity = 0;
        this.CurrentState = States.Idle;
        isJumping = false;
    }
    public void handleCrouchDown()
    {
        this.CurrentState = States.CrouchDown;
        isCrouching = AnimationFrame >= 1;

        if (isCrouching)
            this.CurrentState = States.Crouch;
    }
    public void handleCrouchUp()
    {
        this.CurrentState = States.CrouchUp;
        isCrouching = AnimationFrame >= 1;

        if (isCrouching)
            this.CurrentState = States.Idle;
    }
    public void handleCrouch()
    {
        this.CurrentState = States.Crouch;
        isCrouching = false;

        this.Velocity.X = 0;
        this.Velocity.Y = 0;
    }
    public void handleJump()
    { 
        if (!isJumping)
        {
            this.Velocity.Y = -800;
            this.Gravity = 1500;
            this.isJumping = true;
        }

        this.CurrentState = States.Jump;
    }
    public void handleJumpForward()
    {
        if (!isJumping)
        {
            this.Velocity.Y = -800;
            this.Velocity.X = 250;
            this.Gravity = 1500;
            this.isJumping = true;
        }

        this.CurrentState = States.JumpForward;
    }
    public void handleJumpBackward()
    {
        if (!isJumping)
        {
            this.Velocity.Y = -800;
            this.Velocity.X = -250;
            this.Gravity = 1500;
            this.isJumping = true;
        }

        this.CurrentState = States.JumpBackward;
    }
}