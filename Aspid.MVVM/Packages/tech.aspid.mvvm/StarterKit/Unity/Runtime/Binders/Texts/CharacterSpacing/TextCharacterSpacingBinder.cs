#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{TMP_Text}"/> that binds <see cref="TMP_Text.characterSpacing"/>.
    /// </summary>
    /// <remarks>
    /// Tracking, in font units. A negative value tightens the line, which is ordinary in a title, so only a
    /// non-finite value is refused — TMP rebuilds the mesh from it and the text disappears entirely.
    /// </remarks>
    [Serializable]
    public class TextCharacterSpacingBinder : TargetFloatBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.characterSpacing;
            set
            {
                // Отрицательный трекинг — обычное дело: так поджимают заголовки. Отбрасывается только нефинитное,
                // иначе TMP пересобирает меш с NaN и текст исчезает целиком.
                if (!BinderMath.IsFinite(value)) return;
                Target.characterSpacing = value;
            }
        }

        /// <inheritdoc/>
        public TextCharacterSpacingBinder(
            TMP_Text target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
#endif
