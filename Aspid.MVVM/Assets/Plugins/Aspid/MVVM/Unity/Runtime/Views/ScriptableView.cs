using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a base class for views in a Unity context derived from <see cref="ScriptableObject"/>.
    /// </summary>
    [View]
    public abstract partial class ScriptableView : ScriptableObject { }
}