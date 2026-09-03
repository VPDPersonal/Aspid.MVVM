using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CasterMonoBinder{TFrom, TTo}"/> from <see cref="Vector2"/> to <see cref="Vector3"/>. Defaults to <see cref="Vector2Vector3Converter"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Vector3))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector2 To Vector3 Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Vector2 To Vector3 Caster Binder")]
    public sealed class Vector2ToVector3CasterMonoBinder : CasterMonoBinder<Vector2, Vector3>
    {
        /// <inheritdoc/>
        protected override IConverter<Vector2, Vector3> CreateDefaultConverter() =>
            new Vector2Vector3Converter();
    }
}
