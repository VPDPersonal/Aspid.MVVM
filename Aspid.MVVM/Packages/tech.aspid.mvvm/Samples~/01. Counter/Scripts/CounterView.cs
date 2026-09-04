using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Counter
{
    // [View] makes the source generator bind every serialized binder to the ViewModel member with the same name.
    [View]
    public sealed partial class CounterView : MonoView
    {
        // RequireBinder limits the Inspector to binders that accept the given type.
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _count;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _incrementCommand;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _decrementCommand;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _resetCommand;
    }
}
