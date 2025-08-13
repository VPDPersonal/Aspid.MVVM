using UnityEngine;

namespace Aspid.MVVM.Unity
{
    /// <summary>
    /// Represents a base class for views in a Unity context derived from <see cref="ScriptableObject"/>.
    /// </summary>
    [View]
    public abstract partial class ScriptableView : ScriptableObject, IMonoBinderSource { }
}