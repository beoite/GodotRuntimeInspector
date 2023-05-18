using ImGuiNET;
using System;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyTable
    {
        public static string? SelectedValue = string.Empty;

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
                if (sortsSpecs.Specs.ColumnIndex == 2)
                {
                    if (sortsSpecs.Specs.SortDirection == ImGuiSortDirection.Ascending)
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().CompareValueAscending);
                    }
                    else
                    {
                        Array.Sort(myPropertyInfo, new MyPropertyComparer().CompareValueDescending);
                    }
                }
                sortsSpecs.SpecsDirty = false;
            }
        }

        public static void DrawTable(MyProperty[] myProperties, string id, ImGuiTableFlags flags, System.Numerics.Vector2 tableSize)
        {
            string name = nameof(DrawTable) + id;
            bool border = true;
            if (ImGui.BeginChild(name, tableSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                int numCols = 3;
                if (ImGui.BeginTable(id, numCols, flags, tableSize))
                {
                    float width20 = 0.2f * tableSize.X;
                    float width80 = 0.8f * tableSize.X;
                    float colWidth = width80 / 2f;
                    ImGui.TableSetupColumn(nameof(MyProperty.Index), MyPropertyFlags.ContainerTableColumnFlags(), width20);
                    ImGui.TableSetupColumn(nameof(MyProperty.Name), MyPropertyFlags.ContainerTableColumnFlags(), colWidth);
                    ImGui.TableSetupColumn(nameof(MyProperty.Value), MyPropertyFlags.ContainerTableColumnFlags(), colWidth);
                    ImGui.TableHeadersRow();
                    //ImGuiTableSortSpecsPtr sortsSpecs = ImGui.TableGetSortSpecs();
                    //Sort(sortsSpecs, myProperties);
                    for (int i = 0; i < myProperties.Length; i++)
                    {
                        ImGui.TableNextRow(MyPropertyFlags.NoneTableRowFlags(), GodotRuntimeInspector.MinRowHeight);
                        ImGui.TableNextColumn();
                        ImGui.Text(myProperties[i].Index.ToString());

                        ImGui.TableNextColumn();
                        ImGui.Text(myProperties[i].Name);

                        ImGui.TableNextColumn();
                        string uniqueButtonName = myProperties[i].Value + "###" + myProperties[i].Index + myProperties[i].Name;
                        bool clicked = ImGui.Button(uniqueButtonName);
                        if (clicked)
                        {
                            SelectedValue = myProperties[i].Name + " " + myProperties[i].Value;
                            if (IsJsonValid(SelectedValue))
                            {
                                SelectedValue = PrettyJson(SelectedValue);
                            }
                            Godot.DisplayServer.ClipboardSet(SelectedValue);
                        }
                    }
                    ImGui.EndTable();
                }
                ImGui.EndChild();
            }
        }

        private static string PrettyJson(string unPrettyJson)
        {
            var options = new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(unPrettyJson);

            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }

        private static bool IsJsonValid(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return false;
            }
            try
            {
                using System.Text.Json.JsonDocument jsonDoc = System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch (System.Text.Json.JsonException)
            {
                return false;
            }
        }
    }
}
