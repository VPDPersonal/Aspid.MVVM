// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// A parent → child edge inside a document's nested graph: the child <c>RefIds</c> id and the field path that
    /// holds it <i>relative to the parent's data block</i>, with list elements indexed (e.g. <c>_chargeEffect</c> or
    /// <c>_slots[0].weapon</c>). The view joins this onto the parent's full path so a nested reference shows where it
    /// lives from the document root down. A null child slot is kept as an <see cref="IsEmpty"/> edge (the rid is a null
    /// sentinel) so a cleared nested field is visible too; it points at no node and never recurses.
    /// </summary>
    internal readonly struct ReferenceGraphEdge
    {
        public readonly long Rid;
        public readonly string Label;

        public ReferenceGraphEdge(long rid, string label)
        {
            Rid = rid;
            Label = label;
        }

        /// <summary>
        /// True when the child pointer is a null sentinel (rid &lt; 0) — an unassigned nested slot.
        /// </summary>
        public bool IsEmpty => Rid < 0;
    }
}
