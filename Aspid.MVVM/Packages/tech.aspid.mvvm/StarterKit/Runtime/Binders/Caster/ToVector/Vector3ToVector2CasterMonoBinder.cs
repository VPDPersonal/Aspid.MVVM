using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CasterMonoBinder{TFrom, TTo}"/> from <see cref="Vector3"/> to <see cref="Vector2"/>. Defaults to <see cref="Vector2Vector3Converter"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Vector2))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector3 To Vector2 Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Vector3 To Vector2 Caster Binder")]
    public sealed class Vector3ToVector2CasterMonoBinder : CasterMonoBinder<Vector3, Vector2>
    {
        /// <inheritdoc/>
        protected override IConverter<Vector3, Vector2> CreateDefaultConverter() =>
            new Vector2Vector3Converter();
    }
}
