using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{ScrollRect}"/> that executes a command each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> as the command argument.
    /// Accepts commands typed as <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector2&gt;</see>
    /// or <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector3&gt;</see>.
    /// </summary>
    /// <include file="XmlExampleDoc-ScrollRect-Command-1.1.0.xml" path="doc//member[@name='ScrollRectCommandBinder']/*" />
    [Serializable]
    public sealed class ScrollRectCommandBinder : TargetBinder<ScrollRect>,
        IBinder<IRelayCommand<Vector2>>,
        IBinder<IRelayCommand<Vector3>>
    {
        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;

        private IRelayCommand<Vector2> _vector2Command;
        private IRelayCommand<Vector3> _vector3Command;

        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="interactable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(ScrollRect target, ICanExecuteView interactable, BindMode mode = BindMode.OneWay)
            : this(target, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(ScrollRect target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives the current <see cref="ScrollRect.normalizedPosition"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;Vector3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives the current <see cref="ScrollRect.normalizedPosition"/> cast to <see cref="Vector3"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

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
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2>)null);
            SetValue((IRelayCommand<Vector3>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value);
            else _vector3Command?.Execute(value);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition));

        private void OnCanExecuteChanged(IRelayCommand<Vector3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition));
    }

    /// <summary>
    /// <see cref="TargetBinder{ScrollRect}"/> that executes commands with one additional parameter each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> alongside <see cref="Param"/>.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector2, T&gt;</see>
    /// or <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector3, T&gt;</see>.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded alongside the scroll position when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-ScrollRect-Command-1.1.0.xml" path="doc//member[@name='ScrollRectCommandBinder{1}']/*" />
    [Serializable]
    public class ScrollRectCommandBinder<T> : TargetBinder<ScrollRect>,
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
        /// Gets or sets the additional parameter forwarded alongside the scroll position when the command is executed.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder{T}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="param">The additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="interactable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(
            ScrollRect target,
            T param,
            ICanExecuteView interactable,
            BindMode mode = BindMode.OneWay)
            : this(target, param, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder{T}"/>.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="param">The additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(ScrollRect target, T param, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _param = param;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector2, T&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives <see cref="ScrollRect.normalizedPosition"/> followed by <see cref="Param"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector2, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;Vector3, T&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives <see cref="ScrollRect.normalizedPosition"/> cast to <see cref="Vector3"/> followed by <see cref="Param"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector3, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

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
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2, T>)null);
            SetValue((IRelayCommand<Vector3, T>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param);
            else _vector3Command?.Execute(value, Param);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param));
    }

    /// <summary>
    /// <see cref="TargetBinder{ScrollRect}"/> that executes commands with two additional parameters each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> alongside <see cref="Param1"/> and <see cref="Param2"/>.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector2, T1, T2&gt;</see>
    /// or <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector3, T1, T2&gt;</see>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded alongside the scroll position when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded alongside the scroll position when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-ScrollRect-Command-1.1.0.xml" path="doc//member[@name='ScrollRectCommandBinder{2}']/*" />
    [Serializable]
    public class ScrollRectCommandBinder<T1, T2> : TargetBinder<ScrollRect>,
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
        /// Gets or sets the first additional parameter forwarded alongside the scroll position when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter forwarded alongside the scroll position when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder{T1, T2}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="interactable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(
            ScrollRect target,
            T1 param1,
            T2 param2,
            ICanExecuteView interactable,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder{T1, T2}"/>.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(ScrollRect target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector2, T1, T2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives <see cref="ScrollRect.normalizedPosition"/> followed by <see cref="Param1"/> and <see cref="Param2"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector2, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;Vector3, T1, T2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives <see cref="ScrollRect.normalizedPosition"/> cast to <see cref="Vector3"/> followed by <see cref="Param1"/> and <see cref="Param2"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector3, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

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
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2, T1, T2>)null);
            SetValue((IRelayCommand<Vector3, T1, T2>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2);
            else _vector3Command?.Execute(value, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2));
    }

    /// <summary>
    /// <see cref="TargetBinder{ScrollRect}"/> that executes commands with three additional parameters each time <see cref="ScrollRect.onValueChanged"/> fires,
    /// passing the current <see cref="ScrollRect.normalizedPosition"/> alongside <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector2, T1, T2, T3&gt;</see>
    /// or <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector3, T1, T2, T3&gt;</see>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded alongside the scroll position when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded alongside the scroll position when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded alongside the scroll position when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-ScrollRect-Command-1.1.0.xml" path="doc//member[@name='ScrollRectCommandBinder{3}']/*" />
    [Serializable]
    public class ScrollRectCommandBinder<T1, T2, T3> : TargetBinder<ScrollRect>,
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
        /// Gets or sets the first additional parameter forwarded alongside the scroll position when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter forwarded alongside the scroll position when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the third additional parameter forwarded alongside the scroll position when the command is executed.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder{T1, T2, T3}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="interactable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(
            ScrollRect target,
            T1 param1,
            T2 param2,
            T3 param3,
            ICanExecuteView interactable,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ScrollRectCommandBinder{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="target">The <see cref="ScrollRect"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded alongside the scroll position when the command is executed.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ScrollRectCommandBinder(
            ScrollRect target,
            T1 param1,
            T2 param2,
            T3 param3,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector2, T1, T2, T3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives <see cref="ScrollRect.normalizedPosition"/> followed by <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;Vector3, T1, T2, T3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On <see cref="ScrollRect.onValueChanged"/>, the command receives <see cref="ScrollRect.normalizedPosition"/> cast to <see cref="Vector3"/> followed by <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </summary>
        public void SetValue(IRelayCommand<Vector3, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="ScrollRect.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

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
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<Vector2, T1, T2, T3>)null);
            SetValue((IRelayCommand<Vector3, T1, T2, T3>)null);
        }

        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2, Param3);
            else _vector3Command?.Execute(value, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2, Param3));
    }
}
