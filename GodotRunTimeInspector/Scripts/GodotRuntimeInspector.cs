using ImGuiNET;

namespace RuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
        public const string ResourcePrefix = "res://";
        public const string Extension = ".tscn";
        public static string DebugPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + nameof(GodotRuntimeInspector) + Extension;
        public static bool IsDebug = false;
        public static string SimpleCameraPath = ResourcePrefix + nameof(GodotRuntimeInspector) + "/" + nameof(SimpleCamera) + Extension;
        public static Godot.PackedScene SimpleCameraPackedScene = (Godot.PackedScene)Godot.ResourceLoader.Load<Godot.PackedScene>(SimpleCameraPath);
        public static Godot.Node SimpleCameraNode = new Godot.Node();

        public static double FPS = 0;
        public static int MaxFps = 30;
        public static bool Enabled = true;
        public static float MinRowHeight = 10f;
        public static ImGuiViewportPtr MAINVIEWPORTPTR = new ImGuiViewportPtr();
        public static ImGuiIOPtr IOPTR = null;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Godot.DisplayServer.WindowSetVsyncMode(Godot.DisplayServer.VSyncMode.Disabled);
            Godot.Engine.MaxFps = MaxFps;
            IsDebug = System.String.Equals(DebugPath, SceneFilePath, System.StringComparison.InvariantCultureIgnoreCase);
            if (IsDebug == true)
            {
                // This does not work on export?
                // C# has no preload, so you have to always use ResourceLoader.Load<PackedScene>().
                SimpleCameraNode = SimpleCameraPackedScene.Instantiate();
                SimpleCameraNode.Name = nameof(SimpleCamera);
                // Add the node as a child of the node the script is attached to.
                AddChild(SimpleCameraNode);
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            if (Enabled == false)
            {
                return;
            }

            FPS = 1.0 / delta;
            MAINVIEWPORTPTR = ImGui.GetMainViewport();
            IOPTR = ImGui.GetIO();
            IOPTR.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            IOPTR.DeltaTime = (float)delta;

            // make the central node invisible and inputs pass-thru
            ImGuiDockNodeFlags dockNodeFlags = ImGuiDockNodeFlags.PassthruCentralNode;
            uint id = ImGui.DockSpaceOverViewport(MAINVIEWPORTPTR, dockNodeFlags);

            ImGuiStylePtr style = ImGui.GetStyle();
            style.WindowPadding = new System.Numerics.Vector2(1f, 1f);
            style.FramePadding = new System.Numerics.Vector2(1f, 1f);
            style.CellPadding = new System.Numerics.Vector2(1f, 1f);

            style.WindowRounding = 0f;
            style.ChildRounding = 0f;
            style.PopupRounding = 0f;
            style.FrameRounding = 0f;
            style.ScrollbarRounding = 0f;
            style.GrabRounding = 0f;
            style.TabRounding = 0f;
            style.CellPadding = new System.Numerics.Vector2(0f, 0f);

            System.Numerics.Vector2 nextWindowSize = new System.Numerics.Vector2(MAINVIEWPORTPTR.Size.X / 2f, MAINVIEWPORTPTR.Size.Y / 2f);
            System.Numerics.Vector2 nextWindowPos = System.Numerics.Vector2.Zero;

            ImGui.SetNextWindowSize(nextWindowSize, ImGuiCond.Appearing);
            ImGui.SetNextWindowPos(nextWindowPos, ImGuiCond.Appearing);
            Myimgui.MyPropertyNode.Update(this);

            //ImGui.SetNextWindowSize(nextWindowSize, ImGuiCond.Appearing);
            //ImGui.SetNextWindowPos(nextWindowPos, ImGuiCond.Appearing);
            //ImGui.ShowDemoWindow();
        }

        public override void _Input(Godot.InputEvent @event)
        {
            // stops input from propagating down through each _Input call (improves performance)
            //GetViewport().SetInputAsHandled();
        }

        public static void DisableUI()
        {
            Enabled = false;
            Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
        }

        public static void EnableUI()
        {
            Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Visible;
            Enabled = true;
        }
    }
}