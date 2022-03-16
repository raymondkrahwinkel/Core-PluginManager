using System.Reflection;
using System.Runtime.Loader;

namespace CorePluginManager.Utils;

public static class Assemblies
    {
        /// <summary>
        /// Loads external dll into an assembly
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Assembly LoadAssemblyFromPath(string path)
        {
            string fileNameWithOutExtension = System.IO.Path.GetFileNameWithoutExtension(path);

            bool inCompileLibraries = Microsoft.Extensions.DependencyModel.DependencyContext.Default.CompileLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));
            bool inRuntimeLibraries = Microsoft.Extensions.DependencyModel.DependencyContext.Default.RuntimeLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));

            return inCompileLibraries || inRuntimeLibraries
                ? Assembly.Load(new AssemblyName(fileNameWithOutExtension))
                : AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
        }

        /// <summary>
        /// checks if the assembly is not already loaded in the application
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool AssemblyIsLoaded(Assembly assembly)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return loadedAssemblies.Any(a => a == assembly);
        }

        /// <summary>
        /// checks if the assembly is not already loaded in the application
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static bool AssemblyIsLoaded(AssemblyName assemblyName)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            return loadedAssemblies.Any(a => a.GetName() == assemblyName);
        }

        /// <summary>
        /// Get TypeInfo list for type (extended on type)
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static List<TypeInfo> TypoInfoListByBaseType(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.DefinedTypes)
                    .Where(t => t.BaseType != null && t.BaseType == baseType)
                    .ToList();
        }

        /// <summary>
        /// Get TypeInfo list for type (extended on interface)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> AssemblyTypesByInterface(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                            .ToList();
        }

        /// <summary>
        /// Get TypeInfo list for type (extended on interface)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> AssemblyTypesByBaseType(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => p.BaseType != null && p.BaseType == type && !p.IsInterface)
                            .ToList();
        }

        /// <summary>
        /// Get assembly type by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Assembly? GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);
        }
    }