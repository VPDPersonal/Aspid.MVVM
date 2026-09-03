// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which <see cref="TMPro.TMP_InputField"/> event a binder listens to.
    /// </summary>
    public enum UpdateInputFieldEvent
    {
        /// <summary>
        /// <see cref="TMPro.TMP_InputField.onValueChanged"/>: every text change.
        /// </summary>
        OnValueChanged,

        /// <summary>
        /// <see cref="TMPro.TMP_InputField.onEndEdit"/>: editing finished.
        /// </summary>
        OnEndEdit,

        /// <summary>
        /// <see cref="TMPro.TMP_InputField.onSubmit"/>: Submit pressed.
        /// </summary>
        OnSubmit,

        /// <summary>
        /// <see cref="TMPro.TMP_InputField.onSelect"/>: the field gained focus.
        /// </summary>
        OnSelect,

        /// <summary>
        /// <see cref="TMPro.TMP_InputField.onDeselect"/>: the field lost focus.
        /// </summary>
        OnDeselect,
    }
}
