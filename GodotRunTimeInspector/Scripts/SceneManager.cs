namespace RuntimeInspector.Scripts
{
    public static class SceneManager
    {
        public const string ResourcePrefix = "res://";
        public const string Extension = ".tscn";
        public const string SimpleCamera = nameof(SimpleCamera);
        public const string Terrain = nameof(Terrain);

        public static string DebugPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + nameof(GodotRuntimeInspector) + Extension;

        public static string SimpleCameraPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + SimpleCamera + Extension;
        public static Godot.PackedScene SimpleCameraPackedScene = (Godot.PackedScene)Godot.ResourceLoader.Load<Godot.PackedScene>(SimpleCameraPath);
        public static Godot.Node SimpleCameraNode = new Godot.Node();

        public static string TerrainPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + Terrain + Extension;
        public static Godot.PackedScene TerrainPackedScene = (Godot.PackedScene)Godot.ResourceLoader.Load<Godot.PackedScene>(TerrainPath);
        public static Godot.Node TerrainNode = new Godot.Node();

        public static void Init()
        {
            SimpleCameraNode = SimpleCameraPackedScene.Instantiate();
            SimpleCameraNode.Name = SimpleCamera;

            TerrainNode = TerrainPackedScene.Instantiate();
            TerrainNode.Name = Terrain;
        }

    }
}
