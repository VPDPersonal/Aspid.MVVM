using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}"/> that binds <see cref="GameObject.tag"/> of the object it is attached to.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in Tags and Layers.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Tag")]
    public sealed class GameObjectTagMonoBinder : MonoBinder<string>
    {
        /// <inheritdoc/>
        protected override string Property
        {
            get => gameObject.tag;
            set => gameObject.tag = value;
        }
    }
}
