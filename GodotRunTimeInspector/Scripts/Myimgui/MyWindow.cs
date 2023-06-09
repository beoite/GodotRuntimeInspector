namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyWindow
    {
        public object? TypeInstance = null;
        public System.Guid ID = System.Guid.NewGuid();
        public MyProperty[] MyProperties = new MyProperty[0];
        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public virtual void Update()
        {
            if (TypeInstance != null)
            {
                MyProperties = MyProperty.NewArray(TypeInstance);
            }
            string strID = TypeInstance?.ToString() + "###" + ID;
            if (ImGuiNET.ImGui.Begin(strID, MyPropertyFlags.ContainerWindowFlags()))
            {
                System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
                System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(windowSize.X - GodotRuntimeInspector.MinRowHeight, windowSize.Y - GodotRuntimeInspector.MinRowHeight);
                string tableID = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
                MyPropertyTable.DrawTable(ref MyProperties, tableID, MyPropertyFlags.TableFlags(), tableSize);
                ImGuiNET.ImGui.End();
            }
        }
    }
}
