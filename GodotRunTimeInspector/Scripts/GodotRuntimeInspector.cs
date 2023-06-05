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
        public static float Opacity = 0.9f;
        public static ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();
        public static ImGuiNET.ImGuiIOPtr IOPTR = null;
        public static ImGuiNET.ImGuiStylePtr Style = null;
        public static System.Collections.Generic.Dictionary<string, Myimgui.MyProperty> MyProperties = new System.Collections.Generic.Dictionary<string, Myimgui.MyProperty>();
        public static Godot.InputEvent? InputEvent = null;
        public static uint DockspaceID = 0;

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
            Style = ImGuiNET.ImGui.GetStyle();
            Style.WindowPadding = new System.Numerics.Vector2(0f, 0f);
            Style.FramePadding = new System.Numerics.Vector2(0f, 0f);
            Style.CellPadding = new System.Numerics.Vector2(0f, 0f);
            Style.WindowRounding = 0f;
            Style.ChildRounding = 0f;
            Style.PopupRounding = 0f;
            Style.FrameRounding = 0f;
            Style.ScrollbarRounding = 0f;
            Style.GrabRounding = 0f;
            Style.TabRounding = 0f;
            Style.WindowBorderSize = 0f;
            Style.ChildBorderSize = 0f;
            Style.FrameBorderSize = 0f;
            Style.TabBorderSize = 0f;
            Style.CellPadding = new System.Numerics.Vector2(0f, 0f);
            Style.Colors[(int)ImGuiNET.ImGuiCol.Text] = Palette.CLOUDBLUE.ToVector4();
            Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Opacity);
            Style.Colors[(int)ImGuiNET.ImGuiCol.TableHeaderBg] = Palette.NIGHTBLUE.ToVector4();
            Style.Colors[(int)ImGuiNET.ImGuiCol.TableBorderStrong] = Palette.SEABLUE.ToVector4();
            Style.Colors[(int)ImGuiNET.ImGuiCol.TableBorderLight] = Palette.SKYBLUE.ToVector4();
            Style.Colors[(int)ImGuiNET.ImGuiCol.TableRowBg] = Palette.NIGHTBLUE.ToVector4();
            Style.Colors[(int)ImGuiNET.ImGuiCol.TableRowBgAlt] = Palette.SEABLUE.ToVector4();
            float alignX = 0f;
            float alignY = 0f;
            Style.ButtonTextAlign = new System.Numerics.Vector2(alignX, alignY);
            Style.SelectableTextAlign = new System.Numerics.Vector2(alignX, alignY);
            Style.SeparatorTextAlign = new System.Numerics.Vector2(alignX, alignY);

            IsDebug = System.String.Equals(SceneManager.DebugPath, SceneFilePath, System.StringComparison.InvariantCultureIgnoreCase);
            if (IsDebug == true)
            {
                // load scenes
                SceneManager.Init();
                // Add the node as a child of the node the script is attached to.
                AddChild(SceneManager.SimpleCameraNode);
                //AddChild(SceneManager.TerrainNode);
                TestCubes.Create(this);
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
            
            Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Opacity);

            // make the central node invisible and inputs pass-thru
            ImGuiNET.ImGuiDockNodeFlags dockNodeFlags = ImGuiNET.ImGuiDockNodeFlags.PassthruCentralNode;
            DockspaceID = ImGuiNET.ImGui.DockSpaceOverViewport(MainviewPortPTR, dockNodeFlags);

            // size, position of next appearing window
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X, MainviewPortPTR.Size.Y);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);

            // each window
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowDockID(DockspaceID, ImGuiNET.ImGuiCond.Appearing);
            Myimgui.MyPropertyNode.Update(this);

            if (ShowDebugWindow == true)
            {
                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.MyPropertyTest.Update();
            }

            if (ShowInputWindow == true)
            {
                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.Input.Update(InputEvent);
            }

            if (ShowOSWindow == true)
            {
                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.OS.Update();
            }

            if (ShowDemoWindow == true)
            {
                ImGuiNET.ImGui.ShowDemoWindow();
            }
        }

        public override void _Input(Godot.InputEvent @event)
        {
            // stops input from propagating down through each _Input call (improves performance)
            //GetViewport().SetInputAsHandled();
            InputEvent = @event;
            if (Godot.Input.IsActionPressed(MyInputMap.F1))
            {
                Enabled = !Enabled;
            }
        }
    }
}