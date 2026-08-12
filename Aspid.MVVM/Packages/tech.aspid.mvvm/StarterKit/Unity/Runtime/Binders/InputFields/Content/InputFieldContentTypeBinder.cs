#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;TMP_InputField, TMP_InputField.ContentType&gt;</see> that gets and sets
    /// <see cref="TMP_InputField.contentType"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-Content-1.1.0.xml" path="doc//member[@name='InputFieldContentTypeBinder']/*" />
    [Serializable]
    public class InputFieldContentTypeBinder : TargetBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <inheritdoc/>
        protected sealed override TMP_InputField.ContentType Property
        {
            get => Target.contentType;
            set
            {
                Target.contentType = value;
                Target.ForceLabelUpdate();
            }
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public InputFieldContentTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif