namespace GodotRuntimeInspector.Scripts
{
    [System.Flags]
    public enum Tags
    {
        None = 0b_0000_0000,        // 0
        Field = 0b_0000_0001,       // 1
        Property = 0b_0000_0010,    // 2
        Method = 0b_0000_0100,      // 4
        Static = 0b_0000_1000,      // 8
    }
}
