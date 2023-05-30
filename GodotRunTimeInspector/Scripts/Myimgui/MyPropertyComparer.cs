using System;
using System.Collections.Generic;

namespace RuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyComparer : Comparer<MyProperty>
    {
        //     Less than zero – This instance is less than value.
        //     Zero – This instance is equal to value.
        //     Greater than zero – This instance is greater than value.
        public override int Compare(MyProperty? x, MyProperty? y)
        {
            throw new NotImplementedException();
        }

        //Index

        public int IndexAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Index.CompareTo(y.Index);
        }

        public int IndexDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Index.CompareTo(x.Index);
        }

        // Tag

        public int TagAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Tag.CompareTo(y.Tag);
        }

        public int TagDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Tag.CompareTo(x.Tag);
        }

        // Name

        public int NameAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Name.CompareTo(y.Name);
        }

        public int NameDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Name.CompareTo(x.Name);
        }

        // Type

        public int TypeAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Type.Name.CompareTo(y.Type.Name);
        }

        public int TypeDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Type.Name.CompareTo(x.Type.Name);
        }


        // Clicks

        public int ClicksAscending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Clicks.CompareTo(y.Clicks);
        }

        public int ClicksDescending(MyProperty? x, MyProperty? y)
        {
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Clicks.CompareTo(x.Clicks);
        }
    }
}
