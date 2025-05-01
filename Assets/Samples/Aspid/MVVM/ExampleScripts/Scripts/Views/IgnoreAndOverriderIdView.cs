using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.ExampleScripts.Views
{
    [View]
    public partial class IgnoreAndOverriderIdView : MonoView
    {
        [SerializeField] private MonoBinder _binder;

        [BindId("Binder")]
        [SerializeField] private MonoBinder[] _binders;

        [SerializeField] private bool _isTrue;
        
        [IgnoreBind]
        [SerializeField] private MonoBinder _trueBinder;
        
        [IgnoreBind]
        [SerializeField] private MonoBinder _falseBinder;

        [BindId("Condition")]
        public MonoBinder TrueOrFalseBinder => _isTrue ? _trueBinder : _falseBinder;
    }
}