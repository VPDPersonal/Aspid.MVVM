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
    /// <summary>
    /// MonoBehaviour binder that switches the <see cref="Selectable.colors"/> property on a <see cref="Selectable"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock Switcher")]
    public sealed class SelectableColorBlockSwitcherMonoBinder : SwitcherMonoBinder<Selectable, ColorBlock, Converter>
    {
        protected override void SetValue(ColorBlock value) =>
            CachedComponent.colors = value;
    }
}