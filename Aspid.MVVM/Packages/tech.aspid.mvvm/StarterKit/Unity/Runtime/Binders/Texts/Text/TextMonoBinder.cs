using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that sets the <see cref="TMP_Text.text"/> property.
    /// Also implements <see cref="INumberBinder"/>, allowing numeric values to be formatted and bound as text.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class TextMonoBinder : ComponentMonoBinder<TMP_Text, string>, INumberBinder
    {
        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.text;
            set => CachedComponent.text = value;
        }

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
    }
}