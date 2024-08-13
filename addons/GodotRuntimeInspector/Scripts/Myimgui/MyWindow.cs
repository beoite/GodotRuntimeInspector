namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyWindow
    {
        public object? TypeInstance = null;
        public System.Guid ID = System.Guid.NewGuid();
        public MyProperty[] MyProperties = System.Array.Empty<MyProperty>();
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
                System.Numerics.Vector2 tableSize = new System.Numerics.Vector2(windowSize.X - Config.MinRowHeight, windowSize.Y - Config.MinRowHeight);
                string tableID = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.Name + "TABLE";
                MyPropertyTable.DrawTable(MyProperties, tableID, MyPropertyFlags.TableFlags(), tableSize);
                ImGuiNET.ImGui.End();
            }
        }
    }
}
