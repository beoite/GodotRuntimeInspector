using Godot;
using ImGuiNET;
using System.Reflection;

namespace RuntimeInspector.Scripts.Myimgui
{
    public static class Input
    {
        private static MyProperty[] myProperties = new MyProperty[0];
        private static System.Reflection.PropertyInfo[] props = new System.Reflection.PropertyInfo[0];
        private static System.Reflection.MethodInfo[] methods = new System.Reflection.MethodInfo[0];

        private static void Init(InputEvent inputEvent)
        {
            myProperties = new MyProperty[props.Length + methods.Length];

            for (int i = 0; i < props.Length; i++)
            {
                System.Reflection.PropertyInfo prop = props[i];
                object? val = prop.GetValue(inputEvent);
                MyProperty myProperty = new MyProperty
                {
                    Index = i,
                    Name = prop.Name,
                    Value = Utility.GetStr(val)
                };
                myProperties[i] = myProperty;
            }
            for (int i = 0; i < methods.Length; i++)
            {
                System.Reflection.MethodInfo method = methods[i];
                MyProperty myProperty = new MyProperty();
                myProperty.Index = i;
                myProperty.Name = method.ReturnParameter.Name + " " + method.Name;
                ParameterInfo[] methodparams = method.GetParameters();
                string val = methodparams.Length.ToString();
                for (int j = 0; j < methodparams.Length; j++)
                {
                    val += "( ";
                    val += " " + methodparams[j].ParameterType.ToString();
                    val += " " + methodparams[j].Name;
                    val += " " + methodparams[j].DefaultValue;
                    val += " )";
                }
                myProperty.Value = val;

                myProperties[props.Length + i] = myProperty;
            }
        }

        public static void Update(InputEvent? inputEvent)
        {
            if (inputEvent == null)
            {
                props = new System.Reflection.PropertyInfo[0];
            }
            else
            {
                props = inputEvent.GetType().GetProperties();
                methods = inputEvent.GetType().GetMethods();
                Init(inputEvent);
            }

            if (ImGui.Begin(MethodBase.GetCurrentMethod()?.DeclaringType?.Name, MyPropertyFlags.WindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
                ImGui.Text(nameof(windowSize) + " " + windowSize.ToString());

                string tableID = MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
                MyPropertyTable.DrawTable(myProperties, tableID, MyPropertyFlags.TableFlags(), windowSize);

                ImGui.End();
            }
        }
    }
}
