namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MultilineText
    {
        public System.Guid ID = System.Guid.NewGuid();
        public void Update(ref string input, System.Numerics.Vector2 size)
        {
            ImGuiNET.ImGuiInputTextFlags flags = ImGuiNET.ImGuiInputTextFlags.None;
            flags |= ImGuiNET.ImGuiInputTextFlags.ReadOnly;
            string textID = "###" + ID;
            if (ImGuiNET.ImGui.InputTextMultiline(textID, ref input, uint.MaxValue, size, flags))
            {
            }
        }
    }
}
