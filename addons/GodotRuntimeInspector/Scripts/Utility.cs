using GodotRuntimeInspector.Scripts.Myimgui;
namespace GodotRuntimeInspector.Scripts
{
    public static class Utility
    {
        private static string ToString(object? instance)
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
        public static string ToControlId(MyProperty myProperty)
        {
            string text = ToString(myProperty.Instance);
            string controlId = text + "###" + myProperty.Name;
            return controlId;
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
    }
}
