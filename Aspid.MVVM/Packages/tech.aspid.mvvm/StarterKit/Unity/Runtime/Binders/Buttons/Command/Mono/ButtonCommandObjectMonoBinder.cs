using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ButtonCommandMonoBinder{T}">ButtonCommandMonoBinder&lt;Object&gt;</see> that executes a command
    /// with a <see cref="Object">UnityEngine.Object</see> parameter set in the Inspector.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command Object")]
    public sealed partial class ButtonCommandObjectMonoBinder : ButtonCommandMonoBinder<Object> { }
}
