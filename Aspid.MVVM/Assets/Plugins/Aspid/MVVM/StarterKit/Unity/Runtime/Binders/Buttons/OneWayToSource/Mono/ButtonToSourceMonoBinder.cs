using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{Button}"/> that sends the cached <see cref="Button"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(Button))]
    [AddComponentMenu("Aspid/MVVM/Binders/Button/Button To Source Binder")]
    public sealed class ButtonToSourceMonoBinder : ComponentToSourceMonoBinder<Button> { }
}