using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ButtonCommandMonoBinder{T}">ButtonCommandMonoBinder&lt;string&gt;</see> that executes a command
    /// with a <see langword="string"/> parameter set in the Inspector.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command String")]
    public sealed partial class ButtonCommandStringMonoBinder : ButtonCommandMonoBinder<string> { }
}
