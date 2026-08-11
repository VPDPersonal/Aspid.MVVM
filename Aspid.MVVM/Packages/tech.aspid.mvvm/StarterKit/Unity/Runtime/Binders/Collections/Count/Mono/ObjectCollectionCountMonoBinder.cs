using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="CollectionCountMonoBinder{T}">CollectionCountMonoBinder&lt;object&gt;</see> that reports the
    /// size of a collection of any reference type.
    /// </summary>
    /// <remarks>
    /// The collection interfaces are covariant, so a list of any class — view models, items, strings — is accepted here
    /// without a subclass per element type. A collection of a value type, such as a list of <see langword="int"/>, is not:
    /// covariance does not apply to those, and a project that binds one writes the one-line subclass over its own element
    /// type.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Collection Binder – Count")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Collection Binder – Count")]
    public sealed partial class ObjectCollectionCountMonoBinder : CollectionCountMonoBinder<object> { }
}
