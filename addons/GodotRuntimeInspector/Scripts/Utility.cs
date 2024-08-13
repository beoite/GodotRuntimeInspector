namespace GodotRuntimeInspector.Scripts
{
    public static class Utility
    {
        public static string GetStr(object? val)
        {
            string? str = null;
            try
            {
                if (val != null)
                {
                    str = val.ToString();
                }
            }
            catch (System.Exception) { }
            string strval = string.Empty;
            if (str != null)
            {
                strval = str.Trim();
            }
            return strval;
        }

        public static string GetAnimatedTitle(string? name)
        {
            char[] spin = "|/-\\".ToCharArray();
            int frame = (int)ImGuiNET.ImGui.GetTime() % spin.Length;
            string spinFrame = spin[frame].ToString();
            string animatedTitle = spinFrame + " " + GetStr(name);
            // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            string staticIdentifier = "###" + GetStr(name);
            animatedTitle = animatedTitle + staticIdentifier;
            return animatedTitle;
        }
    }
}
