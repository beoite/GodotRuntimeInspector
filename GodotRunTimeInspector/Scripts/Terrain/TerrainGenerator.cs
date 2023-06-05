namespace RuntimeInspector.Scripts.Terrain
{
    public partial class TerrainGenerator : Godot.Node
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
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X, MainviewPortPTR.Size.Y);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            if (!ImGuiNET.ImGui.Begin(nameof(TerrainGenerator), Myimgui.MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            ImGuiNET.ImGui.End();
        }
    }
}
