using ImGuiNET;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyNode
    {
        public static Godot.Node SelectedNode = new Godot.Node() { Name = nameof(SelectedNode) };
        public static MyProperty[] MyProperties = new MyProperty[0];


        public static Godot.SceneTree? SceneTree = null;
        public static int Counter = -1;
        public static MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public static void Update(Godot.Node node)
        {
            SceneTree = node.GetTree().Root.GetTree();
            if (SelectedNode.Name == nameof(SelectedNode))
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = MyProperty.NewArray(SelectedNode);
            string windowName = Utility.GetAnimatedTitle(SceneTree.CurrentScene.SceneFilePath);
            if (!ImGui.Begin(windowName, MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGui.End();
                return;
            }
            Myimgui.MenuBar.Update();
            System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
            System.Numerics.Vector2 outerTableSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
            int numCols = 2;
            if (ImGui.BeginTable(nameof(MyPropertyNode), numCols, MyPropertyFlags.ContainerTableFlags(), outerTableSize))
            {
                float width40 = 0.4f * windowSize.X;
                float width60 = 0.6f * windowSize.X;
                ImGui.TableSetupColumn("Scene", MyPropertyFlags.ContainerTableColumnFlags(), width40);
                ImGui.TableSetupColumn("Properties", MyPropertyFlags.ContainerTableColumnFlags(), width60);
                //ImGui.TableHeadersRow();

                ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                if (ImGui.TableNextColumn())
                {
                    string name = nameof(Traverse);
                    System.Numerics.Vector2 size = new System.Numerics.Vector2();
                    bool border = true;
                    if (ImGui.BeginChild(name, size, border, MyPropertyFlags.TreeNodeWindowFlags()))
                    {
                        Counter = -1;
                        Traverse(SceneTree.CurrentScene);
                        ImGui.EndChild();
                    }
                }
                if (ImGui.TableNextColumn())
                {
                    float columnWidth = ImGui.GetColumnWidth();
                    System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(columnWidth - GodotRuntimeInspector.MinRowHeight, windowSize.Y - GodotRuntimeInspector.MinRowHeight * 2);
                    MyPropertyTable.DrawTable(ref MyProperties, nameof(MyProperties), MyPropertyFlags.ContainerTableFlags(), tableSize);
                }
                ImGui.EndTable();
            }
            ImGui.End();
        }

        private static void Traverse(Godot.Node node)
        {
            Counter++;
            ImGuiTreeNodeFlags baseFlags = MyPropertyFlags.TreeNodeFlags();
            int childCount = node.GetChildCount();
            if (SelectedNode == node)
            {
                baseFlags |= ImGuiTreeNodeFlags.Selected;
            }
            if (childCount == 0)
            {
                baseFlags |= ImGuiTreeNodeFlags.Leaf;
            }
            if (Counter == 0)
            {
                baseFlags |= ImGuiTreeNodeFlags.DefaultOpen;
            }
            if (ImGui.TreeNodeEx(node.Name + " | " + node.GetPath(), baseFlags))
            {
                if (ImGui.IsItemClicked())
                {
                    SelectedNode = node;
                }
                for (int i = 0; i < childCount; i++)
                {
                    Traverse(node.GetChild(i));
                }
                ImGui.TreePop();
            }
        }
    }
}
