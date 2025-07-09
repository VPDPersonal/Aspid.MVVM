using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;

namespace Samples.Aspid.MVVM.CyclicList
{
    [View]
    public partial class CyclicElementListView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _name;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isCompleted;
    }
}