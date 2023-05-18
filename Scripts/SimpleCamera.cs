using ImGuiNET;

namespace GodotRuntimeInspector.Scripts
{
    public partial class SimpleCamera : Godot.Node
    {
        public double TotalDelta = 0;
        public bool Enabled = false;
        public float Sensitivity = 0.01f;

        private Godot.Vector2 mouseMotion = Godot.Vector2.Zero;
        private Godot.Vector2 wasd = Godot.Vector2.Zero;

        private Godot.Camera3D cam = new Godot.Camera3D();
        // accumulators
        private float rotationX = 0f;
        private float rotationY = 0f;

        private Godot.Transform3D camTransform = new Godot.Transform3D();
        private Godot.Vector3 forwardBack = Godot.Vector3.Zero;
        private Godot.Vector3 leftRight = Godot.Vector3.Zero;

        public override void _Ready()
        {
            var arrow = Godot.ResourceLoader.Load("res://arrow.png");
            Godot.Input.SetCustomMouseCursor(arrow);
            cam = (Godot.Camera3D)GetChild(0);
            TestCubes.Create(this);
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            TotalDelta += delta;
            float multiplier = 1f;
            forwardBack = -cam.Transform.Basis.Z;
            forwardBack *= (float)(wasd.Y * multiplier * delta);

            leftRight = cam.Transform.Basis.X;
            leftRight *= (float)(wasd.X * multiplier * delta);

            Godot.Vector3 newPos = Godot.Vector3.Zero;
            newPos += forwardBack;
            newPos += leftRight;
            cam.GlobalPosition += newPos;
        }

        public override void _Process(double delta)
        {
            if (Enabled == false)
            {
                return;
            }

            Widget.DrawWidget((Godot.Node3D)cam);

            float colWidth = ImGui.GetMainViewport().Size.X / 2f;
            ImGui.SetNextWindowSize(GodotRuntimeInspector.MAINVIEWPORTPTR.Size, ImGuiCond.Always);
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(0f, 0f), ImGuiCond.Always);
            if (ImGui.Begin(nameof(SimpleCamera), Myimgui.MyPropertyFlags.HUDWindowFlags()))
            {
                ImGui.Text(nameof(mouseMotion) + " " + mouseMotion.ToString());
                ImGui.Text(nameof(rotationX) + " " + rotationX.ToString());
                ImGui.Text(nameof(rotationY) + " " + rotationY.ToString());
                ImGui.Text(nameof(wasd) + " " + wasd.ToString());
                ImGui.Text(nameof(camTransform.Basis) + " " + camTransform.Basis.ToString());
                ImGui.Text("Basis. 3×3 matrix used for 3D rotation and scale");
                ImGui.Text("X " + " " + cam.Transform.Basis.X.ToString());
                ImGui.Text("Y " + " " + cam.Transform.Basis.Y.ToString());
                ImGui.Text("Z " + " " + cam.Transform.Basis.Z.ToString());
                ImGui.Text(nameof(cam.GlobalPosition) + " " + cam.GlobalPosition.ToString());
                ImGui.Text(nameof(forwardBack) + " " + forwardBack.ToString());
                ImGui.Text(nameof(leftRight) + " " + leftRight.ToString());

                //ImGui.Text(nameof(camTransform.Basis) + " " + camTransform..ToString());
                // 
                ImGui.End();
            }

            // modify accumulated mouse rotation
            rotationX += mouseMotion.X * -1f * Sensitivity;
            rotationY += mouseMotion.Y * -1f * Sensitivity;
            mouseMotion = Godot.Vector2.Zero;

            camTransform = cam.Transform;
            camTransform.Basis = Godot.Basis.Identity;
            cam.Transform = camTransform;

            // rotation

            cam.RotateObjectLocal(Godot.Vector3.Up, rotationX); // first rotate about Y
            cam.RotateObjectLocal(Godot.Vector3.Right, rotationY); // then rotate about X
        }

        public override void _Input(Godot.InputEvent @event)
        {
            wasd = Godot.Vector2.Zero;
            if (Godot.Input.IsActionPressed(MyInputMap.FORWARD))
            {
                wasd.Y = 1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.LEFT))
            {
                wasd.X = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.BACK))
            {
                wasd.Y = -1f;
            }
            if (Godot.Input.IsActionPressed(MyInputMap.RIGHT))
            {
                wasd.X = 1f;
            }

            if (@event is Godot.InputEventMouseButton)
            {
                Godot.InputEventMouseButton btn = (Godot.InputEventMouseButton)@event;
                if (btn.ButtonIndex == Godot.MouseButton.Right)
                {
                    if (btn.IsPressed())
                    {
                        GodotRuntimeInspector.DisableUI();
                        Enabled = true;
                    }
                    else
                    {
                        Enabled = false;
                        GodotRuntimeInspector.EnableUI();
                    }
                }
            }
            if (@event is Godot.InputEventMouseMotion)
            {
                Godot.InputEventMouseMotion motion = (Godot.InputEventMouseMotion)@event;
                mouseMotion = motion.Relative;
            }
        }
    }
}
