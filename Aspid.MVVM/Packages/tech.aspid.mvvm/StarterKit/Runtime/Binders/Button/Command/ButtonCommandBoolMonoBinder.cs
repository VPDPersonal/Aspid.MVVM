using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ButtonCommandMonoBinder{T}"/> with a <see langword="bool"/> parameter.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Button/Button Binder – Command Bool")]
    public sealed class ButtonCommandBoolMonoBinder : ButtonCommandMonoBinder<bool> { }
}
