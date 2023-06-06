namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class HUD
    {
        static HUD()
        {

        }

        public static void Update()
        {
            string header = nameof(HUD);
            int numCols = 2;
            int numRows = 10;
            float rowHeight = 10f;
            float colWidth = ImGuiNET.ImGui.GetMainViewport().Size.X / 2f;

            ImGuiNET.ImGui.Begin(header, MyPropertyFlags.HUDWindowFlags());

            System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(GodotRuntimeInspector.MainviewPortPTR.Size.X, numRows * rowHeight);
            if (ImGuiNET.ImGui.BeginTable(header + nameof(ImGuiNET.ImGui.BeginTable), numCols, MyPropertyFlags.HUDTableFlags(), tableSize))
            {
                ImGuiNET.ImGui.TableSetupColumn("1", MyPropertyFlags.HUDTableColumnFlags(), colWidth);
                ImGuiNET.ImGui.TableSetupColumn("2", MyPropertyFlags.HUDTableColumnFlags(), colWidth);

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(ImGuiNET.ImGui) + "." + nameof(ImGuiNET.ImGui.GetFrameCount));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(ImGuiNET.ImGui.GetFrameCount().ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(Godot) + "." + nameof(Godot.Time.GetTicksMsec));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(Godot.Time.GetTicksMsec().ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(Godot) + "." + nameof(Godot.Engine.GetFramesPerSecond));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(Godot.Engine.GetFramesPerSecond().ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(Godot) + "." + nameof(Godot.Engine.MaxFps));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(Godot.Engine.MaxFps.ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(GodotRuntimeInspector.FPS));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(GodotRuntimeInspector.FPS.ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(Godot) + "." + nameof(Godot.DisplayServer.WindowGetVsyncMode));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(Godot.DisplayServer.WindowGetVsyncMode().ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(Godot) + "." + nameof(Godot.DisplayServer.WindowGetMode));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(Godot.DisplayServer.WindowGetMode().ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(GodotRuntimeInspector.MainviewPortPTR.Size));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(GodotRuntimeInspector.MainviewPortPTR.Size.ToString());

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(nameof(Godot.OS.GetExecutablePath));
                ImGuiNET.ImGui.TableNextColumn();
                ImGuiNET.ImGui.Text(Godot.OS.GetExecutablePath());
            }
            ImGuiNET.ImGui.EndTable();

            ImGuiNET.ImGui.End();
        }
    }
}
