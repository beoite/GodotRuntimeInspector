namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyTable
    {
        private Godot.Node _selectedNode = new Godot.Node() { Name = nameof(_selectedNode) };

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

                sortsSpecs.SpecsDirty = false;
            }
        }

        public void Update(Godot.Node selectedNode, MyProperty[] myProperties, string id, System.Numerics.Vector2 tableSize)
        {
            _selectedNode = selectedNode;

            string name = nameof(Update) + id;
            System.Reflection.FieldInfo[] fields = typeof(Myimgui.MyProperty).GetFields();
            int numCols = fields.Length;

            if (ImGuiNET.ImGui.BeginTable(id, numCols, MyPropertyFlags.TableFlags(), tableSize))
            {
                float width = tableSize.X / numCols;
                float smallWidth = width / 3f;
                float extraSmallWidth = width / 8f;

                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Index), MyPropertyFlags.TableColumnFlags(), extraSmallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Tags), MyPropertyFlags.TableColumnFlags(), smallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Type), MyPropertyFlags.TableColumnFlags(), smallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Name), MyPropertyFlags.TableColumnFlags(), width);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Instance), MyPropertyFlags.TableColumnFlags(), width);
                ImGuiNET.ImGui.TableSetupColumn("Debug", MyPropertyFlags.TableColumnFlags(), width);

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

                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        MyTypes mytype = Utility.GetMyType(myProperty.Instance);
                        DrawMyType(mytype, myProperty);
                    }

                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        MyTypes mytype = Utility.GetMyType(myProperty.Instance);
                        ImGuiNET.ImGui.Text(mytype.ToString());
                    }
                }

                ImGuiNET.ImGui.EndTable();
            }
        }

        private void DrawMyType(MyTypes mytype, MyProperty myProperty)
        {
            switch (mytype)
            {
                case MyTypes.None:
                    DrawText(myProperty);
                    break;

                case MyTypes.Boolean:
                    DrawMyBoolean(myProperty);
                    break;

                case MyTypes.Number:
                    DrawNumber(myProperty);
                    break;

                case MyTypes.String:
                    DrawString(myProperty);
                    break;

                case MyTypes.Complex:
                    DrawText(myProperty);
                    break;
            }
        }

        private void DrawText(MyProperty myProperty)
        {
            string text = Utility.GetStr(myProperty.Instance);
            ImGuiNET.ImGui.Text(text);
        }

        private void DrawMyBoolean(MyProperty myProperty)
        {
            string text = Utility.GetStr(myProperty.Instance);
            string controlId = text + "###" + myProperty.Name;
            bool mybool = (bool)myProperty.Instance;

            if (ImGuiNET.ImGui.Checkbox(controlId, ref mybool))
            {
                SetSelectedNodeValue(myProperty, mybool);
            }
        }

        private void DrawNumber(MyProperty myProperty)
        {
            // InputInt
            bool drawInt = myProperty.Instance is sbyte;
            drawInt = drawInt || myProperty.Instance is byte;
            drawInt = drawInt || myProperty.Instance is short;
            drawInt = drawInt || myProperty.Instance is ushort;
            drawInt = drawInt || myProperty.Instance is int;

            if (drawInt == true)
            {
                string text = Utility.GetStr(myProperty.Instance);
                string controlId = text + "###" + myProperty.Name;
                int myint = System.Convert.ToInt32(myProperty.Instance);

                if (ImGuiNET.ImGui.InputInt(controlId, ref myint))
                {
                    SetSelectedNodeValue(myProperty, myint);
                }
            }

            // InputDouble
            bool drawDouble = myProperty.Instance is uint;
            drawDouble = drawDouble || myProperty.Instance is long;
            drawDouble = drawDouble || myProperty.Instance is ulong;
            drawDouble = drawDouble || myProperty.Instance is float;
            drawDouble = drawDouble || myProperty.Instance is double;
            drawDouble = drawDouble || myProperty.Instance is decimal;

            if (drawDouble == true)
            {
                string text = Utility.GetStr(myProperty.Instance);
                string controlId = text + "###" + myProperty.Name;
                double mydouble = System.Convert.ToDouble(myProperty.Instance);

                if (ImGuiNET.ImGui.InputDouble(controlId, ref mydouble))
                {
                    SetSelectedNodeValue(myProperty, mydouble);
                }
            }
        }

        private void DrawString(MyProperty myProperty)
        {
            string text = Utility.GetStr(myProperty.Instance);
            string controlId = text + "###" + myProperty.Name;
            string mystring = myProperty.Instance.ToString();

            if (ImGuiNET.ImGui.InputText(text, ref mystring, Config.InputTextMaxLength, MyPropertyFlags.InputTextFlags()))
            {
                SetSelectedNodeValue(myProperty, mystring);
            }
        }

        public void SetSelectedNodeValue(MyProperty myProperty, object value)
        {
            System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public;

            System.Type systemType = _selectedNode.GetType();

            System.Reflection.FieldInfo field = systemType.GetField(myProperty.Name, bindingFlags);
            if (field != null)
            {
                if (myProperty.Instance is sbyte)
                {
                    sbyte result;
                    if (sbyte.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is byte)
                {
                    byte result;
                    if (byte.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is short)
                {
                    short result;
                    if (short.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is ushort)
                {
                    ushort result;
                    if (ushort.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is int)
                {
                    int result;
                    if (int.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is uint)
                {
                    uint result;
                    if (uint.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is long)
                {
                    long result;
                    if (long.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else if (myProperty.Instance is ulong)
                {
                    ulong result;
                    if (ulong.TryParse(value.ToString(), out result))
                    {
                        field.SetValue(_selectedNode, result);
                    }
                }
                else
                {
                    field.SetValue(_selectedNode, value);
                }
            }

            System.Reflection.PropertyInfo prop = systemType.GetProperty(myProperty.Name, bindingFlags);
            if (null != prop && prop.CanWrite)
            {
                if (myProperty.Instance is sbyte)
                {
                    sbyte result;
                    if (sbyte.TryParse(value.ToString(), out result))
                    {
                        prop.SetValue(_selectedNode, result, null);
                    }
                }
                else if (myProperty.Instance is byte)
                {
                    byte result;
                    if (byte.TryParse(value.ToString(), out result))
                    {
                        prop.SetValue(_selectedNode, result, null);
                    }
                }
                else if (myProperty.Instance is short)
                {
                    short result;
                    if (short.TryParse(value.ToString(), out result))
                    {
                        prop.SetValue(_selectedNode, result, null);
                    }
                }
                else if (myProperty.Instance is ushort)
                {
                    ushort result;
                    if (ushort.TryParse(value.ToString(), out result))
                    {
                        prop.SetValue(_selectedNode, result, null);
                    }
                }
                else if (myProperty.Instance is int)
                {
                    int result;
                    if (int.TryParse(value.ToString(), out result))
                    {
                        prop.SetValue(_selectedNode, result, null);
                    }
                }
                else
                {
                    prop.SetValue(_selectedNode, value, null);
                }
            }
        }
    }
}
