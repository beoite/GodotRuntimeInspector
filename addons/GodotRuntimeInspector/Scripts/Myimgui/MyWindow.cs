using System;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyWindow
    {
        private System.Numerics.Vector2 windowSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topLeftSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topRightSize = System.Numerics.Vector2.Zero;

        private MyProperty _myProperty;

        ImGuiNET.ImGuiWindowFlags imGuiWindowFlags = new ImGuiNET.ImGuiWindowFlags();

        public MyWindow(MyProperty myProperty)
        {
            _myProperty = myProperty;

            imGuiWindowFlags |= ImGuiNET.ImGuiWindowFlags.NoSavedSettings;
        }

        public void Update()
        {
            string controlId = Utility.ToControlId(_myProperty);

            if (!ImGuiNET.ImGui.Begin(controlId, imGuiWindowFlags))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            windowSize = new System.Numerics.Vector2(600, 400);
            windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - (Config.MinRowHeight));
            topSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - Config.MinRowHeight);
            topLeftSize = new System.Numerics.Vector2(windowSize.X * 0.5f, topSize.Y);
            topRightSize = new System.Numerics.Vector2(windowSize.X * 0.5f, topSize.Y);

            if (ImGuiNET.ImGui.BeginTable(nameof(topSize), 2, MyPropertyFlags.TableFlags(), topSize))
            {
                ImGuiNET.ImGui.TableSetupColumn(nameof(topLeftSize), MyPropertyFlags.TableColumnFlags(), topLeftSize.X);
                ImGuiNET.ImGui.TableSetupColumn(nameof(topRightSize), MyPropertyFlags.TableColumnFlags(), topRightSize.X);

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.TableRowFlags(), Config.MinRowHeight);

                // left side, scene tree view
                if (ImGuiNET.ImGui.TableNextColumn())
                {

                }

                // right side, field/property table
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    if (ImGuiNET.ImGui.Button("Close", new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), Config.MinRowHeight)))
                    {
                        MyWindowManager.Remove(_myProperty);
                    }
                }

                ImGuiNET.ImGui.EndTable();
            }

            ImGuiNET.ImGui.End();

        }
    }
}
