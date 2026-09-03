using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="RectTransform.pivot"/>.
    /// </summary>
    /// <remarks>
    /// Values outside 0..1 are legal; only a non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_Pivot")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – Pivot")]
    public class RectTransformPivotMonoBinder : ComponentMonoBinder<RectTransform, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.pivot;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.pivot = value;
            }
        }
    }
}
