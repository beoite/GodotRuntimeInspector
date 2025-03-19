namespace GodotRuntimeInspector.Scripts
{
    public static class MyInputMap
    {
        public static void Init()
        {
            InitKey(nameof(Config.EnabledKey), Config.EnabledKey);
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
