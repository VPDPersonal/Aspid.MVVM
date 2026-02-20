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