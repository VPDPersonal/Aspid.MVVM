using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal sealed class TypeInfo
    {
        internal readonly string Name;
        internal readonly string Assembly;
        internal readonly string Namespace;
        internal readonly string AssemblyQualifiedName;

        /// <summary>
        /// Tooltip override from <see cref="TypeSelectorDisplayAttribute.Tooltip"/>; falls back to
        /// <see cref="Type.FullName"/> when no override is supplied.
        /// </summary>
        internal readonly string Tooltip;

        /// <summary>
        /// Raw icon identifier from <see cref="TypeSelectorDisplayAttribute.Icon"/>; <see langword="null"/>
        /// when no icon was requested.
        /// </summary>
        internal readonly string Icon;

        /// <summary>
        /// Normalized display-name override from <see cref="TypeSelectorDisplayAttribute.Name"/>;
        /// <see langword="null"/> when the type keeps its real name.
        /// </summary>
        internal readonly string CustomName;

        /// <summary>
        /// Normalized <see cref="TypeSelectorDisplayAttribute.Group"/> path segments (split on <c>/</c>,
        /// trimmed, empty segments dropped); <see langword="null"/> when the type stays under its namespace.
        /// </summary>
        internal readonly string[] GroupPath;

        /// <summary>
        /// The label the picker shows for this type: the <see cref="CustomName"/> override when present,
        /// the real (short) type name otherwise.
        /// </summary>
        internal string Label => CustomName ?? Name;

        internal TypeInfo(Type type)
        {
            Name = TypeUtility.FormatGenericName(type);
            Assembly = type.Assembly.GetName().Name;
            AssemblyQualifiedName = type.AssemblyQualifiedName;
            Namespace = string.IsNullOrEmpty(type.Namespace) ? TypeSelectorHelpers.GlobalNamespace : type.Namespace;

            var item = type.GetCustomAttribute<TypeSelectorDisplayAttribute>(inherit: false);

            Tooltip = type.FullName;
            Icon = null;
            CustomName = TypeSelectorHelpers.GetCustomDisplayName(type);
            GroupPath = null;

            if (item is null) return;

            Icon = string.IsNullOrWhiteSpace(item.Icon) ? null : item.Icon;
            GroupPath = ParseGroupPath(item.Group);

            if (!string.IsNullOrWhiteSpace(item.Tooltip))
                Tooltip = item.Tooltip;
        }

        // "Combat / Melee //" → ["Combat", "Melee"]; null when nothing survives, so a blank-only Group degrades to
        // the namespace placement. Sentinel segments are dropped too — the picker keys off DisplayName == "<None>",
        // so a group node named after a sentinel would impersonate it.
        private static string[] ParseGroupPath(string group)
        {
            if (string.IsNullOrWhiteSpace(group)) return null;

            var segments = group.Split('/')
                .Select(segment => segment.Trim())
                .Where(segment => segment.Length > 0 &&
                    segment != TypeSelectorHelpers.NoneOption &&
                    segment != TypeSelectorHelpers.GlobalNamespace)
                .ToArray();

            return segments.Length > 0 ? segments : null;
        }

        /// <summary>
        /// Collects the type infos shown in the selector. <paramref name="additionalTypes"/> are appended
        /// verbatim (bypassing the base-type, name and <paramref name="allow"/> checks) so callers can inject
        /// entries — such as open generic definitions — that the standard <see cref="Type.IsAssignableFrom"/>
        /// scan cannot match.
        /// </summary>
        internal static List<TypeInfo> GetAllTypeInfos(
            Type[] baseTypes,
            TypeAllow allow,
            Func<Type, bool> filter = null,
            IEnumerable<Type> additionalTypes = null)
        {
            var result = new List<TypeInfo>();

            result.AddRange(TypeUtility.EnumerateDomainTypes()
                .Where(t => baseTypes.All(baseType => baseType.IsAssignableFrom(t)) &&
                    !t.IsDefined(typeof(CompilerGeneratedAttribute), false) &&
                    !t.Name.Contains("<") &&
                    !t.Name.Contains(">") &&
                    (allow.HasFlag(TypeAllow.Abstract) || t.IsInterface || !t.IsAbstract) &&
                    (allow.HasFlag(TypeAllow.Interface) || !t.IsInterface) &&
                    (filter is null || filter(t)))
                .Select(type => new TypeInfo(type)));

            if (additionalTypes is not null)
            {
                var existing = new HashSet<string>(result.Select(info => info.AssemblyQualifiedName));

                result.AddRange(additionalTypes
                    .Where(type => type is not null && existing.Add(type.AssemblyQualifiedName))
                    .Select(type => new TypeInfo(type)));
            }

            return result;
        }
    }
}
