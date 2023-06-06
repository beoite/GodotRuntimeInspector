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
            char[] spin = "|/-\\".ToCharArray();
            int frame = (int)(ImGuiNET.ImGui.GetTime()) % spin.Length;
            string spinFrame = spin[frame].ToString();
            string animatedTitle = spinFrame + " " + GetStr(name);
            // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            string staticIdentifier = "###" + GetStr(name);
            animatedTitle = animatedTitle + staticIdentifier;
            return animatedTitle;
        }

        public static string PrettyJson(string unPrettyJson)
        {
            var options = new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(unPrettyJson);

            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }

        public static bool IsJsonValid(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return false;
            }
            try
            {
                using System.Text.Json.JsonDocument jsonDoc = System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch (System.Text.Json.JsonException)
            {
                return false;
            }
        }

        public static string SHA256String(byte[] buffer)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            foreach (byte b in buffer)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
