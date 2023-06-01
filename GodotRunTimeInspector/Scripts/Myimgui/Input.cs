﻿namespace RuntimeInspector.Scripts.Myimgui
{
    public static class Input
    {
        private static MyProperty[] myProperties = new MyProperty[0];
        private static MyPropertyTable myPropertyTable = new MyPropertyTable();

        public static void Update(Godot.InputEvent? inputEvent)
        {
            myProperties = MyProperty.NewArray(inputEvent);

            if (ImGuiNET.ImGui.Begin(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name, MyPropertyFlags.WindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
                System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(windowSize.X - GodotRuntimeInspector.MinRowHeight, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
                string tableID = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
                myPropertyTable.DrawTable(ref myProperties, tableID, MyPropertyFlags.TableFlags(), tableSize);
                ImGuiNET.ImGui.End();
            }
        }
    }
}
