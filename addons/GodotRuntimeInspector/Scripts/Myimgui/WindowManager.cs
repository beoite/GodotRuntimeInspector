namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class WindowManager
    {
        public static MainWindow MainWindow = new MainWindow();

        public static MultilineTextWindow MultilineTextWindow = new MultilineTextWindow();

        public static System.Collections.Generic.Dictionary<System.Guid, MyPropertyInspector> MyPropertyInspectors = new System.Collections.Generic.Dictionary<System.Guid, MyPropertyInspector>();

        public static MyLog MyLog = new MyLog();

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
            ImGuiNET.ImGuiViewportPtr imGuiViewportPtr = ImGuiNET.ImGui.GetMainViewport();

            // size
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(Config.WindowSizeX, Config.WindowSizeY);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);

            // position
            ImGuiNET.ImGui.SetNextWindowPos(System.Numerics.Vector2.Zero, ImGuiNET.ImGuiCond.Appearing);

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
