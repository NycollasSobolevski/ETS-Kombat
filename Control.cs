
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

}
