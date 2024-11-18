using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands.Adapters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(MonoCommandAdapter))]
    public partial class CommandMonoBinder : MonoBinder, IBinder<IRelayCommand>
    {
        [SerializeField] private MonoCommandAdapter _commandAdapter;

        private void Awake()
        {
            if (!_commandAdapter)
                _commandAdapter = GetComponent<MonoCommandAdapter>();
        }

        [BinderLog]
        public void SetValue(IRelayCommand value) => _commandAdapter.Command = value;
    }
    
    public abstract partial class CommandMonoBinder<T> : MonoBinder, IBinder<IRelayCommand<T>>
    {
        [SerializeField] private MonoCommandAdapter<T> _commandAdapter;

        private void Awake()
        {
            if (!_commandAdapter)
                _commandAdapter = GetComponent<MonoCommandAdapter<T>>();
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T> value) => _commandAdapter.Command = value;
    }

    public abstract partial class CommandMonoBinder<T1, T2> : MonoBinder, IBinder<IRelayCommand<T1, T2>>
    {
        [SerializeField] private MonoCommandAdapter<T1, T2> _commandAdapter;

        private void Awake()
        {
            if (!_commandAdapter)
                _commandAdapter = GetComponent<MonoCommandAdapter<T1, T2>>();
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) => _commandAdapter.Command = value;
    }

    public abstract partial class CommandMonoBinder<T1, T2, T3> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3>>
    {
        [SerializeField] private MonoCommandAdapter<T1, T2, T3> _commandAdapter;

        private void Awake()
        {
            if (!_commandAdapter)
                _commandAdapter = GetComponent<MonoCommandAdapter<T1, T2, T3>>();
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) => _commandAdapter.Command = value;
    }
    
    public abstract partial class CommandMonoBinder<T1, T2, T3, T4> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [SerializeField] private MonoCommandAdapter<T1, T2, T3, T4> _commandAdapter;

        private void Awake()
        {
            if (!_commandAdapter)
                _commandAdapter = GetComponent<MonoCommandAdapter<T1, T2, T3, T4>>();
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) => _commandAdapter.Command = value;
    }
}