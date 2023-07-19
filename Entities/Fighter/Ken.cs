using static Stage;
using static FighterDirection;

public class Ken : Fighter
{
    public Ken()
    {
        this.Image =  new Bitmap("./Assets/Sprites/Guilherme.png");
        this.Position = new Point(500, STAGE_FLOOR);
        this.Size = new Size(300, 300);
        this.Velocity = 200;
        this.AnimationTimer = 1;
        Direction = FighterDirection.LEFT;

        setFowardFrames();
        setBackwardFrames();
        
    }

    public override void Draw(Graphics g)
    {
        var container = g.BeginContainer();

        Frame = Frames[AnimationName][AnimationFrame];

        ChangeX(g);

        g.DrawImage(
            this.Image,
            new RectangleF(
                this.Rectangle.X - Frame.OriginPoint.X,
                this.Rectangle.Y - Frame.OriginPoint.Y,
                this.Rectangle.Width,
                this.Rectangle.Height
            ),
            Frame.ToRectangle(),
            GraphicsUnit.Pixel
        );

        g.EndContainer(container);
        this.DrawDebug(g);
    }

    public override void Update(Graphics g, TimeSpan t)
    {
        this.MoveX(t);
        if (Position.X > 1920 - this.Size.Width || Position.X < 0 + this.Size.Width)
        {
            this.Velocity = -this.Velocity;

            if (AnimationName == AnimationName.Foward)
                this.AnimationName = AnimationName.Backward;
    
            else if (AnimationName == AnimationName.Backward)
                this.AnimationName = AnimationName.Foward;

        }
        
        if (DateTime.Now - lastFrame > TimeSpan.FromMilliseconds(60))
        {
            AnimationFrame++;
            lastFrame = DateTime.Now;
        }

        if (AnimationFrame >= Frames[AnimationName].Count)
            AnimationFrame = 0;
    }

    public override void DrawDebug(Graphics g)
    {
        g.FillRectangle(
            Brushes.Red,
            new RectangleF(
                Rectangle.X + Frame.OriginPoint.X,
                Rectangle.Y + Frame.OriginPoint.Y,
                10,
                10
            )
        );
    }


    //! ANIMATION FRAMES AND SPRITES
    void setFowardFrames()
    {
        List<Frame> fowardFrames = new List<Frame>();

        fowardFrames.Add(
            new Frame(
                new Point(170, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 1, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 2, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 2, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 3, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 4, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );
        fowardFrames.Add(
            new Frame(
                new Point(170 + 76 * 5, 152),
                new Size(76, 104),
                new Point(0, 0)
            )
        );

        this.Frames.Add(AnimationName.Foward, fowardFrames);
    }
    void setBackwardFrames()
    {
        List<Frame> backwardFrames = new List<Frame>();

        for (int i = 0; i < 6; i++)
        {
            backwardFrames.Add(
                new Frame(
                    new Point(718 + 77 * i, 150),
                    new Size(77, 105),
                    new Point(0, 0)
                )
            );
        }

        this.Frames.Add(
            AnimationName.Backward,
            backwardFrames
        );
    }
}