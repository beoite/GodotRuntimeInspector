namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyComparer : System.Collections.Generic.Comparer<MyProperty>
    {
        //     Less than zero – This instance is less than value.
        //     Zero – This instance is equal to value.
        //     Greater than zero – This instance is greater than value.
        public override int Compare(MyProperty? x, MyProperty? y)
        {
            return 0;
        }
        //Index
        public static int IndexAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Index.CompareTo(y.Index);
        }
        public static int IndexDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Index.CompareTo(x.Index);
        }
        // Tag
        public static int TagAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Tags.CompareTo(y.Tags);
        }
        public static int TagDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Tags.CompareTo(x.Tags);
        }
        // Type
        public static int TypeAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Type.Name.CompareTo(y.Type.Name);
        }
        public static int TypeDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Type.Name.CompareTo(x.Type.Name);
        }
        // Name
        public static int NameAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Name.CompareTo(y.Name);
        }
        public static int NameDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Name.CompareTo(x.Name);
        }
        // Instance
        public static int InstanceAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Name.CompareTo(y.Name);
        }
        public static int InstanceDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Name.CompareTo(x.Name);
        }
    }
}
