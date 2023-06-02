using Godot;

namespace RuntimeInspector.Scripts
{
    public static class TestCubes
    {
        public static System.Collections.Generic.List<Godot.StaticBody3D> cubes = new System.Collections.Generic.List<Godot.StaticBody3D>();

        public static void Create(Godot.Node parent)
        {
            for (int i = 0; i < 333; i++)
            {
                float range = 33f;
                Godot.StaticBody3D testCube = CreateCube(nameof(testCube) + i);
                float x = new Godot.RandomNumberGenerator().RandfRange(-range, range);
                float y = new Godot.RandomNumberGenerator().RandfRange(-range, range);
                float z = new Godot.RandomNumberGenerator().RandfRange(-range, range);
                testCube.GlobalTransform = new Godot.Transform3D(Godot.Basis.Identity, new Godot.Vector3(x, y, z));
                parent.AddChild(testCube);
                cubes.Add(testCube);
            }
        }

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
