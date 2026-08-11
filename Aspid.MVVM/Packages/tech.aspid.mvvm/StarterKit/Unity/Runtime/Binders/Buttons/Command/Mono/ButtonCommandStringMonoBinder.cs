using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ButtonCommandMonoBinder{T}">ButtonCommandMonoBinder&lt;string&gt;</see> that executes a command
    /// with a <see langword="string"/> parameter set in the Inspector.
    /// </summary>
    /// <remarks>
    /// The generic base cannot be added as a component — Unity needs a concrete type — so binding a command with a
    /// constant parameter meant writing a one-line C# class per parameter type first. This is that class for
    /// string: a scene name, an item id, a URL.
    /// </remarks>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command String")]
    public sealed partial class ButtonCommandStringMonoBinder : ButtonCommandMonoBinder<string> { }
}
