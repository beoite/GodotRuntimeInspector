namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyInspector
    {
        public MyProperty MyProperty;

        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public MyPropertyInspector(MyProperty myProperty)
        {
            MyProperty = myProperty;
        }

        public void Update(System.Numerics.Vector2 size)
        {
            string controlId = Utility.ToControlId(MyProperty);

            if (ImGuiNET.ImGui.Button(controlId + " (Close)", new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), Config.MinRowHeight)))
            {
                WindowManager.Remove(MyProperty);
            }

            MyProperties = MyProperty.NewArray(MyProperty.Instance);

            if (MyProperties.Length == 0)
            {
                return;
            }

            MyPropertyTable.Update(null, MyProperties, nameof(MyProperties), size);
        }
    }
}
