using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
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