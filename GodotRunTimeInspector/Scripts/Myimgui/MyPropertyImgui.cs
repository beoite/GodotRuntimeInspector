namespace RuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyImgui
    {
        private System.Guid id = System.Guid.NewGuid();

        public void Update(MyProperty myProperty, System.Numerics.Vector2 bottomTableSize)
        {
            string name = nameof(MyPropertyImgui) + id;
            bool border = true;
            if (ImGuiNET.ImGui.BeginChild(name, bottomTableSize, border, MyPropertyFlags.ContainerWindowFlagsAlt()))
            {
                System.Reflection.FieldInfo[] fields = typeof(Myimgui.MyProperty).GetFields();
                int numCols = 1;
                if (ImGuiNET.ImGui.BeginTable(id.ToString(), numCols, MyPropertyFlags.TableFlags(), bottomTableSize))
                {
                    ImGuiNET.ImGui.TableSetupColumn(id.ToString(), MyPropertyFlags.ContainerTableColumnFlags(), bottomTableSize.X);

                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGuiNET.ImGui.TableNextColumn();
                    ImGuiNET.ImGui.Text(myProperty.Tags.ToString());

                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGuiNET.ImGui.TableNextColumn();
                    ImGuiNET.ImGui.Text(myProperty.Type.ToString());

                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGuiNET.ImGui.TableNextColumn();
                    ImGuiNET.ImGui.Text(myProperty.Name);

                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGuiNET.ImGui.TableNextColumn();
                    float columnWidth = ImGuiNET.ImGui.GetColumnWidth();
                    System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, GodotRuntimeInspector.MinRowHeight);
                    bool clicked = ImGuiNET.ImGui.Button(Utility.GetStr(myProperty.Instance) + "###" + id, size);
                    if (clicked)
                    {
                        if (GodotRuntimeInspector.MyProperties.ContainsKey(myProperty.Name) == false)
                        {
                            GodotRuntimeInspector.MyProperties.Add(myProperty.Name, myProperty);
                        }
                    }

                    ImGuiNET.ImGui.EndTable();
                }
                ImGuiNET.ImGui.EndChild();
            }
        }
    }
}
