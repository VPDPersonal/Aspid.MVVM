using System;
using Aspid.FastTools.Types.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The candidate filter every managed-reference picker in the References window shares, so an inline card fix and
    /// a bulk group fix can never offer different type sets for the same constraint.
    /// </summary>
    internal static class ManagedReferenceFilter
    {
        /// <summary>
        /// Concrete types assignable to <paramref name="constraint"/>, plus the open generic definitions that can
        /// close over it. A <see langword="null"/> or <see cref="object"/> constraint falls back to unconstrained
        /// (any managed-reference type).
        /// </summary>
        public static TypeSelectorFilter For(Type constraint)
        {
            var baseType = constraint ?? typeof(object);

            return new TypeSelectorFilter
            {
                Types = new[] { baseType },
                Predicate = SerializeReferenceHelpers.IsAssignableManagedReference,
                AdditionalTypes = baseType == typeof(object) ? null : GenericTypeResolver.GetAssignableGenericDefinitions(baseType),
                ArgumentFilter = SerializeReferenceHelpers.IsValidGenericArgument,
            };
        }
    }
}
