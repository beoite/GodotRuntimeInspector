using ImGuiNET;
using System;

namespace RuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyImgui
    {
        private Guid id = Guid.NewGuid();

        public void Update(MyProperty myProperty)
        {
            if (myProperty.Clicks > 0)
            {
                ImGui.SetNextWindowFocus();
                myProperty.Clicks = 0;
            }

            if (!ImGui.Begin(myProperty.Name, MyPropertyFlags.WindowFlags()))
            {
                ImGui.End();
                return;
            }
            System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
            System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - GodotRuntimeInspector.MinRowHeight);

            string name = nameof(MyPropertyImgui) + id;
            bool border = true;
            if (ImGui.BeginChild(name, tableSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                System.Reflection.FieldInfo[] fields = typeof(Myimgui.MyProperty).GetFields();
                int numCols = 1;
                if (ImGui.BeginTable(id.ToString(), numCols, MyPropertyFlags.TableFlags(), tableSize))
                {
                    ImGui.TableSetupColumn(myProperty.Name, MyPropertyFlags.ContainerTableColumnFlags(), tableSize.X);
                    //ImGui.TableHeadersRow();
                    //ImGuiTableSortSpecsPtr sortsSpecs = ImGui.TableGetSortSpecs();
                    //Sort(sortsSpecs, myProperties);

                    ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGui.TableNextColumn();
                    ImGui.Text(myProperty.Name);

                    ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGui.TableNextColumn();
                    ImGui.Text(myProperty.Type.ToString());

                    ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                    ImGui.TableNextColumn();
                    float columnWidth = ImGui.GetColumnWidth();
                    System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, GodotRuntimeInspector.MinRowHeight);
                    bool clicked = ImGui.Button(Utility.GetStr(myProperty.Instance) + "###" + myProperty.Name, size);
                    if (clicked)
                    {
                        if (GodotRuntimeInspector.MyProperties.ContainsKey(myProperty.Name) == false)
                        {
                            GodotRuntimeInspector.MyProperties.Add(myProperty.Name, myProperty);
                        }
                    }

                    ImGui.EndTable();
                }
                ImGui.EndChild();
            }
            ImGui.End();
        }
    }
}
