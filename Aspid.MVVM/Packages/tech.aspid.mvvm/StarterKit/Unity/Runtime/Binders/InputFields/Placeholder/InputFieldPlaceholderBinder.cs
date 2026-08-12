#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetObjectBinder{T1, T2}">TargetObjectBinder&lt;TMP_InputField, Graphic&gt;</see> that binds
    /// <see cref="TMP_InputField.placeholder"/>.
    /// </summary>
    /// <remarks>
    /// The graphic shown while the field is empty. Swapping it is how a hint changes with the mode the form is in —
    /// "search by name" against "search by id" — without a second field.
    /// <para/>
    /// Unity does not enable or disable the graphic itself here; the field shows and hides whichever graphic it is
    /// given. A destroyed one arrives as <see langword="null"/>, which leaves the field with no placeholder rather
    /// than with a reference that throws on the next keystroke.
    /// </remarks>
    [Serializable]
    public class InputFieldPlaceholderBinder : TargetObjectBinder<TMP_InputField, Graphic>
    {
        /// <inheritdoc/>
        protected sealed override Graphic? Property
        {
            get => Target.placeholder;
            set => Target.placeholder = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public InputFieldPlaceholderBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif
