namespace GodotRuntimeInspector.Scripts
{
    public static class MyInputMap
    {
        public static Godot.StringName gri_F1 = nameof(gri_F1);

        public static void Init()
        {
            InitKey(gri_F1, Godot.Key.F1);
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
