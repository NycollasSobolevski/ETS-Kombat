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
    MediumPuch,
    HeavyPunch,
    LightKick,
    MediumKick,
    HeavyKick,
}
