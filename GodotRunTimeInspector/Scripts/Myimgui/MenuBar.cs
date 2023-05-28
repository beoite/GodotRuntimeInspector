using ImGuiNET;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MenuBar
    {
        public static void Update()
        {
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("Menu"))
                {
                    string txtEnabled = nameof(GodotRuntimeInspector.Enabled) + "\t(" + MyInputMap.F1 + ")";
                    bool enabled = ImGui.Checkbox(txtEnabled, ref GodotRuntimeInspector.Enabled);
                    bool showDemoWindow = ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDemoWindow), ref GodotRuntimeInspector.ShowDemoWindow);
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }
        }
    }
}
