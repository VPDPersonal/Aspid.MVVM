using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="TMP_Text.text"/>, also from
    /// numbers.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text")]
    public partial class TextMonoBinder : ComponentMonoBinder<TMP_Text, string>, INumberBinder
    {
        [Tooltip("Culture numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.text;
            set => CachedComponent.text = value;
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
    }
}
