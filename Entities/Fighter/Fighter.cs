#pragma warning disable


public abstract class Fighter : Entity
{   
    // !Static

    protected static Dictionary<States, States[]> validStates = new Dictionary<States, States[]>() {
        {States.Backward, new States[] {States.Backward, States.Idle}},
        {States.Forward, new States[] {States.Forward, States.Idle}},
        
        {States.Jump, new States[] {States.Jump, States.Idle}},
        {States.JumpForward, new States[] {States.JumpForward, States.Idle}},
        {States.JumpBackward, new States[] {States.JumpBackward, States.Idle}},
        
        {States.CrouchDown, new States[] {States.CrouchDown, States.Idle}},
        {States.Crouch, new States[] {States.CrouchDown}},
        {States.CrouchUp, new States[] {States.Crouch}},

        {States.LightKick, new States[] {States.LightKick, States.Idle, States.Crouch, States.LightPunch, States.HeavyKick}},

        {States.Idle, new States[] {States.Idle, States.Backward, States.Forward, States.Crouch, States.Jump, States.LightKick, States.MediumKick, States.HeavyKick, States.LightPunch, States.MediumPunch, States.HeavyPunch}},
    };


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
    public Frame Frame { get; protected set; }
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
    protected int AnimationTimer { get; set; } = 300;
    protected bool isJumping = false;
    protected bool isCrouching = false;
    public Fighter Enemy { get; set; }
    
    // !FUNCTIONS
    public abstract void Update(Graphics g, DateTime t);
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

        if (Frame.HitBox != null)
            g.DrawRectangle(
                Pens.Red,
                new Rectangle(
                    (int)Frame.HitBox.X,
                    (int)Frame.HitBox.Y,
                    (int)Frame.HitBox.Width,
                    (int)Frame.HitBox.Height
                )
            );
        
        if (Frame.HurtBox != null)
            g.DrawRectangle(
                Pens.Blue,
                new Rectangle(
                    (int)Frame.HurtBox.X,
                    (int)Frame.HurtBox.Y,
                    (int)Frame.HurtBox.Width,
                    (int)Frame.HurtBox.Height
                )
            );
        
        if (Frame.PushBox != null)
                g.DrawRectangle(
                    Pens.Green,
                    new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Frame.PushBox.Width,
                    (int)Frame.PushBox.Height
                )
                );

        if (Frame.ThrowBox != null)
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
        if (validStates[state].Contains(state))
            switch(state)
            {
                case States.Backward:
                    initBackward();
                    break;
                case States.Forward:
                    initForward();
                    break;
                case States.Idle:
                    initIdle();
                    break;
                case States.CrouchDown:
                    initCrouchDown();
                    break;
                case States.CrouchUp:
                    initCrouchUp();
                    break;
                case States.Crouch:
                    initCrouch();
                    break;
                case States.Jump:
                    initJump();
                    break;
                case States.JumpForward:
                    initJumpForward();
                    break;
                case States.JumpBackward:
                    initJumpBackward();
                    break;
                case States.LightKick:
                    initLightKick();
                    break;
                case States.MediumKick:
                    initMediumKick();
                    break;
                case States.HeavyKick:
                    initHeavyKick();
                    break;
                case States.LightPunch:
                    initLightPunch();
                    break;
                case States.MediumPunch:
                    initMediumPunch();
                    break;
                case States.HeavyPunch:
                    initHeavyPunch();
                    break;
            }
    }

    public void Move(DateTime t)
    {
        this.Position = new PointF(this.Position.X + Velocity.X * this.AnimationTimer, this.Position.Y + Velocity.Y * this.AnimationTimer);
    }
    
    // *Change SpriteDirection

    public void UpdateAnimation(DateTime t)
    {
        if ((t - lastFrame).TotalMilliseconds >= AnimationTimer)
        {
            AnimationFrame++;
            lastFrame = DateTime.Now;
        }

        if (AnimationFrame >= Frames[CurrentState].Count)
            AnimationFrame = 0;
        
        Frame = Frames[CurrentState][AnimationFrame];
    }
    public void ChangeSpriteDirectionX(Graphics g)
    {
        getDirection();
        int directionValue = (int)(Direction);
        if (directionValue == -1)
            g.TranslateTransform((this.Position.X + 2 * Frame.Size.Width) * 2, 0);
        g.ScaleTransform(directionValue, 1);
    }
    public void getDirection()
        => this.Direction = this.Position.X > Enemy.Position.X?
            FighterDirection.LEFT : FighterDirection.RIGHT;
    
    #region Handle
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

    // ?Basic Attack Functions
    public void handleLightKick()
    {
        this.CurrentState = States.LightKick;
    }
    public void handleMediumKick()
    {
        this.CurrentState = States.MediumKick;
    }
    public void handleHeavyKick()
    {
        this.CurrentState = States.HeavyKick;
    }
    public void handleLightPunch()
    {
        this.CurrentState = States.LightPunch;
    }
    public void handleMediumPunch()
    {
        this.CurrentState = States.MediumPunch;
    }
    public void handleHeavyPunch()
    {
        this.CurrentState = States.HeavyPunch;
    }
    
    #endregion

    #region StateInit
    // ?State Init Functions
    public void initIdle()
    {
        this.Velocity.X = 0;

        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.Idle][AnimationFrame];
    }
    public void initBackward()
    {
        this.Velocity.X = -250 * (int)Direction;

        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.Backward][AnimationFrame];
    }
    public void initForward()
    {
        this.Velocity.X = 250 * (int)Direction;

        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.Forward][AnimationFrame];
    }
    public void initCrouchDown()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.CrouchDown][AnimationFrame];
    }
    public void initCrouchUp()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.CrouchUp][AnimationFrame];
    }
    public void initCrouch()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.Crouch][AnimationFrame];
    }
    public void initJump()
    {
        this.Velocity.Y = -800;

        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.Jump][AnimationFrame];
    }
    public void initJumpForward()
    {
        this.Velocity.X = 250 * (int)Direction;

        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.JumpForward][AnimationFrame];
    }
    public void initJumpBackward()
    {
        this.Velocity.X = -250 * (int)Direction;

        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.JumpBackward][AnimationFrame];
    }
    public void initLightKick()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.LightKick][AnimationFrame];
    }
    public void initMediumKick()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.MediumKick][AnimationFrame];
    }
    public void initHeavyKick()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.HeavyKick][AnimationFrame];
    }
    public void initLightPunch()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.LightPunch][AnimationFrame];
    }
    public void initMediumPunch()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.MediumPunch][AnimationFrame];
    }
    public void initHeavyPunch()
    {
        this.AnimationTimer = 0;
        this.AnimationFrame = 0;
        Frame = Frames[States.HeavyPunch][AnimationFrame];
    }
    #endregion

}