public interface Entity
{
    void Update(Graphics g, TimeSpan t);
    void Draw(Graphics g);
    void DrawDebug(Graphics g);
}