#pragma warning disable
using static Stage;


public abstract class Fighter : Entity
{   
    # region BasicProps

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
    public bool Debug { get; set; } = true;
    public string Name { get; set; }
    public Life HealthPoints { get; set; }
    private int hp = 1000;
    #endregion
    public Fighter(PointF initialPosition, int health)
    {
        hp = health;
        SetData();
        this.AnimationTimer = 1;
        this.Size = new Size(200, 236);
        this.Position = new PointF(initialPosition.X, initialPosition.Y - STAGE_FLOOR);

        StateObjects = new Dictionary<States, FighterStateObject>()
        {
            {   
                States.Idle,
                new FighterStateObject(initIdle, handleIdle,
                new List<States>(){
                    States.Backward, States.Forward,
                    States.CrouchUp,
                    States.LightKick, States.MediumKick, States.HeavyKick,
                    States.LightPunch, States.MediumPunch, States.HeavyPunch,
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
                    States.Idle, States.Forward
                })
            },
            {
                States.Forward,
                new FighterStateObject(initForward, handleForward,
                new List<States>(){
                    States.Idle, States.Backward
                })
            },
            {
                States.CrouchDown,
                new FighterStateObject(initCrouchDown, handleCrouchDown,
                new List<States>(){
                    States.Idle, States.Backward, States.Forward
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
            },
            {
                States.LightKick,
                new FighterStateObject(initLightKick, handleLightKick,
                new List<States>(){
                    States.Idle, States.Crouch, States.JumpForward, States.JumpForward
                })
            },
            {
                States.LightPunch,
                new FighterStateObject(initLightKick, handleLightKick,
                new List<States>(){
                    States.Idle, States.Crouch, States.JumpForward, States.JumpForward
                })
            },
            {
                States.MediumKick,
                new FighterStateObject(initMediumKick, handleMediumKick,
                new List<States>(){
                    States.Idle, States.Crouch, States.JumpForward, States.JumpForward
                })
            },
            {
                States.MediumPunch,
                new FighterStateObject(initMediumPunch, handleMediumPunch,
                new List<States>(){
                    States.Idle, States.Crouch, States.JumpForward, States.JumpForward
                })
            },

        };
    }
    public void Draw(Graphics g)
    {
        var container = g.BeginContainer();
        
        this.ChangeSpriteDirectionX(g);
        ChangeState(CurrentState);
        
        if (Debug)
            this.DrawDebug(g);
        
        DrawFighter(g);

        g.EndContainer(container);

        this.HealthPoints.Draw(g, hp);
        
        
    }
    public virtual void DrawFighter(Graphics g)
    {
        g.DrawImage(
            this.Image,
            this.Rectangle,
            Frame.ToRectangle(),
            GraphicsUnit.Pixel
        );
    }
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
        g.FillRectangle(
            Brushes.Aqua,
            new RectangleF(
                this.Position.X + this.Size.Width - 50,
                this.Position.Y - this.Size.Height,
                this.Rectangle.Width,
                this.Rectangle.Height
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
            $"Velocity: X {this.Velocity.X} | Y {this.Velocity.Y}\n" +
            $"Position: X {this.Position.X}| Y {this.Position.Y}\n" +
            $"State: {this.CurrentState}",
            new Font("arial", 10),
            Brushes.Black,
            new PointF(this.Direction == FighterDirection.LEFT ? 0 : ScreenSize.Width - 300, 0)
        );        
    }
    public virtual void Update(Graphics g, DateTime t)
    {
        var container = g.BeginContainer();
        Move(t);
        this.UpdateAnimation(t);
        this.UpdateStageConstraints();

        this.StateObjects[CurrentState].Update(t);
        

        g.EndContainer(container);
    }
    
    # region SpriteDirection
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

            // apply gravity
            this.Velocity.Y += Gravity * (float)calc;

            this.Position = new PointF(
                (float)(this.Position.X + Velocity.X * calc),
                (float)(this.Position.Y + Velocity.Y * calc)
            );
        }
        public void ChangeState(States newState)
        {
            if (StateObjects[newState].ValidFrom.Contains(this.CurrentState))
            {
                this.CurrentState = newState;
                StateObjects[this.CurrentState].Init();
            }
        }
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
            int directionValue = (int)(Direction);
            if (directionValue == -1)
                g.TranslateTransform((this.Position.X + 2 * Frame.Size.Width) * 2, 0);
            g.ScaleTransform(directionValue, 1);
            
            if (
                this.CurrentState == States.Jump ||
                this.CurrentState == States.JumpForward ||
                this.CurrentState == States.JumpBackward
            )
                return;
            
            getDirection();
        }
        public void getDirection()
        => this.Direction = this.Position.X > Enemy.Position.X?
            FighterDirection.LEFT : FighterDirection.RIGHT;
    
    #endregion
    #region HandleState
    public void handleBackward(DateTime t)
    {
        if (Control.KeyMapping.Map[Keys.A])
            this.Velocity.X = -250 * (int)Direction;
    }
    public void handleForward(DateTime t)
    {
        if (Control.KeyMapping.Map[Keys.D])
            this.Velocity.X = 250 * (int)Direction;
    }
    public void handleIdle(DateTime t)
    {
        this.Velocity.X = 0;
        this.Velocity.Y = 0;
    }
    public void handleCrouchDown(DateTime t)
    {
        if (Control.KeyMapping.Map[Keys.S])
        {
            isCrouching = AnimationFrame >= 1;

            if (isCrouching)
                this.CurrentState = States.Crouch;
        }
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
        // this.Velocity.Y += Gravity * (float)(t - lastFrame).TotalSeconds;
    }
    public void handleJumpForward(DateTime t)
    {
        handleJump(t);
        this.Velocity.X = 250 * (int)Direction;
    }
    public void handleJumpBackward(DateTime t)
    {
        handleJump(t);
        this.Velocity.X = -250 * (int)Direction;
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
        this.Velocity.Y = 0;

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
        this.Gravity = 1_000;
        isJumping = true;
    }
    public void initJumpForward()
    {
        this.initJump();
        this.Velocity.X = 250 * (int)Direction;
    }
    public void initJumpBackward()
    {
        this.initJump();
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

    #region SetData
    public void SetData()
    {
        setFrames();
        setHurtboxes();
        setHitboxes();
        setPushboxes();
        setThrowboxes();
        Frame = Frames[States.Idle][0];
    }
    #region setFrames
        protected virtual void setFrames()
        {
            setIdleFrames();
            setForwardFrames();
            setBackwardFrames();
            setJumpingFrames();
            setCrouchFrames();
            setLightPunchFrames();
            setMediumPunchFrames();
        }
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
    #region setHurtBoxes
        protected virtual void setHurtboxes()
        {
            setHurtbox(States.Idle);
            setHurtbox(States.Backward);
            setHurtbox(States.Jump);
            setHurtbox(States.JumpBackward);
            setHurtbox(States.Crouch);
        }
        private void setHurtbox(States state)
        {
            if (state == States.Jump)
            {
                var hurtboxes = new RectangleF[] {
                    new RectangleF(0, 0, 72 * 2, 115 * 2),
                    new RectangleF(0, 0, 61 * 2, 105 * 2),
                    new RectangleF(0, 0, 61 * 2, 80 * 2),
                    new RectangleF(0, 0, 61 * 2, 105 * 2),
                    new RectangleF(0, 0, 72 * 2, 115 * 2),
                };

                for (int i = 0; i < Frames[States.Jump].Count; i++)
                    Frames[States.Jump][i].HurtBox = hurtboxes[i];
            }
            if (state == States.Idle)
            {
                var hurtboxes = new RectangleF[] {
                    new RectangleF(0, 0, (68 * 2), (103 * 2)),
                    new RectangleF(0, 0, (68 * 2), (103 * 2)),
                    new RectangleF(0, 0, (68 * 2), (103 * 2)),
                    new RectangleF(0, 0, (68 * 2), (103 * 2)),
                    new RectangleF(0, 0, (68 * 2), (103 * 2)),
                    new RectangleF(0, 0, (68 * 2), (103 * 2)),
                };

                for (int i = 0; i < Frames[States.Idle].Count; i++)
                    Frames[States.Idle][i].HurtBox = hurtboxes[i];
            }
            if (state == States.Backward || state == States.Forward)
            {
                var hurtboxes = new RectangleF[] {
                    new RectangleF(0, 0, (55 * 2), (106 * 2)),
                    new RectangleF(0, 0, (66 * 2), (105 * 2)),
                    new RectangleF(0, 0, (80 * 2), (118 * 2)),
                    new RectangleF(0, 0, (100 * 2), (105 * 2)),
                    new RectangleF(0, 0, (66 * 2), (105 * 2)),
                    new RectangleF(0, 0, (66 * 2), (105 * 2)),
                };

                //TODO: backward foreward  
                for(int i = 0; i < Frames[States.Forward].Count; i++)
                    Frames[States.Forward][i].HurtBox = hurtboxes[i];
                for(int i = 0; i < Frames[States.Backward].Count; i++)
                    Frames[States.Backward][i].HurtBox = hurtboxes[i];            
            }
            if (state == States.Crouch)
            {
                var hurtboxes = new RectangleF[] {
                    new RectangleF(0, 0, 72 * 2, 75 * 2),
                };

                for (int i = 0; i < Frames[States.Crouch].Count; i++)
                    Frames[States.Crouch][i].HurtBox = hurtboxes[i];
            }
            if (state == States.JumpBackward || state == States.JumpForward)
            {
                var hurtboxes = new RectangleF[] {
                    new RectangleF(0, 0, 75*2, 118*2),
                    new RectangleF(0, 0, 63*2, 104*2),
                    new RectangleF(0, 0, 60*2, 81*2),
                    new RectangleF(0, 0, 63*2, 104*2),
                    new RectangleF(0, 0, 75*2, 118*2),
                    new RectangleF(0, 0, 75*2, 118*2),
                };

                for(int i = 0; i < Frames[States.JumpForward].Count; i++ )
                    Frames[States.JumpForward][i].HurtBox = hurtboxes[i];
                for(int i = 0; i < Frames[States.JumpBackward].Count; i++ )
                    Frames[States.JumpBackward][i].HurtBox = hurtboxes[i];
            }
        }
    #endregion
    #region setHitboxes
        protected virtual void setHitboxes()
        {
            Frames[States.LightPunch][1].HitBoxInit =
                new RectangleF( 80, 40, 110, 40 );
        }
    #endregion
    #region setPushboxes
        protected virtual void setPushboxes()
        {
            setPush();
        }
        private void setPush()
        {
            foreach (States state in Enum.GetValues(typeof(States)))
            {
                try
                {
                    foreach (var frame in Frames[state])
                    {
                        frame.PushBox = new RectangleF(
                            this.Rectangle.X,
                            this.Rectangle.Y,
                            200 * 0.7f,
                            204
                        );
                    }
                }catch (Exception ex){
                    continue;
                }
            }
        }
    
    #endregion
    
    //TODO :: setThrowboxes
    #region setThrowboxes
        protected virtual void setThrowboxes()
        {
            
        }
    #endregion
    #endregion
}