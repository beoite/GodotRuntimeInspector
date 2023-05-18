namespace GodotRuntimeInspector.Scripts
{
    public static class Widget
    {
        public static void DrawWidget(Godot.Node3D node3d)
        {
            Godot.Vector3 from = new Godot.Vector3(0f, 0f, 0f);
            Godot.Vector3 to = new Godot.Vector3(10f, 10f, 10f);
            uint collisionMask = uint.MaxValue;
            Godot.Collections.Array<Godot.Rid> exclude = new Godot.Collections.Array<Godot.Rid>();
            Godot.PhysicsDirectSpaceState3D spaceState = node3d.GetWorld3D().DirectSpaceState;
            Godot.PhysicsRayQueryParameters3D query = Godot.PhysicsRayQueryParameters3D.Create(from, to, collisionMask, exclude);
            Godot.Collections.Dictionary result = spaceState.IntersectRay(query);
            if (result.Count > 0)
            {
                // hit
            }
            else
            {
                // no hit
            }

    
        }
    }
}
