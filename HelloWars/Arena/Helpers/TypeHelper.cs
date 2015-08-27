using System;
using System.Reflection;

namespace Arena.Helpers
{
    public static class TypeHelper<T>
    {
        public static T GetType(string typeName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return (T)Activator.CreateInstance(assembly.GetType(typeName));
        }
    }
}
