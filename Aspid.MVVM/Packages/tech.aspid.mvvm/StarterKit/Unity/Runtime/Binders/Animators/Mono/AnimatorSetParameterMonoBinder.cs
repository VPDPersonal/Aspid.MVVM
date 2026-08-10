using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{Animator}"/> that sets a typed parameter on an
    /// <see cref="Animator"/> component when the bound ViewModel value changes.
    /// </summary>
    /// <typeparam name="T">The type of the Animator parameter value.</typeparam>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWay"/>, <see cref="BindMode.OneTime"/>, and
    /// <see cref="BindMode.OneWayToSource"/>. In <see cref="BindMode.OneWayToSource"/> mode the binder
    /// exposes <see cref="SetParameter"/> to the ViewModel either as a plain <see cref="Action{T}"/>
    /// or as an <see cref="IRelayCommand{T}"/> whose <see cref="IRelayCommand.CanExecute()"/> mirrors <see cref="CanExecute(T)"/>.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>, 
        IBinder<T>,
        IReverseBinder<Action<T>>,
        IReverseBinder<IRelayCommand<T>>
    {
        event Action<Action<T>> IReverseBinder<Action<T>>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }
        
        event Action<IRelayCommand<T>> IReverseBinder<IRelayCommand<T>>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }
        
        [NonSerialized] private T _value;
        [NonSerialized] private bool _hasValue;

        private IRelayCommand<T> _command;
        private AnimatorParameterProbe _probe;
        private Action<Action<T>> _reverseAction;
        private Action<IRelayCommand<T>> _reverseCommand;

        [field: SerializeField]
        [field: Tooltip("The name of the Animator parameter to set.")]
        protected string ParameterName { get; private set; }

        /// <summary>
        /// The Animator parameter type this binder sets, inferred from <typeparamref name="T"/>.
        /// Returns <see langword="null"/> when <typeparamref name="T"/> is none of the types an
        /// <see cref="Animator"/> parameter can hold, in which case the name is checked on its own.
        /// </summary>
        protected virtual AnimatorControllerParameterType? ParameterType =>
            AnimatorParameterTypes.Of<T>();

        /// <summary>
        /// Re-applies the last value received from the ViewModel, if there was one, and notifies the bound command
        /// that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// Re-applying is skipped until a value has actually arrived. It used to run unconditionally, writing
        /// <c>default(T)</c> into the Animator on every enable — resetting a parameter the ViewModel had set and
        /// never telling it. Both reverse paths now record the value they push, so re-enabling restores what the
        /// ViewModel last asked for rather than a zero.
        /// <para/>
        /// When overriding this method, always call <c>base.OnEnable()</c> to preserve
        /// the parameter re-application and command notification behavior.
        /// </remarks>
        protected virtual void OnEnable()
        {
            if (_hasValue) SetParameterInternal(_value);

            _command?.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Notifies the bound command that <see cref="IRelayCommand.CanExecute()"/> may have changed.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnDisable()</c> to preserve
        /// the command notification behavior.
        /// </remarks>
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        /// <summary>
        /// Receives <paramref name="value"/> from the ViewModel and forwards it to <see cref="SetParameter"/>
        /// if <see cref="CanExecute"/> returns <see langword="true"/>.
        /// </summary>
        /// <param name="value">The new parameter value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(T value)
        {
            _value = value;
            SetParameterInternal(value);
        }

        private void SetParameterInternal(T value)
        {
            _value = value;
            _hasValue = true;

            if (!CanExecute(value)) return;
            
            SetParameter(value);
        }

        /// <summary>
        /// Called when binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, exposes <see cref="SetParameter"/> to the ViewModel
        /// as an <see cref="IRelayCommand{T}"/> or a plain <see cref="Action{T}"/>.
        /// </summary>
        protected sealed override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            
            if (_reverseCommand is not null)
            {
                _command = new RelayCommand<T>(SetParameterInternal, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(SetParameterInternal);
            }
        }

        /// <summary>
        /// Called when the binding is being released.
        /// Clears the internal command and notifies reverse-binder subscribers with <see langword="null"/>.
        /// </summary>
        protected sealed override void OnUnbinding()
        {
            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }

        /// <summary>
        /// Applies <paramref name="value"/> to the Animator parameter identified by <see cref="ParameterName"/>.
        /// </summary>
        /// <param name="value">The value to apply to the Animator parameter.</param>
        protected abstract void SetParameter(T value);

        /// <summary>
        /// Determines whether the Animator parameter may be set.
        /// Returns <see langword="true"/> when the <see cref="Animator"/>'s <see cref="UnityEngine.GameObject"/> is
        /// active in the hierarchy and <see cref="ParameterName"/> names a parameter its controller actually has.
        /// </summary>
        /// <remarks>
        /// The activity check comes first because it is the cheaper of the two and because a binder on an inactive
        /// object has nothing to say about a parameter it is not going to set.
        /// </remarks>
        /// <param name="value">The value that would be applied (not used in the default implementation).</param>
        protected virtual bool CanExecute(T value) =>
            CachedComponent && CachedComponent.gameObject.activeInHierarchy &&
            _probe.IsUsable(CachedComponent, ParameterName, ParameterType, this);
    }
}