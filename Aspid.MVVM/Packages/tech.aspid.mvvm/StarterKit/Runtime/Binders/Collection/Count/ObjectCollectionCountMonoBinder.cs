using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CollectionCountMonoBinder{T}">CollectionCountMonoBinder&lt;object&gt;</see> for a collection of any reference type.
    /// </summary>
    /// <remarks>
    /// Covariance makes a list of any class bindable here. A list of a value type is not: close <see cref="CollectionCountMonoBinder{T}"/> over it instead.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Collection Binder – Count")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Collection Binder – Count")]
    public sealed class ObjectCollectionCountMonoBinder : CollectionCountMonoBinder<object> { }
}
