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
                if (ImGui.BeginMenu("[Menu \t]"))
                {
                    string txtEnabled = nameof(GodotRuntimeInspector.Enabled) + "\t(" + MyInputMap.F1 + ")";
                    bool enabled = ImGui.Checkbox(txtEnabled, ref GodotRuntimeInspector.Enabled);
                    bool showDemoWindow = ImGui.Checkbox(nameof(GodotRuntimeInspector.ShowDemoWindow), ref GodotRuntimeInspector.ShowDemoWindow);
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("[MyProperties " + GodotRuntimeInspector.MyProperties.Count + "]"))
                {
                    if (ImGui.MenuItem("Close All"))
                    {
                        GodotRuntimeInspector.MyProperties.Clear();
                        GodotRuntimeInspector.TotalClicks = 0;
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
