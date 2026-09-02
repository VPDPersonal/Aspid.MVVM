#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;TMP_InputField, Graphic&gt;</see> that binds
    /// <see cref="TMP_InputField.placeholder"/>.
    /// </summary>
    /// <remarks>
    /// Unity does not enable or disable the graphic itself; the field shows and hides whichever graphic it is given.
    /// A destroyed graphic arrives as <see langword="null"/>, leaving the field with no placeholder rather than a
    /// reference that throws on the next keystroke.
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
#endif
