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
    /// <see cref="EnumMonoBinder{Selectable, ColorBlock, Converter}"/> that sets the <see cref="Selectable.colors"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock Enum")]
    public sealed class SelectableColorBlockEnumMonoBinder : EnumMonoBinder<Selectable, ColorBlock, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            CachedComponent.colors = value;
    }
}