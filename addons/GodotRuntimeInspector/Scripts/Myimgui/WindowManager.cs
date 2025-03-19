namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class WindowManager
    {
        public static MultilineTextWindow MultilineTextWindow = new MultilineTextWindow();
        public static System.Collections.Generic.Dictionary<System.Guid, MyPropertyInspector> MyPropertyInspectors = new System.Collections.Generic.Dictionary<System.Guid, MyPropertyInspector>();
        public static void Add(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);
            bool contains = MyPropertyInspectors.ContainsKey(myProperty.Id);
            if (contains == false)
            {
                MyPropertyInspector myPropertyInspector = new MyPropertyInspector(myProperty);
                MyPropertyInspectors.Add(myProperty.Id, myPropertyInspector);
            }
        }
        public static void Remove(MyProperty myProperty)
        {
            bool removed = MyPropertyInspectors.Remove(myProperty.Id);
        }
    }
}
