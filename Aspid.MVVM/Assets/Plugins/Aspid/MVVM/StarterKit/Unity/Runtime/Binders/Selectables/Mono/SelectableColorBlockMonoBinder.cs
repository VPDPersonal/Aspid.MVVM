using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Selectable))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock")]
    public class SelectableColorBlockMonoBinder : ComponentMonoBinder<Selectable, ColorBlock, Converter>
    {
        protected sealed override ColorBlock Property
        {
            get => CachedComponent.colors;
            set => CachedComponent.colors = value;
        }
    }
}