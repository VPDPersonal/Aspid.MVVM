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
    /// <remarks>
    /// The generic base cannot be added as a component — Unity needs a concrete type — so binding a command with a
    /// constant parameter meant writing a one-line C# class per parameter type first. This is that class for
    /// Object: a prefab to spawn, an asset to apply, a target to act on.
    /// </remarks>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command Object")]
    public sealed partial class ButtonCommandObjectMonoBinder : ButtonCommandMonoBinder<Object> { }
}
