public class Player
{
    public Fighter SelectedFighter { get; set; }
    public Player(Fighter fighter)
    {
        SelectedFighter = fighter;
    }
}