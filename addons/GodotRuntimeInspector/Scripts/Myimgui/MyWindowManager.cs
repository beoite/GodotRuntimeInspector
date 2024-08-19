using System.Linq;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class MyWindowManager
    {
        private static System.Collections.Generic.Dictionary<string, MyWindow> MyWindows = new System.Collections.Generic.Dictionary<string, MyWindow>();

        public static void Add(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);

            bool contains = MyWindows.ContainsKey(controlId);

            if (contains == false)
            {
                MyWindow myWindow = new MyWindow(myProperty);

                MyWindows.Add(controlId, myWindow);
            }
        }

        public static void Remove(MyProperty myProperty)
        {
            string controlId = Utility.ToControlId(myProperty);

            bool removed = MyWindows.Remove(controlId);
        }

        public static void Update()
        {
            string[] keys = MyWindows.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                MyWindow myWindow = MyWindows[key];
                myWindow.Update();
            }
        }
    }
}
