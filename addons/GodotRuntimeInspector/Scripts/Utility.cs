using GodotRuntimeInspector.Scripts.Myimgui;
using System.Net.NetworkInformation;

namespace GodotRuntimeInspector.Scripts
{
    public static class Utility
    {
        public static string GetStr(object? instance)
        {
            string? str = null;
            try
            {
                if (instance != null)
                {
                    str = instance.ToString();
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

        public static MyTypes GetMyType(object? instance)
        {
            if (instance?.IsBoolean() == true)
            {
                return MyTypes.Boolean;
            }
            else if (instance?.IsNumber() == true)
            {
                return MyTypes.Number;
            }
            else if (instance?.IsString() == true)
            {
                return MyTypes.String;
            }
            else if (instance is not null)
            {
                return MyTypes.Complex;
            }

            return MyTypes.None;
        }

        public static bool IsBoolean(this object value)
        {
            return value is bool;
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        public static bool IsString(this object value)
        {
            return value is string;
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
