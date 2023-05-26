using System;
using ImGuiNET;
using System.Reflection;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class Container
    {
        private static int width = 256;
        private static int height = 256;
        private static bool invert = false;
        private static bool in3DSpace = false;
        private static float skirt = 0.1f;
        private static bool normalize = true;
        private static MyProperty[] myProperties = new MyProperty[0];
        private static System.Reflection.PropertyInfo[] props = new System.Reflection.PropertyInfo[0];
        private static Godot.FastNoiseLite noise = new Godot.FastNoiseLite();
        private static Godot.Image? imageSeamless = null;
        private static Godot.Image? imageTest = null;
        private static Godot.Image? imageScrollNoise = null;
        private static int selectedImage = -1;
        private static Godot.Image[] images = new Godot.Image[0];
        private static Godot.ImageTexture[] textures = new Godot.ImageTexture[0];
        private static IntPtr[] pointers = new IntPtr[0];
        private static string info = string.Empty;
        private static string? name = string.Empty;
        private static bool dropdown = false;

        static Container()
        {
            name = MethodBase.GetCurrentMethod()?.DeclaringType?.Name;

            imageTest = Godot.Image.Create(width, height, false, Godot.Image.Format.Rgbaf);
            imageTest.Fill(Palette.NewColor());
            imageTest.ResourceName = nameof(imageTest);

            //imageTest.SavePng("res://" + nameof(imageTest) + ".png");
            imageSeamless = noise.GetSeamlessImage(width, height, invert, in3DSpace, skirt, normalize);
            imageSeamless.ResourceName = nameof(imageSeamless);

            imageScrollNoise = noise.GetSeamlessImage(width, height, invert, in3DSpace, skirt, normalize); ;
            imageScrollNoise.Rotate180();
            imageScrollNoise.ResourceName = nameof(imageScrollNoise);

            images = new Godot.Image[3];
            images[0] = imageTest;
            images[1] = imageSeamless;
            images[2] = imageScrollNoise;

            textures = new Godot.ImageTexture[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                textures[i] = Godot.ImageTexture.CreateFromImage(images[i]);
            }

            pointers = new IntPtr[textures.Length];
            for (int i = 0; i < textures.Length; i++)
            {
                pointers[i] = ImGuiGodot.ImGuiGD.BindTexture(textures[i]);
            }
            selectedImage = 1;
            BuildMyProperties();
        }

        private static void BuildMyProperties()
        {
            info = nameof(info) + "> " + images[selectedImage].GetFormat() + " " + images[selectedImage].GetSize();
            props = images[selectedImage].GetType().GetProperties();
            myProperties = new MyProperty[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                System.Reflection.PropertyInfo prop = props[i];
                object? val = prop.GetValue(images[selectedImage]);
                MyProperty myProperty = new MyProperty
                {
                    Index = i,
                    Name = images[selectedImage].GetType().Namespace + "." + images[selectedImage].GetType().Name + "." + prop.Name,
                    Value = Utility.GetStr(val)
                };
                myProperties[i] = myProperty;
            }
        }

        public static void Update()
        {
            if (ImGui.Begin(Utility.GetAnimatedTitle(name), MyPropertyFlags.ContainerWindowFlags()))
            {
                float padding = 10f;
                int numCols = 2;
                System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
                System.Numerics.Vector2 containerTableSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - Godot.Mathf.Pow(padding, 2.11f));
                System.Numerics.Vector2 halfContainerTableSize = new System.Numerics.Vector2((containerTableSize.X / numCols) - (padding * numCols), containerTableSize.Y - padding);

                ImGui.Text(info);
                if (ImGui.SmallButton(images[selectedImage].ResourceName))
                {
                    dropdown = !dropdown;
                }
                if (dropdown == true)
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        bool isSelected = selectedImage == i;
                        if (ImGui.MenuItem(images[i].ResourceName, null, isSelected))
                        {
                            selectedImage = i;
                            BuildMyProperties();
                            dropdown = false;
                        }
                    }
                }

                string container = name + nameof(container);
                if (ImGui.BeginTable(container, numCols, MyPropertyFlags.ContainerTableFlags(), containerTableSize))
                {
                    ImGui.TableSetupColumn("Inspector", MyPropertyFlags.ContainerTableColumnFlags(), halfContainerTableSize.X);
                    ImGui.TableSetupColumn("Viewer", MyPropertyFlags.ContainerTableColumnFlags(), halfContainerTableSize.X);

                    ImGui.TableNextRow(MyPropertyFlags.HeadersTableRowFlags());
                    ImGui.TableNextColumn();

                    string tableID = name + "TABLE";
                    MyPropertyTable.DrawTable(myProperties, tableID, MyPropertyFlags.TableFlags(), halfContainerTableSize);

                    ImGui.TableNextColumn();

                    if (ImGui.BeginTabBar("MyTabBar", MyPropertyFlags.ContainerTabBarFlags()))
                    {
                        if (ImGui.BeginTabItem("Image"))
                        {
                            System.Numerics.Vector2 size = halfContainerTableSize;
                            System.Numerics.Vector2 uv0 = new System.Numerics.Vector2(0f, 0f);
                            System.Numerics.Vector2 uv1 = new System.Numerics.Vector2(1f, 1f);
                            System.Numerics.Vector4 tint_col = new System.Numerics.Vector4(1f, 1f, 1f, 1f);
                            System.Numerics.Vector4 border_col = new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 1f);
                            ImGui.Image(pointers[selectedImage], size, uv0, uv1, tint_col, border_col);
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Text"))
                        {
                            bool result = ImGui.InputTextMultiline("##source", ref MyPropertyTable.SelectedValue, 8192, halfContainerTableSize, MyPropertyFlags.ContainerInputTextFlags());
                            ImGui.EndTabItem();
                        }
                        ImGui.EndTabBar();
                    }

                    ImGui.EndTable();
                }
                ImGui.End();
            }
        }
    }
}
