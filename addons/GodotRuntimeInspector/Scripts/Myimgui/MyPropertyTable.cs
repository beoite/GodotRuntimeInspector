using static System.Net.Mime.MediaTypeNames;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyTable
    {
        private static unsafe void Sort(ImGuiNET.ImGuiTableSortSpecsPtr sortsSpecs, MyProperty[] myPropertyInfo)
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
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.IndexAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.IndexDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 1)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.TagAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.TagDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 2)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.TypeAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.TypeDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 3)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.NameAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.NameDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 4)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.InstanceAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.InstanceDescending);
                    }
                }

                if (sortsSpecs.Specs.ColumnIndex == 5)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiNET.ImGuiSortDirection.Ascending)
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.ClicksAscending);
                    }
                    else
                    {
                        System.Array.Sort(myPropertyInfo, MyPropertyComparer.ClicksDescending);
                    }
                }
                sortsSpecs.SpecsDirty = false;
            }
        }

        public void DrawTable(MyProperty[] myProperties, string id, ImGuiNET.ImGuiTableFlags flags, System.Numerics.Vector2 tableSize)
        {
            string name = nameof(DrawTable) + id;

            if (ImGuiNET.ImGui.BeginChild(name, tableSize)) //tableSize, border: border, MyPropertyFlags.TreeNodeWindowFlags()))
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
                    ImGuiNET.ImGui.TableSetupColumn("Debug", MyPropertyFlags.TableColumnFlags(), extraSmallWidth);

                    ImGuiNET.ImGui.TableHeadersRow();
                    ImGuiNET.ImGuiTableSortSpecsPtr sortsSpecs = ImGuiNET.ImGui.TableGetSortSpecs();
                    Sort(sortsSpecs, myProperties);

                    for (int i = 0; i < myProperties.Length; i++)
                    {
                        MyProperty myProperty = myProperties[i];

                        if (myProperty.Instance == null)
                        {
                            continue;
                        }

                        ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), Config.MinRowHeight);

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

                        MyTypes mytype = Utility.GetMyType(myProperty.Instance);

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            float columnWidth = ImGuiNET.ImGui.GetColumnWidth();
                            System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, Config.MinRowHeight);

                            string text = Utility.GetStr(myProperty.Instance);
                            string controlId = text + "###" + myProperty.Name;
                            ImGuiNET.ImGuiInputTextFlags imGuiInputTextFlags = ImGuiNET.ImGuiInputTextFlags.None;

                            switch (mytype)
                            {
                                case MyTypes.None:
                                    ImGuiNET.ImGui.Text(text);
                                    break;
                                case MyTypes.Boolean:
                                    bool instance = (bool)myProperty.Instance;
                                    if (ImGuiNET.ImGui.Checkbox(text, ref instance))
                                    {
                                        myProperty.Instance = instance;
                                    }
                                    break;
                                case MyTypes.Number:
                                    if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, uint.MaxValue, size, imGuiInputTextFlags))
                                    {

                                    }
                                    break;
                                case MyTypes.String:
                                    if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, uint.MaxValue, size, imGuiInputTextFlags))
                                    {

                                    }
                                    break;
                            }

                        }

                        if (ImGuiNET.ImGui.TableNextColumn())
                        {
                            ImGuiNET.ImGui.Text(nameof(mytype) + " " + mytype);
                        }
                    }
                    ImGuiNET.ImGui.EndTable();
                }
                ImGuiNET.ImGui.EndChild();
            }
        }
    }
}
