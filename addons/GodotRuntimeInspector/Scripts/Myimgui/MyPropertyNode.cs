﻿using System.Linq;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyNode
    {
        public Godot.Node SelectedNode = new Godot.Node() { Name = nameof(SelectedNode) };

        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public Godot.SceneTree? SceneTree = null;

        public int Counter = -1;

        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        private System.Numerics.Vector2 windowSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topLeftSize = System.Numerics.Vector2.Zero;

        private System.Numerics.Vector2 topRightSize = System.Numerics.Vector2.Zero;

        private Godot.WeakRef WeakRef = new Godot.WeakRef();

        private readonly Godot.Node NothingSelected = new Godot.Node() { Name = nameof(NothingSelected) };

        private void Traverse(Godot.Node? node)
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

        private void TopRow()
        {
            if (ImGuiNET.ImGui.BeginTable(nameof(topSize), 2, MyPropertyFlags.TableFlags(), topSize))
            {
                ImGuiNET.ImGui.TableSetupColumn(nameof(topLeftSize), MyPropertyFlags.TableColumnFlags(), topLeftSize.X);
                ImGuiNET.ImGui.TableSetupColumn(nameof(topRightSize), MyPropertyFlags.TableColumnFlags(), topRightSize.X);
                ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), topSize.Y);

                // left side, scene tree view
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    Counter = -1;
                    Traverse(SceneTree?.CurrentScene);
                }

                // right side, field/property table
                if (ImGuiNET.ImGui.TableNextColumn())
                {
                    MyPropertyTable.DrawTable(MyProperties, nameof(topRightSize), MyPropertyFlags.TableFlags(), topRightSize);
                }

                ImGuiNET.ImGui.EndTable();
            }
        }

        public void Update(Godot.Node node)
        {
            SceneTree = node.GetTree().Root.GetTree();
            if (!ImGuiNET.ImGui.Begin(Utility.GetAnimatedTitle(SceneTree.CurrentScene.SceneFilePath), MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }

            // build MyProperties
            if (WeakRef.GetRef().Obj == null)
            {
                SelectedNode = NothingSelected;
            }
            if (SelectedNode.Name == nameof(SelectedNode))
            {
                SelectedNode = SceneTree.CurrentScene;
            }
            MyProperties = MyProperty.NewArray(SelectedNode);

            // menu
            Myimgui.MenuBar.Update();

            // sizes
            windowSize = ImGuiNET.ImGui.GetWindowSize();
            windowSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - (Config.MinRowHeight));
            topSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - Config.MinRowHeight);
            topLeftSize = new System.Numerics.Vector2(windowSize.X * 0.3f, topSize.Y);
            topRightSize = new System.Numerics.Vector2(windowSize.X * 0.7f, topSize.Y);

            // window
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

        private void SetSelectedNodeValue(MyProperty myProperty, object value)
        {
            System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public;

            System.Type systemType = SelectedNode.GetType();

            System.Reflection.FieldInfo field = systemType.GetField(myProperty.Name, bindingFlags);
            if (field != null)
            {
                field.SetValue(SelectedNode, value);
            }

            System.Reflection.PropertyInfo prop = systemType.GetProperty(myProperty.Name, bindingFlags);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(SelectedNode, value, null);
            }
        }
    }
}

