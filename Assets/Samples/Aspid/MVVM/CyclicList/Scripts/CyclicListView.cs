using Aspid.Collections.Observable;
using Aspid.MVVM;
using Aspid.MVVM.Unity;
using UnityEngine;

namespace Samples.Aspid.MVVM.CyclicList
{
    [View]
    public partial class CyclicListView : MonoView
    {
        [RequireBinder(typeof(IReadOnlyObservableList<IViewModel>))]
        [SerializeField] private MonoBinder[] _items;
    }
}
