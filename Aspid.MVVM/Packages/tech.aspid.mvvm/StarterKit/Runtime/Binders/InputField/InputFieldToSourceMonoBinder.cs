using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="TMP_InputField"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField To Source Binder")]
    public sealed class InputFieldToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_InputField> { }
}
