namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyTable
    {
        private unsafe void Sort(ImGuiNET.ImGuiTableSortSpecsPtr sortsSpecs, MyProperty[] myPropertyInfo)
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
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().IndexAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().IndexDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 1)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().TagAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().TagDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 2)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().TypeAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().TypeDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 3)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().NameAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().NameDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 4)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().InstanceAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().InstanceDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 5)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().ClicksAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, new MyPropertyComparer().ClicksDescending);
                    }
                }
                sortsSpecs.SpecsDirty = false;
            }
        }

        public void DrawTable(ref MyProperty[] myProperties, string id, ImGuiNET.ImGuiTableFlags flags, System.Numerics.Vector2 tableSize)
        {
            string name = nameof(DrawTable) + id;
            bool border = true;
            if (ImGuiNET.ImGui.BeginChild(name, tableSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                System.Reflection.FieldInfo[] fields = typeof(Myimgui.MyProperty).GetFields();
                int numCols = fields.Length;
                if (ImGuiNET.ImGui.BeginTable(id, numCols, flags, tableSize))
                {
                    float width = tableSize.X / numCols;
                    float smallWidth = width / 3f;
                    float extraSmallWidth = width / 8f;

                    ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Index), MyPropertyFlags.TableColumnFlags(), extraSmallWidth);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Tags), MyPropertyFlags.TableColumnFlags(), smallWidth);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Type), MyPropertyFlags.TableColumnFlags(), smallWidth);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Name), MyPropertyFlags.TableColumnFlags(), width);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Instance), MyPropertyFlags.TableColumnFlags(), width);
                    ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Clicks), MyPropertyFlags.TableColumnFlags(), extraSmallWidth);

                    ImGuiNET.ImGui.TableHeadersRow();
                    ImGuiNET.ImGuiTableSortSpecsPtr sortsSpecs = ImGuiNET.ImGui.TableGetSortSpecs();
                    Sort(sortsSpecs, myProperties);
                    for (int i = 0; i < myProperties.Length; i++)
                    {
                        MyProperty myProperty = myProperties[i];

                        ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            ImGuiNET.ImGui.Text(myProperty.Index.ToString());
                        }

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            ImGuiNET.ImGui.Text(myProperty.Tags.ToString());
                        }

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            ImGuiNET.ImGui.Text(myProperty.Type.ToString());
                        }

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            string[] split = myProperty.Name.Split("/");
                            ImGuiNET.ImGui.Text(split[split.Length - 1]);
                        }

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            float columnWidth = ImGuiNET.ImGui.GetColumnWidth();
                            System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, GodotRuntimeInspector.MinRowHeight);
                            bool clicked = ImGuiNET.ImGui.Button(Utility.GetStr(myProperty.Instance) + "###" + myProperty.Name, size);
                            if (clicked)
                            {
                                myProperty.Clicks++;
                                myProperties[i] = myProperty;
                            }
                        }

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            ImGuiNET.ImGui.Text(myProperty.Clicks.ToString());
                        }
                    }
                    ImGuiNET.ImGui.EndTable();
                }
                ImGuiNET.ImGui.EndChild();
            }
        }
    }
}
