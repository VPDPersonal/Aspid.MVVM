using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}">MonoBinder&lt;int&gt;</see> that binds the
    /// <see cref="GameObject.layer"/> of the object this component is attached to.
    /// </summary>
    /// <remarks>
    /// Only the object itself changes layer, not its children — the same as assigning the property by hand.
    /// An index that names no layer is logged as an error and written nowhere.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Layer")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Layer")]
    public sealed class GameObjectLayerMonoBinder : MonoBinder<int>
    {
        private const int MaxLayer = 31;

        /// <inheritdoc/>
        protected override int Property
        {
            get => gameObject.layer;
            set
            {
                if (value is < 0 or > MaxLayer)
                {
                    this.LogError($"the layer {value} does not exist", "The layer is left unchanged.");
                    return;
                }

                gameObject.layer = value;
            }
        }
    }
}
