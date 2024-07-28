using System;
using System.Collections.Generic;

public class ReflHelper
{
    public static Dictionary<string, Type> typeCache = new Dictionary<string, Type>();
    public static Type GetTypeByName(string name)
    {
        {
            if (typeCache.TryGetValue(name, out var type))
            {
                return type;
            }
        }

        var asys = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var a in asys)
        {
            Type type = a.GetType(name);
            if (type != null)
            {
                typeCache.Add(name, type);
                return type;
            }
        }
        return null;
    }
}