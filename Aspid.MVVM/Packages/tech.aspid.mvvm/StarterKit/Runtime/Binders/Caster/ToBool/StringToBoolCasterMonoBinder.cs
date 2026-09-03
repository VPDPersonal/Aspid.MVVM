using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CasterMonoBinder{TFrom, TTo}"/> from <see cref="string"/> to <see langword="bool"/>. Defaults to <see cref="StringEmptyToBoolConverter"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Bool Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Bool Caster Binder")]
    public sealed class StringToBoolCasterMonoBinder : CasterMonoBinder<string, bool>
    {
        /// <inheritdoc/>
        protected override IConverter<string, bool> CreateDefaultConverter() =>
            new StringEmptyToBoolConverter();
    }
}
