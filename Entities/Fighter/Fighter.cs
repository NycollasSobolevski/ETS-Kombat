#pragma warning disable


public abstract class Fighter : Entity
{   
    // *Basic Props
    # region BasicProps
    public abstract void Update(Graphics g, DateTime t);
    public abstract void Draw(Graphics g);
    // protected static Dictionary<States, States[]> validStates = new Dictionary<States, States[]>() {
    //     {States.Backward, new States[] {States.Backward, States.Idle}},
    //     {States.Forward, new States[] {States.Forward, States.Idle}},
        
    //     {States.Jump, new States[] {States.Jump, States.Idle}},
    //     {States.JumpForward, new States[] {States.JumpForward, States.Idle}},
    //     {States.JumpBackward, new States[] {States.JumpBackward, States.Idle}},
        
    //     {States.CrouchDown, new States[] {States.CrouchDown, States.Idle}},
    //     {States.Crouch, new States[] {States.CrouchDown}},
    //     {States.CrouchUp, new States[] {States.Crouch}},

    //     {States.LightKick, new States[] {States.LightKick, States.Idle, States.Crouch, States.LightPunch, States.HeavyKick}},

    //     {States.Idle, new States[] {States.Idle, States.Backward, States.Forward, States.Crouch, States.Jump, States.LightKick, States.MediumKick, States.HeavyKick, States.LightPunch, States.MediumPunch, States.HeavyPunch}},
    // };
    public Dictionary<States, List<Frame>> Frames { get; set; } = new Dictionary<States, List<Frame>>();
    public States CurrentState { get; set; }
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
    public Dictionary<States, FighterStateObject> StateObjects;
    #endregion
    public Fighter()
    {
        StateObjects = new Dictionary<States, FighterStateObject>()
        {
            {   
                States.Idle,
                new FighterStateObject(initIdle, handleIdle,
                new List<States>(){
                    States.Backward, States.Forward, States.CrouchUp, States.Jump
                })
            },
            {
                States.Jump,
                new FighterStateObject(initJump, handleJump,
                new List<States>() {
                    States.Idle,
                })
            },
            {
                States.JumpBackward,
                new FighterStateObject(initJumpBackward, handleJumpBackward,
                new List<States>(){
                    States.Idle, States.Backward
                })
            },
            {
                States.JumpForward,
                new FighterStateObject(initJumpForward, handleJumpForward,
                new List<States>(){
                    States.Idle, States.Forward
                })
            },
            {
                States.Backward,
                new FighterStateObject(initBackward, handleBackward,
                new List<States>(){
                    States.Idle, States.JumpBackward
                })
            },
            {
                States.Forward,
                new FighterStateObject(initForward, handleForward,
                new List<States>(){
                    States.Idle, States.JumpForward
                })
            },
            {
                States.CrouchDown,
                new FighterStateObject(initCrouchDown, handleCrouchDown,
                new List<States>(){
                    States.Idle
                })
            },
            {
                States.CrouchUp,
                new FighterStateObject(initCrouchUp, handleCrouchUp,
                new List<States>(){
                    States.Crouch
                })
            },
            {
                States.Crouch,
                new FighterStateObject(initCrouch, handleCrouch,
                new List<States>(){
                    States.CrouchDown
                })
            }
        };
    }

    // !FUNCTIONS
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
        g.DrawString(
            $"Velocity: X {this.Velocity.X} | Y {this.Velocity.Y}" +
            $"Position: X {this.Position.X}| Y {this.Position.Y}" +
            $"State: {this.CurrentState}",
            new Font("arial", 10),
            Brushes.Black,
            new PointF(this.Position.X, 0)
        );        
    }
    public void ChangeState(States newState)
    {
        if (StateObjects[newState].ValidFrom.Contains(this.CurrentState) || newState == this.CurrentState)
            switch(newState)
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
    public void Move(DateTime t)
    {
        var calc = (t - lastFrame).TotalSeconds;
        this.Position = new PointF(
            (float)(this.Position.X + Velocity.X * calc),
            (float)(this.Position.Y + Velocity.Y * calc)
        );
    }
    
    # region SpriteDirection
    public void UpdateAnimation(DateTime t)
    {
        AnimationTimer = 60;
        if ((t - lastFrame).TotalMilliseconds > AnimationTimer)
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
    
    #endregion
    #region Handle
    // ?Basic Movement Functions
    public void handleBackward(DateTime t)
    {
        
    }
    public void handleForward(DateTime t)
    {

    }
    public void handleIdle(DateTime t)
    {

    }
    public void handleCrouchDown(DateTime t)
    {
        this.CurrentState = States.CrouchDown;
        isCrouching = AnimationFrame >= 1;

        if (isCrouching)
            this.CurrentState = States.Crouch;
    }
    public void handleCrouchUp(DateTime t)
    {
        this.CurrentState = States.CrouchUp;
        isCrouching = AnimationFrame >= 1;

        if (isCrouching)
            this.CurrentState = States.Idle;
    }
    public void handleCrouch(DateTime t)
    {
        this.CurrentState = States.Crouch;
        isCrouching = false;

        this.Velocity.X = 0;
        this.Velocity.Y = 0;
    }
    public void handleJump(DateTime t)
    { 
        this.Velocity.Y += (this.Gravity * (t - lastFrame).Seconds);
        this.CurrentState = States.Jump;
    }
    public void handleJumpForward(DateTime t)
    {

    }
    public void handleJumpBackward(DateTime t)
    {

    }
    public void handleLightKick(DateTime t)
    {
        this.CurrentState = States.LightKick;
    }
    public void handleMediumKick(DateTime t)
    {
        this.CurrentState = States.MediumKick;
    }
    public void handleHeavyKick(DateTime t)
    {
        this.CurrentState = States.HeavyKick;
    }
    public void handleLightPunch(DateTime t)
    {
        this.CurrentState = States.LightPunch;
    }
    public void handleMediumPunch(DateTime t)
    {
        this.CurrentState = States.MediumPunch;
    }
    public void handleHeavyPunch(DateTime t)
    {
        this.CurrentState = States.HeavyPunch;
    }
    #endregion
    #region StateInit
    // ?State Init Functions
    public void initIdle()
    {
        this.Velocity.X = 0;
    }
    public void initBackward()
    {
        this.Velocity.X = -250 * (int)Direction;
    }
    public void initForward()
    {
        this.Velocity.X = 250 * (int)Direction;
    }
    public void initCrouchDown()
    {

    }
    public void initCrouchUp()
    {

    }
    public void initCrouch()
    {

    }
    public void initJump()
    {
        this.Velocity.Y = -800;
        this.Gravity = 1000;
        isJumping = true;
    }
    public void initJumpForward()
    {
        this.Velocity.X = 250 * (int)Direction;
    }
    public void initJumpBackward()
    {
        this.Velocity.X = -250 * (int)Direction;
    }
    public void initLightKick()
    {

    }
    public void initMediumKick()
    {

    }
    public void initHeavyKick()
    {

    }
    public void initLightPunch()
    {

    }
    public void initMediumPunch()
    {

    }
    public void initHeavyPunch()
    {

    }
    #endregion

}