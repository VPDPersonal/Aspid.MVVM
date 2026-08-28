#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_Text, bool}"/> that binds <see cref="TMP_Text.enableAutoSizing"/>.
    /// </summary>
    [Serializable]
    public class TextAutoSizeBinder : TargetBinder<TMP_Text, bool>
    {
        /// <inheritdoc/>
        public TextAutoSizeBinder(
            TMP_Text target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enableAutoSizing;
            set => Target.enableAutoSizing = value;
        }
    }
}
#endif
