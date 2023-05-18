using Godot;

namespace GodotRuntimeInspector.Scripts
{
    public static class CubeCreator
    {
        //StaticBody3D staticBody3D = CubeCreator.CreateCube();
        //AddChild(staticBody3D);
        public static StaticBody3D CreateCube(string name)
        {
            StaticBody3D staticBody3D = new StaticBody3D();
            staticBody3D.Name = name;
            CollisionShape3D collisionShape3D = new CollisionShape3D();
            collisionShape3D.Name = nameof(collisionShape3D);
            MeshInstance3D meshInstance3D = new MeshInstance3D();
            meshInstance3D.Name = nameof(meshInstance3D);
            staticBody3D.AddChild(collisionShape3D);
            collisionShape3D.AddChild(meshInstance3D);
            BoxMesh cubeMesh = new BoxMesh();
            meshInstance3D.Mesh = cubeMesh;
            BoxShape3D cube = new BoxShape3D();
            collisionShape3D.Shape = cube;
            return staticBody3D;
        }
    }
}
