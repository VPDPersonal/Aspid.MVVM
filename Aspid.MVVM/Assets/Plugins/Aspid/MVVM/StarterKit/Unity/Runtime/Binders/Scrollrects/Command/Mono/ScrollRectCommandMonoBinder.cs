using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{ScrollRect}"/> that executes a command each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> as the command argument.
    /// Accepts commands typed as <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector2&gt;</see>
    /// or <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector3&gt;</see>.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/ScrollRect Binder – Command")]
    public sealed partial class ScrollRectCommandMonoBinder : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2>>,
        IBinder<IRelayCommand<Vector3>>
    {
        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;

        private IRelayCommand<Vector2> _vector2Command;
        private IRelayCommand<Vector3> _vector3Command;

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="ScrollRect.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to both <c>SetValue</c> overloads to detach command references
        /// and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
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
    /// Abstract <see cref="ComponentMonoBinder{ScrollRect}"/> that executes a command each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> and an additional parameter as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector2, T&gt;</see>
    /// or <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector3, T&gt;</see>.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded alongside the scroll position.</typeparam>
    public abstract partial class ScrollRectCommandMonoBinder<T> : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2, T>>,
        IBinder<IRelayCommand<Vector3, T>>
    {
        [Tooltip("The additional parameter forwarded alongside the scroll position when the command is executed.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;

        private IRelayCommand<Vector2, T> _vector2Command;
        private IRelayCommand<Vector3, T> _vector3Command;

        /// <summary>
        /// Gets or sets the additional parameter forwarded alongside the scroll position.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector2, T&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector3, T&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="ScrollRect.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to both <c>SetValue</c> overloads to detach command references
        /// and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
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
    /// Abstract <see cref="ComponentMonoBinder{ScrollRect}"/> that executes a command each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> and two additional parameters as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector2, T1, T2&gt;</see>
    /// or <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector3, T1, T2&gt;</see>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter.</typeparam>
    public abstract partial class ScrollRectCommandMonoBinder<T1, T2> : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2, T1, T2>>,
        IBinder<IRelayCommand<Vector3, T1, T2>>
    {
        [Tooltip("The first additional parameter forwarded alongside the scroll position when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded alongside the scroll position when the command is executed.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;

        private IRelayCommand<Vector2, T1, T2> _vector2Command;
        private IRelayCommand<Vector3, T1, T2> _vector3Command;

        /// <summary>
        /// Gets or sets the first additional parameter.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector2, T1, T2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector3, T1, T2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="ScrollRect.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to both <c>SetValue</c> overloads to detach command references
        /// and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
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
    /// Abstract <see cref="ComponentMonoBinder{ScrollRect}"/> that executes a command each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> and three additional parameters as the command arguments.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector2, T1, T2, T3&gt;</see>
    /// or <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector3, T1, T2, T3&gt;</see>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter.</typeparam>
    public abstract partial class ScrollRectCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2, T1, T2, T3>>,
        IBinder<IRelayCommand<Vector3, T1, T2, T3>>
    {
        [Tooltip("The first additional parameter forwarded alongside the scroll position when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded alongside the scroll position when the command is executed.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third additional parameter forwarded alongside the scroll position when the command is executed.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;

        private IRelayCommand<Vector2, T1, T2, T3> _vector2Command;
        private IRelayCommand<Vector3, T1, T2, T3> _vector3Command;

        /// <summary>
        /// Gets or sets the first additional parameter.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the third additional parameter.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector2, T1, T2, T3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector3, T1, T2, T3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="ScrollRect.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to both <c>SetValue</c> overloads to detach command references
        /// and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
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
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2, Param3));
    }
}
