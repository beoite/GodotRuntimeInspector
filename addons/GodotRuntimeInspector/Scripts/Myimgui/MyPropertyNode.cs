using System.Linq;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyNode
    {
        public static Godot.Node SelectedNode = new Godot.Node() { Name = nameof(SelectedNode) };

        public static MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public static Godot.SceneTree? SceneTree = null;

        public static int Counter = -1;

        public static MyPropertyTable MyPropertyTable = new MyPropertyTable();

        private static System.Numerics.Vector2 windowSize = System.Numerics.Vector2.Zero;

        private static System.Numerics.Vector2 topSize = System.Numerics.Vector2.Zero;

        private static System.Numerics.Vector2 topLeftSize = System.Numerics.Vector2.Zero;

        private static System.Numerics.Vector2 topRightSize = System.Numerics.Vector2.Zero;

        private static Godot.WeakRef WeakRef = new Godot.WeakRef();

        private static readonly Godot.Node NothingSelected = new Godot.Node() { Name = nameof(NothingSelected) };

        private static void Traverse(Godot.Node? node)
        {
            if (node == null)
            {
                return;
            }
            Counter++;
            ImGuiNET.ImGuiTreeNodeFlags baseFlags = MyPropertyFlags.TreeNodeFlags();
            int childCount = node.GetChildCount();
            if (SelectedNode == node)
            {
                baseFlags |= ImGuiNET.ImGuiTreeNodeFlags.Selected;
            }
            if (childCount == 0)
            {
                baseFlags |= ImGuiNET.ImGuiTreeNodeFlags.Leaf;
            }
            if (Counter == 0)
            {
                baseFlags |= ImGuiNET.ImGuiTreeNodeFlags.DefaultOpen;
            }

            System.Numerics.Vector4 currentColor = Config.Style.Colors[(int)ImGuiNET.ImGuiCol.Text];
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.Text] = Palette.CLOUDBLUE.ToVector4();
            bool processModeDisabled = node.ProcessMode == Godot.Node.ProcessModeEnum.Disabled;
            if (processModeDisabled == true)
            {
                Config.Style.Colors[(int)ImGuiNET.ImGuiCol.Text] = Palette.NIGHTBLUE.ToVector4();
            }
            if (ImGuiNET.ImGui.TreeNodeEx(node.Name + " | " + node.GetPath(), baseFlags))
            {
                if (ImGuiNET.ImGui.IsItemClicked())
                {
                    SelectedNode = node;
                    WeakRef = Godot.WeakRef.WeakRef(node);
                }
                for (int i = 0; i < childCount; i++)
                {
                    Traverse(node.GetChild(i));
                }
                ImGuiNET.ImGui.TreePop();
            }
            Config.Style.Colors[(int)ImGuiNET.ImGuiCol.Text] = currentColor;
        }

        private static void TopRow()
        {
            if (ImGuiNET.ImGui.BeginTable(nameof(topSize), 2, MyPropertyFlags.TableFlags(), topSize))
            {
                ImGuiNET.ImGui.TableSetupColumn(nameof(topLeftSize), MyPropertyFlags.TableColumnFlags(), topLeftSize.X);
                ImGuiNET.ImGui.TableSetupColumn(nameof(topRightSize), MyPropertyFlags.TableColumnFlags(), topRightSize.X);
                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), topSize.Y);
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    topLeftSize = new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), topSize.Y);
                    if (ImGuiNET.ImGui.BeginChild(nameof(Traverse), topLeftSize))
                    {
                        Counter = -1;
                        Traverse(SceneTree?.CurrentScene);
                        ImGuiNET.ImGui.EndChild();
                    }
                }
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    topRightSize = new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), topSize.Y);
                    MyPropertyTable.DrawTable(MyProperties, nameof(topRightSize), MyPropertyFlags.TableFlags(), topRightSize);
                }
                ImGuiNET.ImGui.EndTable();
            }
        }

        public static void Update(Godot.Node node)
        {
            SceneTree = node.GetTree().Root.GetTree();
            if (!ImGuiNET.ImGui.Begin(Utility.GetAnimatedTitle(SceneTree.CurrentScene.SceneFilePath), MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }
            if (WeakRef.GetRef().Obj == null)
            {
                SelectedNode = NothingSelected;
            }
            if (SelectedNode.Name == nameof(SelectedNode))
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = MyProperty.NewArray(SelectedNode);

            Myimgui.MenuBar.Update();

            windowSize = ImGuiNET.ImGui.GetWindowSize();
            windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - (Config.MinRowHeight));
            topSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - Config.MinRowHeight);
            topLeftSize = new System.Numerics.Vector2(windowSize.X * 0.3f, topSize.Y);
            topRightSize = new System.Numerics.Vector2(windowSize.X * 0.7f, topSize.Y);

            if (ImGuiNET.ImGui.BeginTable(nameof(MyPropertyNode), 1, MyPropertyFlags.TableFlags(), windowSize))
            {
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyPropertyNode), MyPropertyFlags.TableColumnFlags(), windowSize.X);
                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), topSize.Y);
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    TopRow();
                }
                ImGuiNET.ImGui.EndTable();
            }
            ImGuiNET.ImGui.End();
        }
    }
}

