namespace GodotRuntimeInspector.Scripts
{
    public static class Config
    {
        public static bool Enabled = true;
        
        public static float MinRowHeight = 25f;
        
        public static float Opacity = 0.9f;
        
        public static ImGuiNET.ImGuiViewportPtr MainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();
        
        public static ImGuiNET.ImGuiIOPtr IOPTR = null;
        
        public static ImGuiNET.ImGuiStylePtr Style = null;
        
        public static uint DockspaceID = 0;

        public static bool ShowDemoWindow = false;

        public static float Width = 800f;

        public static float Height = 200f;

        public static bool Log = false;

        public static double TotalDelta = 0;
    }
}
