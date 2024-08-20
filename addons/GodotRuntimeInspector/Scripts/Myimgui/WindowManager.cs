namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class WindowManager
    {
        public static MainWindow MainWindow = new MainWindow();

        public static MultilineTextWindow MultilineTextWindow = new MultilineTextWindow();

        public static System.Collections.Generic.Dictionary<System.Guid, MyPropertyInspector> MyPropertyInspectors = new System.Collections.Generic.Dictionary<System.Guid, MyPropertyInspector>();

        public static MyLog MyLog = new MyLog();

        public static System.Numerics.Vector2 WindowSize = System.Numerics.Vector2.Zero;

        public static void Add(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);

            bool contains = MyPropertyInspectors.ContainsKey(myProperty.Id);

            if (contains == false)
            {
                MyPropertyInspector myPropertyInspector = new MyPropertyInspector(myProperty);

                MyPropertyInspectors.Add(myProperty.Id, myPropertyInspector);
            }
        }

        public static void Remove(MyProperty myProperty)
        {
            bool removed = MyPropertyInspectors.Remove(myProperty.Id);
        }

        public static void Update(Godot.Node node)
        {
            // size
            Godot.Vector2 visibleRect = node.GetViewport().GetVisibleRect().Size;
            WindowSize = new System.Numerics.Vector2(visibleRect.X, visibleRect.Y * Config.MainWindowSize);
            ImGuiNET.ImGui.SetNextWindowSize(WindowSize, ImGuiNET.ImGuiCond.Appearing);

            // position
            System.Numerics.Vector2 windowPos = System.Numerics.Vector2.Zero;
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);

            // main window
            MainWindow.Update(node);

            // log window
            if (Config.Log == true)
            {
                MultilineTextWindow.Update(MyLog.LogPath, ref MyLog.LogData);
            }

            // demo window
            if (Config.ShowDemoWindow == true)
            {
                ImGuiNET.ImGui.ShowDemoWindow();
            }
        }
    }
}
