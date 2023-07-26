public static class Stage
{
    public static int STAGE_FLOOR = 200;
    public static int FIGHTER_WIDTH = 50;
}
public enum FighterDirection
{
    LEFT = -1,
    RIGHT = 1
}
public enum States
{
    Forward,
    Backward,
    Jump,
    JumpForward,
    JumpBackward,
    Idle,
    CrouchDown,
    CrouchUp,
    Crouch,
    LightPunch,
    MediumPunch,
    HeavyPunch,
    LightKick,
    MediumKick,
    HeavyKick,
}

public class FighterStateObject
{
    public Action<DateTime> Update { get; set; }
    public Action Init { get; set; }
    public List<States> ValidFrom { get; set; }
    public FighterStateObject(Action init, Action<DateTime> update, List<States> validFrom)
    {
        Init = init;
        Update = update;
        this.ValidFrom = validFrom;
    }
}

