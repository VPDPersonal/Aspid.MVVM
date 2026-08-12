using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{RectMask2D}"/> that binds <see cref="RectMask2D.padding"/>.
    /// </summary>
    /// <remarks>
    /// How far the mask is inset on each side — what a safe area, a notch or a sidebar that slides in changes at
    /// runtime. The component had no binder at all.
    /// <para/>
    /// The property is a <see cref="Vector4"/> and the bound value is a <see cref="Vector3"/>: the fourth side
    /// keeps whatever it had, so binding three sides does not silently zero the fourth. A non-finite component is
    /// refused — the mask's rect is computed from these numbers.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectMask2D), serializePropertyNames: "m_Padding")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Mask/RectMask2D Binder – Padding")]
    public class RectMask2DPaddingMonoBinder : ComponentVector3MonoBinder<RectMask2D>
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.padding;
            set
            {
                // Vector4 у RectMask2D: биндер принимает Vector3 и Vector2 через базу, W остаётся прежним,
                // иначе привязка одной стороны обнуляла бы четвёртую.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y) || !BinderMath.IsFinite(value.z)) return;
                var padding = CachedComponent.padding;
                CachedComponent.padding = new Vector4(value.x, value.y, value.z, padding.w);
            }
        }
    }
}
