#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Tables;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="TableEntryReference"/>.
    /// </summary>
    internal static class TableEntryReferenceExtensions
    {
        /// <summary>
        /// Returns the entry's key name, or <see langword="null"/> when the reference carries none.
        /// </summary>
        /// <remarks>
        /// A reference stored by id is reported: resolving it to a name needs table data a binder does not have.
        /// </remarks>
        /// <param name="reference">The reference to read.</param>
        /// <param name="owner">The binder reading it.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        /// <returns>The key name; <see langword="null"/> for an empty reference or one stored by id.</returns>
        public static string ToKeyName(this TableEntryReference reference, IBinder owner, Object context = null)
        {
            if (reference.ReferenceType is TableEntryReference.Type.Name) return reference.Key;
            if (reference.ReferenceType is TableEntryReference.Type.Empty) return null;

            owner.LogError(
                problem: $"the table entry is referenced by id ({reference.KeyId}), which carries no key name",
                consequence: "The ViewModel receives null.",
                context: context);

            return null;
        }
    }
}
#endif
