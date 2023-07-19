public class Frame
{
    public Size Size { get; set; }
    public Point PointInSpriteSheet { get; set; }
    public Point OriginPoint { get; set; }
    public Frame(Point pointInSpriteSheet, Size size, Point originPoint)
    {
        PointInSpriteSheet = pointInSpriteSheet;
        Size = size;
        if (originPoint.X != 0 && originPoint.Y != 0)
            OriginPoint = originPoint;
        else
            OriginPoint = new Point(size.Width/2, size.Height);
    }

    public Rectangle ToRectangle()
        => new Rectangle(PointInSpriteSheet, Size);
}