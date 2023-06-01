
namespace RuntimeInspector.Scripts.Myimgui
{
    [System.Flags]
    public enum Tags
    {
        None = 0b_0000_0000,        // 0
        Field = 0b_0000_0001,       // 1
        Property = 0b_0000_0010,    // 2
        Method = 0b_0000_0100,      // 4
        Static = 0b_0000_1000,      // 8
        //Friday = 0b_0001_0000,    // 16
        //Saturday = 0b_0010_0000,  // 32
        //Sunday = 0b_0100_0000,    // 64
        //Weekend = Saturday | Sunday
    }
}
