#pragma warning disable
public class Frame
{
    public Size Size { get; set; }
    public Point PointInSpriteSheet { get; set; }
    public Point OriginPoint { get; set; }
    public Rectangle? PushBox { get; set; } 
    public Rectangle[]? HitBox { get; set; } 
    public Rectangle? ThrowBox { get; set; } 
    public Rectangle[]? HurtBox { get; set; } 

    public Frame(Point pointInSpriteSheet, Size size, Point originPoint)
    {
        PointInSpriteSheet = pointInSpriteSheet;
        Size = size;
        if (originPoint.X != 0 && originPoint.Y != 0)
            OriginPoint = originPoint;
        else
            OriginPoint = new Point(
                //! essa conta
                Size.Width,
                Size.Height * 2
            );
    }
    public Frame(Point pointInSpriteSheet, Size size, Point originPoint,
        Rectangle[]? hurtbox, Rectangle[]? hitbox, Rectangle? pushbox, Rectangle? throwbox)
    {
        PointInSpriteSheet = pointInSpriteSheet;
        Size = size;
        if (originPoint.X != 0 && originPoint.Y != 0)
            OriginPoint = originPoint;
        else
            OriginPoint = new Point(
                //! essa conta
                Size.Width,
                Size.Height * 2
            );
        this.HitBox = hitbox;
        this.HurtBox = hurtbox;
        this.PushBox = pushbox;
        this.ThrowBox = throwbox;
    }

    public Rectangle ToRectangle()
        => new Rectangle(PointInSpriteSheet, Size);
}