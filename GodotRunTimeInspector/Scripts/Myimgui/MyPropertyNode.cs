using ImGuiNET;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyNode
    {
        private static MyProperty[] myProperties = new MyProperty[0];
        private static Godot.Node selected = new Godot.Node() { Name = nameof(selected) };
        private static Godot.SceneTree? sceneTree = null;
        private static int counter = -1;
        private static MyPropertyTable myPropertyTable = new MyPropertyTable();

        static MyPropertyNode()
        {

        }

        private static void BuildMyProperties()
        {
            System.Reflection.FieldInfo[] fields = selected.GetType().GetFields();
            System.Reflection.PropertyInfo[] props = selected.GetType().GetProperties();
            int length = fields.Length + props.Length;
            myProperties = new MyProperty[length];
            int combinedIndex = -1;
            // Fields
            for (int i = 0; i < fields.Length; i++)
            {
                combinedIndex++;
                System.Reflection.FieldInfo field = fields[i];
                object? val = field.GetValue(selected);
                MyProperty myProperty = new MyProperty
                {
                    Index = i,
                    Tag = Tag.Field,
                    Name = field.Name,
                    Type = field.FieldType,
                    Instance = val
                };
                myProperties[combinedIndex] = myProperty;
            }
            // Properties
            for (int i = 0; i < props.Length; i++)
            {
                combinedIndex++;
                System.Reflection.PropertyInfo prop = props[i];
                object? val = prop.GetValue(selected, null);
                MyProperty myProperty = new MyProperty
                {
                    Index = i,
                    Tag = Tag.Property,
                    Name = prop.Name,
                    Type = prop.PropertyType,
                    Instance = val
                };
                myProperties[combinedIndex] = myProperty;
            }
        }

        public static void Update(Godot.Node node)
        {
            sceneTree = node.GetTree().Root.GetTree();
            if (selected.Name == nameof(selected))
            {
                selected = sceneTree.CurrentScene;
            }
            string windowName = Utility.GetAnimatedTitle(sceneTree.CurrentScene.SceneFilePath);
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
                BuildMyProperties();
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
                        counter = -1;
                        Traverse(sceneTree.CurrentScene);
                        ImGui.EndChild();
                    }
                }
                if (ImGui.TableNextColumn())
                {
                    float columnWidth = ImGui.GetColumnWidth();
                    System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(columnWidth, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
                    myPropertyTable.DrawTable(myProperties, nameof(myProperties), MyPropertyFlags.ContainerTableFlags(), tableSize);
                }
                ImGui.EndTable();
            }

        }

        private static void Traverse(Godot.Node node)
        {
            counter++;
            ImGuiTreeNodeFlags baseFlags = MyPropertyFlags.TreeNodeFlags();
            int childCount = node.GetChildCount();
            if (selected == node)
            {
                baseFlags |= ImGuiTreeNodeFlags.Selected;
            }
            if (childCount == 0)
            {
                baseFlags |= ImGuiTreeNodeFlags.Leaf;
            }
            if (counter == 0)
            {
                baseFlags |= ImGuiTreeNodeFlags.DefaultOpen;
            }
            if (ImGui.TreeNodeEx(node.Name + " | " + node.GetPath(), baseFlags))
            {
                if (ImGui.IsItemClicked())
                {
                    selected = node;
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
