using Godot;
using System.Collections.Generic;

namespace GodotRuntimeInspector.Scripts
{
    public static class Palette
    {
        // http://androidarts.com/palette/16pal.htm

        public const string VOID_hex = "#000000";
        public const string GRAY_hex = "#9D9D9D";
        public const string WHITE_hex = "#FFFFFF";
        public const string RED_hex = "#BE2633";
        public const string MEAT_hex = "#E06F8B";
        public const string DARKBROWN_hex = "#493C2B";
        public const string BROWN_hex = "#A46422";
        public const string ORANGE_hex = "#EB8931";
        public const string YELLOW_hex = "#F7E26B";
        public const string DARKGREEN_hex = "#2F484E";
        public const string GREEN_hex = "#44891A";
        public const string SLIMEGREEN_hex = "#A3CE27";
        public const string NIGHTBLUE_hex = "#1B2632";
        public const string SEABLUE_hex = "#005784";
        public const string SKYBLUE_hex = "#31A2F2";
        public const string CLOUDBLUE_hex = "#B2DCEF";

        // rgb values, overwritten by hex in the constuctor
        public static Color VOID = new Color(0, 0, 0, 1);
        public static Color GRAY = new Color(157, 157, 157, 1);
        public static Color WHITE = new Color(255, 255, 255, 1);
        public static Color RED = new Color(190, 38, 51, 1);
        public static Color MEAT = new Color(224, 111, 139, 1);
        public static Color DARKBROWN = new Color(73, 60, 43, 1);
        public static Color BROWN = new Color(164, 100, 34, 1);
        public static Color ORANGE = new Color(235, 137, 49, 1);
        public static Color YELLOW = new Color(247, 226, 107, 1);
        public static Color DARKGREEN = new Color(47, 72, 78, 1);
        public static Color GREEN = new Color(68, 137, 26, 1);
        public static Color SLIMEGREEN = new Color(163, 206, 39, 1);
        public static Color NIGHTBLUE = new Color(27, 38, 50, 1);
        public static Color SEABLUE = new Color(0, 87, 132, 1);
        public static Color SKYBLUE = new Color(49, 162, 242, 1);
        public static Color CLOUDBLUE = new Color(178, 220, 239, 1);

        private static List<Color> colors = new List<Color>();
        static Palette()
        {
            colors.Add(VOID = Color.FromHtml(VOID_hex));
            colors.Add(GRAY = Color.FromHtml(GRAY_hex));
            colors.Add(WHITE = Color.FromHtml(WHITE_hex));
            colors.Add(RED = Color.FromHtml(RED_hex));
            colors.Add(MEAT = Color.FromHtml(MEAT_hex));
            colors.Add(DARKBROWN = Color.FromHtml(DARKBROWN_hex));
            colors.Add(BROWN = Color.FromHtml(BROWN_hex));
            colors.Add(ORANGE = Color.FromHtml(ORANGE_hex));
            colors.Add(YELLOW = Color.FromHtml(YELLOW_hex));
            colors.Add(DARKGREEN = Color.FromHtml(DARKGREEN_hex));
            colors.Add(GREEN = Color.FromHtml(GREEN_hex));
            colors.Add(SLIMEGREEN = Color.FromHtml(SLIMEGREEN_hex));
            colors.Add(NIGHTBLUE = Color.FromHtml(NIGHTBLUE_hex));
            colors.Add(SEABLUE = Color.FromHtml(SEABLUE_hex));
            colors.Add(SKYBLUE = Color.FromHtml(SKYBLUE_hex));
            colors.Add(CLOUDBLUE = Color.FromHtml(CLOUDBLUE_hex));
        }

        public static Color NewColor()
        {
            int index = new Godot.RandomNumberGenerator().RandiRange(0, colors.Count - 1);
            return colors[index];
        }

        public static System.Numerics.Vector4 ToVector4(this Color color)
        {
            System.Numerics.Vector4 v4 = new System.Numerics.Vector4();
            v4.X = color.R;
            v4.Y = color.G;
            v4.Z = color.B;
            v4.W = color.A;
            return v4;

        }
    }
}
