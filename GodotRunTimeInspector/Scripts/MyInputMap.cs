namespace GodotRuntimeInspector.Scripts
{
    public static class MyInputMap
    {
        public static Godot.StringName F1 = nameof(F1);
        public static Godot.StringName FORWARD = nameof(FORWARD);
        public static Godot.StringName LEFT = nameof(LEFT);
        public static Godot.StringName BACK = nameof(BACK);
        public static Godot.StringName RIGHT = nameof(RIGHT);
        public static Godot.StringName Q = nameof(Q);
        public static Godot.StringName E = nameof(E);
        public static Godot.StringName LEFTSHIFT = nameof(LEFTSHIFT);
        public static Godot.StringName SPACE = nameof(SPACE);

        static MyInputMap()
        {
            InitKey(F1, Godot.Key.F1);
            InitKey(FORWARD, Godot.Key.W);
            InitKey(LEFT, Godot.Key.A);
            InitKey(BACK, Godot.Key.S);
            InitKey(RIGHT, Godot.Key.D);
            InitKey(Q, Godot.Key.Q);
            InitKey(E, Godot.Key.E);
            InitKey(LEFTSHIFT, Godot.Key.Shift);
            InitKey(SPACE, Godot.Key.Space);
        }

        private static void InitKey(Godot.StringName name, Godot.Key key)
        {
            Godot.InputEventKey ev = new Godot.InputEventKey();
            ev.PhysicalKeycode = key;
            Godot.InputMap.AddAction(name);
            Godot.InputMap.ActionAddEvent(name, ev);
        }
    }
}
