#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{TMP_Text}"/> that binds <see cref="TMP_Text.maxVisibleCharacters"/>.
    /// </summary>
    /// <remarks>
    /// The default is large enough to mean "all of them"; <c>0</c> hides the text without clearing it.
    /// </remarks>
    [Serializable]
    public class TextMaxVisibleCharactersBinder : TargetIntBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.maxVisibleCharacters;
            set => Target.maxVisibleCharacters = value;
        }

        /// <inheritdoc/>
        public TextMaxVisibleCharactersBinder(
            TMP_Text target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
#endif
