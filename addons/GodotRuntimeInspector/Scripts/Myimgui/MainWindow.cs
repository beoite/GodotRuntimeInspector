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
            // size
            Godot.Vector2 visibleRect = node.GetViewport().GetVisibleRect().Size;
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(visibleRect.X, visibleRect.Y * 0.3f);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);

            // position
            System.Numerics.Vector2 windowPos = System.Numerics.Vector2.Zero;
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);

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

            System.Numerics.Vector2 topSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - Config.MinRowHeight);
            System.Numerics.Vector2 topLeftSize = new System.Numerics.Vector2(windowSize.X * 0.3f, topSize.Y);
            System.Numerics.Vector2 topRightSize = new System.Numerics.Vector2(windowSize.X * 0.7f, topSize.Y);

            // table
            if (ImGuiNET.ImGui.BeginTable(nameof(topSize), 2, MyImguiFlags.TableFlags(), topSize))
            {
                ImGuiNET.ImGui.TableSetupColumn(nameof(topLeftSize), MyImguiFlags.TableColumnFlags(), topLeftSize.X);
                ImGuiNET.ImGui.TableSetupColumn(nameof(topRightSize), MyImguiFlags.TableColumnFlags(), topRightSize.X);

                ImGuiNET.ImGui.TableNextRow(MyImguiFlags.TableRowFlags(), Config.MinRowHeight);

                // left side, scene tree view
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    Counter = -1;
                    Traverse(sceneTree.CurrentScene);
                }

                // right side, field/property table
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    MyPropertyTable.Update(SelectedNode, MyProperties, nameof(MyProperties), topRightSize);
                }

                ImGuiNET.ImGui.EndTable();
            }

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

