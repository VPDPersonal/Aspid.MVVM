using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}"/> that binds <see cref="GameObject.layer"/> of the object it is attached to.
    /// </summary>
    /// <remarks>
    /// An index that names no layer is reported and not written; children keep their layer.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Layer")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Layer")]
    public sealed class GameObjectLayerMonoBinder : MonoBinder<int>
    {
        /// <inheritdoc/>
        protected override int Property
        {
            get => gameObject.layer;
            set => gameObject.SetLayer(value, this);
        }
    }
}
