using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Attributes;
using Common.Interfaces;

namespace Common.Helpers
{
    public static class TypeHelper<T>
    {
        public static T GetType(string typeName)
        {
            var assembly = Assembly.GetEntryAssembly();
            return (T)Activator.CreateInstance(assembly.GetType(typeName));
        }

        public static T CreateInstance(Type type)
        {
            return (T) Activator.CreateInstance(type);
        }

        static TypeHelper()
        {

            var currentAssembly = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(currentAssembly.Location);
            foreach (var file in Directory.GetFiles(path, "Game.*.dll"))
            {
                Assembly.LoadFrom(file);
            }
        } 

        public static Type GetGameType(string gameType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var games = assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => typeof(IGame).IsAssignableFrom(type) && !type.IsInterface);
            var t = games.Single(type => type.GetCustomAttribute<GameTypeAttribute>().Type == gameType);
            return t;
        }
    }
}
