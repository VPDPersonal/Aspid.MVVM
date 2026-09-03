using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CasterMonoBinder{TFrom, TTo}"/> from <see cref="string"/> to <see langword="float"/>. Defaults to <see cref="StringToFloatConverter"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Float Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Float Caster Binder")]
    public sealed class StringToFloatCasterMonoBinder : CasterMonoBinder<string, float>
    {
        /// <inheritdoc/>
        protected override IConverter<string, float> CreateDefaultConverter() =>
            new StringToFloatConverter();
    }
}
