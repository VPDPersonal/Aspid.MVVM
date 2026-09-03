using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TMP_InputField}"/> that sends the cached <see cref="TMP_InputField"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField To Source Binder")]
    public sealed class InputFieldToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_InputField> { }
}