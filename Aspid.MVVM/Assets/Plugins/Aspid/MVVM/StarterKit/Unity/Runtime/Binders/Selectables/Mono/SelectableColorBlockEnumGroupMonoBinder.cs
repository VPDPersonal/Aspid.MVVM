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
    /// MonoBehaviour binder that sets the <see cref="Selectable.colors"/> property on a group of <see cref="Selectable"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock EnumGroup")]
    public sealed class SelectableColorBlockEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, ColorBlock, Converter>
    {
        protected override void SetValue(Selectable element, ColorBlock value) =>
            element.colors = value;
    }
}