namespace GodotRuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
        public static ImGuiNET.ImGuiStylePtr Style = ImGuiNET.ImGui.GetStyle();

        public static ImGuiNET.ImGuiIOPtr IOPTR = ImGuiNET.ImGui.GetIO();

        public bool Mybool = false;

        public sbyte Mysbyte = 0;

        public byte Mybyte = 0;

        public short Myshort = 0;

        public ushort Myushort = 0;

        public int Myint = 0;

        public uint Myuint = 0;

        public long Mylong = 0;

        public ulong Myulong = 0;

        public float Myfloat = 0;

        public double Mydouble = 0;

        public decimal Mydecimal = 0;

        public string Mystring = nameof(Mystring);

        public System.Numerics.Vector2 SystemVector2 = new System.Numerics.Vector2();

        public System.Numerics.Vector3 SystemVector3 = new System.Numerics.Vector3();

        public Godot.Vector2 GodotVector2 = new Godot.Vector2();

        public Godot.Vector3 GodotVector3 = new Godot.Vector3();

        public Godot.Quaternion GodotQuaternion = new Godot.Quaternion();

        public ImGuiNET.ImGuiDockNodeFlags DockNodeFlags = ImGuiNET.ImGuiDockNodeFlags.PassthruCentralNode;

        public uint DockspaceID = 0;

        public override void _Ready()
        {
            base._Ready();

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
            Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Config.Opacity);
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

            MyInputMap.Init();

            //System.Diagnostics.Debugger.Launch();
        }

        public override void _Process(double delta)
        {
            if (Config.Enabled == false)
            {
                return;
            }

            Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Config.Opacity);

            if (Config.Docking == true)
            {
                Docking();
            }

            Myimgui.WindowManager.Update(this);
        }

        public override void _UnhandledKeyInput(Godot.InputEvent @event)
        {
            base._UnhandledKeyInput(@event);

            if (Godot.Input.IsActionPressed(MyInputMap.gri_F1))
            {
                Config.Enabled = !Config.Enabled;
            }
        }

        private void Docking()
        {
            IOPTR = ImGuiNET.ImGui.GetIO();

            IOPTR.ConfigFlags |= ImGuiNET.ImGuiConfigFlags.DockingEnable;

            DockspaceID = ImGuiNET.ImGui.DockSpaceOverViewport(DockspaceID, ImGuiNET.ImGui.GetMainViewport(), DockNodeFlags);
        }
    }
}