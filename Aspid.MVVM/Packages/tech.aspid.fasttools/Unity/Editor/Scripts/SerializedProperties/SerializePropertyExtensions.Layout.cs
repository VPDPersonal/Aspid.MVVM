using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static partial class SerializePropertyExtensions
    {
        /// <summary>
        /// Returns <c>true</c> when the property is a <see cref="SerializedPropertyType.Generic"/>
        /// value (a plain serializable struct/class) with visible children — i.e. draws as an
        /// expandable foldout. Single-line values return <c>false</c>.
        /// </summary>
        /// <remarks>
        /// Managed references (<see cref="SerializedPropertyType.ManagedReference"/>) also render
        /// with a foldout but are not covered by this check.
        /// </remarks>
        /// <param name="property">The property whose drawing shape is being queried.</param>
        /// <returns><c>true</c> if the property draws with a foldout arrow; otherwise <c>false</c>.</returns>
        public static bool HasFoldout(this SerializedProperty property) =>
            property.propertyType is SerializedPropertyType.Generic && property.hasVisibleChildren;
    }
}
