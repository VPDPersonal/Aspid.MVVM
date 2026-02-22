using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    /// <summary>
    /// Editor-only attribute that registers a <see cref="MonoBinder"/> class in the "Add Binder" context menu
    /// based solely on the target component type. Unlike <see cref="AddBinderContextMenuAttribute"/>,
    /// this attribute does not support property auto-population or custom menu paths.
    /// Can be applied multiple times to associate a binder with several component types.
    /// </summary>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AddBinderContextMenuByTypeAttribute : Attribute
    {
        /// <summary>
        /// The component type this binder entry is associated with.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Initializes the attribute for the specified component type.
        /// </summary>
        /// <param name="componentType">The component type whose context menu should include this binder.</param>
        public AddBinderContextMenuByTypeAttribute(Type componentType)
        {
            Type = componentType;
        }
    }
}