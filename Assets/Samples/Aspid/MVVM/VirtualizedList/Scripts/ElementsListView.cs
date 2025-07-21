using System.Collections.Generic;
using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [View]
    public partial class ElementsListView : MonoView
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
        [SerializeField] private MonoBinder[] _items;
        
        [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
        [SerializeField] private MonoBinder[] _isOnTrueItems;
       
        [RequireBinder(typeof(IReadOnlyList<IViewModel>))]
        [SerializeField] private MonoBinder[] _isOnFalseItems;
    }
}
