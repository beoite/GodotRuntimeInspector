namespace GodotRuntimeInspector.Scripts
{
    public static class SceneManager
    {
        public const string ResourcePrefix = "res://";
        public const string Extension = ".tscn";
        
        public static string DebugPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + nameof(GodotRuntimeInspector) + Extension;

        public const string SimpleCamera = nameof(SimpleCamera);
        public static string SimpleCameraPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + SimpleCamera + Extension;
        public static Godot.PackedScene SimpleCameraPackedScene = (Godot.PackedScene)Godot.ResourceLoader.Load<Godot.PackedScene>(SimpleCameraPath);
        public static Godot.Node SimpleCameraNode = new Godot.Node();

        public static void Init()
        {
            SimpleCameraNode = SimpleCameraPackedScene.Instantiate();
            SimpleCameraNode.Name = SimpleCamera;
        }
    }
}
