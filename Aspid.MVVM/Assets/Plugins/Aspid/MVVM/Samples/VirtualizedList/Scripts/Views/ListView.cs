using UnityEngine;
using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [View]
    public sealed partial class ListView : MonoView
    {
        [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
        [SerializeField] private MonoBinder[] _items;
        
        [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
        [SerializeField] private MonoBinder[] _isOnTrueItems;
    }
}
