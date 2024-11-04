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

            MyInputMap.Init();

            //System.Diagnostics.Debugger.Launch();
        }

        public override void _Process(double delta)
        {
            if (Config.Enabled == false)
            {
                return;
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
    }
}