namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class NoiseSeamless
    {
        public System.Guid ID = System.Guid.NewGuid();
        public Godot.FastNoiseLite FastNoise = new Godot.FastNoiseLite();
        public Godot.Image GodotImage = new Godot.Image();
        public Godot.ImageTexture GodotImageTexture = new Godot.ImageTexture();
        public int TextureChangedCounter = 0;
        public int Width = 128;
        public int Height = 128;
        public bool Invert = false;
        public bool In3DSpace = false;
        public float Skirt = 1.0f;
        public bool Normalize = true;
        public Myimgui.FastNoiseImgui FastNoiseImgui = new FastNoiseImgui();

        public NoiseSeamless()
        {
            Init();
        }

        public void Init()
        {
            GodotImage = FastNoise.GetSeamlessImage(Width, Height, Invert, In3DSpace, Skirt, Normalize);
            GodotImageTexture = Godot.ImageTexture.CreateFromImage(GodotImage);
        }

        public void Update()
        {
            string strID = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "###" + ID;
            if (ImGuiNET.ImGui.Begin(strID, MyPropertyFlags.ContainerWindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
                windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - GodotRuntimeInspector.MinRowHeight * 2f);
                float leftSize = windowSize.X * 0.4f;
                float rightSize = windowSize.X * 0.6f;
                if (ImGuiNET.ImGui.BeginTable(ID.ToString(), 2, MyPropertyFlags.TableFlags(), windowSize))
                {
                    ImGuiNET.ImGui.TableSetupColumn(nameof(leftSize), MyPropertyFlags.TableColumnFlags(), leftSize);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(rightSize), MyPropertyFlags.TableColumnFlags(), rightSize);
                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), windowSize.Y);
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        if (ImGuiNET.ImGui.BeginTable(ID.ToString(), 1, MyPropertyFlags.TableFlags(), new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), windowSize.Y)))
                        {
                            ImGuiNET.ImGui.TableSetupColumn(nameof(NoiseSeamless), MyPropertyFlags.TableColumnFlags(), ImGuiNET.ImGui.GetColumnWidth());
                            ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), windowSize.Y);
                            if (ImGuiNET.ImGui.TableNextColumn())
                            {
                                int init = 0;
                                if (ImGuiNET.ImGui.InputInt(nameof(Width), ref Width))
                                {
                                    init++;
                                }
                                if (ImGuiNET.ImGui.InputInt(nameof(Height), ref Height))
                                {
                                    init++;
                                }
                                if (ImGuiNET.ImGui.Checkbox(nameof(Invert), ref Invert))
                                {
                                    init++;
                                }
                                if (ImGuiNET.ImGui.Checkbox(nameof(In3DSpace), ref In3DSpace))
                                {
                                    init++;
                                }
                                if (ImGuiNET.ImGui.InputFloat(nameof(Skirt), ref Skirt))
                                {
                                    init++;
                                }
                                if (ImGuiNET.ImGui.Checkbox(nameof(Normalize), ref Normalize))
                                {
                                    init++;
                                }
                                Init();
                                FastNoiseImgui.Update(ref FastNoise);
                            }
                            ImGuiNET.ImGui.EndTable();
                        }
                    }
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        ImGuiNET.ImGui.Image((System.IntPtr)GodotImageTexture.GetRid().Id, new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), windowSize.Y));
                    }
                    ImGuiNET.ImGui.EndTable();
                }
                //ImGuiNET.ImGui.Image((System.IntPtr)GodotImageTexture.GetRid().Id, windowSize);
                ImGuiNET.ImGui.End();
            }
        }
    }
}
