namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyChild
    {
        public object? TypeInstance = null;
        public System.Guid ID = System.Guid.NewGuid();
        public MyProperty[] MyProperties = new MyProperty[0];
        public MyPropertyTable MyPropertyTable = new MyPropertyTable();

        public void Update(System.Numerics.Vector2 size)
        {
            if (TypeInstance != null)
            {
                MyProperties = MyProperty.NewArray(TypeInstance);
            }
            string strID = TypeInstance?.ToString() + "###" + ID;
            if (ImGuiNET.ImGui.BeginChild(strID, size, false, MyPropertyFlags.TreeNodeWindowFlags()))
            {
                string tableID = ID + "TABLE";
                MyPropertyTable.DrawTable(MyProperties, tableID, MyPropertyFlags.TableFlags(), size);
                ImGuiNET.ImGui.EndChild();
            }
        }
    }
}
