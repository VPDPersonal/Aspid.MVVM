using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ButtonCommandMonoBinder{T}">ButtonCommandMonoBinder&lt;int&gt;</see> that executes a command
    /// with an <see langword="int"/> parameter set in the Inspector.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command Int")]
    public sealed partial class ButtonCommandIntMonoBinder : ButtonCommandMonoBinder<int> { }
}
