using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that holds a bound <see cref="IRelayCommand"/> and exposes <see cref="CanExecute"/> and <see cref="Execute"/> for it.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Command/Binder – Command")]
    [AddBinderContextMenu(typeof(Component), serializePropertyNames: "m_Calls", Path = "Add General Binder/Command/Command Binder")]
    public partial class CommandMonoBinder : MonoBinder, IBinder<IRelayCommand>
    {
        private IRelayCommand _command;

        /// <summary>
        /// Gets the bound command, or <see langword="null"/> when unbound.
        /// </summary>
        protected IRelayCommand Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds <paramref name="value"/> and calls <see cref="OnSetValue"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a command is bound. Override to react to the change.
        /// </summary>
        /// <param name="value">The bound command, or <see langword="null"/> when unbound.</param>
        protected virtual void OnSetValue(IRelayCommand value) { }

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> fires and right after binding.
        /// </summary>
        /// <param name="command">The bound command.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand command) { }

        /// <summary>
        /// Returns whether the bound command can execute.
        /// </summary>
        /// <returns><see langword="true"/> if a command is bound and can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute() =>
            Command?.CanExecute() ?? false;

        /// <summary>
        /// Executes the bound command, if any.
        /// </summary>
        public void Execute() =>
            Command?.Execute();
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that holds a bound <see cref="IRelayCommand{T}"/> and exposes <see cref="CanExecute"/> and <see cref="Execute"/> for it.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public abstract partial class CommandMonoBinder<T> : MonoBinder, IBinder<IRelayCommand<T>>
    {
        private IRelayCommand<T> _command;

        /// <summary>
        /// Gets the bound command, or <see langword="null"/> when unbound.
        /// </summary>
        protected IRelayCommand<T> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds <paramref name="value"/> and calls <see cref="OnSetValue"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<T> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a command is bound. Override to react to the change.
        /// </summary>
        /// <param name="value">The bound command, or <see langword="null"/> when unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T> value) { }

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> fires and right after binding.
        /// </summary>
        /// <param name="command">The bound command.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T> command) { }

        /// <summary>
        /// Returns whether the bound command can execute with the given parameter.
        /// </summary>
        /// <param name="param1">The command parameter.</param>
        /// <returns><see langword="true"/> if a command is bound and can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T param1) =>
            Command?.CanExecute(param1) ?? false;

        /// <summary>
        /// Executes the bound command, if any, with the given parameter.
        /// </summary>
        /// <param name="param1">The command parameter.</param>
        public void Execute(T param1) =>
            Command?.Execute(param1);
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that holds a bound <see cref="IRelayCommand{T1, T2}"/> and exposes <see cref="CanExecute"/> and <see cref="Execute"/> for it.
    /// </summary>
    /// <typeparam name="T1">The type of the first command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second command parameter.</typeparam>
    public abstract partial class CommandMonoBinder<T1, T2> : MonoBinder, IBinder<IRelayCommand<T1, T2>>
    {
        private IRelayCommand<T1, T2> _command;

        /// <summary>
        /// Gets the bound command, or <see langword="null"/> when unbound.
        /// </summary>
        protected IRelayCommand<T1, T2> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds <paramref name="value"/> and calls <see cref="OnSetValue"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a command is bound. Override to react to the change.
        /// </summary>
        /// <param name="value">The bound command, or <see langword="null"/> when unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T1, T2> value) { }

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> fires and right after binding.
        /// </summary>
        /// <param name="command">The bound command.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2> command) { }

        /// <summary>
        /// Returns whether the bound command can execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <returns><see langword="true"/> if a command is bound and can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T1 param1, T2 param2) =>
            Command?.CanExecute(param1, param2) ?? false;

        /// <summary>
        /// Executes the bound command, if any, with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        public void Execute(T1 param1, T2 param2) =>
            Command?.Execute(param1, param2);
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that holds a bound <see cref="IRelayCommand{T1, T2, T3}"/> and exposes <see cref="CanExecute"/> and <see cref="Execute"/> for it.
    /// </summary>
    /// <typeparam name="T1">The type of the first command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second command parameter.</typeparam>
    /// <typeparam name="T3">The type of the third command parameter.</typeparam>
    public abstract partial class CommandMonoBinder<T1, T2, T3> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3>>
    {
        private IRelayCommand<T1, T2, T3> _command;

        /// <summary>
        /// Gets the bound command, or <see langword="null"/> when unbound.
        /// </summary>
        protected IRelayCommand<T1, T2, T3> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds <paramref name="value"/> and calls <see cref="OnSetValue"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a command is bound. Override to react to the change.
        /// </summary>
        /// <param name="value">The bound command, or <see langword="null"/> when unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T1, T2, T3> value) { }

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> fires and right after binding.
        /// </summary>
        /// <param name="command">The bound command.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) { }

        /// <summary>
        /// Returns whether the bound command can execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <returns><see langword="true"/> if a command is bound and can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T1 param1, T2 param2, T3 param3) =>
            Command?.CanExecute(param1, param2, param3) ?? false;

        /// <summary>
        /// Executes the bound command, if any, with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        public void Execute(T1 param1, T2 param2, T3 param3) =>
            Command?.Execute(param1, param2, param3);
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that holds a bound <see cref="IRelayCommand{T1, T2, T3, T4}"/> and exposes <see cref="CanExecute"/> and <see cref="Execute"/> for it.
    /// </summary>
    /// <typeparam name="T1">The type of the first command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second command parameter.</typeparam>
    /// <typeparam name="T3">The type of the third command parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth command parameter.</typeparam>
    public abstract partial class CommandMonoBinder<T1, T2, T3, T4> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        private IRelayCommand<T1, T2, T3, T4> _command;

        /// <summary>
        /// Gets the bound command, or <see langword="null"/> when unbound.
        /// </summary>
        protected IRelayCommand<T1, T2, T3, T4> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds <paramref name="value"/> and calls <see cref="OnSetValue"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a command is bound. Override to react to the change.
        /// </summary>
        /// <param name="value">The bound command, or <see langword="null"/> when unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T1, T2, T3, T4> value) { }

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> fires and right after binding.
        /// </summary>
        /// <param name="command">The bound command.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command) { }

        /// <summary>
        /// Returns whether the bound command can execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        /// <returns><see langword="true"/> if a command is bound and can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T1 param1, T2 param2, T3 param3, T4 param4) =>
            Command?.CanExecute(param1, param2, param3, param4) ?? false;

        /// <summary>
        /// Executes the bound command, if any, with the given parameters.
        /// </summary>
        /// <param name="param1">The first command parameter.</param>
        /// <param name="param2">The second command parameter.</param>
        /// <param name="param3">The third command parameter.</param>
        /// <param name="param4">The fourth command parameter.</param>
        public void Execute(T1 param1, T2 param2, T3 param3, T4 param4) =>
            Command?.Execute(param1, param2, param3, param4);
    }
}
