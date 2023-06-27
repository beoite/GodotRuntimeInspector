using System.Linq;

namespace GodotRuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
        public double TotalDelta = 0;
        public static bool IsDebug = false;
        public static double FPS = 0;
        public static int MaxFps = 30;
        public static bool Enabled = true;
        public static bool Hide = false;
        public static float MinRowHeight = 25f;
        public static float Opacity = 0.9f;
        public static ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();
        public static ImGuiNET.ImGuiIOPtr IOPTR = null;
        public static ImGuiNET.ImGuiStylePtr Style = null;
        public static Godot.InputEvent? InputEvent = null;
        public static uint DockspaceID = 0;
        public static Myimgui.Noise ImageNoise = new Myimgui.Noise();
        public static Myimgui.NoiseSeamless ImageNoiseSeamless = new Myimgui.NoiseSeamless();

        // Windows
        public static bool ShowDemoWindow = false;
        public static bool Debug = false;
        public static bool Input = false;
        public static bool RenderingDevice = false;
        public static bool Log = false;
        public static bool LogDebug = false;
        public static bool Noise = false;
        public static System.Collections.Generic.Dictionary<Myimgui.MyWindow, bool> MyWindowDictionary = new System.Collections.Generic.Dictionary<Myimgui.MyWindow, bool>();
        public static Myimgui.MyWindow WindowDebug = new Myimgui.MyWindow();
        public static Myimgui.MyWindow WindowInput = new Myimgui.MyWindow();
        public static Myimgui.MyWindow WindowRenderingDevice = new Myimgui.MyWindow();
        public static Myimgui.MyWindow WindowLogDebug = new Myimgui.MyWindow();
        public static Myimgui.MyWindow WindowImage = new Myimgui.MyWindow();
        public static Myimgui.MultilineTextWindow MultilineTextWindow = new Myimgui.MultilineTextWindow();

        public static MyLog MyLog = new MyLog();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            WindowDebug.MyProperties = Myimgui.MyPropertyTest.Init();

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
                //AddChild(SceneManager.TestNode);
                TestCubes.Create(this);
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            TotalDelta += delta;

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
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X, MainviewPortPTR.Size.Y / 4f);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);

            // each window
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowDockID(DockspaceID, ImGuiNET.ImGuiCond.Appearing);
            Myimgui.MyPropertyNode.Update(this);

            WindowRenderingDevice.TypeInstance = Godot.RenderingServer.GetRenderingDevice();
            WindowInput.TypeInstance = InputEvent;
            WindowLogDebug.TypeInstance = MyLog;

            MyWindowDictionary[WindowDebug] = Debug;
            MyWindowDictionary[WindowInput] = Input;
            MyWindowDictionary[WindowRenderingDevice] = RenderingDevice;
            MyWindowDictionary[WindowLogDebug] = LogDebug;

            Myimgui.MyWindow[] keys = MyWindowDictionary.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                Myimgui.MyWindow window = keys[i];
                if (MyWindowDictionary[window] == true)
                {
                    window.Update();
                }
            }

            if (Noise == true)
            {
                //windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                //windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                //ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                //ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                //ImageNoiseSeamless.Update();

                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                ImageNoise.Update();

                //windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                //windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                //ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                //ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                //WindowImage.TypeInstance = ImageNoiseSeamless.FastNoise;
                //WindowImage.Update();
            }

            if (Log == true)
            {
                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                MyLog.Update(TotalDelta);
                MultilineTextWindow.Update(MyLog.LogPath, ref MyLog.LogData);
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