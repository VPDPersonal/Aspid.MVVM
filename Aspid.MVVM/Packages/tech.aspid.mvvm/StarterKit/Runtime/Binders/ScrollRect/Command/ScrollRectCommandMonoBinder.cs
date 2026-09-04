using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="ScrollRect.onValueChanged"/> with the normalized position.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T}"/> with a <see cref="Vector2"/> or <see cref="Vector3"/> position.
    /// </remarks>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Command")]
    public sealed partial class ScrollRectCommandMonoBinder : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2>>,
        IBinder<IRelayCommand<Vector3>>
    {
        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _interactable;

        private IRelayCommand<Vector2> _vector2Command;
        private IRelayCommand<Vector3> _vector3Command;

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_vector2Command is not null) OnCanExecuteChanged(_vector2Command);
            else if (_vector3Command is not null) OnCanExecuteChanged(_vector3Command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2>)null);
            SetValue((IRelayCommand<Vector3>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value);
            else _vector3Command?.Execute(value);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition));

        private void OnCanExecuteChanged(IRelayCommand<Vector3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition));
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="ScrollRect.onValueChanged"/> with the normalized position and <see cref="Param"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2}"/> with a <see cref="Vector2"/> or <see cref="Vector3"/> position.
    /// </remarks>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    public abstract partial class ScrollRectCommandMonoBinder<T> : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2, T>>,
        IBinder<IRelayCommand<Vector3, T>>
    {
        [Tooltip("Extra parameter passed after the position.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _interactable;

        private IRelayCommand<Vector2, T> _vector2Command;
        private IRelayCommand<Vector3, T> _vector3Command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the position.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_vector2Command is not null) OnCanExecuteChanged(_vector2Command);
            else if (_vector3Command is not null) OnCanExecuteChanged(_vector3Command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2, T>)null);
            SetValue((IRelayCommand<Vector3, T>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param);
            else _vector3Command?.Execute(value, Param);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param));
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="ScrollRect.onValueChanged"/> with the normalized position and <see cref="Param1"/>,
    /// <see cref="Param2"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2, T3}"/> with a <see cref="Vector2"/> or <see cref="Vector3"/> position.
    /// </remarks>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    public abstract partial class ScrollRectCommandMonoBinder<T1, T2> : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2, T1, T2>>,
        IBinder<IRelayCommand<Vector3, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the position.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the position.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _interactable;

        private IRelayCommand<Vector2, T1, T2> _vector2Command;
        private IRelayCommand<Vector3, T1, T2> _vector3Command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the position.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the position.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_vector2Command is not null) OnCanExecuteChanged(_vector2Command);
            else if (_vector3Command is not null) OnCanExecuteChanged(_vector3Command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2, T1, T2>)null);
            SetValue((IRelayCommand<Vector3, T1, T2>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2);
            else _vector3Command?.Execute(value, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2));
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="ScrollRect.onValueChanged"/> with the normalized position and <see cref="Param1"/>,
    /// <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2, T3, T4}"/> with a <see cref="Vector2"/> or <see cref="Vector3"/>
    /// position.
    /// </remarks>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    public abstract partial class ScrollRectCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2, T1, T2, T3>>,
        IBinder<IRelayCommand<Vector3, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the position.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the position.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the position.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _interactable;

        private IRelayCommand<Vector2, T1, T2, T3> _vector2Command;
        private IRelayCommand<Vector3, T1, T2, T3> _vector3Command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the position.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the position.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the position.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_vector2Command is not null) OnCanExecuteChanged(_vector2Command);
            else if (_vector3Command is not null) OnCanExecuteChanged(_vector3Command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2, T1, T2, T3>)null);
            SetValue((IRelayCommand<Vector3, T1, T2, T3>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2, Param3);
            else _vector3Command?.Execute(value, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(
                command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(
                command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2, Param3));
    }
}
