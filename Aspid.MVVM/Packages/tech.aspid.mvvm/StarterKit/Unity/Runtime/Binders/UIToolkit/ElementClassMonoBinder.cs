using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{VisualElement}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> that adds and removes one USS class.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Class")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementClassMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
    {
        [Tooltip("The USS class toggled by the bound value.")]
        [SerializeField] private string _class;

        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <summary>
        /// Adds the class when <paramref name="value"/> is <see langword="true"/>, and removes it otherwise.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// Logs an error and does nothing when no class name is set: an empty class name would silently do nothing on
        /// every value.
        /// </remarks>
        [BinderLog]
        public void SetValue(bool value)
        {
            var element = Element;
            if (element is null) return;

            if (string.IsNullOrWhiteSpace(_class))
            {
                this.LogError("no USS class is set", "The element is left unchanged.");
                return;
            }

            element.EnableInClassList(_class, _converter?.Convert(value) ?? value);
        }
    }
}
