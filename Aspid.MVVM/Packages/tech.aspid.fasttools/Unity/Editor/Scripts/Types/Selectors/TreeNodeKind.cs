// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// The role a <see cref="TreeNode"/> plays in the rendered list, used to style and gate
    /// interaction (section headers are not selectable and never show a star toggle).
    /// </summary>
    internal enum TreeNodeKind
    {
        /// <summary>
        /// A regular hierarchy node (type leaf, namespace or category container).
        /// </summary>
        Default,

        /// <summary>
        /// A non-selectable header that introduces the Favorites or Recents section.
        /// </summary>
        SectionTitle,
    }
}
