using ImGuiNET;
using System;

namespace RuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyTable
    {
        private unsafe void Sort(ImGuiTableSortSpecsPtr sortsSpecs, MyProperty[] myPropertyInfo)
        {
            if (myPropertyInfo.Length == 0 || myPropertyInfo.Length < 2)
            {
                return;
            }
            if (sortsSpecs.NativePtr == null)
            {
                return;
            }
            if (sortsSpecs.SpecsCount == 0)
            {
                return;
            }
            if (sortsSpecs.SpecsDirty == true)
            {
                if (sortsSpecs.Specs.ColumnIndex == 0)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().IndexAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().IndexDescending);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 1)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().TagAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().TagDescending);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 2)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().NameAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().NameDescending);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 3)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().TypeAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().TypeDescending);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 4)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().Compare);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().Compare);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 5)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().Compare);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().Compare);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 6)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().ClicksAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().ClicksDescending);
                    }
                }
                sortsSpecs.SpecsDirty = false;
            }
        }

        public void DrawTable(ref MyProperty[] myProperties, string id, ImGuiTableFlags flags, System.Numerics.Vector2 tableSize)
        {
            string name = nameof(DrawTable) + id;
            bool border = true;
            if (ImGui.BeginChild(name, tableSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                System.Reflection.FieldInfo[] fields = typeof(Myimgui.MyProperty).GetFields();
                int numCols = fields.Length;
                if (ImGui.BeginTable(id, numCols, flags, tableSize))
                {
                    float width = tableSize.X / numCols;

                    ImGui.TableSetupColumn(nameof(MyProperty.Index), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableSetupColumn(nameof(MyProperty.Tag), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableSetupColumn(nameof(MyProperty.Name), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableSetupColumn(nameof(MyProperty.Type), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableSetupColumn(nameof(MyProperty.Instance), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableSetupColumn(nameof(MyProperty.MyPropertyImgui), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableSetupColumn(nameof(MyProperty.Clicks), MyPropertyFlags.ContainerTableColumnFlags(), width);
                    ImGui.TableHeadersRow();
                    ImGuiTableSortSpecsPtr sortsSpecs = ImGui.TableGetSortSpecs();
                    Sort(sortsSpecs, myProperties);
                    for (int i = 0; i < myProperties.Length; i++)
                    {
                        MyProperty myProperty = myProperties[i];

                        ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperty.Index.ToString());

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperty.Tag.ToString());

                        ImGui.TableNextColumn();
                        string[] split = myProperty.Name.Split("/");
                        ImGui.Text(split[split.Length - 1]);

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperty.Type.ToString());

                        ImGui.TableNextColumn();
                        float columnWidth = ImGui.GetColumnWidth();
                        System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, GodotRuntimeInspector.MinRowHeight);
                        bool clicked = ImGui.Button(Utility.GetStr(myProperty.Instance) + "###" + myProperty.Name, size);
                        if (clicked)
                        {
                            myProperty.Clicks++;
                            if (GodotRuntimeInspector.MyProperties.ContainsKey(myProperty.Name) == false)
                            {
                                GodotRuntimeInspector.MyProperties.Add(myProperty.Name, myProperty);
                            }
                        }

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperty.MyPropertyImgui.ToString());

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperty.Clicks.ToString());
                    }
                    ImGui.EndTable();
                }
                ImGui.EndChild();
            }
        }
    }
}
