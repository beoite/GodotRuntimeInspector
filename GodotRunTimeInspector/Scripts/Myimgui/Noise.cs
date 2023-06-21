namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class Noise
    {
        public System.Guid ID = System.Guid.NewGuid();
        public MyChild MyChild = new MyChild();
        public Godot.FastNoiseLite FastNoise = new Godot.FastNoiseLite();
        public Godot.NoiseTexture2D GodotTexture = new Godot.NoiseTexture2D();
        public Godot.Image GodotImage = new Godot.Image();
        public Godot.ImageTexture GodotImageTexture = new Godot.ImageTexture();
        public int TextureChangedCounter = 0;

        public void Init()
        {
            GodotTexture.Noise = FastNoise;
            if (TextureChangedCounter == 0)
            {
                GodotTexture.Changed += TextureChanged;
            }
        }

        private void TextureChanged()
        {
            TextureChangedCounter++;
            GetImage();
        }

        private void GetImage()
        {
            GodotImage = GodotTexture.GetImage();
            GodotImageTexture = Godot.ImageTexture.CreateFromImage(GodotImage);
        }

        public void Update()
        {
            string strID = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "###" + ID;
            if (ImGuiNET.ImGui.Begin(strID, MyPropertyFlags.ContainerWindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
                windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - GodotRuntimeInspector.MinRowHeight * 2f);
                System.Numerics.Vector2 leftSize = new System.Numerics.Vector2(windowSize.X * 0.4f, windowSize.Y);
                System.Numerics.Vector2 rightSize = new System.Numerics.Vector2(windowSize.X * 0.6f, windowSize.Y);
                if (ImGuiNET.ImGui.BeginTable(ID.ToString(), 2, MyPropertyFlags.TableFlags(), windowSize))
                {
                    ImGuiNET.ImGui.TableSetupColumn(nameof(leftSize), MyPropertyFlags.TableColumnFlags(), leftSize.X);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(rightSize), MyPropertyFlags.TableColumnFlags(), rightSize.X);
                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), windowSize.Y);
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        int init = 0;
                        init += Myimgui.FastNoise.Update(ref FastNoise);
                        if (init > 0)
                        {
                            GetImage();
                        }
                    }
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        rightSize = new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), windowSize.Y);
                        ImGuiNET.ImGui.Image((System.IntPtr)GodotImageTexture.GetRid().Id, rightSize);
                    }
                    ImGuiNET.ImGui.EndTable();
                }
                ImGuiNET.ImGui.End();
            }
        }
    }
}
