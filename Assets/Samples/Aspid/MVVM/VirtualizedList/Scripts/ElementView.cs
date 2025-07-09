using Aspid.MVVM;
using Aspid.MVVM.Unity;
using UnityEngine;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [View]
    public partial class ElementView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _name;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isCompleted;
    }
}