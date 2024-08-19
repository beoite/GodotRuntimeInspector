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

        private ImGuiNET.ImGuiWindowFlags imGuiWindowFlags = new ImGuiNET.ImGuiWindowFlags();

        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();

        public MyWindow(MyProperty myProperty)
        {
            _myProperty = myProperty;

            imGuiWindowFlags |= ImGuiNET.ImGuiWindowFlags.NoSavedSettings;

            MainviewPortPTR = ImGuiNET.ImGui.GetMainViewport();
        }

        public void Update()
        {
            // size, position of main window
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(MainviewPortPTR.Size.X, MainviewPortPTR.Size.Y / 4f);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);

            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowDockID(Config.DockspaceID, ImGuiNET.ImGuiCond.Appearing);

            string controlId = Utility.ToControlId(_myProperty);

            if (!ImGuiNET.ImGui.Begin(controlId, imGuiWindowFlags))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            if (ImGuiNET.ImGui.Button("Close", new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), Config.MinRowHeight)))
            {
                MyWindowManager.Remove(_myProperty);
            }

            MyProperties = MyProperty.NewArray(_myProperty.Instance);
            MyPropertyTable.Update(null, MyProperties, nameof(MyProperties), topRightSize);

            ImGuiNET.ImGui.End();

        }
    }
}
