#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Tables;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a <see cref="TableEntryReference"/> back as the string the binders exchange with the ViewModel.
    /// </summary>
    internal static class TableEntryReferences
    {
        /// <summary>
        /// Returns the entry's key name, or <see langword="null"/> with a diagnostic when the reference does not
        /// carry one.
        /// </summary>
        /// <remarks>
        /// The implicit conversion to <see cref="string"/> yields <see cref="TableEntryReference.Key"/>, which is
        /// filled only for a reference stored by name. Picking an entry through the Localization inspector stores
        /// it by <em>id</em> instead — the common case — so the conversion produced <see langword="null"/> and the
        /// binders handed that to the ViewModel in <see cref="BindMode.OneWayToSource"/> as if it were the entry.
        /// Resolving an id to its name needs the shared table data loaded, which a binder cannot assume, so the
        /// value is still <see langword="null"/>; what changes is that it now says so instead of passing silently.
        /// </remarks>
        /// <param name="reference">The reference to read.</param>
        /// <param name="owner">The binder reading it; used to name the source in the diagnostic.</param>
        public static string ToKeyName(this TableEntryReference reference, Object owner)
        {
            if (reference.ReferenceType is TableEntryReference.Type.Name) return reference.Key;
            if (reference.ReferenceType is TableEntryReference.Type.Empty) return null;

            Debug.LogError(
                $"[{(owner ? owner.GetType().Name : "Localization binder")}] The table entry is referenced by id " +
                $"({reference.KeyId}), which carries no key name, so the ViewModel receives null instead of the " +
                "entry. Reference the entry by name, or convert the id in the ViewModel where the table is known.",
                owner);

            return null;
        }
    }
}
#endif
