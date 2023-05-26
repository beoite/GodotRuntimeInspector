using ImGuiNET;

namespace RuntimeInspector.Scripts.Myimgui
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
            float colWidth = ImGui.GetMainViewport().Size.X / 2f;

            ImGui.Begin(header, MyPropertyFlags.HUDWindowFlags());

            System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(GodotRuntimeInspector.MAINVIEWPORTPTR.Size.X, numRows * rowHeight);
            if (ImGui.BeginTable(header + nameof(ImGui.BeginTable), numCols, MyPropertyFlags.HUDTableFlags(), tableSize))
            {
                ImGui.TableSetupColumn("1", MyPropertyFlags.HUDTableColumnFlags(), colWidth);
                ImGui.TableSetupColumn("2", MyPropertyFlags.HUDTableColumnFlags(), colWidth);

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(ImGui) + "." + nameof(ImGui.GetFrameCount));
                ImGui.TableNextColumn();
                ImGui.Text(ImGui.GetFrameCount().ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(Godot) + "." + nameof(Godot.Time.GetTicksMsec));
                ImGui.TableNextColumn();
                ImGui.Text(Godot.Time.GetTicksMsec().ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(Godot) + "." + nameof(Godot.Engine.GetFramesPerSecond));
                ImGui.TableNextColumn();
                ImGui.Text(Godot.Engine.GetFramesPerSecond().ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(Godot) + "." + nameof(Godot.Engine.MaxFps));
                ImGui.TableNextColumn();
                ImGui.Text(Godot.Engine.MaxFps.ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(GodotRuntimeInspector.FPS));
                ImGui.TableNextColumn();
                ImGui.Text(GodotRuntimeInspector.FPS.ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(Godot) + "." + nameof(Godot.DisplayServer.WindowGetVsyncMode));
                ImGui.TableNextColumn();
                ImGui.Text(Godot.DisplayServer.WindowGetVsyncMode().ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(Godot) + "." + nameof(Godot.DisplayServer.WindowGetMode));
                ImGui.TableNextColumn();
                ImGui.Text(Godot.DisplayServer.WindowGetMode().ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(GodotRuntimeInspector.MAINVIEWPORTPTR.Size));
                ImGui.TableNextColumn();
                ImGui.Text(GodotRuntimeInspector.MAINVIEWPORTPTR.Size.ToString());

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags());
                ImGui.TableNextColumn();
                ImGui.Text(nameof(Godot.OS.GetExecutablePath));
                ImGui.TableNextColumn();
                ImGui.Text(Godot.OS.GetExecutablePath());
            }
            ImGui.EndTable();

            ImGui.End();
        }
    }
}
