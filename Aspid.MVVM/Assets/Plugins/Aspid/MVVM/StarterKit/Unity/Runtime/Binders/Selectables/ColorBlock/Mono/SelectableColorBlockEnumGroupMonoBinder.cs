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
    /// <see cref="EnumGroupMonoBinder{Selectable, ColorBlock, Converter}"/> that sets the <see cref="Selectable.colors"/>
    /// property on each <see cref="Selectable"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock EnumGroup")]
    public sealed class SelectableColorBlockEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, ColorBlock, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(Selectable element, ColorBlock value) =>
            element.colors = value;
    }
}