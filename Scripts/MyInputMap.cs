namespace GodotRuntimeInspector.Scripts
{
    public static class MyInputMap
    {
        public static Godot.StringName FORWARD = nameof(FORWARD);
        public static Godot.StringName LEFT = nameof(LEFT);
        public static Godot.StringName BACK = nameof(BACK);
        public static Godot.StringName RIGHT = nameof(RIGHT);

        public static void Init()
        {
            InitKey(FORWARD, Godot.Key.W);
            InitKey(LEFT, Godot.Key.A);
            InitKey(BACK, Godot.Key.S);
            InitKey(RIGHT, Godot.Key.D);
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
