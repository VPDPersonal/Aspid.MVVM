using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.Collections.Observable;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [View]
    public partial class ElementsListView : MonoView
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        [RequireBinder(typeof(IReadOnlyObservableList<IViewModel>))]
        [SerializeField] private MonoBinder[] _isOnTrueItems;
       
        [RequireBinder(typeof(IReadOnlyObservableList<IViewModel>))]
        [SerializeField] private MonoBinder[] _isOnFalseItems;
    }
}
