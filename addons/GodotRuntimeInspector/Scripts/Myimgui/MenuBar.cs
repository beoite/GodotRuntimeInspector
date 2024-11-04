namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class MenuBar
    {
        public static void Update()
        {
            if (ImGuiNET.ImGui.BeginMenuBar())
            {
                if (ImGuiNET.ImGui.BeginMenu("Menu"))
                {
                    string txtEnabled = nameof(Config.Enabled) + "\t(F1)";
                    bool enabled = ImGuiNET.ImGui.Checkbox(txtEnabled, ref Config.Enabled);
                    bool showDemoWindow = ImGuiNET.ImGui.Checkbox(nameof(Config.ShowDemoWindow), ref Config.ShowDemoWindow);
                    bool log = ImGuiNET.ImGui.Checkbox(nameof(Config.Log), ref Config.Log);
                    bool showDebugColumns = ImGuiNET.ImGui.Checkbox(nameof(Config.ShowDebugColumns), ref Config.ShowDebugColumns);

                    ImGuiNET.ImGui.EndMenu();
                }
                ImGuiNET.ImGui.EndMenuBar();
            }
        }
    }
}
