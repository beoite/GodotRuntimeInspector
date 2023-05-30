using ImGuiNET;
using System.Reflection;

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
            for (int i = 0; i < myProperties.Length; i++)
            {
                MyProperty newProperty = new MyProperty
                {
                    Index = i,
                    Name = Rng().ToString(),
                    Instance = new object()
                };
                myProperties[i] = newProperty;
            }
        }

        private static int Rng()
        {
            Godot.RandomNumberGenerator random = new Godot.RandomNumberGenerator();
            random.Randomize();
            int rng = random.RandiRange(0, 59);
            return rng;
        }

        public static void Update()
        {
            if (ImGui.Begin(MethodBase.GetCurrentMethod()?.DeclaringType?.Name, MyPropertyFlags.WindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
                if (ImGui.SmallButton(nameof(Init)))
                {
                    Init();
                }


                string tableID = MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
                string name = nameof(name);
                bool border = true;
                System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(windowSize.X, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
                if (ImGui.BeginChild(name, tableSize, border, MyPropertyFlags.TreeNodeWindowFlags()))
                {
                    myPropertyTable.DrawTable(ref myProperties, tableID, MyPropertyFlags.TableFlags(), windowSize);
                    ImGui.EndChild();
                }
                ImGui.End();
            }
        }
    }
}
