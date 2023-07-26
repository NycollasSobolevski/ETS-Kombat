
using Microsoft.VisualBasic.Devices;

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
                { Keys.L, false},
                { Keys.U, false},
                { Keys.I, false},
                { Keys.O, false},
                { Keys.Right, false},
                { Keys.Left, false},
                { Keys.Up, false},
                { Keys.Down, false},
                { Keys.NumPad1, false},
                { Keys.NumPad2, false},
                { Keys.NumPad3, false},
                { Keys.NumPad4, false},
                { Keys.NumPad5, false},
                { Keys.NumPad6, false},

                { Keys.Q, false},
                { Keys.E, false},
                
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

    public static States GetState(Entity entity)
    {
        if (entity is Ruyviu)
            return GetRuyviuState();
        else if (entity is Joe)
            return GetJoeState();
        else
            return States.Idle;
    }

    private static States GetRuyviuState()
    {
        States state = States.Idle;
        if (KeyMapping.Map[Keys.D])
            state = States.Forward;
        if (KeyMapping.Map[Keys.A])
            state = States.Backward;
        if (KeyMapping.Map[Keys.W])
            state = States.Jump;
        if (KeyMapping.Map[Keys.S])
            state = States.Crouch;
        if (KeyMapping.Map[Keys.J])
            state = States.LightKick;
        if (KeyMapping.Map[Keys.K])
            state = States.MediumKick;
        if (KeyMapping.Map[Keys.L])
            state = States.HeavyKick;
        if (KeyMapping.Map[Keys.U])
            state = States.LightPunch;
        if (KeyMapping.Map[Keys.I])
            state = States.MediumPunch;
        if (KeyMapping.Map[Keys.O])
            state = States.HeavyPunch;
        if (KeyMapping.Map[Keys.Q])
            state = States.JumpBackward;
        if (KeyMapping.Map[Keys.E])
            state = States.JumpForward;

        return state;
    }

    private static States GetJoeState()
    {
        States state = States.Idle;
        if (KeyMapping.Map[Keys.Right])
            state = States.Forward;
        if (KeyMapping.Map[Keys.Left])
            state = States.Backward;
        if (KeyMapping.Map[Keys.Up])
            state = States.Jump;
        if (KeyMapping.Map[Keys.Down])
            state = States.Crouch;
        if (KeyMapping.Map[Keys.NumPad1])
            state = States.LightKick;
        if (KeyMapping.Map[Keys.NumPad2])
            state = States.MediumKick;
        if (KeyMapping.Map[Keys.NumPad3])
            state = States.HeavyKick;
        if (KeyMapping.Map[Keys.NumPad4])
            state = States.LightPunch;
        if (KeyMapping.Map[Keys.NumPad5])
            state = States.MediumPunch;
        if (KeyMapping.Map[Keys.NumPad6])
            state = States.HeavyPunch;

        return state;
    }
}
