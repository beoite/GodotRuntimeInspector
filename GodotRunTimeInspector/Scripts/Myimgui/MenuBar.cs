using ImGuiNET;
using System.Linq;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MenuBar
    {
        public static void Update()
        {
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("| Menu \t |"))
                {
                    string txtEnabled = nameof(GodotRuntimeInspector.Enabled) + "\t(" + MyInputMap.F1 + ")";
                    bool enabled = ImGui.Checkbox(txtEnabled, ref GodotRuntimeInspector.Enabled);
                    bool showDemoWindow = ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDemoWindow), ref GodotRuntimeInspector.ShowDemoWindow);
                    bool debugEnabled = ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDebugWindow), ref GodotRuntimeInspector.ShowDebugWindow);
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("| Windows " + GodotRuntimeInspector.MyProperties.Count + "|"))
                {
                    if (ImGui.MenuItem("Close All"))
                    {
                        GodotRuntimeInspector.MyProperties.Clear();
                    }
                    string[] keys = GodotRuntimeInspector.MyProperties.Keys.ToArray();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        string key = keys[i];
                        MyProperty myProperty = GodotRuntimeInspector.MyProperties[key];
                        if (ImGui.MenuItem(key))
                        {
                            myProperty.Clicks++;
                        }
                    }
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }
        }
    }
}
