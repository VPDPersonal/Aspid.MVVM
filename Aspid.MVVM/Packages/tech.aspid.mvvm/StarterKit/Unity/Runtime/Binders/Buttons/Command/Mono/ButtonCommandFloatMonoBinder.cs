using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ButtonCommandMonoBinder{T}">ButtonCommandMonoBinder&lt;float&gt;</see> that executes a command
    /// with a <see langword="float"/> parameter set in the Inspector.
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command Float")]
    public sealed partial class ButtonCommandFloatMonoBinder : ButtonCommandMonoBinder<float> { }
}
