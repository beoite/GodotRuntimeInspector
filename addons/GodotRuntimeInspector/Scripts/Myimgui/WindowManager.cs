using System.Linq;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class WindowManager
    {
        public static MainWindow MainWindow = new MainWindow();

        public static MultilineTextWindow MultilineTextWindow = new MultilineTextWindow();

        public static System.Collections.Generic.Dictionary<string, MyWindow> MyWindows = new System.Collections.Generic.Dictionary<string, MyWindow>();

        public static MyLog MyLog = new MyLog();

        public static System.Numerics.Vector2 WindowSize = System.Numerics.Vector2.Zero;

        public static void Add(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);

            bool contains = MyWindows.ContainsKey(controlId);

            if (contains == false)
            {
                MyWindow myWindow = new MyWindow(myProperty);

                MyWindows.Add(controlId, myWindow);
            }
        }

        public static void Remove(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);

            bool removed = MyWindows.Remove(controlId);
        }

        public static void Update(Godot.Node node)
        {
            // size
            Godot.Vector2 visibleRect = node.GetViewport().GetVisibleRect().Size;
            WindowSize = new System.Numerics.Vector2(visibleRect.X, visibleRect.Y * 0.3f);
            ImGuiNET.ImGui.SetNextWindowSize(WindowSize, ImGuiNET.ImGuiCond.Appearing);

            // position
            System.Numerics.Vector2 windowPos = System.Numerics.Vector2.Zero;
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);

            // main window
            MainWindow.Update(node);

            // child windows
            string[] keys = MyWindows.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                // size
                ImGuiNET.ImGui.SetNextWindowSize(WindowSize, ImGuiNET.ImGuiCond.Appearing);

                // position
                windowPos = new System.Numerics.Vector2(0, visibleRect.Y - WindowSize.Y);
                ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);

                string key = keys[i];
                MyWindow myWindow = MyWindows[key];
                myWindow.Update();
            }

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
