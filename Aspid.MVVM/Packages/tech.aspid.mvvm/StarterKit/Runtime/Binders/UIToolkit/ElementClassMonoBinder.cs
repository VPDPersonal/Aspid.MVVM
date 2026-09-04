using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that adds or removes one USS class by the bound
    /// <see langword="bool"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Class")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementClassMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
    {
        [Tooltip("USS class toggled by the value.")]
        [SerializeField] private string _class;

        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(bool value)
        {
            var element = Element;
            if (element is null) return;

            if (string.IsNullOrWhiteSpace(_class))
            {
                this.LogError(
                    problem: "no USS class is set",
                    consequence: "The element is left unchanged.");

                return;
            }

            element.EnableInClassList(_class, _converter?.Convert(value) ?? value);
        }
    }
}
