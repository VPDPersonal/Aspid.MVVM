using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds
    /// <see cref="TMP_InputField.placeholder"/>.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> leaves the field without a placeholder.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Placeholder")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Placeholder")]
    public class InputFieldPlaceholderMonoBinder : ComponentObjectMonoBinder<TMP_InputField, Graphic>
    {
        /// <inheritdoc/>
        protected sealed override Graphic Property
        {
            get => CachedComponent.placeholder;
            set => CachedComponent.placeholder = value;
        }
    }
}
