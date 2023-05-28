using ImGuiNET;

namespace RuntimeInspector.Scripts.Myimgui
{
    public class MyPropertyInspector
    {
        public bool IsVisible = false;
        public bool BringToFront = true;

        public void Update(MyProperty myProperty)
        {
            if (IsVisible == false)
            {
                return;
            }
            ImGuiWindowFlags windowFlags = new ImGuiWindowFlags();
            if (BringToFront == true)
            {

            }
            if (ImGui.Begin(myProperty.Name, windowFlags))
            {
                System.Numerics.Vector2 windowSize = ImGui.GetWindowSize();
            }

        }
    }
}
