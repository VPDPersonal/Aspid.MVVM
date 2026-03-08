using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property
    /// on a <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing")]
    public class HorizontalOrVerticalLayoutSpacingMonoBinder : ComponentFloatMonoBinder<HorizontalOrVerticalLayoutGroup>, INumberBinder
    {
        protected sealed override float Property
        {
            get => CachedComponent.spacing;
            set => CachedComponent.spacing = value;
        }
    }
}