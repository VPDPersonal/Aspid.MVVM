using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds
    /// <see cref="RectTransform.anchoredPosition"/> or <see cref="RectTransform.anchoredPosition3D"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchoredPosition")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition")]
    public class RectTransformAnchoredPositionMonoBinder : ComponentMonoBinder<RectTransform, Vector3>,
        IVector3Binder
    {
        [Tooltip("Self: anchoredPosition, World: anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetAnchoredPosition(_space);
            set => CachedComponent.SetAnchoredPosition(value, _space);
        }
    }
}
