namespace Examples
{
    public partial class TestWindow : Godot.Node
    {
        public ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();

        public override void _EnterTree()
        {
            MainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
        }
        public override void _Ready()
        {

        }

        public override void _Process(double delta)
        {
            Imgui();
        }

        private void Imgui()
        {
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X / 4f, MainviewPortPTR.Size.Y / 2f);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, MainviewPortPTR.Size.Y - windowSize.Y);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            if (!ImGuiNET.ImGui.Begin(nameof(TestWindow), GodotRuntimeInspector.Scripts.Myimgui.MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            ImGuiNET.ImGui.End();
        }
    }
}
