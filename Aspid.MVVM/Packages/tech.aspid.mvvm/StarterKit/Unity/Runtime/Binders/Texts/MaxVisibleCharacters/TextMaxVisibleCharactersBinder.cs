#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder<TMP_Text>"/> that binds <see cref="TMP_Text.maxVisibleCharacters"/>.
    /// </summary>
    /// <remarks>
    /// How many characters of the text are drawn — the property behind a typewriter reveal, and the one way to
    /// animate text without rebuilding the string on every frame. It had no binder, so the usual workaround was to
    /// bind a growing substring instead, which reflows the layout on every character. The default is a number
    /// large enough to mean "all of them"; <c>0</c> hides the text without clearing it.
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
