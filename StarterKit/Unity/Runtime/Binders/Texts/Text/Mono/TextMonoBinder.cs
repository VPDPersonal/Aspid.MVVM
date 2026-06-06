#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentStringMonoBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.text"/> property.
    /// Also implements <see cref="INumberBinder"/>, allowing numeric values to be formatted and bound as text.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current text
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class TextMonoBinder : ComponentStringMonoBinder<TMP_Text>, INumberBinder
    {
        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.text;
            set => CachedComponent.text = value;
        }

        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
    }
}
#endif