using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.SamplesVirtualizedList
{
    [View]
    public sealed partial class ItemView : MonoView
    {
        [BindId("Number")]
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _name;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isCompleted;
    }
}