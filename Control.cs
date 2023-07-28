
#pragma warning disable
public static class Control
{
    // For player 1 controls
    // W A S D to walk
    // J K L U I O to attack
    // J light kick
    // K medium kick
    // L heavy kick
    // U light punch
    // I medium punch
    // O heavy punch

    // For player 2 controls
    // Arrow keys to walk
    // 1 2 3 4 5 6 to attack
    // 1 light kick
    // 2 medium kick
    // 3 heavy kick
    // 4 light punch
    // 5 medium punch
    // 6 heavy punch

    public static class KeyMapping
    {
        public static Dictionary<Keys, bool> Map { get; private set; } =
            new Dictionary<Keys, bool>() {
                { Keys.D, false},
                { Keys.A, false},
                { Keys.W, false},
                { Keys.S, false},
                { Keys.J, false},
                { Keys.K, false},
                // { Keys.L, false},
                { Keys.U, false},
                { Keys.I, false},
                // { Keys.O, false},
                { Keys.Right, false},
                { Keys.Left, false},
                { Keys.Up, false},
                { Keys.Down, false},
                // { Keys.NumPad1, false},
                // { Keys.NumPad2, false},
                // { Keys.NumPad3, false},
                { Keys.NumPad4, false},
                // { Keys.NumPad5, false},
                // { Keys.NumPad6, false},
                
                //? Assistence keys
                { Keys.OemMinus, false},
            };
    }

    public static void GetInstance(Form form)
    {
        form.KeyDown += (e, k) => {
            if (KeyMapping.Map.ContainsKey(k.KeyCode))
                KeyMapping.Map[k.KeyCode] = true;
            
            if (k.KeyCode == Keys.Escape)
                form.Close();
        };

        form.KeyUp += (e, k) => {
            if (KeyMapping.Map.ContainsKey(k.KeyCode))
                KeyMapping.Map[k.KeyCode] = false;
        };
    }

    public static States GetState(Player player, Fighter f)
    {
        if (player is Player1)
            return GetP1State(f);
        else if (player is Player2)
            return GetP2State(f);
        else
            return States.Idle;
    }

    private static bool isCrouchingP1;
    private static bool isCrouchingP2;
    private static States GetP1State(Fighter fighter)
    {
        States state = States.Idle;

        if (KeyMapping.Map[Keys.A])
            if (fighter.Direction == FighterDirection.LEFT)
                state = States.Forward;
            else
                state = States.Backward;
        if (KeyMapping.Map[Keys.D])
            if (fighter.Direction == FighterDirection.LEFT)
                state = States.Backward;
            else
                state = States.Forward;
    
        if (KeyMapping.Map[Keys.W])
            state = States.Jump;

        if (KeyMapping.Map[Keys.S])
        {
            state = States.CrouchDown;
            isCrouchingP1 = true;
        }
        if (isCrouchingP1 && !KeyMapping.Map[Keys.S])
        {
            state = States.CrouchUp;
            isCrouchingP1 = false;
        }

        if (KeyMapping.Map[Keys.J])
            state = States.LightKick;
        if (KeyMapping.Map[Keys.K])
            state = States.MediumKick;
        // if (KeyMapping.Map[Keys.L])
        //     state = States.HeavyKick;
        if (KeyMapping.Map[Keys.U])
            state = States.LightPunch;
        if (KeyMapping.Map[Keys.I])
            state = States.MediumPunch;
        // if (KeyMapping.Map[Keys.O])
        //     state = States.HeavyPunch;

        if (KeyMapping.Map[Keys.D] && KeyMapping.Map[Keys.W])
            if (fighter.Direction == FighterDirection.RIGHT)
                state = States.JumpForward;
            else
                state = States.JumpBackward;

        if (KeyMapping.Map[Keys.A] && KeyMapping.Map[Keys.W])
            if (fighter.Direction == FighterDirection.RIGHT)
                state = States.JumpBackward;
            else
                state = States.JumpForward;

        return state;
    }

    private static States GetP2State(Fighter fighter)
    {
        States state = States.Idle;
        
        if (KeyMapping.Map[Keys.Left])
            if (fighter.Direction == FighterDirection.LEFT)
                state = States.Forward;
            else
                state = States.Backward;
        if (KeyMapping.Map[Keys.Right])
            if (fighter.Direction == FighterDirection.LEFT)
                state = States.Backward;
            else
                state = States.Forward;

        if (KeyMapping.Map[Keys.Up])
            state = States.Jump;

        if (KeyMapping.Map[Keys.Down])
        {
            state = States.CrouchDown;
            isCrouchingP2 = true;
        }
        if (isCrouchingP2 && !KeyMapping.Map[Keys.Down])
        {
            state = States.CrouchUp;
            isCrouchingP2 = false;
        }

        // if (KeyMapping.Map[Keys.NumPad1])
        //     state = States.LightKick;
        // if (KeyMapping.Map[Keys.NumPad2])
        //     state = States.MediumKick;
        // if (KeyMapping.Map[Keys.NumPad3])
        //     state = States.HeavyKick;
        if (KeyMapping.Map[Keys.NumPad4])
            state = States.LightPunch;
        // if (KeyMapping.Map[Keys.NumPad5])
        //     state = States.MediumPunch;
        // if (KeyMapping.Map[Keys.NumPad6])
        //     state = States.HeavyPunch;
        
        if (KeyMapping.Map[Keys.Right] && KeyMapping.Map[Keys.Up])
            if (fighter.Direction == FighterDirection.RIGHT)
                state = States.JumpForward;
            else
                state = States.JumpBackward;
        if (KeyMapping.Map[Keys.Left] && KeyMapping.Map[Keys.Up])
            if (fighter.Direction == FighterDirection.RIGHT)
                state = States.JumpBackward;
            else
                state = States.JumpForward;

        return state;
    }
}
