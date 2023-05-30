namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyTest
    {
        private static MyProperty[] myProperties = new MyProperty[0];
        private static MyPropertyTable myPropertyTable = new MyPropertyTable();

        static MyPropertyTest()
        {
            Init();
        }

        private static void Init()
        {
            int rows = 100;
            myProperties = new MyProperty[rows];

            using (System.Security.Cryptography.SHA256 mySHA256 = System.Security.Cryptography.SHA256.Create())
            {
                for (int i = 0; i < myProperties.Length; i++)
                {
                    ulong hashInput = Godot.Time.GetTicksMsec();
                    hashInput += (ulong)i;
                    byte[] hashBytes = System.BitConverter.GetBytes(hashInput);
                    if (System.BitConverter.IsLittleEndian)
                    {
                        System.Array.Reverse(hashBytes);
                    }
                    byte[] hashValue = mySHA256.ComputeHash(hashBytes);
                    MyProperty newProperty = new MyProperty
                    {
                        Index = i,
                        Name = Utility.SHA256String(hashValue),
                        Instance = new object()
                    };
                    myProperties[i] = newProperty;
                }
            }
        }

        public static void Update()
        {
            if (!ImGuiNET.ImGui.Begin(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name, MyPropertyFlags.WindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }
            System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();

            if (ImGuiNET.ImGui.SmallButton(nameof(Init)))
            {
                Init();
            }

            string tableID = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
            string name = nameof(name);
            bool border = true;
            System.Numerics.Vector2 childSize = new System.Numerics.Vector2(windowSize.X - GodotRuntimeInspector.MinRowHeight, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
            if (ImGuiNET.ImGui.BeginChild(name, childSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(childSize.X - GodotRuntimeInspector.MinRowHeight, childSize.Y - GodotRuntimeInspector.MinRowHeight);
                myPropertyTable.DrawTable(ref myProperties, tableID, MyPropertyFlags.TableFlags(), tableSize);
                ImGuiNET.ImGui.EndChild();
            }
            ImGuiNET.ImGui.End();
        }
    }
}
