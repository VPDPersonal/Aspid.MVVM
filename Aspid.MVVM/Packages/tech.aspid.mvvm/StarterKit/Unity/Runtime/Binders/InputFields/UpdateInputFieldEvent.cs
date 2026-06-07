// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which <see cref="TMPro.TMP_InputField"/> event triggers command execution or ViewModel notifications.
    /// </summary>
    public enum UpdateInputFieldEvent
    {
        /// <summary>Triggered whenever the input field text value changes.</summary>
        OnValueChanged,
        /// <summary>Triggered when the user finishes editing the input field.</summary>
        OnEndEdit,
        /// <summary>Triggered when the user presses Submit.</summary>
        OnSubmit,
        /// <summary>Triggered when the input field gains focus.</summary>
        OnSelect,
        /// <summary>Triggered when the input field loses focus.</summary>
        OnDeselect,
    }
}