using ImGuiNET;
using System;

namespace RuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyTable
    {
        public MyProperty SelectedProperty = new MyProperty();

        private static void Sort(ImGuiTableSortSpecsPtr sortsSpecs, MyProperty[] myPropertyInfo)
        {
            if (myPropertyInfo.Length == 0 || myPropertyInfo.Length < 2)
            {
                return;
            }
            if (sortsSpecs.SpecsDirty == true)
            {
                if (sortsSpecs.Specs.ColumnIndex == 0)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().CompareIndexAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().CompareIndexDescending);
                    }
                }
                if (sortsSpecs.Specs.ColumnIndex == 1)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().CompareNameAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().CompareNameDescending);
                    }
                }
                sortsSpecs.SpecsDirty = false;
            }
        }

        public void DrawTable(MyProperty[] myProperties, string id, ImGuiTableFlags flags, System.Numerics.Vector2 tableSize)
        {
            string name = nameof(DrawTable) + id;
            bool border = true;
            if (ImGui.BeginChild(name, tableSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                System.Reflection.FieldInfo[] fields = typeof(Myimgui.MyProperty).GetFields();
                int numCols = fields.Length;
                if (ImGui.BeginTable(id, numCols, flags, tableSize))
                {
                    float widthIndex = 0.1f * tableSize.X;
                    float widthAccessor = 0.1f * tableSize.X;
                    float widthName = 0.2f * tableSize.X;
                    float widthType = 0.2f * tableSize.X;
                    float widthInstance = 0.4f * tableSize.X;

                    ImGui.TableSetupColumn(nameof(MyProperty.Index), MyPropertyFlags.ContainerTableColumnFlags(), widthIndex);
                    ImGui.TableSetupColumn(nameof(MyProperty.Tag), MyPropertyFlags.ContainerTableColumnFlags(), widthAccessor);
                    ImGui.TableSetupColumn(nameof(MyProperty.Name), MyPropertyFlags.ContainerTableColumnFlags(), widthName);
                    ImGui.TableSetupColumn(nameof(MyProperty.Type), MyPropertyFlags.ContainerTableColumnFlags(), widthType);
                    ImGui.TableSetupColumn(nameof(MyProperty.Instance), MyPropertyFlags.ContainerTableColumnFlags(), widthInstance);
                    //ImGui.TableHeadersRow();
                    //ImGuiTableSortSpecsPtr sortsSpecs = ImGui.TableGetSortSpecs();
                    //Sort(sortsSpecs, myProperties);
                    for (int i = 0; i < myProperties.Length; i++)
                    {
                        ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperties[i].Index.ToString());

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperties[i].Tag.ToString());

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperties[i].Name);

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperties[i].Type.ToString());

                        ImGui.TableNextColumn();
                        float columnWidth = ImGui.GetColumnWidth();
                        System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, GodotRuntimeInspector.MinRowHeight);
                        bool clicked = ImGui.Button(Utility.GetStr(myProperties[i].Instance), size);
                        if (clicked)
                        {
                            SelectedProperty = myProperties[i];
                            if (GodotRuntimeInspector.MyPropertyInspectors.ContainsKey(SelectedProperty) == false)
                            {
                                Myimgui.MyPropertyInspector myPropertyInspector = new Myimgui.MyPropertyInspector();
                                GodotRuntimeInspector.MyPropertyInspectors.Add(SelectedProperty, myPropertyInspector);
                            }
                            GodotRuntimeInspector.MyPropertyInspectors[SelectedProperty].IsVisible = true;
                        }
                    }
                    ImGui.EndTable();
                }
                ImGui.EndChild();
            }
        }
    }
}
