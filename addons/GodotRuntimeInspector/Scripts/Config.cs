﻿namespace GodotRuntimeInspector.Scripts
{
    public static class Config
    {
        public static bool Enabled = true;
        
        public static float MinRowHeight = 25f;
        
        public static float Opacity = 0.9f;
        
        public static ImGuiNET.ImGuiStylePtr Style = null;
        
        public static uint DockspaceID = 0;

        public static bool ShowDemoWindow = false;

        public static bool Log = false;

        public static uint InputTextMaxLength = 64;

        public static float MainWindowSize = 0.5f;
    }
}
