using ImGuiNET;

namespace RuntimeInspector.Scripts
{
    public partial class SimpleCamera : Godot.Node
    {
        public double TotalDelta = 0;
        public bool Enabled = false;
        public float Sensitivity = 0.01f;

        public Godot.Vector2 MouseMotion = Godot.Vector2.Zero;
        public Godot.Vector2 WASD = Godot.Vector2.Zero;

        public Godot.Camera3D CAM = new Godot.Camera3D();
        // accumulators
        public float RotationX = 0f;
        public float RotationY = 0f;

        public Godot.Transform3D CamTransform = new Godot.Transform3D();
        public Godot.Vector3 ForwardBack = Godot.Vector3.Zero;
        public Godot.Vector3 LeftRight = Godot.Vector3.Zero;

        private static ImGuiViewportPtr mainviewPortPTR = new ImGuiViewportPtr();

        public override void _Ready()
        {
            var arrow = Godot.ResourceLoader.Load("res://arrow.png");
            Godot.Input.SetCustomMouseCursor(arrow);
            CAM = (Godot.Camera3D)GetChild(0);
            TestCubes.Create(this);
            mainviewPortPTR = ImGui.GetMainViewport();
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            TotalDelta += delta;
            float multiplier = 1f;
            ForwardBack = -CAM.Transform.Basis.Z;
            ForwardBack *= (float)(WASD.Y * multiplier * delta);

            LeftRight = CAM.Transform.Basis.X;
            LeftRight *= (float)(WASD.X * multiplier * delta);

            Godot.Vector3 newPos = Godot.Vector3.Zero;
            newPos += ForwardBack;
            newPos += LeftRight;
            CAM.GlobalPosition += newPos;
        }

        public override void _Process(double delta)
        {
            if (Enabled == false)
            {
                return;
            }

            Widget.DrawWidget((Godot.Node3D)CAM);

            float colWidth = mainviewPortPTR.Size.X / 2f;
            ImGui.SetNextWindowSize(mainviewPortPTR.Size, ImGuiCond.Always);
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(0f, 0f), ImGuiCond.Always);
            if (ImGui.Begin(nameof(SimpleCamera), Myimgui.MyPropertyFlags.HUDWindowFlags()))
            {
                ImGui.Text(nameof(MouseMotion) + " " + MouseMotion.ToString());
                ImGui.Text(nameof(RotationX) + " " + RotationX.ToString());
                ImGui.Text(nameof(RotationY) + " " + RotationY.ToString());
                ImGui.Text(nameof(WASD) + " " + WASD.ToString());
                ImGui.Text(nameof(CamTransform.Basis) + " " + CamTransform.Basis.ToString());
                ImGui.Text("Basis. 3×3 matrix used for 3D rotation and scale");
                ImGui.Text("X " + " " + CAM.Transform.Basis.X.ToString());
                ImGui.Text("Y " + " " + CAM.Transform.Basis.Y.ToString());
                ImGui.Text("Z " + " " + CAM.Transform.Basis.Z.ToString());
                ImGui.Text(nameof(CAM.GlobalPosition) + " " + CAM.GlobalPosition.ToString());
                ImGui.Text(nameof(ForwardBack) + " " + ForwardBack.ToString());
                ImGui.Text(nameof(LeftRight) + " " + LeftRight.ToString());

                //ImGui.Text(nameof(camTransform.Basis) + " " + camTransform..ToString());
                // 
                ImGui.End();
            }

            // modify accumulated mouse rotation
            RotationX += MouseMotion.X * -1f * Sensitivity;
            RotationY += MouseMotion.Y * -1f * Sensitivity;
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
            WASD = Godot.Vector2.Zero;
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

            if (@event is Godot.InputEventMouseButton)
            {
                Godot.InputEventMouseButton btn = (Godot.InputEventMouseButton)@event;
                if (btn.ButtonIndex == Godot.MouseButton.Right)
                {
                    if (btn.IsPressed())
                    {
                        GodotRuntimeInspector.Hide = true;
                        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
                    }
                    else
                    {
                        GodotRuntimeInspector.Hide = false;
                        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Visible;
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
