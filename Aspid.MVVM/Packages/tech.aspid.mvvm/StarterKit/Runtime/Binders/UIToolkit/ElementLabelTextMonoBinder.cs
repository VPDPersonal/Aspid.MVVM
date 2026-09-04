using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that binds <see cref="TextElement.text"/> of a
    /// <see cref="Label"/> from any value.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> is written as an empty string.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Label Text")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UIToolkit/Element Binder – Label Text")]
    public sealed partial class ElementLabelTextMonoBinder : VisualElementMonoBinder<Label>, IAnyBinder
    {
        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value)
        {
            var element = Element;
            if (element is not null)
                element.text = value?.ToString() ?? string.Empty;
        }
    }
}
