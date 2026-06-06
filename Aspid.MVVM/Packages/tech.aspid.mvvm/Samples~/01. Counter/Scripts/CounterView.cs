using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Counter
{
    [View]
    public sealed partial class CounterView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _count;
    
        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder _incrementCommand;
    }
}