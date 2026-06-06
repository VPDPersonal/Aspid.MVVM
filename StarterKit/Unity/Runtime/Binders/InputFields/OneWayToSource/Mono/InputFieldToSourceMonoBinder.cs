#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TMP_InputField}"/> that sends the current bound property value
    /// of a <see cref="TMP_InputField"/> back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField To Source Binder")]
    public sealed class InputFieldToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_InputField> { }
}
#endif