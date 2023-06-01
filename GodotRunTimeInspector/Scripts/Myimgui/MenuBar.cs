using System.Linq;

namespace RuntimeInspector.Scripts.Myimgui
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
                    bool enabled = ImGuiNET.ImGui.Checkbox(txtEnabled, ref GodotRuntimeInspector.Enabled);
                    bool showDemoWindow = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDemoWindow), ref GodotRuntimeInspector.ShowDemoWindow);
                    bool debugEnabled = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDebugWindow), ref GodotRuntimeInspector.ShowDebugWindow);
                    bool inputEnabled = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowInputWindow), ref GodotRuntimeInspector.ShowInputWindow);
                    bool osEnabled = ImGuiNET.ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowOSWindow), ref GodotRuntimeInspector.ShowOSWindow);
                    ImGuiNET.ImGui.EndMenu();
                }

                if (ImGuiNET.ImGui.BeginMenu("| Windows " + GodotRuntimeInspector.MyProperties.Count + "|"))
                {
                    if (ImGuiNET.ImGui.MenuItem("Close All"))
                    {
                        GodotRuntimeInspector.MyProperties.Clear();
                    }
                    string[] keys = GodotRuntimeInspector.MyProperties.Keys.ToArray();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        string key = keys[i];
                        MyProperty myProperty = GodotRuntimeInspector.MyProperties[key];
                        if (ImGuiNET.ImGui.MenuItem(key))
                        {
                            myProperty.Clicks++;
                        }
                    }
                    ImGuiNET.ImGui.EndMenu();
                }

                ImGuiNET.ImGui.EndMenuBar();
            }
        }
    }
}
