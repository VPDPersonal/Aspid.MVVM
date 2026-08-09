using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Editor-side utilities for working with <see cref="Type"/> names and the loaded type domain,
    /// shared by the type-selector infrastructure.
    /// </summary>
    internal static class TypeUtility
    {
        /// <summary>
        /// Removes the CLR generic-arity suffix (<c>Modifier`1</c> → <c>Modifier</c>) from a raw type name.
        /// Names without a backtick are returned unchanged.
        /// </summary>
        internal static string StripArity(string name)
        {
            var tick = name.IndexOf('`');
            return tick >= 0 ? name[..tick] : name;
        }

        /// <summary>
        /// Short display name for a type: generic types are rendered with angle-bracket arguments
        /// (<c>Modifier&lt;Single&gt;</c>, nested — <c>Modifier&lt;Modifier&lt;Int32&gt;&gt;</c>)
        /// instead of the raw arity form (<c>Modifier`1</c>). Non-generic types are returned unchanged.
        /// </summary>
        internal static string FormatGenericName(Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var baseName = StripArity(type.Name);
            var arguments = string.Join(", ", type.GetGenericArguments().Select(FormatGenericName));

            return $"{baseName}<{arguments}>";
        }

        /// <summary>
        /// Enumerates every type across all currently loaded assemblies, dropping the entries that fail to load
        /// in a partially-loadable assembly (<see cref="ReflectionTypeLoadException"/>).
        /// </summary>
        internal static IEnumerable<Type> EnumerateDomainTypes()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;

                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    types = exception.Types.Where(type => type is not null).ToArray();
                }

                foreach (var type in types)
                    yield return type;
            }
        }
    }
}
