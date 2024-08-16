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
                    //DrawMyBoolean(myProperty);
                    break;

                case MyTypes.Number:
                    //DrawMyNumber(myProperty);
                    DrawDefault(myProperty);
                    break;

                case MyTypes.String:
                    //DrawMyString(myProperty);
                    DrawDefault(myProperty);
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

            }
        }

        private void DrawMyNumber(MyProperty myProperty)
        {
            string text = Utility.GetStr(myProperty.Instance);
            string controlId = text + "###" + myProperty.Name;
            float columnWidth = ImGuiNET.ImGui.GetColumnWidth();
            System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, Config.MinRowHeight);

            ImGuiNET.ImGuiInputTextFlags imGuiInputTextFlags = ImGuiNET.ImGuiInputTextFlags.None;
            uint maxValue = int.MaxValue;

            if (myProperty.Instance is sbyte)
            {
                sbyte value = (sbyte)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is byte)
            {
                byte value = (byte)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is short)
            {
                short value = (short)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is ushort)
            {
                ushort value = (ushort)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is int)
            {
                int value = (int)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is uint)
            {
                uint value = (uint)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is long)
            {
                long value = (long)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is ulong)
            {
                ulong value = (ulong)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is float)
            {
                float value = (float)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is double)
            {
                double value = (double)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
            if (myProperty.Instance is decimal)
            {
                decimal value = (decimal)myProperty.Instance;
                if (ImGuiNET.ImGui.InputTextMultiline(controlId, ref text, maxValue, size, imGuiInputTextFlags))
                {

                }
            }
        }

        private unsafe void DrawMyString(MyProperty myProperty)
        {
            string text = Utility.GetStr(myProperty.Instance);

            string controlId = text + "###" + myProperty.Name;

            float columnWidth = ImGuiNET.ImGui.GetColumnWidth();

            System.Numerics.Vector2 size = new System.Numerics.Vector2(columnWidth, Config.MinRowHeight);

            ImGuiNET.ImGuiInputTextFlags imGuiInputTextFlags = ImGuiNET.ImGuiInputTextFlags.None;

            uint maxValue = int.MaxValue;

            //string mystring = (string)myProperty.Instance;
            //byte[] buf = Encoding.ASCII.GetBytes(mystring);
            //uint buf_size = 64;
            //ImGuiNET.ImGuiInputTextFlags flags = ImGuiNET.ImGuiInputTextFlags.None;

            //ImGuiNET.ImGuiInputTextCallback imGuiInputTextCallback = new ImGuiNET.ImGuiInputTextCallback(MyCallBack);

            //if (ImGuiNET.ImGui.InputText(text, buf, buf_size, flags, imGuiInputTextCallback))
            //{

            //}
        }

        private unsafe int MyCallBack(ImGuiNET.ImGuiInputTextCallbackData* data)
        {
            Godot.GD.Print(nameof(MyCallBack));

            return 0;
        }
    }
}
