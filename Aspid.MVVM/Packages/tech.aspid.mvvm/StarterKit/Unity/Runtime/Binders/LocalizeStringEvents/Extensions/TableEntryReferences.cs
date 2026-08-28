#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Tables;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for <see cref="TableEntryReference"/>.
    /// </summary>
    internal static class TableEntryReferences
    {
        /// <summary>
        /// Returns the entry's key name, or <see langword="null"/> with a diagnostic when the reference does not
        /// carry one.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="null"/> when the reference is stored by id rather than by name, since resolving
        /// an id to its name needs the shared table data a binder cannot assume it has.
        /// </remarks>
        /// <param name="reference">The reference to read.</param>
        /// <param name="owner">The binder reading it; used to name the source in the diagnostic.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        public static string ToKeyName(this TableEntryReference reference, IBinder owner, Object context = null)
        {
            if (reference.ReferenceType is TableEntryReference.Type.Name) return reference.Key;
            if (reference.ReferenceType is TableEntryReference.Type.Empty) return null;

            owner.LogError(
                problem: $"the table entry is referenced by id ({reference.KeyId}), which carries no key name",
                consequence: "The ViewModel receives null; reference the entry by name, or convert the id in the ViewModel where the table is known.",
                context: context);

            return null;
        }
    }
}
#endif
