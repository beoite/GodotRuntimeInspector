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
                float eulerY = new Godot.RandomNumberGenerator().RandfRange(-range, range);
                testCube.RotateObjectLocal(Godot.Vector3.Up, eulerY);
                parent.AddChild(testCube);
                cubes.Add(testCube);
            }
        }

        //StaticBody3D staticBody3D = CubeCreator.CreateCube();
        //AddChild(staticBody3D);
        public static Godot.StaticBody3D CreateCube(string name)
        {
            Godot.StaticBody3D staticBody3D = new Godot.StaticBody3D();
            staticBody3D.Name = name;
            Godot.CollisionShape3D collisionShape3D = new Godot.CollisionShape3D();
            collisionShape3D.Name = nameof(collisionShape3D);
            Godot.MeshInstance3D meshInstance3D = new Godot.MeshInstance3D();
            meshInstance3D.Name = nameof(meshInstance3D);
            staticBody3D.AddChild(collisionShape3D);
            collisionShape3D.AddChild(meshInstance3D);
            Godot.BoxMesh cubeMesh = new Godot.BoxMesh();
            meshInstance3D.Mesh = cubeMesh;
            Godot.BoxShape3D cube = new Godot.BoxShape3D();
            collisionShape3D.Shape = cube;
            return staticBody3D;
        }
    }
}
