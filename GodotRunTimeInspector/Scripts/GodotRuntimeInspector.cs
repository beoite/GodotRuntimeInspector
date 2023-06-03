namespace RuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
        public static bool IsDebug = false;
        public static double FPS = 0;
        public static int MaxFps = 30;
        public static bool Enabled = true;
        public static bool Hide = false;
        public static bool ShowDemoWindow = false;
        public static bool ShowDebugWindow = false;
        public static bool ShowInputWindow = false;
        public static bool ShowOSWindow = false;
        public static float MinRowHeight = 25f;
        public static ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();
        public static ImGuiNET.ImGuiIOPtr IOPTR = null;
        public static System.Collections.Generic.Dictionary<string, Myimgui.MyProperty> MyProperties = new System.Collections.Generic.Dictionary<string, Myimgui.MyProperty>();

        private static Godot.InputEvent inputEvent;
        private static uint dockspaceID = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            // Godot configuration
            Godot.DisplayServer.WindowSetVsyncMode(Godot.DisplayServer.VSyncMode.Disabled);
            Godot.Engine.MaxFps = MaxFps;

            // pointers to MainViewport and IO
            MainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
            IOPTR = ImGuiNET.ImGui.GetIO();
            IOPTR.ConfigFlags |= ImGuiNET.ImGuiConfigFlags.DockingEnable;

            // style
            ImGuiNET.ImGuiStylePtr style = ImGuiNET.ImGui.GetStyle();
            style.WindowPadding = new System.Numerics.Vector2(0f, 0f);
            style.FramePadding = new System.Numerics.Vector2(0f, 0f);
            style.CellPadding = new System.Numerics.Vector2(0f, 0f);
            style.WindowRounding = 0f;
            style.ChildRounding = 0f;
            style.PopupRounding = 0f;
            style.FrameRounding = 0f;
            style.ScrollbarRounding = 0f;
            style.GrabRounding = 0f;
            style.TabRounding = 0f;
            style.WindowBorderSize = 0f;
            style.ChildBorderSize = 0f;
            style.FrameBorderSize = 0f;
            style.TabBorderSize = 0f;
            style.CellPadding = new System.Numerics.Vector2(0f, 0f);
            style.Colors[(int)ImGuiNET.ImGuiCol.Text] = Palette.CLOUDBLUE.ToVector4();
            style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4();
            style.Colors[(int)ImGuiNET.ImGuiCol.TableHeaderBg] = Palette.NIGHTBLUE.ToVector4();
            style.Colors[(int)ImGuiNET.ImGuiCol.TableBorderStrong] = Palette.SEABLUE.ToVector4();
            style.Colors[(int)ImGuiNET.ImGuiCol.TableBorderLight] = Palette.SKYBLUE.ToVector4();
            style.Colors[(int)ImGuiNET.ImGuiCol.TableRowBg] = Palette.NIGHTBLUE.ToVector4();
            style.Colors[(int)ImGuiNET.ImGuiCol.TableRowBgAlt] = Palette.SEABLUE.ToVector4();

            // load the debug scene
            IsDebug = System.String.Equals(SceneManager.DebugPath, SceneFilePath, System.StringComparison.InvariantCultureIgnoreCase);
            if (IsDebug == true)
            {
                // Add the node as a child of the node the script is attached to.
                AddChild(SceneManager.SimpleCameraNode);
            }
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
            ImGuiNET.ImGuiDockNodeFlags dockNodeFlags = ImGuiNET.ImGuiDockNodeFlags.PassthruCentralNode;
            dockspaceID = ImGuiNET.ImGui.DockSpaceOverViewport(MainviewPortPTR, dockNodeFlags);

            // size of next appearing window
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);

            // each window
            windowPos = new System.Numerics.Vector2(0f, 0f);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            Myimgui.MyPropertyNode.Update(this);

            if (ShowDebugWindow == true)
            {
                windowPos = new System.Numerics.Vector2(0f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.MyPropertyTest.Update();
            }

            if (ShowInputWindow == true)
            {
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.Input.Update(inputEvent);
            }

            if (ShowOSWindow == true)
            {
                windowPos = new System.Numerics.Vector2(0f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.OS.Update();
            }

            if (ShowDemoWindow == true)
            {
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.ShowDemoWindow();
            }
        }

        public override void _Input(Godot.InputEvent @event)
        {
            // stops input from propagating down through each _Input call (improves performance)
            //GetViewport().SetInputAsHandled();
            inputEvent = @event;
            if (Godot.Input.IsActionPressed(MyInputMap.F1))
            {
                Enabled = !Enabled;
            }
        }
    }
}