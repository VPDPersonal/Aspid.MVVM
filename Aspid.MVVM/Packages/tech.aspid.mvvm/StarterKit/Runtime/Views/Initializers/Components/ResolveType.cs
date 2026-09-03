// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Where an <see cref="InitializeComponent{T}"/> takes its instance from.
    /// </summary>
    public enum ResolveType
    {
        /// <summary>A <see cref="UnityEngine.Component"/> reference.</summary>
        Component,

        /// <summary>A serialized plain C# instance.</summary>
        Reference,

        /// <summary>A <see cref="UnityEngine.ScriptableObject"/> reference.</summary>
        ScriptableObject,
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION

        /// <summary>Resolved from the DI container by type name.</summary>
        Di,
#endif
    }
}
