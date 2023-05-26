using ImGuiNET;
using System.Reflection;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyTest
    {
        private static MyProperty[] myProperties = new MyProperty[0];

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
                    Value = i.ToString() + nameof(MyProperty.Value)
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
                ImGui.Text(nameof(windowSize) + " " + windowSize.ToString());

                if (ImGui.SmallButton(nameof(Init)))
                {
                    Init();
                }

                string tableID = MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
                MyPropertyTable.DrawTable(myProperties, tableID, MyPropertyFlags.TableFlags(), windowSize);
                ImGui.End();
            }
        }
    }
}
