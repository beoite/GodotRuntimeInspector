namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class Image
    {
        public System.Guid ID = System.Guid.NewGuid();
        public Godot.NoiseTexture2D GodotTexture = new Godot.NoiseTexture2D();
        public Godot.Image GodotImage = new Godot.Image();
        public Godot.ImageTexture GodotImageTexture = new Godot.ImageTexture();
        public int TextureChangedCounter = 0;

        public void Init()
        {
            GodotTexture.Noise = new Godot.FastNoiseLite();
            GodotTexture.Changed += TextureChanged;
        }

        private void TextureChanged()
        {
            TextureChangedCounter++;
            GodotImage = GodotTexture.GetImage();
            GodotImageTexture = Godot.ImageTexture.CreateFromImage(GodotImage);
        }

        public void Update()
        {
            string strID = "IMG###" + ID;
            if (ImGuiNET.ImGui.Begin(strID, MyPropertyFlags.ContainerWindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
                ImGuiNET.ImGui.Image((System.IntPtr)GodotImageTexture.GetRid().Id, windowSize);
                ImGuiNET.ImGui.End();
            }
        }
    }
}
