using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TMP_Text}"/> that sends the cached <see cref="TMP_Text"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text To Source Binder")]
    public sealed class TextToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_Text> { }
}