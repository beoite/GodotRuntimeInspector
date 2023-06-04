namespace RuntimeInspector.Scripts
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
        public static Godot.Color VOID = new Godot.Color(0, 0, 0, 1);
        public static Godot.Color GRAY = new Godot.Color(157, 157, 157, 1);
        public static Godot.Color WHITE = new Godot.Color(255, 255, 255, 1);
        public static Godot.Color RED = new Godot.Color(190, 38, 51, 1);
        public static Godot.Color MEAT = new Godot.Color(224, 111, 139, 1);
        public static Godot.Color DARKBROWN = new Godot.Color(73, 60, 43, 1);
        public static Godot.Color BROWN = new Godot.Color(164, 100, 34, 1);
        public static Godot.Color ORANGE = new Godot.Color(235, 137, 49, 1);
        public static Godot.Color YELLOW = new Godot.Color(247, 226, 107, 1);
        public static Godot.Color DARKGREEN = new Godot.Color(47, 72, 78, 1);
        public static Godot.Color GREEN = new Godot.Color(68, 137, 26, 1);
        public static Godot.Color SLIMEGREEN = new Godot.Color(163, 206, 39, 1);
        public static Godot.Color NIGHTBLUE = new Godot.Color(27, 38, 50, 1);
        public static Godot.Color SEABLUE = new Godot.Color(0, 87, 132, 1);
        public static Godot.Color SKYBLUE = new Godot.Color(49, 162, 242, 1);
        public static Godot.Color CLOUDBLUE = new Godot.Color(178, 220, 239, 1);

        private static System.Collections.Generic.List<Godot.Color> colors = new System.Collections.Generic.List<Godot.Color>();

        static Palette()
        {
            colors.Add(VOID = Godot.Color.FromHtml(VOID_hex));
            colors.Add(GRAY = Godot.Color.FromHtml(GRAY_hex));
            colors.Add(WHITE = Godot.Color.FromHtml(WHITE_hex));
            colors.Add(RED = Godot.Color.FromHtml(RED_hex));
            colors.Add(MEAT = Godot.Color.FromHtml(MEAT_hex));
            colors.Add(DARKBROWN = Godot.Color.FromHtml(DARKBROWN_hex));
            colors.Add(BROWN = Godot.Color.FromHtml(BROWN_hex));
            colors.Add(ORANGE = Godot.Color.FromHtml(ORANGE_hex));
            colors.Add(YELLOW = Godot.Color.FromHtml(YELLOW_hex));
            colors.Add(DARKGREEN = Godot.Color.FromHtml(DARKGREEN_hex));
            colors.Add(GREEN = Godot.Color.FromHtml(GREEN_hex));
            colors.Add(SLIMEGREEN = Godot.Color.FromHtml(SLIMEGREEN_hex));
            colors.Add(NIGHTBLUE = Godot.Color.FromHtml(NIGHTBLUE_hex));
            colors.Add(SEABLUE = Godot.Color.FromHtml(SEABLUE_hex));
            colors.Add(SKYBLUE = Godot.Color.FromHtml(SKYBLUE_hex));
            colors.Add(CLOUDBLUE = Godot.Color.FromHtml(CLOUDBLUE_hex));
        }

        public static Godot.Color NewColor()
        {
            int index = new Godot.RandomNumberGenerator().RandiRange(0, colors.Count - 1);
            return colors[index];
        }

        public static System.Numerics.Vector4 ToVector4(this Godot.Color color, float alpha = 1f)
        {
            System.Numerics.Vector4 v4 = new System.Numerics.Vector4();
            v4.X = color.R;
            v4.Y = color.G;
            v4.Z = color.B;
            v4.W = alpha;
            return v4;
        }
    }
}
