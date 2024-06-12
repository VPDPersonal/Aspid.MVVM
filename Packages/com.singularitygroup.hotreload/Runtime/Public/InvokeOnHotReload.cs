using System;

namespace SingularityGroup.HotReload {
    /// <summary>
    /// Methods with this attribute will get invoked after a hot reload
    /// </summary>
    /// <remarks>
    /// The method with this attribute needs to have no parameters.
    /// Further more it needs to either be static or and instance method inside a <see cref="UnityEngine.MonoBehaviour"/>.
    /// For the latter case the method of all instances of the <see cref="UnityEngine.MonoBehaviour"/> will be called.
    /// In case the method has a return value it will be ignored.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class InvokeOnHotReload : Attribute {
    }

}
