using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="TMP_Text"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text To Source Binder")]
    public sealed class TextToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_Text> { }
}
