using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TComponent}"/> for <see cref="Button"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Button))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Button/Button To Source Binder")]
    public sealed class ButtonToSourceMonoBinder : ComponentToSourceMonoBinder<Button> { }
}
