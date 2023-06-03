namespace RuntimeInspector.Scripts
{
    public partial class SimpleCamera : Godot.Node
    {
        public double TotalDelta = 0;
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
            var arrow = Godot.ResourceLoader.Load("res://arrow.png");
            Godot.Input.SetCustomMouseCursor(arrow);
            CAM = (Godot.Camera3D)GetChild(0);
            TestCubes.Create(this);
            mainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            TotalDelta += delta;

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

            Imgui();

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

        private void Imgui()
        {
            Widget.DrawWidget((Godot.Node3D)CAM);
            ImGuiNET.ImGui.SetNextWindowSize(mainviewPortPTR.Size, ImGuiNET.ImGuiCond.Always);
            ImGuiNET.ImGui.SetNextWindowPos(new System.Numerics.Vector2(0f, 0f), ImGuiNET.ImGuiCond.Always);
            if (ImGuiNET.ImGui.Begin(nameof(SimpleCamera), Myimgui.MyPropertyFlags.HUDWindowFlags()))
            {
                ImGuiNET.ImGui.Text(nameof(multiplier) + " " + multiplier.ToString("0.00000000"));
                ImGuiNET.ImGui.Text(nameof(inputCounter) + " " + inputCounter.ToString("0.00000000"));
                ImGuiNET.ImGui.End();
            }
        }

        public override void _Input(Godot.InputEvent @event)
        {
            inputCounter++;
            WASD = Godot.Vector2.Zero;
            UpDown = Godot.Vector3.Zero;
            multiplier = 1f;
            if (Godot.Input.IsActionPressed(MyInputMap.FORWARD))
            {
                WASD.Y = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.LEFT))
            {
                WASD.X = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.BACK))
            {
                WASD.Y = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.RIGHT))
            {
                WASD.X = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.Q))
            {
                UpDown.Y = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.E))
            {
                UpDown.Y = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.LEFTSHIFT))
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
                        GodotRuntimeInspector.Hide = true;
                        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
                        Enabled = true;
                    }
                    else
                    {
                        GodotRuntimeInspector.Hide = false;
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
