#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetStringBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.text"/> property.
    /// Also implements <see cref="INumberBinder"/>, allowing numeric values to be formatted and bound as text.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Text-1.1.0.xml" path="doc//member[@name='TextBinder']/*" />
    [Serializable]
    public class TextBinder : TargetStringBinder<TMP_Text>, INumberBinder
    {
        /// <inheritdoc/>
        protected sealed override string? Property
        {
            get => Target.text;
            set => Target.text = value;
        }

        [Tooltip("Determines the culture used when converting numeric values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        /// <inheritdoc/>
        public TextBinder(TMP_Text target, IConverter<string?, string?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Formats the value using the configured <see cref="CultureInfoMode"/> and sets <see cref="TMP_Text.text"/>.
        /// </summary>
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
    }
}
#endif