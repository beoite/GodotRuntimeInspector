namespace GodotRuntimeInspector.Scripts
{
    public partial class SimpleCamera : Godot.Node
    {
        public bool Enabled = false;
        public Godot.Vector2 MouseMotion = Godot.Vector2.Zero;
        public Godot.Vector2 WASD = Godot.Vector2.Zero;
        public Godot.Camera3D CAM = new Godot.Camera3D();
        public float RotationX = 0f;
        public float RotationY = 0f;
        public Godot.Transform3D CamTransform = new Godot.Transform3D();
        public Godot.Vector3 ForwardBack = Godot.Vector3.Zero;
        public Godot.Vector3 LeftRight = Godot.Vector3.Zero;
        public Godot.Vector3 UpDown = Godot.Vector3.Zero;
        public float multiplier = 1f;
        public uint inputCounter = 0;

        private static ImGuiNET.ImGuiViewportPtr mainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();

        public override void _Ready()
        {
            CAM = (Godot.Camera3D)GetChild(0);
            mainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);

            ForwardBack = -CAM.Transform.Basis.Z;
            ForwardBack *= (float)(WASD.Y * multiplier * delta);

            LeftRight = CAM.Transform.Basis.X;
            LeftRight *= (float)(WASD.X * multiplier * delta);

            Godot.Vector3 upDown = CAM.Transform.Basis.Y;
            upDown *= (float)(UpDown.Y * multiplier * delta);

            Godot.Vector3 newPos = Godot.Vector3.Zero;
            newPos += ForwardBack;
            newPos += LeftRight;
            newPos += upDown;
            CAM.GlobalPosition += newPos;
        }

        public override void _Process(double delta)
        {
            if (Enabled == false)
            {
                return;
            }

            // modify accumulated mouse rotation
            RotationX += MouseMotion.X * -1f * Config.Sensitivity;
            RotationY += MouseMotion.Y * -1f * Config.Sensitivity;
            MouseMotion = Godot.Vector2.Zero;

            CamTransform = CAM.Transform;
            CamTransform.Basis = Godot.Basis.Identity;
            CAM.Transform = CamTransform;

            // rotation
            CAM.RotateObjectLocal(Godot.Vector3.Up, RotationX); // first rotate about Y
            CAM.RotateObjectLocal(Godot.Vector3.Right, RotationY); // then rotate about X
        }

        public override void _Input(Godot.InputEvent @event)
        {
            inputCounter++;
            WASD = Godot.Vector2.Zero;
            UpDown = Godot.Vector3.Zero;
            multiplier = 1f;
            if (Godot.Input.IsActionPressed(MyInputMap.gri_FORWARD))
            {
                WASD.Y = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.gri_LEFT))
            {
                WASD.X = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.gri_BACK))
            {
                WASD.Y = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.gri_RIGHT))
            {
                WASD.X = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.gri_Q))
            {
                UpDown.Y = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.gri_E))
            {
                UpDown.Y = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.gri_LEFTSHIFT))
            {
                multiplier = 10f;
            }

            if (@event is Godot.InputEventMouseButton)
            {
                Godot.InputEventMouseButton btn = (Godot.InputEventMouseButton)@event;
                if (btn.ButtonIndex == Godot.MouseButton.Right)
                {
                    if (btn.IsPressed())
                    {
                        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
                        Enabled = true;
                    }
                    else
                    {
                        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Visible;
                        Enabled = false;
                    }
                }
            }
            if (@event is Godot.InputEventMouseMotion)
            {
                Godot.InputEventMouseMotion motion = (Godot.InputEventMouseMotion)@event;
                MouseMotion = motion.Relative;
            }
        }
    }
}
