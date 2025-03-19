namespace GodotRuntimeInspector.Scripts.Myimgui
{
    public class MyProperty
    {
        public System.Guid Id = System.Guid.NewGuid();
        public int Index = 0;
        public Tags Tags = Tags.None;
        public System.Type Type = typeof(MyProperty);
        public string Name = string.Empty;
        public object? Instance = null;
        public static MyProperty[] NewArray(object? instance)
        {
            if (instance is null)
            {
                return System.Array.Empty<MyProperty>();
            }
            System.Reflection.FieldInfo[] fields = System.Array.Empty<System.Reflection.FieldInfo>();
            System.Reflection.PropertyInfo[] props = System.Array.Empty<System.Reflection.PropertyInfo>();
            System.Reflection.MethodInfo[] methods = System.Array.Empty<System.Reflection.MethodInfo>();
            fields = instance.GetType().GetFields();
            props = instance.GetType().GetProperties();
            methods = instance.GetType().GetMethods();
            int length = fields.Length + props.Length + methods.Length;
            MyProperty[] myProperties = new MyProperty[length];
            int combinedIndex = -1;
            // Fields
            for (int i = 0; i < fields.Length; i++)
            {
                combinedIndex++;
                System.Reflection.FieldInfo field = fields[i];
                object? val = field.GetValue(instance);
                Tags tags = Tags.Field;
                if (field.IsStatic == true)
                {
                    tags |= Tags.Static;
                }
                MyProperty myProperty = new MyProperty
                {
                    Index = combinedIndex,
                    Tags = tags,
                    Type = field.FieldType,
                    Name = field.Name,
                    Instance = val
                };
                myProperties[combinedIndex] = myProperty;
            }
            // Properties
            for (int i = 0; i < props.Length; i++)
            {
                combinedIndex++;
                System.Reflection.PropertyInfo prop = props[i];
                if (prop.CanRead == true)
                {
                    System.Reflection.ParameterInfo[] indexParams = prop.GetIndexParameters();
                    if (indexParams.Length > 0)
                    {
                        continue;
                    }
                    object? val = prop.GetValue(instance, null);
                    Tags tags = Tags.Property;
                    MyProperty myProperty = new MyProperty
                    {
                        Index = combinedIndex,
                        Tags = tags,
                        Type = prop.PropertyType,
                        Name = prop.Name,
                        Instance = val
                    };
                    myProperties[combinedIndex] = myProperty;
                }
            }
            // Methods
            for (int i = 0; i < methods.Length; i++)
            {
                combinedIndex++;
                System.Reflection.MethodInfo method = methods[i];
                if (method.IsPublic == false)
                {
                    continue;
                }
                Tags tags = Tags.Method;
                if (method.IsStatic == true)
                {
                    tags |= Tags.Static;
                }
                MyProperty myProperty = new MyProperty();
                myProperty.Index = combinedIndex;
                myProperty.Tags = tags;
                myProperty.Type = method.ReturnType;
                System.Reflection.ParameterInfo[] methodparams = method.GetParameters();
                string signature = " ( ";
                for (int j = 0; j < methodparams.Length; j++)
                {
                    signature += " " + methodparams[j].ParameterType.ToString();
                    signature += " " + methodparams[j].Name;
                    signature += " " + methodparams[j].DefaultValue;
                    signature += ",";
                }
                signature = signature.Substring(0, signature.Length - 1);
                signature += " )";
                myProperty.Name = method.Name + signature;
                myProperty.Instance = null;
                myProperties[combinedIndex] = myProperty;
            }
            return myProperties;
        }
    }
}
