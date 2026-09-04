using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ButtonCommandMonoBinder{T}"/> with an <see langword="int"/> parameter.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Button/Button Binder – Command Int")]
    public sealed class ButtonCommandIntMonoBinder : ButtonCommandMonoBinder<int> { }
}
