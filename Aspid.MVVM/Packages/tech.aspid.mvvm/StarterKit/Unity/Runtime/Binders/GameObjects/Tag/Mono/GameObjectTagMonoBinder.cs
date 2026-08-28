using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}">MonoBinder&lt;string&gt;</see> that binds the <see cref="GameObject.tag"/> of the
    /// <see cref="GameObject"/> this component is attached to.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in the Tags and Layers settings.
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
