namespace GodotRuntimeInspector.Scripts
{
    public static class MyInputMap
    {
        public static Godot.StringName ESC = nameof(ESC);
        public static Godot.StringName F1 = nameof(F1);
        public static Godot.StringName FORWARD = nameof(FORWARD);
        public static Godot.StringName LEFT = nameof(LEFT);
        public static Godot.StringName BACK = nameof(BACK);
        public static Godot.StringName RIGHT = nameof(RIGHT);
        public static Godot.StringName Q = nameof(Q);
        public static Godot.StringName E = nameof(E);
        public static Godot.StringName C = nameof(C);
        public static Godot.StringName F = nameof(F);
        public static Godot.StringName R = nameof(R);
        public static Godot.StringName Z = nameof(Z);
        public static Godot.StringName X = nameof(X);
        public static Godot.StringName LEFTSHIFT = nameof(LEFTSHIFT);
        public static Godot.StringName SPACE = nameof(SPACE);

        public static Godot.StringName NUM1 = nameof(NUM1);
        public static Godot.StringName NUM2 = nameof(NUM2);
        public static Godot.StringName NUM3 = nameof(NUM3);
        public static Godot.StringName NUM4 = nameof(NUM4);
        public static Godot.StringName NUM5 = nameof(NUM5);
        public static Godot.StringName NUM6 = nameof(NUM6);
        public static Godot.StringName NUM7 = nameof(NUM7);
        public static Godot.StringName NUM8 = nameof(NUM8);
        public static Godot.StringName NUM9 = nameof(NUM9);
        public static Godot.StringName NUM0 = nameof(NUM0);

        static MyInputMap()
        {
            InitKey(ESC, Godot.Key.Escape);
            InitKey(F1, Godot.Key.F1);
            InitKey(FORWARD, Godot.Key.W);
            InitKey(LEFT, Godot.Key.A);
            InitKey(BACK, Godot.Key.S);
            InitKey(RIGHT, Godot.Key.D);
            InitKey(Q, Godot.Key.Q);
            InitKey(E, Godot.Key.E);
            InitKey(C, Godot.Key.C);
            InitKey(F, Godot.Key.F);
            InitKey(R, Godot.Key.R);
            InitKey(Z, Godot.Key.Z);
            InitKey(X, Godot.Key.X);
            InitKey(LEFTSHIFT, Godot.Key.Shift);
            InitKey(SPACE, Godot.Key.Space);

            InitKey(NUM1, Godot.Key.Key1);
            InitKey(NUM2, Godot.Key.Key2);
            InitKey(NUM3, Godot.Key.Key3);
            InitKey(NUM4, Godot.Key.Key4);
            InitKey(NUM5, Godot.Key.Key5);
            InitKey(NUM6, Godot.Key.Key6);
            InitKey(NUM7, Godot.Key.Key7);
            InitKey(NUM8, Godot.Key.Key8);
            InitKey(NUM9, Godot.Key.Key9);
            InitKey(NUM0, Godot.Key.Key0);
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
