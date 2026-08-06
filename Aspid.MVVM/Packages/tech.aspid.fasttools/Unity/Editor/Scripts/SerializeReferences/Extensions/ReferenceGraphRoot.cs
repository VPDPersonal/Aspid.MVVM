// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// A field pointer from a document's body into its <c>RefIds</c> block — a root of the reference tree. The
    /// <see cref="Label"/> is the full field path that holds the reference (best effort), with list elements indexed,
    /// e.g. <c>_weapon</c> or <c>_alternates[0]</c> or <c>_config._slots[2]</c>. A field that holds nothing (its
    /// pointer is Unity's null sentinel) is kept as an <see cref="IsEmpty"/> root so an unassigned / cleared slot
    /// stays visible in the graph rather than silently dropping out — it has no <c>RefIds</c> node behind it.
    /// </summary>
    internal readonly struct ReferenceGraphRoot
    {
        public readonly long Rid;
        public readonly string Label;

        public ReferenceGraphRoot(long rid, string label)
        {
            Rid = rid;
            Label = label;
        }

        /// <summary>
        /// True when the pointer is a null sentinel (rid &lt; 0) — an unassigned [SerializeReference] slot.
        /// </summary>
        public bool IsEmpty => Rid < 0;
    }
}
