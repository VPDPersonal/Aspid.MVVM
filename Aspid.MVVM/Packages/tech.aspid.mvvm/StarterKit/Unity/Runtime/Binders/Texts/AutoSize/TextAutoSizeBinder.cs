#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{TMP_Text}"/> that binds <see cref="TMP_Text.enableAutoSizing"/>.
    /// </summary>
    /// <remarks>
    /// Whether the text shrinks to fit its box. Turning it on is how a name of unknown length stops
    /// overflowing, and turning it off is how a number stops jumping in size between frames — both are
    /// decisions a ViewModel makes about the value it is showing.
    /// </remarks>
    [Serializable]
    public class TextAutoSizeBinder : TargetBoolBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.enableAutoSizing;
            set => Target.enableAutoSizing = value;
        }

        /// <inheritdoc/>
        public TextAutoSizeBinder(
            TMP_Text target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
#endif
