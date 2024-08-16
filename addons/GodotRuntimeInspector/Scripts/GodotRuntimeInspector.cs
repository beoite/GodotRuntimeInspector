using System.Linq;

namespace GodotRuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
        public MyLog MyLog = new MyLog();

        public Myimgui.MultilineTextWindow MultilineTextWindow = new Myimgui.MultilineTextWindow();

        public ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();

        public ImGuiNET.ImGuiIOPtr IOPTR = null;

        public ImGuiNET.ImGuiDockNodeFlags DockNodeFlags = ImGuiNET.ImGuiDockNodeFlags.PassthruCentralNode;

        public bool MyFieldBool = false;

        public int MyFieldInt0 = 0;
        public int MyFieldInt1 = 1;
        public int MyFieldInt2 = 2;

        public string MyFieldString0 = nameof(MyFieldString0);
        public string MyFieldString1 = nameof(MyFieldString1);
        public string MyFieldString2 = nameof(MyFieldString2);

        public int MyPropertyInt0 { get; set; } = 0;
        public int MyPropertyInt1 { get; set; } = 1;
        public int MyPropertyInt2 { get; set; } = 2;

        public string MyPropertyString0 { get; set; } = nameof(MyPropertyString0);
        public string MyPropertyString1 { get; set; } = nameof(MyPropertyString1);
        public string MyPropertyString2 { get; set; } = nameof(MyPropertyString2);

        public Myimgui.MyPropertyNode MainWindow = new Myimgui.MyPropertyNode();

        public override void _Ready()
        {
            base._Ready();

            // pointers to MainViewport and IO
            MainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
            IOPTR = ImGuiNET.ImGui.GetIO();
            IOPTR.ConfigFlags |= ImGuiNET.ImGuiConfigFlags.DockingEnable;

            // pointers to MainViewport and IO
            MainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
            IOPTR = ImGuiNET.ImGui.GetIO();
            IOPTR.ConfigFlags |= ImGuiNET.ImGuiConfigFlags.DockingEnable;

            // style
            Config.Style = ImGuiNET.ImGui.GetStyle();
            Config.Style.WindowPadding = new System.Numerics.Vector2(0f, 0f);
            Config.Style.FramePadding = new System.Numerics.Vector2(0f, 0f);
            Config.Style.CellPadding = new System.Numerics.Vector2(0f, 0f);
            Config.Style.WindowRounding = 0f;
            Config.Style.ChildRounding = 0f;
            Config.Style.PopupRounding = 0f;
            Config.Style.FrameRounding = 0f;
            Config.Style.ScrollbarRounding = 0f;
            Config.Style.GrabRounding = 0f;
            Config.Style.TabRounding = 0f;
            Config.Style.WindowBorderSize = 0f;
            Config.Style.ChildBorderSize = 0f;
            Config.Style.FrameBorderSize = 0f;
            Config.Style.TabBorderSize = 0f;
            Config.Style.CellPadding = new System.Numerics.Vector2(0f, 0f);
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.Text] = Palette.CLOUDBLUE.ToVector4();
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Config.Opacity);
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.TableHeaderBg] = Palette.NIGHTBLUE.ToVector4();
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.TableBorderStrong] = Palette.SEABLUE.ToVector4();
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.TableBorderLight] = Palette.SKYBLUE.ToVector4();
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.TableRowBg] = Palette.NIGHTBLUE.ToVector4();
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.TableRowBgAlt] = Palette.SEABLUE.ToVector4();
            float alignX = 0f;
            float alignY = 0f;
            Config.Style.ButtonTextAlign = new System.Numerics.Vector2(alignX, alignY);
            Config.Style.SelectableTextAlign = new System.Numerics.Vector2(alignX, alignY);
            Config.Style.SeparatorTextAlign = new System.Numerics.Vector2(alignX, alignY);

            MyInputMap.Init();
        }

        public override void _Process(double delta)
        {
            if (Config.Enabled == false)
            {
                return;
            }

            Config.TotalDelta += delta;

            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Config.Opacity);

            Config.DockspaceID = ImGuiNET.ImGui.DockSpaceOverViewport(Config.DockspaceID, MainviewPortPTR, DockNodeFlags);

            // size, position of main window
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X, MainviewPortPTR.Size.Y / 4f);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);

            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowDockID(Config.DockspaceID, ImGuiNET.ImGuiCond.Appearing);

            // main window
            MainWindow.Update(this);

            // demo window
            if (Config.ShowDemoWindow == true)
            {
                ImGuiNET.ImGui.ShowDemoWindow();
            }

            // log window
            if (Config.Log == true)
            {
                windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                windowPos = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 2f, MainviewPortPTR.Size.Y / 2f);
                ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
                MultilineTextWindow.Update(MyLog.LogPath + " " + MyLog.LastLogRead, ref MyLog.LogData);
            }
        }

        public override void _UnhandledKeyInput(Godot.InputEvent @event)
        {
            base._UnhandledKeyInput(@event);

            if (Godot.Input.IsActionPressed(MyInputMap.gri_F1))
            {
                Config.Enabled = !Config.Enabled;
            }
        }
    }
}