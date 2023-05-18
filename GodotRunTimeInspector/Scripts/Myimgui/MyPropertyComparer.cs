using System;
using System.Collections.Generic;

namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyComparer : Comparer<MyProperty>
    {
        public override int Compare(MyProperty? x, MyProperty? y)
        {
            throw new NotImplementedException();
        }

        public int CompareIndexAscending(MyProperty? x, MyProperty? y)
        {
            //     Less than zero – This instance is less than value.
            //     Zero – This instance is equal to value.
            //     Greater than zero – This instance is greater than value.
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Index.CompareTo(y.Index);
        }

        public int CompareIndexDescending(MyProperty? x, MyProperty? y)
        {
            //     Less than zero – This instance is less than value.
            //     Zero – This instance is equal to value.
            //     Greater than zero – This instance is greater than value.
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Index.CompareTo(x.Index);
        }

        public int CompareNameAscending(MyProperty? x, MyProperty? y)
        {
            //     Less than zero – This instance is less than value.
            //     Zero – This instance is equal to value.
            //     Greater than zero – This instance is greater than value.
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Name.CompareTo(y.Name);
        }

        public int CompareNameDescending(MyProperty? x, MyProperty? y)
        {
            //     Less than zero – This instance is less than value.
            //     Zero – This instance is equal to value.
            //     Greater than zero – This instance is greater than value.
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Name.CompareTo(x.Name);
        }

        public int CompareValueAscending(MyProperty? x, MyProperty? y)
        {
            //     Less than zero – This instance is less than value.
            //     Zero – This instance is equal to value.
            //     Greater than zero – This instance is greater than value.
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Value.CompareTo(y.Value);
        }

        public int CompareValueDescending(MyProperty? x, MyProperty? y)
        {
            //     Less than zero – This instance is less than value.
            //     Zero – This instance is equal to value.
            //     Greater than zero – This instance is greater than value.
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Value.CompareTo(x.Value);
        }
    }
}
