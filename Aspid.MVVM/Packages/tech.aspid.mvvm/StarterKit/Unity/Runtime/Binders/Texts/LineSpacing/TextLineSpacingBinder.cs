#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{TMP_Text}"/> that binds <see cref="TMP_Text.lineSpacing"/>.
    /// </summary>
    /// <remarks>
    /// Non-finite values are ignored — TMP would otherwise rebuild the mesh from <see cref="float.NaN"/>
    /// and the text disappears entirely.
    /// </remarks>
    [Serializable]
    public class TextLineSpacingBinder : TargetFloatBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.lineSpacing;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.lineSpacing = value;
            }
        }

        /// <inheritdoc/>
        public TextLineSpacingBinder(
            TMP_Text target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
#endif
