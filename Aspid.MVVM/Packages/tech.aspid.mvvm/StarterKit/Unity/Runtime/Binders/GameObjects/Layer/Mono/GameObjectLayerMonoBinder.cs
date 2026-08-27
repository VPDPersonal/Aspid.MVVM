using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinderWithConverter{TProperty}">MonoBinderWithConverter&lt;int&gt;</see> that binds the
    /// <see cref="GameObject.layer"/> of the object this component is attached to.
    /// </summary>
    /// <remarks>
    /// Only the object itself changes layer, not its children — the same as assigning the property by hand.
    /// An index that names no layer is logged as an error and written nowhere.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Layer")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Layer")]
    public sealed class GameObjectLayerMonoBinder : MonoBinderWithConverter<int>
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
                    Debug.LogError($"[{nameof(GameObjectLayerMonoBinder)}] Layer {value} does not exist; ignored.", context: this);
                    return;
                }

                gameObject.layer = value;
            }
        }
    }
}
