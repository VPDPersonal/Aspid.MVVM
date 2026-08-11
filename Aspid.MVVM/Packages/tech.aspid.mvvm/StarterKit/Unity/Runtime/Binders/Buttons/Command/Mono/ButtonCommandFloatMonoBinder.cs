using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ButtonCommandMonoBinder{T}">ButtonCommandMonoBinder&lt;float&gt;</see> that executes a command
    /// with a <see langword="float"/> parameter set in the Inspector.
    /// </summary>
    /// <remarks>
    /// The generic base cannot be added as a component — Unity needs a concrete type — so binding a command with a
    /// constant parameter meant writing a one-line C# class per parameter type first. This is that class for
    /// float: a volume step, a zoom factor, a duration.
    /// </remarks>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command Float")]
    public sealed partial class ButtonCommandFloatMonoBinder : ButtonCommandMonoBinder<float> { }
}
