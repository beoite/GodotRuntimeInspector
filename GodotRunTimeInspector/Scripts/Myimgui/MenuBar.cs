using System.Linq;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class MenuBar
    {
        public static void Update()
        {
            if (ImGuiNET.ImGui.BeginMenuBar())
            {
                if (ImGuiNET.ImGui.BeginMenu("| Menu \t |"))
                {
                    string txtEnabled = nameof(GodotRuntimeInspector.Enabled) + "\t(" + MyInputMap.F1 + ")";
                    bool opacity = ImGuiNET.ImGui.SliderFloat(nameof(GodotRuntimeInspector.Opacity), ref GodotRuntimeInspector.Opacity, 0f, 1f);
                    bool enabled = ImGuiNET.ImGui.Checkbox(txtEnabled, ref GodotRuntimeInspector.Enabled);
                    bool showDemoWindow = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDemoWindow), ref GodotRuntimeInspector.ShowDemoWindow);
                    bool debugEnabled = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.Debug), ref GodotRuntimeInspector.Debug);
                    bool inputEnabled = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.Input), ref GodotRuntimeInspector.Input);
                    bool osEnabled = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.RenderingDevice), ref GodotRuntimeInspector.RenderingDevice);
                    
                    ImGuiNET.ImGui.EndMenu();
                }

                ImGuiNET.ImGui.EndMenuBar();
            }
        }
    }
}
