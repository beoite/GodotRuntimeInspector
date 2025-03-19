namespace GodotRuntimeInspector.Scripts
{
    public partial class GodotRuntimeInspector : Godot.Node
    {
        public bool Mybool = false;
        public sbyte Mysbyte = 0;
        public byte Mybyte = 0;
        public short Myshort = 0;
        public ushort Myushort = 0;
        public int Myint = 0;
        public uint Myuint = 0;
        public long Mylong = 0;
        public ulong Myulong = 0;
        public float Myfloat = 0;
        public double Mydouble = 0;
        public decimal Mydecimal = 0;
        public string Mystring = nameof(Mystring);
        public System.Numerics.Vector2 SystemVector2 = new();
        public System.Numerics.Vector3 SystemVector3 = new();
        public Godot.Vector2 GodotVector2 = new();
        public Godot.Vector3 GodotVector3 = new();
        public Godot.Quaternion GodotQuaternion = new();
        public Myimgui.MyProperty[] MyProperties = [];
        public int Counter = -1;
        public Myimgui.MyPropertyTable MyPropertyTable = new();
        public static readonly Godot.Node NothingSelected = new() { Name = nameof(NothingSelected) };
        public Godot.Node SelectedNode = NothingSelected;
        public Godot.SceneTree SceneTree = new();
        private void SetSelectedNode()
        {
            SceneTree = GetTree().Root.GetTree();
            if (SelectedNode.Name == NothingSelected.Name)
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = Myimgui.MyProperty.NewArray(SelectedNode);
        }
        private void Traverse(Godot.Node? node)
        {
            if (node == null)
            {
                return;
            }
            Counter++;
            ImGuiNET.ImGuiTreeNodeFlags baseFlags = Flags.TreeNodeFlags();
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
            if (ImGuiNET.ImGui.TreeNodeEx(node.GetPath(), baseFlags))
            {
                if (ImGuiNET.ImGui.IsItemClicked())
                {
                    SelectedNode = node;
                }
                for (int i = 0; i < childCount; i++)
                {
                    Traverse(node.GetChild(i));
                }
                ImGuiNET.ImGui.TreePop();
            }
        }
        public override void _Ready()
        {
            base._Ready();
            MyInputMap.Init();
        }
        public override void _Process(double delta)
        {
            if (Config.Enabled == false)
            {
                return;
            }
            string name = SceneFilePath;
            if (SceneTree.CurrentScene is not null)
            {
                name = SceneTree.CurrentScene.SceneFilePath;
            }
            Godot.Vector2I windowGetSize = Godot.DisplayServer.WindowGetSize();
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(windowGetSize.X / 2, windowGetSize.Y / 3f);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(System.Numerics.Vector2.Zero, ImGuiNET.ImGuiCond.Appearing);
            bool begin = ImGuiNET.ImGui.Begin(name + "###" + nameof(GodotRuntimeInspector), Flags.WindowFlags());
            if (begin)
            {
                SetSelectedNode();
                System.Numerics.Vector2 contentRegionAvail = ImGuiNET.ImGui.GetContentRegionAvail();
                // table
                int cols = 2;
                if (ImGuiNET.ImGui.BeginTable(nameof(GodotRuntimeInspector), cols, Flags.TableFlags(), contentRegionAvail))
                {
                    contentRegionAvail = ImGuiNET.ImGui.GetContentRegionAvail();
                    float col1Width = contentRegionAvail.X * 0.33f;
                    ImGuiNET.ImGui.TableSetupColumn("Nodes", Flags.TableColumnFlags(), col1Width);
                    float col2Width = contentRegionAvail.X * 0.66f;
                    ImGuiNET.ImGui.TableSetupColumn("Properties", Flags.TableColumnFlags(), col2Width);
                    ImGuiNET.ImGui.TableNextRow();
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        Traverse(this);
                    }
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        contentRegionAvail = ImGuiNET.ImGui.GetContentRegionAvail();
                        MyPropertyTable.Update(SelectedNode, MyProperties, nameof(MyPropertyTable), contentRegionAvail);
                    }
                    ImGuiNET.ImGui.EndTable();
                }
            }
            ImGuiNET.ImGui.End();
        }
        public override void _UnhandledKeyInput(Godot.InputEvent @event)
        {
            base._UnhandledKeyInput(@event);
            if (Godot.Input.IsActionPressed(nameof(Config.EnabledKey)))
            {
                Config.Enabled = !Config.Enabled;
            }
        }
    }
}