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
                        DrawMyType(mytype, myProperty);
                    }

                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        ImGuiNET.ImGui.Text(nameof(mytype) + " " + mytype);
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
                    DrawDefault(myProperty);
                    break;

                case MyTypes.Boolean:
                    DrawMyBoolean(myProperty);
                    break;

                case MyTypes.Number:
                    DrawMyNumber(myProperty);
                    break;

                case MyTypes.String:
                    DrawMyString(myProperty);
                    break;
            }
        }

        private void DrawDefault(MyProperty myProperty)
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

        private void DrawMyNumber(MyProperty myProperty)
        {
            // int
            bool drawInt = myProperty.Instance is sbyte;
            drawInt = drawInt || myProperty.Instance is byte;
            drawInt = drawInt || myProperty.Instance is short;
            drawInt = drawInt || myProperty.Instance is ushort;
            drawInt = drawInt || myProperty.Instance is int;

            if(drawInt == true)
            {
                string text = Utility.GetStr(myProperty.Instance);
                string controlId = text + "###" + myProperty.Name;
                string mynumber = string.Empty;

                int value = (int)myProperty.Instance;
                mynumber = value.ToString();

                if (ImGuiNET.ImGui.InputInt(controlId, ref value))
                {
                    SetSelectedNodeValue(myProperty, value);
                }
            }

            // int64
            //bool drawInt64 = myProperty.Instance is uint;
            //drawInt64 = drawInt64 || myProperty.Instance is long;
            //drawInt64 = drawInt64 || myProperty.Instance is ulong;
            //if (drawInt == true)
            //{
            //    //ulong value = (ulong)myProperty.Instance;

            //    nint myscaler = (nint)myProperty.Instance;

            //    if (ImGuiNET.ImGui.InputScalar(text, ImGuiNET.ImGuiDataType.S64, myscaler))
            //    {
            //        SetSelectedNodeValue(myProperty, myscaler);
            //    }
            //}

            // float
            //bool drawFloat = myProperty.Instance is float;
            //drawFloat = drawFloat || myProperty.Instance is double;
            //drawFloat = drawFloat || myProperty.Instance is decimal;

            //if (drawInt == true)
            //{
            //    float value = (float)myProperty.Instance;
            //    mynumber = value.ToString();

            //    if (ImGuiNET.ImGui.InputFloat(text, ref value))
            //    {
            //        SetSelectedNodeValue(myProperty, value);
            //    }
            //}
        }

        private void DrawMyString(MyProperty myProperty)
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
                field.SetValue(_selectedNode, value);
            }

            System.Reflection.PropertyInfo prop = systemType.GetProperty(myProperty.Name, bindingFlags);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(_selectedNode, value, null);
            }
        }
    }
}
