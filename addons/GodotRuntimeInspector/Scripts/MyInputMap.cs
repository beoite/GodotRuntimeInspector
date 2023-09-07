namespace GodotRuntimeInspector.Scripts
{
    public static class MyInputMap
    {
        public static Godot.StringName gri_F1 = nameof(gri_F1);
        public static Godot.StringName gri_FORWARD = nameof(gri_FORWARD);
        public static Godot.StringName gri_LEFT = nameof(gri_LEFT);
        public static Godot.StringName gri_BACK = nameof(gri_BACK);
        public static Godot.StringName gri_RIGHT = nameof(gri_RIGHT);
        public static Godot.StringName gri_Q = nameof(gri_Q);
        public static Godot.StringName gri_E = nameof(gri_E);
        public static Godot.StringName gri_LEFTSHIFT = nameof(gri_LEFTSHIFT);

        static MyInputMap()
        {
            InitKey(gri_F1, Godot.Key.F1);
            InitKey(gri_FORWARD, Godot.Key.W);
            InitKey(gri_LEFT, Godot.Key.A);
            InitKey(gri_BACK, Godot.Key.S);
            InitKey(gri_RIGHT, Godot.Key.D);
            InitKey(gri_Q, Godot.Key.Q);
            InitKey(gri_E, Godot.Key.E);
            InitKey(gri_LEFTSHIFT, Godot.Key.Shift);
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
