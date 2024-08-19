namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MainWindow
    {
        public Godot.Node SelectedNode = new Godot.Node() { Name = nameof(SelectedNode) };

        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public int Counter = -1;

        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        private System.Numerics.Vector2 windowSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topLeftSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topRightSize = System.Numerics.Vector2.Zero;

        private Godot.WeakRef WeakRef = new Godot.WeakRef();

        private readonly Godot.Node NothingSelected = new Godot.Node() { Name = nameof(NothingSelected) };

        private ImGuiNET.ImGuiViewportPtr _mainviewPortPTR = new ImGuiNET.ImGuiViewportPtr();

        public MainWindow()
        {

        }

        public MainWindow(ImGuiNET.ImGuiViewportPtr mainviewPortPTR)
        {
            _mainviewPortPTR = mainviewPortPTR;
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

        public void Update(Godot.Node node)
        {
            // size, position of main window
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(_mainviewPortPTR.Size.X, _mainviewPortPTR.Size.Y / 4f);
            System.Numerics.Vector2 windowPos = new System.Numerics.Vector2(0f, 0f);

            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(windowPos, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowDockID(Config.DockspaceID, ImGuiNET.ImGuiCond.Appearing);

            string windowTitle = node.Name + "###ssdadasd";
            if (!ImGuiNET.ImGui.Begin(windowTitle, MyImguiFlags.WindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            // build MyProperties
            if (WeakRef.GetRef().Obj == null)
            {
                SelectedNode = NothingSelected;
            }
            else
            {
                SelectedNode = node;
            }

            MyProperties = MyProperty.NewArray(SelectedNode);

            // menu
            MenuBar.Update();

            // sizes
            windowSize = ImGuiNET.ImGui.GetWindowSize();
            windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - (Config.MinRowHeight));
            topSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - Config.MinRowHeight);
            topLeftSize = new System.Numerics.Vector2(windowSize.X * 0.3f, topSize.Y);
            topRightSize = new System.Numerics.Vector2(windowSize.X * 0.7f, topSize.Y);

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
                    Traverse(node);
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
    }
}

