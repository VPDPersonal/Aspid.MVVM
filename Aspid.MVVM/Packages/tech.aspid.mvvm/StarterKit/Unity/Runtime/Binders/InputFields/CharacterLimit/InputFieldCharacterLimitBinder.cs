#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.characterLimit"/>.
    /// </summary>
    /// <remarks>
    /// How many characters the field accepts, where <c>0</c> means no limit. Nothing is sanitised: Unity maps a
    /// negative limit to <c>0</c> itself. Lowering the limit does not shorten text that is already in the field —
    /// it only constrains what can be typed next, which is worth knowing before binding it to a value that
    /// shrinks.
    /// </remarks>
    [Serializable]
    public class InputFieldCharacterLimitBinder : TargetIntBinder<TMP_InputField>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.characterLimit;
            set => Target.characterLimit = value;
        }

        /// <inheritdoc/>
        public InputFieldCharacterLimitBinder(
            TMP_InputField target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
#endif
