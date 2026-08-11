#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;TMP_Text, FontStyles&gt;</see> that binds
    /// <see cref="TMP_Text.fontStyle"/>.
    /// </summary>
    /// <remarks>
    /// Bold, italic, underline, strikethrough — the flags a rules panel, a diff view or a chat log turns on per
    /// line. <see cref="FontStyles"/> is a flag enum, so the ViewModel sends the whole set: one style is a
    /// combination, not a binder of its own.
    /// </remarks>
    [Serializable]
    public class TextFontStyleBinder : TargetBinder<TMP_Text, FontStyles>
    {
        /// <inheritdoc/>
        protected sealed override FontStyles Property
        {
            get => Target.fontStyle;
            set => Target.fontStyle = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public TextFontStyleBinder(TMP_Text target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif
