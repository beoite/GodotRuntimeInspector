using ImGuiNET;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class Utility
    {
        public static string GetStr(object? val)
        {
            string? str = string.Empty;
            if (val != null)
            {
                str = val.ToString();
            }
            string strval = string.Empty;
            if (str != null)
            {
                strval = str.Trim();
            }
            return strval;
        }

        public static string GetAnimatedTitle(string? name)
        {
            string animatedTitle = string.Empty;
            char[] spin = "|/-\\".ToCharArray();
            int frame = (int)(ImGui.GetTime()) % spin.Length;
            string spinFrame = spin[frame].ToString();
            animatedTitle = spinFrame + " " + GetStr(name);
            // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            string staticIdentifier = "###" + GetStr(name);
            animatedTitle = animatedTitle + staticIdentifier;
            return animatedTitle;
        }
    }
}
