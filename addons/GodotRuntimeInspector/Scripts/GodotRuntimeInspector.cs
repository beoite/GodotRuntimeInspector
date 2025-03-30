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
        public static readonly Godot.Node NothingSelected = new() { Name = nameof(NothingSelected) };
        public Godot.Node SelectedNode = NothingSelected;
        public MyProperty[] MyProperties = [];
        public MyPropertyTable MyPropertyTable = new();
        public Godot.SceneTree SceneTree = new();
        private void Traverse(Godot.Node? node)
        {
            if (node == null)
            {
                return;
            }
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
            Godot.Vector2I windowGetSize = Godot.DisplayServer.WindowGetSize();
            System.Numerics.Vector2 windowSize = new System.Numerics.Vector2(windowGetSize.X / 2, windowGetSize.Y / 3f);
            ImGuiNET.ImGui.SetNextWindowSize(windowSize, ImGuiNET.ImGuiCond.Appearing);
            ImGuiNET.ImGui.SetNextWindowPos(System.Numerics.Vector2.Zero, ImGuiNET.ImGuiCond.Appearing);
            SceneTree = GetTree().Root.GetTree();
            bool selectedNodeInstanceValid = Godot.GodotObject.IsInstanceValid(SelectedNode);
            if(selectedNodeInstanceValid is false)
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            if (SelectedNode == NothingSelected)
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = MyProperty.NewArray(SelectedNode);
            string name = SelectedNode.Name + " " + SceneTree.CurrentScene.SceneFilePath;
            bool begin = ImGuiNET.ImGui.Begin(name + "###" + nameof(GodotRuntimeInspector), Flags.WindowFlags());
            if (begin)
            {
                System.Numerics.Vector2 contentRegionAvail = ImGuiNET.ImGui.GetContentRegionAvail();
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
                        Traverse(GetTree().Root);
                    }
                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        MyPropertyTable.Update(SelectedNode, MyProperties, nameof(MyPropertyTable));
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