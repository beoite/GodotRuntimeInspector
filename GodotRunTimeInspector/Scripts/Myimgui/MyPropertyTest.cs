namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public static class MyPropertyTest
    {
        public static MyProperty[] Init()
        {
            int rows = 100;
            MyProperty[] myProperties = new MyProperty[rows];
            using (System.Security.Cryptography.SHA256 mySHA256 = System.Security.Cryptography.SHA256.Create())
            {
                for (int i = 0; i < myProperties.Length; i++)
                {
                    ulong hashInput = Godot.Time.GetTicksMsec();
                    hashInput += (ulong)i;
                    byte[] hashBytes = System.BitConverter.GetBytes(hashInput);
                    if (System.BitConverter.IsLittleEndian)
                    {
                        System.Array.Reverse(hashBytes);
                    }
                    byte[] hashValue = mySHA256.ComputeHash(hashBytes);
                    MyProperty newProperty = new MyProperty
                    {
                        Index = i,
                        Name = Utility.SHA256String(hashValue),
                        Instance = new object()
                    };
                    myProperties[i] = newProperty;
                }
            }
            return myProperties;
        }
    }
}
