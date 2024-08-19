namespace GodotRuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
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

        public override void _Ready()
        {
            base._Ready();

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

            //System.Diagnostics.Debugger.Launch();
        }

        public override void _Process(double delta)
        {
            if (Config.Enabled == false)
            {
                return;
            }

            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.WindowBg] = Palette.VOID.ToVector4(Config.Opacity);

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
    }
}