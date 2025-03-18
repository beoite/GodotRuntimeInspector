#if TOOLS
namespace GodotRuntimeInspector.Scripts
{
    [Godot.Tool]
    public partial class PlaceholderPlugin : Godot.EditorPlugin
    {
        public override void _EnterTree() {}
        public override void _ExitTree() {}
    }
}
#endif