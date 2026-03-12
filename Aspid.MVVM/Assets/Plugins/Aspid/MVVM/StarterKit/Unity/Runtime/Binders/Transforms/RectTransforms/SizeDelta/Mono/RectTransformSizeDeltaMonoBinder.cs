using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.sizeDelta"/>
    /// according to the configured <see cref="SizeDeltaMode"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="RectTransform.sizeDelta"/> is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta")]
    public class RectTransformSizeDeltaMonoBinder : ComponentVector3MonoBinder<RectTransform>
    {
        [Tooltip("Determines which axes of sizeDelta are modified: Width only, Height only, or both (SizeDelta).")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.sizeDelta;
            set => CachedComponent.SetSizeDelta(value, _sizeMode);
        }
    }
}