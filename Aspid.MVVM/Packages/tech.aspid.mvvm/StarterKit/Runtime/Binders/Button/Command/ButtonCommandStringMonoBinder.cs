using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ButtonCommandMonoBinder{T}"/> with a <see langword="string"/> parameter.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Button/Button Binder – Command String")]
    public sealed class ButtonCommandStringMonoBinder : ButtonCommandMonoBinder<string> { }
}
