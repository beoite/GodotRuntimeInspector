namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MultilineTextWindow
    {
        public System.Guid ID = System.Guid.NewGuid();
        public MultilineText MultilineText = new MultilineText();

        public void Update(string name, ref string input)
        {
            string strID = name + "###" + ID;

            if (!ImGuiNET.ImGui.Begin(strID, MyPropertyFlags.ContainerWindowFlags()))
            {
                ImGuiNET.ImGui.End();
                return;
            }
            System.Numerics.Vector2 windowSize = ImGuiNET.ImGui.GetWindowSize();
            windowSize = new System.Numerics.Vector2(windowSize.X - GodotRuntimeInspector.MinRowHeight, windowSize.Y - GodotRuntimeInspector.MinRowHeight * 2f);
            MultilineText.Update(ref input, windowSize);
            ImGuiNET.ImGui.End();
        }
    }
}
