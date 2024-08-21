using System.Linq;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MainWindow
    {
        public Godot.Node SelectedNode = new Godot.Node() { Name = nameof(SelectedNode) };

        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public int Counter = -1;

        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public Godot.WeakRef WeakRef = new Godot.WeakRef();

        public readonly Godot.Node NothingSelected = new Godot.Node() { Name = nameof(NothingSelected) };

        public void Update(Godot.Node node)
        {
            // window start
            string controlId = node.SceneFilePath;

            if (!ImGuiNET.ImGui.Begin(controlId, MyImguiFlags.WindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            // selected
            Godot.SceneTree sceneTree = node.GetTree().Root.GetTree();

            if (WeakRef.GetRef().Obj == null)
            {
                SelectedNode = NothingSelected;
            }
            if (SelectedNode.Name == nameof(SelectedNode) || SelectedNode.Name == nameof(NothingSelected))
            {
                SelectedNode = sceneTree.CurrentScene;
            }

            MyProperties = MyProperty.NewArray(SelectedNode);

            // menu
            MenuBar.Update();

            // table
            System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
            float bottomRowHeight = Config.MinRowHeight * 8;
            System.Numerics.Vector2 topRowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - bottomRowHeight);
            System.Numerics.Vector2 bottomRowSize = new System.Numerics.Vector2(windowSize.X, bottomRowHeight);

            if (ImGuiNET.ImGui.BeginTable(nameof(MainWindow), 2, MyImguiFlags.TableFlags(), topRowSize))
            {
                float col1Width = 0.33f;
                float col2Width = 0.66f;

                ImGuiNET.ImGui.TableSetupColumn(nameof(Traverse), MyImguiFlags.TableColumnFlags(), topRowSize.X * col1Width);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyPropertyTable), MyImguiFlags.TableColumnFlags(), topRowSize.X * col2Width);

                ImGuiNET.ImGui.TableNextRow(MyImguiFlags.TableRowFlags());

                // left side, scene tree view
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    Counter = -1;
                    Traverse(sceneTree.CurrentScene);
                }

                // right side, field/property table
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    //if (ImGuiNET.ImGui.BeginChild(nameof(MyPropertyTable)))
                    //{
                    float colWidth = ImGuiNET.ImGui.GetColumnWidth();
                    System.Numerics.Vector2 propertyTableSize = new System.Numerics.Vector2(colWidth, topRowSize.Y - Config.MinRowHeight);

                    MyPropertyTable.Update(SelectedNode, MyProperties, nameof(MyProperties), propertyTableSize);

                    //    ImGuiNET.ImGui.EndChild();
                    //}
                }

                ImGuiNET.ImGui.EndTable();
            }

            // children
            //if (ImGuiNET.ImGui.BeginChild(nameof(WindowManager.MyPropertyInspectors)))
            //{
            System.Guid[] keys = WindowManager.MyPropertyInspectors.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                System.Guid key = keys[i];

                MyPropertyInspector myPropertyInspector = WindowManager.MyPropertyInspectors[key];

                myPropertyInspector.Update(bottomRowSize);
            }

            //ImGuiNET.ImGui.EndChild();
            //}

            ImGuiNET.ImGui.End();
        }

        private void Traverse(Godot.Node? node)
        {
            if (node == null)
            {
                return;
            }
            Counter++;
            ImGuiNET.ImGuiTreeNodeFlags baseFlags = MyImguiFlags.TreeNodeFlags();
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
                Config.Style.Colors[(int)ImGuiNET.ImGuiCol.Text] = Palette.MEAT.ToVector4();
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

    }
}

