﻿namespace GodotRuntimeInspector.Scripts.Myimgui
{
    /*
        C#      Range 	Size 	.NET type
        sbyte   -128 to 127 	Signed 8-bit integer 	System.SByte
        byte    0 to 255 	Unsigned 8-bit integer 	System.Byte
        short   -32,768 to 32,767 	Signed 16-bit integer 	System.Int16
        ushort  0 to 65,535 	Unsigned 16-bit integer 	System.UInt16
        int     -2,147,483,648 to 2,147,483,647 	Signed 32-bit integer 	System.Int32
        uint    0 to 4,294,967,295 	Unsigned 32-bit integer 	System.UInt32
        long    -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 	Signed 64-bit integer 	System.Int64
        ulong 	0 to 18,446,744,073,709,551,615 	Unsigned 64-bit integer 	System.UInt64
        nint 	Depends on platform (computed at runtime) 	Signed 32-bit or 64-bit integer 	System.IntPtr
        nuint 	Depends on platform (computed at runtime) 	Unsigned 32-bit or 64-bit integer 	System.UIntPtr
     */

    public enum MyTypes 
    {
        None = 0,
        Boolean = 1,
        Number = 2,
        String = 3,
        Complex = 4
    }
}
