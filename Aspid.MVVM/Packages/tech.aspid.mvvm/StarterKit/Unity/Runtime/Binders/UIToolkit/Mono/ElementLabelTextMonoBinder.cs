using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{Label}"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> and
    /// <see cref="IAnyBinder"/> that sets <see cref="TextElement.text"/>.
    /// </summary>
    /// <remarks>
    /// The UI Toolkit counterpart of the text binder, and the first thing any View needs. Accepts any bound value, not
    /// only a string: a number or an enum reaches a label the same way it reaches a TextMeshPro one.
    /// <para/>
    /// A <see langword="null"/> value is written as an empty string — a label showing the word <c>null</c> is never what
    /// was meant.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Label Text")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UIToolkit/Element Binder – Label Text")]
    public sealed partial class ElementLabelTextMonoBinder : VisualElementMonoBinder<Label>, IAnyBinder
    {
        /// <summary>
        /// Sets the label's text to <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue<T>(T value)
        {
            var element = Element;
            if (element is null) return;

            element.text = value?.ToString() ?? string.Empty;
        }
    }
}
