public class Player
{
    public Fighter SelectedFighter { get; set; }
    public List<Keys> keys = new List<Keys>();
    public Player(Fighter fighter)
    {
        SelectedFighter = fighter;
    }
}


public static class Player1
{
    public static Player BuildPlayer(Fighter fighter)
    {
        Player player = new Player(fighter);
        player.keys.Add(Keys.A);
        player.keys.Add(Keys.D);
        player.keys.Add(Keys.S);
        player.keys.Add(Keys.W);
        player.keys.Add(Keys.J);
        player.keys.Add(Keys.K);
        player.keys.Add(Keys.L);
        player.keys.Add(Keys.U);
        player.keys.Add(Keys.I);
        player.keys.Add(Keys.O);
        return player;
    }
}
public static class Player2
{
    public static Player BuildPlayer(Fighter fighter)
    {
        Player player = new Player(fighter);
        player.keys.Add(Keys.Left);
        player.keys.Add(Keys.Right);
        player.keys.Add(Keys.Down);
        player.keys.Add(Keys.Up);
        player.keys.Add(Keys.NumPad1);
        player.keys.Add(Keys.NumPad2);
        player.keys.Add(Keys.NumPad3);
        player.keys.Add(Keys.NumPad4);
        player.keys.Add(Keys.NumPad5);
        player.keys.Add(Keys.NumPad6);
        return player;
    }
}