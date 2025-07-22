using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;

namespace Aspid.MVVM.SamplesVirtualizedList
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
