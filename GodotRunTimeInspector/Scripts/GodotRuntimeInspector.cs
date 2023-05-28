using ImGuiNET;
using System.Collections.Generic;

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
        public static bool Hide = false;
        public static bool ShowDemoWindow = false;
        public static float MinRowHeight = 33f;
        public static float WindowIndent = 33f;
        public static ImGuiViewportPtr MainviewPortPTR = new ImGuiViewportPtr();
        public static ImGuiIOPtr IOPTR = null;
        public static Dictionary<Myimgui.MyProperty, Myimgui.MyPropertyInspector> MyPropertyInspectors = new Dictionary<Myimgui.MyProperty, Myimgui.MyPropertyInspector>();

        private static uint dockspaceID = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            // Godot configuration
            Godot.DisplayServer.WindowSetVsyncMode(Godot.DisplayServer.VSyncMode.Disabled);
            Godot.Engine.MaxFps = MaxFps;

            // load the SimpleCamera scene
            IsDebug = System.String.Equals(DebugPath, SceneFilePath, System.StringComparison.InvariantCultureIgnoreCase);
            if (IsDebug == true)
            {
                SimpleCameraNode = SimpleCameraPackedScene.Instantiate();
                SimpleCameraNode.Name = nameof(SimpleCamera);
                // Add the node as a child of the node the script is attached to.
                AddChild(SimpleCameraNode);
            }

            // pointers to MainViewport and IO
            MainviewPortPTR = ImGui.GetMainViewport();
            IOPTR = ImGui.GetIO();
            IOPTR.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

            // style
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

            // input setup
            MyInputMap.Init();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            if (Enabled == false || Hide == true)
            {
                return;
            }

            FPS = 1.0 / delta;
            IOPTR.DeltaTime = (float)delta;

            // make the central node invisible and inputs pass-thru
            ImGuiDockNodeFlags dockNodeFlags = ImGuiDockNodeFlags.PassthruCentralNode;
            dockspaceID = ImGui.DockSpaceOverViewport(MainviewPortPTR, dockNodeFlags);

            // size of next appearing window
            System.Numerics.Vector2 nextWindowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
            System.Numerics.Vector2 nextWindowPos = System.Numerics.Vector2.Zero;

            // each window
            ImGui.SetNextWindowSize(nextWindowSize, ImGuiCond.Appearing);
            ImGui.SetNextWindowPos(nextWindowPos, ImGuiCond.Appearing);
            Myimgui.MyPropertyNode.Update(this);

            foreach (var key in MyPropertyInspectors.Keys)
            {
                ImGui.SetNextWindowSize(nextWindowSize, ImGuiCond.Appearing);
                ImGui.SetNextWindowPos(nextWindowPos, ImGuiCond.Appearing);
                //MyPropertyInspectors[key].Update(key);
            }

            if (ShowDemoWindow == true)
            {
                ImGui.SetNextWindowSize(nextWindowSize, ImGuiCond.Appearing);
                ImGui.SetNextWindowPos(nextWindowPos, ImGuiCond.Appearing);
                ImGui.ShowDemoWindow();
            }
        }

        public override void _Input(Godot.InputEvent @event)
        {
            // stops input from propagating down through each _Input call (improves performance)
            //GetViewport().SetInputAsHandled();

            if (Godot.Input.IsActionPressed(MyInputMap.F1))
            {
                Enabled = !Enabled;
            }
        }
    }
}