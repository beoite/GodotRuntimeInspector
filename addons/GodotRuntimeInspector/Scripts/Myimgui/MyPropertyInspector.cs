namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyInspector
    {
        public MyProperty MyProperty;

        public System.Numerics.Vector2 WindowSize = System.Numerics.Vector2.Zero;

        public System.Numerics.Vector2 TopSize = System.Numerics.Vector2.Zero;

        public System.Numerics.Vector2 TopLeftSize = System.Numerics.Vector2.Zero;

        public System.Numerics.Vector2 TopRightSize = System.Numerics.Vector2.Zero;

        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();

        public MyPropertyInspector(MyProperty myProperty)
        {
            MyProperty = myProperty;
        }

        public void Update()
        {
            string controlId = Utility.ToControlId(MyProperty);

            if (ImGuiNET.ImGui.Button(MyProperty.Name + " (Close)", new System.Numerics.Vector2(ImGuiNET.ImGui.GetColumnWidth(), Config.MinRowHeight)))
            {
                WindowManager.Remove(MyProperty);
            }

            MyProperties = MyProperty.NewArray(MyProperty.Instance);

            MyPropertyTable.Update(null, MyProperties, nameof(MyProperties), TopRightSize);
        }
    }
}
