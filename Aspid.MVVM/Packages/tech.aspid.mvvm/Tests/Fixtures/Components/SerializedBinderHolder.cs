using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A component holding a non-Mono binder in a <c>[SerializeReference]</c> field, the way a View does, so a test
    /// can have Unity rebuild the binder through deserialization.
    /// </summary>
    public sealed class SerializedBinderHolder : MonoBehaviour
    {
        [SerializeReference] public IBinder Binder;
    }
}
