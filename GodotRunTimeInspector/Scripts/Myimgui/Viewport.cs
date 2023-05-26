using ImGuiNET;
using System.Reflection;


namespace RuntimeInspector.Scripts.Myimgui
{
    public static class Viewport
    {
        private static MyProperty[] myProperties = new MyProperty[0];

        private static System.Reflection.PropertyInfo[] props = new System.Reflection.PropertyInfo[0];

        private static string animatedTitle = string.Empty;

        private static void Init()
        {
            myProperties = new MyProperty[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                System.Reflection.PropertyInfo prop = props[i];
                object? val = prop.GetValue(GodotRuntimeInspector.MAINVIEWPORTPTR, null);
                MyProperty myProperty = new MyProperty
                {
                    Index = i,
                    Name = prop.Name,
                    Value = Utility.GetStr(val)
                };
                myProperties[i] = myProperty;
            }
        }

        public static void Update()
        {
            props = GodotRuntimeInspector.MAINVIEWPORTPTR.GetType().GetProperties();

            if (myProperties.Length == 0)
            {
                Init();
            }
            char[] spin = "|/-\\".ToCharArray();
            int frame = (int)ImGui.GetTime() % spin.Length;
            string spinFrame = spin[frame].ToString();
            animatedTitle = spinFrame + " " + ImGui.GetTime().ToString("0.00");

            // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            string staticIdentifier = "###" + MethodBase.GetCurrentMethod()?.DeclaringType?.Name;
            animatedTitle = animatedTitle + staticIdentifier;
            if (ImGui.Begin(animatedTitle, MyPropertyFlags.WindowFlags()))
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
