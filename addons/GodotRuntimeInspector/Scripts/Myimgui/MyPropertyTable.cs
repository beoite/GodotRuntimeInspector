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
                float width = tableSize.X / 2;
                float smallWidth = width / 3f;
                float extraSmallWidth = width / 8f;

                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Index), MyPropertyFlags.TableColumnFlags(), extraSmallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Tags), MyPropertyFlags.TableColumnFlags(), extraSmallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Type), MyPropertyFlags.TableColumnFlags(), smallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Name), MyPropertyFlags.TableColumnFlags(), smallWidth);
                ImGuiNET.ImGui.TableSetupColumn(nameof(MyProperty.Instance), MyPropertyFlags.TableColumnFlags(), width);

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

                    ImGuiNET.ImGui.TableNextRow(MyPropertyFlags.TableRowFlags(), Config.MinRowHeight);

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
                        MyTypes mytype = Utility.GetMyType(myProperty.Instance);
                        ImGuiNET.ImGui.Text("(" + mytype.ToString() + ") " + myProperty.Type.ToString());
                    }

                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        ImGuiNET.ImGui.Text(myProperty.Name);
                    }

                    if (ImGuiNET.ImGui.TableNextColumn())
                    {
                        DrawMyType(myProperty);
                    }
                }

                ImGuiNET.ImGui.EndTable();
            }
        }

        private void DrawMyType(MyProperty myProperty)
        {
            MyTypes mytype = Utility.GetMyType(myProperty.Instance);

            switch (mytype)
            {
                case MyTypes.Complex:
                    DrawComplex(myProperty);
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

                case MyTypes.Vector2:
                    DrawVector2(myProperty);
                    break;

                case MyTypes.Vector3:
                    DrawVector3(myProperty);
                    break;
            }
        }

        private void DrawComplex(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);
            string mystring = myProperty.Instance.ToString();

            if (ImGuiNET.ImGui.Button(controlId, new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), Config.MinRowHeight)))
            {
                MyWindowManager.Add(myProperty);
            }
        }

        private void DrawMyBoolean(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);
            bool mybool = (bool)myProperty.Instance;

            if (ImGuiNET.ImGui.Checkbox(controlId, ref mybool))
            {
                SetSelectedNodeValue(myProperty, mybool);
            }
        }

        private void DrawNumber(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);

            // InputInt
            bool drawInt = myProperty.Instance is sbyte;
            drawInt = drawInt || myProperty.Instance is byte;
            drawInt = drawInt || myProperty.Instance is short;
            drawInt = drawInt || myProperty.Instance is ushort;
            drawInt = drawInt || myProperty.Instance is int;

            if (drawInt == true)
            {
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
                double mydouble = System.Convert.ToDouble(myProperty.Instance);

                if (ImGuiNET.ImGui.InputDouble(controlId, ref mydouble))
                {
                    SetSelectedNodeValue(myProperty, mydouble);
                }
            }
        }

        private void DrawString(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);
            string mystring = myProperty.Instance.ToString();

            if (ImGuiNET.ImGui.InputText(controlId, ref mystring, Config.InputTextMaxLength, MyPropertyFlags.InputTextFlags()))
            {
                SetSelectedNodeValue(myProperty, mystring);
            }
        }

        private void DrawVector2(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);
            System.Numerics.Vector2 systemvector2 = System.Numerics.Vector2.Zero;

            if (myProperty.Instance is System.Numerics.Vector2)
            {
                systemvector2 = (System.Numerics.Vector2)myProperty.Instance;
            }
            else if (myProperty.Instance is Godot.Vector2)
            {
                Godot.Vector2 godotvector2 = (Godot.Vector2)myProperty.Instance;
                systemvector2 = new System.Numerics.Vector2(godotvector2.X, godotvector2.Y);
            }

            if (ImGuiNET.ImGui.DragFloat2(controlId, ref systemvector2))
            {
                if (myProperty.Instance is System.Numerics.Vector2)
                {
                    SetSelectedNodeValue(myProperty, systemvector2);
                }
                else if (myProperty.Instance is Godot.Vector2)
                {
                    Godot.Vector2 godotvector2 = new Godot.Vector2(systemvector2.X, systemvector2.Y);
                    SetSelectedNodeValue(myProperty, godotvector2);
                }
            }
        }

        private void DrawVector3(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);
            System.Numerics.Vector3 systemvector3 = System.Numerics.Vector3.Zero;

            if (myProperty.Instance is System.Numerics.Vector3)
            {
                systemvector3 = (System.Numerics.Vector3)myProperty.Instance;
            }
            else if (myProperty.Instance is Godot.Vector3)
            {
                Godot.Vector3 godotvector3 = (Godot.Vector3)myProperty.Instance;
                systemvector3 = new System.Numerics.Vector3(godotvector3.X, godotvector3.Y, godotvector3.Z);
            }

            if (ImGuiNET.ImGui.DragFloat3(controlId, ref systemvector3))
            {
                if (myProperty.Instance is System.Numerics.Vector3)
                {
                    SetSelectedNodeValue(myProperty, systemvector3);
                }
                else if (myProperty.Instance is Godot.Vector3)
                {
                    Godot.Vector3 godotvector3 = new Godot.Vector3(systemvector3.X, systemvector3.Y, systemvector3.Z);
                    SetSelectedNodeValue(myProperty, godotvector3);
                }
            }
        }

        public void SetSelectedNodeValue(MyProperty myProperty, object value)
        {
            System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public;

            System.Type systemType = _selectedNode.GetType();

            System.Reflection.FieldInfo? field = systemType.GetField(myProperty.Name, bindingFlags);

            TrySetField(field, myProperty, value);

            System.Reflection.PropertyInfo? prop = systemType.GetProperty(myProperty.Name, bindingFlags);

            TrySetProperty(prop, myProperty, value);

        }

        private void TrySetField(System.Reflection.FieldInfo? field, MyProperty myProperty, object value)
        {
            if (field is null)
            {
                return;
            }

            if (myProperty.Instance is bool)
            {
                if (bool.TryParse(value.ToString(), out bool result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is sbyte)
            {
                if (sbyte.TryParse(value.ToString(), out sbyte result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is byte)
            {
                if (byte.TryParse(value.ToString(), out byte result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is short)
            {
                if (short.TryParse(value.ToString(), out short result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is ushort)
            {
                if (ushort.TryParse(value.ToString(), out ushort result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is int)
            {
                if (int.TryParse(value.ToString(), out int result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is uint)
            {
                if (uint.TryParse(value.ToString(), out uint result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is long)
            {
                if (long.TryParse(value.ToString(), out long result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is ulong)
            {
                if (ulong.TryParse(value.ToString(), out ulong result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is float)
            {
                if (float.TryParse(value.ToString(), out float result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is double)
            {
                if (double.TryParse(value.ToString(), out double result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is decimal)
            {
                if (decimal.TryParse(value.ToString(), out decimal result))
                {
                    field.SetValue(_selectedNode, result);
                }
            }
            else if (myProperty.Instance is string)
            {
                field.SetValue(_selectedNode, value.ToString());

            }
            else if (myProperty.Instance is System.Numerics.Vector2)
            {
                System.Numerics.Vector2 result = (System.Numerics.Vector2)value;
                field.SetValue(_selectedNode, result);
            }
            else if (myProperty.Instance is Godot.Vector2)
            {
                Godot.Vector2 result = (Godot.Vector2)value;
                field.SetValue(_selectedNode, result);
            }
            else if (myProperty.Instance is System.Numerics.Vector3)
            {
                System.Numerics.Vector3 result = (System.Numerics.Vector3)value;
                field.SetValue(_selectedNode, result);
            }
            else if (myProperty.Instance is Godot.Vector3)
            {
                Godot.Vector3 result = (Godot.Vector3)value;
                field.SetValue(_selectedNode, result);
            }
        }

        private void TrySetProperty(System.Reflection.PropertyInfo? prop, MyProperty myProperty, object value)
        {
            if (prop is null)
            {
                return;
            }

            if (prop.CanWrite == false)
            {
                return;
            }

            if (myProperty.Instance is bool)
            {
                if (bool.TryParse(value.ToString(), out bool result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is sbyte)
            {
                if (sbyte.TryParse(value.ToString(), out sbyte result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is byte)
            {
                if (byte.TryParse(value.ToString(), out byte result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is short)
            {
                if (short.TryParse(value.ToString(), out short result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is ushort)
            {
                if (ushort.TryParse(value.ToString(), out ushort result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is int)
            {
                if (int.TryParse(value.ToString(), out int result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is uint)
            {
                if (uint.TryParse(value.ToString(), out uint result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is long)
            {
                if (long.TryParse(value.ToString(), out long result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is ulong)
            {
                if (ulong.TryParse(value.ToString(), out ulong result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is float)
            {
                if (float.TryParse(value.ToString(), out float result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is double)
            {
                if (double.TryParse(value.ToString(), out double result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is decimal)
            {
                if (decimal.TryParse(value.ToString(), out decimal result))
                {
                    prop.SetValue(_selectedNode, result, null);
                }
            }
            else if (myProperty.Instance is string)
            {
                prop.SetValue(_selectedNode, value.ToString(), null);
            }
            else if (myProperty.Instance is System.Numerics.Vector2)
            {
                System.Numerics.Vector2 result = (System.Numerics.Vector2)value;
                prop.SetValue(_selectedNode, result, null);
            }
            else if (myProperty.Instance is Godot.Vector2)
            {
                Godot.Vector2 result = (Godot.Vector2)value;
                prop.SetValue(_selectedNode, result, null);
            }
            else if (myProperty.Instance is System.Numerics.Vector3)
            {
                System.Numerics.Vector3 result = (System.Numerics.Vector3)value;
                prop.SetValue(_selectedNode, result, null);
            }
            else if (myProperty.Instance is Godot.Vector3)
            {
                Godot.Vector3 result = (Godot.Vector3)value;
                prop.SetValue(_selectedNode, result, null);
            }
        }
    }
}
