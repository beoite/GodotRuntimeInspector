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

        public static void Update(Godot.Node node)
        {
            SceneTree = node.GetTree().Root.GetTree();
            if (SelectedNode.Name == nameof(SelectedNode))
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = MyProperty.NewArray(SelectedNode);
            string windowName = Utility.GetAnimatedTitle(SceneTree.CurrentScene.SceneFilePath);

            if (!ImGuiNET.ImGui.Begin(windowName, MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }
            Myimgui.MenuBar.Update();
            System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
            System.Numerics.Vector2 outerTableSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
            int numCols = 2;
            if (ImGuiNET.ImGui.BeginTable(nameof(MyPropertyNode), numCols, MyPropertyFlags.ContainerTableFlags(), outerTableSize))
            {
                float width40 = 0.4f * windowSize.X;
                float width60 = 0.6f * windowSize.X;
                ImGuiNET.ImGui.TableSetupColumn("Scene", MyPropertyFlags.ContainerTableColumnFlags(), width40);
                ImGuiNET.ImGui.TableSetupColumn("Properties", MyPropertyFlags.ContainerTableColumnFlags(), width60);
                //ImGui.TableHeadersRow();

                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    string name = nameof(Traverse);
                    System.Numerics.Vector2 size = new System.Numerics.Vector2();
                    bool border = true;
                    if (ImGuiNET.ImGui.BeginChild(name, size, border, MyPropertyFlags.TreeNodeWindowFlags()))
                    {
                        Counter = -1;
                        Traverse(SceneTree.CurrentScene);
                        ImGuiNET.ImGui.EndChild();
                    }
                }
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    float columnWidth = ImGuiNET.ImGui.GetColumnWidth();
                    float topHeight = outerTableSize.Y * 0.7f - GodotRuntimeInspector.MinRowHeight;
                    float bottomHeight = outerTableSize.Y * 0.4f - GodotRuntimeInspector.MinRowHeight;

                    System.Numerics.Vector2 topTableSize = new System.Numerics.Vector2(columnWidth - GodotRuntimeInspector.MinRowHeight, topHeight);
                    MyPropertyTable.DrawTable(ref MyProperties, "top", MyPropertyFlags.ContainerTableFlags(), topTableSize);

                    System.Numerics.Vector2 bottomTableSize = new System.Numerics.Vector2(columnWidth - GodotRuntimeInspector.MinRowHeight, bottomHeight);
                    if (ImGuiNET.ImGui.BeginChild("bottom", bottomTableSize, false, MyPropertyFlags.ContainerWindowFlags()))
                    {
                        string tabBarID = "bottomTabBar";
                        ImGuiNET.ImGuiTabBarFlags flags = new ImGuiNET.ImGuiTabBarFlags();
                        flags |= ImGuiNET.ImGuiTabBarFlags.Reorderable;
                        flags |= ImGuiNET.ImGuiTabBarFlags.AutoSelectNewTabs;
                        flags |= ImGuiNET.ImGuiTabBarFlags.TabListPopupButton;
                        string[] keys = GodotRuntimeInspector.MyProperties.Keys.ToArray();
                        if (ImGuiNET.ImGui.BeginTabBar(tabBarID, flags))
                        {
                            for (int i = 0; i < keys.Length; i++)
                            {
                                string key = keys[i];
                                Myimgui.MyProperty myProperty = GodotRuntimeInspector.MyProperties[key];
                                bool tabOpen = true;
                                ImGuiNET.ImGuiTabItemFlags tabItemFlags = ImGuiNET.ImGuiTabItemFlags.None;
                                if (myProperty.Clicks > 0)
                                {
                                    tabItemFlags |= ImGuiNET.ImGuiTabItemFlags.SetSelected;
                                    myProperty.Clicks = 0;
                                }
                                if (ImGuiNET.ImGui.BeginTabItem(key, ref tabOpen, tabItemFlags))
                                {
                                    myProperty.MyPropertyImgui.Update(myProperty, bottomTableSize);
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
                ImGuiNET.ImGui.EndTable();
            }
            ImGuiNET.ImGui.End();
        }

        private static void Traverse(Godot.Node node)
        {
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
    }
}
