using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CasterMonoBinder{TFrom, TTo}"/> from <see cref="string"/> to <see langword="int"/>. Defaults to <see cref="StringToIntConverter"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(int))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Int Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Int Caster Binder")]
    public sealed class StringToIntCasterMonoBinder : CasterMonoBinder<string, int>
    {
        /// <inheritdoc/>
        protected override IConverter<string, int> CreateDefaultConverter() =>
            new StringToIntConverter();
    }
}
