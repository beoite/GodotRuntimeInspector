using System.Linq;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyNode
    {
        public static Godot.Node SelectedNode = new Godot.Node() { Name = nameof(SelectedNode) };
        public static MyProperty[] MyProperties = new MyProperty[0];


        public static Godot.SceneTree? SceneTree = null;
        public static int Counter = -1;
        public static MyPropertyTable MyPropertyTable = new MyPropertyTable();

        private static System.Numerics.Vector2 windowSize = System.Numerics.Vector2.Zero;
        private static System.Numerics.Vector2 bottomSize = System.Numerics.Vector2.Zero;
        private static System.Numerics.Vector2 topSize = System.Numerics.Vector2.Zero;
        private static System.Numerics.Vector2 topLeftSize = System.Numerics.Vector2.Zero;
        private static System.Numerics.Vector2 topRightSize = System.Numerics.Vector2.Zero;

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
            if (ImGuiNET.ImGui.TreeNodeEx(node.Name + " | " + node.GetPath(), baseFlags))
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
                    if (ImGuiNET.ImGui.BeginChild(nameof(Traverse), topLeftSize, false, MyPropertyFlags.TreeNodeWindowFlags()))
                    {
                        Counter = -1;
                        Traverse(SceneTree?.CurrentScene);
                        ImGuiNET.ImGui.EndChild();
                    }
                }
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    topRightSize = new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), topSize.Y);
                    MyPropertyTable.DrawTable(ref MyProperties, nameof(topRightSize), MyPropertyFlags.TableFlags(), topRightSize);
                }
                ImGuiNET.ImGui.EndTable();
            }
        }

        private static void TabBar()
        {
            string[] keys = GodotRuntimeInspector.MyProperties.Keys.ToArray();
            if (ImGuiNET.ImGui.BeginChild(nameof(bottomSize), bottomSize, true, MyPropertyFlags.MyPropertyWindowFlags()))
            {
                if (ImGuiNET.ImGui.BeginTabBar(nameof(TabBar), MyPropertyFlags.TabBarFlags()))
                {
                    if (ImGuiNET.ImGui.TabItemButton("Close All [" + GodotRuntimeInspector.MyProperties.Count + "]", MyPropertyFlags.TabItemFlagsLeading()))
                    {
                        GodotRuntimeInspector.MyProperties.Clear();
                    }
                    for (int i = 0; i < keys.Length; i++)
                    {
                        string key = keys[i];
                        bool exists = GodotRuntimeInspector.MyProperties.ContainsKey(key);
                        Myimgui.MyProperty myProperty = new MyProperty();
                        if (exists == true)
                        {
                            myProperty = GodotRuntimeInspector.MyProperties[key];
                        }
                        bool tabOpen = true;
                        ImGuiNET.ImGuiTabItemFlags tabItemFlags = ImGuiNET.ImGuiTabItemFlags.None;
                        if (myProperty.Clicks > 0)
                        {
                            tabItemFlags = ImGuiNET.ImGuiTabItemFlags.SetSelected;
                            myProperty.Clicks = 0;
                        }
                        if (ImGuiNET.ImGui.BeginTabItem(key, ref tabOpen, tabItemFlags))
                        {
                            System.Numerics.Vector2 size = new System.Numerics.Vector2(bottomSize.X, bottomSize.Y - GodotRuntimeInspector.MinRowHeight);
                            myProperty.MyPropertyImgui.Update(myProperty, size);
                            ImGuiNET.ImGui.EndTabItem();
                        }
                        if (tabOpen == false)
                        {
                            GodotRuntimeInspector.MyProperties.Remove(key);
                        }
                    }
                    ImGuiNET.ImGui.EndTabBar();
                }
                ImGuiNET.ImGui.EndChild();
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
            if (SelectedNode.Name == nameof(SelectedNode))
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = MyProperty.NewArray(SelectedNode);

            Myimgui.MenuBar.Update();

            windowSize = ImGuiNET.ImGui.GetWindowSize();
            windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - (GodotRuntimeInspector.MinRowHeight * 2));
            float bottom = GodotRuntimeInspector.MinRowHeight * 3;
            topSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - bottom);
            topLeftSize = new System.Numerics.Vector2(windowSize.X * 0.4f, topSize.Y);
            topRightSize = new System.Numerics.Vector2(windowSize.X * 0.6f, topSize.Y);
            bottomSize = new System.Numerics.Vector2(windowSize.X, bottom);

            if (ImGuiNET.ImGui.BeginTable(nameof(MyPropertyNode), 1, MyPropertyFlags.TableFlags(), windowSize))
            {
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyPropertyNode), MyPropertyFlags.TableColumnFlags(), windowSize.X);
                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), topSize.Y);
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    TopRow();
                }
                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), bottom);
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    TabBar();
                }
            }
            ImGuiNET.ImGui.EndTable();
        }
    }
}

