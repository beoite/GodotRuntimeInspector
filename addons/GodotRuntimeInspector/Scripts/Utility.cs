using GodotRuntimeInspector.Scripts.Myimgui;
using System.Net.NetworkInformation;

namespace GodotRuntimeInspector.Scripts
{
    public static class Utility
    {
        public static string ToString(object? instance)
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
            else if (instance?.IsVector2() == true)
            {
                return MyTypes.Vector2;
            }
            else if (instance?.IsVector3() == true)
            {
                return MyTypes.Vector3;
            }
            else
            {
                return MyTypes.Complex;
            }
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

        public static bool IsVector2(this object value)
        {
            return value is System.Numerics.Vector2
                    || value is Godot.Vector2;
        }

        public static bool IsVector3(this object value)
        {
            return value is System.Numerics.Vector3
                    || value is Godot.Vector3;
        }

        public static string GetAnimatedTitle(string? name)
        {
            char[] spin = "|/-\\".ToCharArray();
            int frame = (int)ImGuiNET.ImGui.GetTime() % spin.Length;
            string spinFrame = spin[frame].ToString();
            string animatedTitle = spinFrame + " " + ToString(name);
            // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            string staticIdentifier = "###" + ToString(name);
            animatedTitle = animatedTitle + staticIdentifier;
            return animatedTitle;
        }
    }
}
