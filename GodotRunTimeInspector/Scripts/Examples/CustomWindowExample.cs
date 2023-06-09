namespace Examples
{
    public partial class CustomWindowExample : Godot.Node
    {
        public static bool ShowCustomWindowExample = false;
        public GodotRuntimeInspector.Scripts.Myimgui.MyWindow MyWindow = new GodotRuntimeInspector.Scripts.Myimgui.MyWindow();
        public ExmapleData MyExmapleData = new ExmapleData();

        public override void _Process(double delta)
        {
            MyWindow.TypeInstance = MyExmapleData;
            GodotRuntimeInspector.Scripts.GodotRuntimeInspector.MyWindowDictionary[MyWindow] = ShowCustomWindowExample;
        }
    }
}
