using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal sealed class TypeInfo
    {
        public readonly string Name;
        public readonly string Assembly;
        public readonly string FullName;
        public readonly string Namespace;
        public readonly string AssemblyQualifiedName;

        public TypeInfo(Type type)
        {
            Name = type.Name;
            FullName = type.FullName;
            Assembly = type.Assembly.GetName().Name;
            AssemblyQualifiedName = type.AssemblyQualifiedName;
            Namespace = string.IsNullOrEmpty(type.Namespace) ? Constants.GlobalNamespace : type.Namespace;
        }
        
        public static List<TypeInfo> GetAllTypeInfos(Type[] baseTypes, TypeAllow allow)
        {
            var result = new List<TypeInfo>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    result
                        .AddRange(assembly.GetTypes()
                        .Where(t => baseTypes.All(baseType => baseType.IsAssignableFrom(t)) &&
                            !t.IsDefined(typeof(CompilerGeneratedAttribute), false) &&
                            !t.Name.Contains("<") &&
                            !t.Name.Contains(">") &&
                            (allow.HasFlag(TypeAllow.Abstract) || !t.IsAbstract) &&
                            (allow.HasFlag(TypeAllow.Interface) || !t.IsInterface))
                        .Select(type => new TypeInfo(type)));
                }
                catch
                {
                    // ignored
                }
            }

            return result;
        }
    }
}
