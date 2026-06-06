#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField, TMP_InputField.ContentType}"/> that gets and sets
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
        public InputFieldContentTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif