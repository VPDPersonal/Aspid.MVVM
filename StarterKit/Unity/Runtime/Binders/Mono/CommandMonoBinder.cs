using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that binds an <see cref="IRelayCommand"/> and exposes
    /// <see cref="CanExecute()"/> and <see cref="Execute()"/> as pass-through helpers.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Command/Binder – Command")]
    [AddBinderContextMenu(typeof(Component), serializePropertyNames: "m_Calls", Path = "Add General Binder/Command/Command Binder")]
    public partial class CommandMonoBinder : MonoBinder, IBinder<IRelayCommand>
    {
        private IRelayCommand _command;

        /// <summary>
        /// Gets the currently bound <see cref="IRelayCommand"/>.
        /// </summary>
        protected IRelayCommand Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and invokes <see cref="OnSetValue"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <summary>
        /// Called when the binder is unbound. Releases the bound command reference.
        /// </summary>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a new command is bound via <see cref="SetValue"/>.
        /// </summary>
        /// <param name="value">The newly bound command, or <see langword="null"/> if unbound.</param>
        protected virtual void OnSetValue(IRelayCommand value) { }

        /// <summary>
        /// Indicates whether the bound command can currently execute.
        /// </summary>
        /// <returns><see langword="true"/> if the bound command can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute() =>
            Command?.CanExecute() ?? false;

        /// <summary>
        /// Executes the bound command if one is set.
        /// </summary>
        public void Execute() =>
            Command?.Execute();

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> event fires.
        /// </summary>
        /// <param name="command">The command whose state changed.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand command) { }
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that binds an <see cref="IRelayCommand{T}"/> and exposes
    /// <see cref="CanExecute"/> and <see cref="Execute"/> as pass-through helpers.
    /// </summary>
    /// <typeparam name="T">The type of the parameter passed to the command.</typeparam>
    public abstract partial class MonoCommandBinder<T> : MonoBinder, IBinder<IRelayCommand<T>>
    {
        private IRelayCommand<T> _command;

        /// <summary>
        /// Gets the currently bound <see cref="IRelayCommand{T}"/>.
        /// </summary>
        protected IRelayCommand<T> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}"/> and invokes <see cref="OnSetValue"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <summary>
        /// Called when the binder is unbound. Releases the bound command reference.
        /// </summary>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a new command is bound via <see cref="SetValue"/>.
        /// </summary>
        /// <param name="value">The newly bound command, or <see langword="null"/> if unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T> value) { }

        /// <summary>
        /// Indicates whether the bound command can currently execute with the given parameter.
        /// </summary>
        /// <param name="param1">The parameter to pass to <see cref="IRelayCommand{T}.CanExecute"/>.</param>
        /// <returns><see langword="true"/> if the bound command can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T param1) =>
            Command?.CanExecute(param1) ?? false;

        /// <summary>
        /// Executes the bound command with the given parameter if one is set.
        /// </summary>
        /// <param name="param1">The parameter to pass to <see cref="IRelayCommand{T}.Execute"/>.</param>
        public void Execute(T param1) =>
            Command?.Execute(param1);

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> event fires.
        /// </summary>
        /// <param name="command">The command whose state changed.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T> command) { }
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that binds an <see cref="IRelayCommand{T1, T2}"/> and exposes
    /// <see cref="CanExecute"/> and <see cref="Execute"/> as pass-through helpers.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter passed to the command.</typeparam>
    /// <typeparam name="T2">The type of the second parameter passed to the command.</typeparam>
    public abstract partial class MonoCommandBinder<T1, T2> : MonoBinder, IBinder<IRelayCommand<T1, T2>>
    {
        private IRelayCommand<T1, T2> _command;

        /// <summary>
        /// Gets the currently bound <see cref="IRelayCommand{T1, T2}"/>.
        /// </summary>
        protected IRelayCommand<T1, T2> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}"/> and invokes <see cref="OnSetValue"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <summary>
        /// Called when the binder is unbound. Releases the bound command reference.
        /// </summary>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a new command is bound via <see cref="SetValue"/>.
        /// </summary>
        /// <param name="value">The newly bound command, or <see langword="null"/> if unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T1, T2> value) { }

        /// <summary>
        /// Indicates whether the bound command can currently execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter to pass to <see cref="IRelayCommand{T1, T2}.CanExecute"/>.</param>
        /// <param name="param2">The second parameter to pass to <see cref="IRelayCommand{T1, T2}.CanExecute"/>.</param>
        /// <returns><see langword="true"/> if the bound command can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T1 param1, T2 param2) =>
            Command?.CanExecute(param1, param2) ?? false;

        /// <summary>
        /// Executes the bound command with the given parameters if one is set.
        /// </summary>
        /// <param name="param1">The first parameter to pass to <see cref="IRelayCommand{T1, T2}.Execute"/>.</param>
        /// <param name="param2">The second parameter to pass to <see cref="IRelayCommand{T1, T2}.Execute"/>.</param>
        public void Execute(T1 param1, T2 param2) =>
            Command?.Execute(param1, param2);

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> event fires.
        /// </summary>
        /// <param name="command">The command whose state changed.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2> command) { }
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that binds an <see cref="IRelayCommand{T1, T2, T3}"/> and exposes
    /// <see cref="CanExecute"/> and <see cref="Execute"/> as pass-through helpers.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter passed to the command.</typeparam>
    /// <typeparam name="T2">The type of the second parameter passed to the command.</typeparam>
    /// <typeparam name="T3">The type of the third parameter passed to the command.</typeparam>
    public abstract partial class MonoCommandBinder<T1, T2, T3> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3>>
    {
        private IRelayCommand<T1, T2, T3> _command;

        /// <summary>
        /// Gets the currently bound <see cref="IRelayCommand{T1, T2, T3}"/>.
        /// </summary>
        protected IRelayCommand<T1, T2, T3> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}"/> and invokes <see cref="OnSetValue"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <summary>
        /// Called when the binder is unbound. Releases the bound command reference.
        /// </summary>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a new command is bound via <see cref="SetValue"/>.
        /// </summary>
        /// <param name="value">The newly bound command, or <see langword="null"/> if unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T1, T2, T3> value) { }

        /// <summary>
        /// Indicates whether the bound command can currently execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter to pass to <see cref="IRelayCommand{T1, T2, T3}.CanExecute"/>.</param>
        /// <param name="param2">The second parameter to pass to <see cref="IRelayCommand{T1, T2, T3}.CanExecute"/>.</param>
        /// <param name="param3">The third parameter to pass to <see cref="IRelayCommand{T1, T2, T3}.CanExecute"/>.</param>
        /// <returns><see langword="true"/> if the bound command can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T1 param1, T2 param2, T3 param3) =>
            Command?.CanExecute(param1, param2, param3) ?? false;

        /// <summary>
        /// Executes the bound command with the given parameters if one is set.
        /// </summary>
        /// <param name="param1">The first parameter to pass to <see cref="IRelayCommand{T1, T2, T3}.Execute"/>.</param>
        /// <param name="param2">The second parameter to pass to <see cref="IRelayCommand{T1, T2, T3}.Execute"/>.</param>
        /// <param name="param3">The third parameter to pass to <see cref="IRelayCommand{T1, T2, T3}.Execute"/>.</param>
        public void Execute(T1 param1, T2 param2, T3 param3) =>
            Command?.Execute(param1, param2, param3);

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> event fires.
        /// </summary>
        /// <param name="command">The command whose state changed.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) { }
    }

    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that binds an <see cref="IRelayCommand{T1, T2, T3, T4}"/> and exposes
    /// <see cref="CanExecute"/> and <see cref="Execute"/> as pass-through helpers.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter passed to the command.</typeparam>
    /// <typeparam name="T2">The type of the second parameter passed to the command.</typeparam>
    /// <typeparam name="T3">The type of the third parameter passed to the command.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter passed to the command.</typeparam>
    public abstract partial class MonoCommandBinder<T1, T2, T3, T4> : MonoBinder, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        private IRelayCommand<T1, T2, T3, T4> _command;

        /// <summary>
        /// Gets the currently bound <see cref="IRelayCommand{T1, T2, T3, T4}"/>.
        /// </summary>
        protected IRelayCommand<T1, T2, T3, T4> Command
        {
            get => _command;
            private set => CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}"/> and invokes <see cref="OnSetValue"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value)
        {
            Command = value;
            OnSetValue(value);
        }

        /// <summary>
        /// Called when the binder is unbound. Releases the bound command reference.
        /// </summary>
        protected override void OnUnbound() =>
            Command = null;

        /// <summary>
        /// Called after a new command is bound via <see cref="SetValue"/>.
        /// </summary>
        /// <param name="value">The newly bound command, or <see langword="null"/> if unbound.</param>
        protected virtual void OnSetValue(IRelayCommand<T1, T2, T3, T4> value) { }

        /// <summary>
        /// Indicates whether the bound command can currently execute with the given parameters.
        /// </summary>
        /// <param name="param1">The first parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecute"/>.</param>
        /// <param name="param2">The second parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecute"/>.</param>
        /// <param name="param3">The third parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecute"/>.</param>
        /// <param name="param4">The fourth parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.CanExecute"/>.</param>
        /// <returns><see langword="true"/> if the bound command can execute; otherwise <see langword="false"/>.</returns>
        public bool CanExecute(T1 param1, T2 param2, T3 param3, T4 param4) =>
            Command?.CanExecute(param1, param2, param3, param4) ?? false;

        /// <summary>
        /// Executes the bound command with the given parameters if one is set.
        /// </summary>
        /// <param name="param1">The first parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.Execute"/>.</param>
        /// <param name="param2">The second parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.Execute"/>.</param>
        /// <param name="param3">The third parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.Execute"/>.</param>
        /// <param name="param4">The fourth parameter to pass to <see cref="IRelayCommand{T1, T2, T3, T4}.Execute"/>.</param>
        public void Execute(T1 param1, T2 param2, T3 param3, T4 param4) =>
            Command?.Execute(param1, param2, param3, param4);

        /// <summary>
        /// Called when the bound command's <see cref="IRelayCommand.CanExecuteChanged"/> event fires.
        /// </summary>
        /// <param name="command">The command whose state changed.</param>
        protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command) { }
    }
}
