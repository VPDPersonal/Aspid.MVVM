using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Static helpers for atomically replacing a bound command reference while managing <c>CanExecuteChanged</c> subscriptions.
    /// </summary>
    public static class CommandBinderExtensions
    {
        /// <summary>
        /// Replaces <paramref name="command"/> with <paramref name="value"/>,
        /// transferring the <c>CanExecuteChanged</c> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <c>CanExecuteChanged</c> and invoked immediately after binding.
        /// Pass <see langword="null"/> to skip subscription.
        /// </param>
        public static void UpdateCommand(ref IRelayCommand command, IRelayCommand value, in Action<IRelayCommand> onCanExecuteChanged = null)
        {
            if (command == value) return;

            if (command is not null && onCanExecuteChanged is not null)
                command.CanExecuteChanged -= onCanExecuteChanged;

            command = value;

            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }

        /// <summary>
        /// Replaces <paramref name="command"/> with <paramref name="value"/>,
        /// transferring the <c>CanExecuteChanged</c> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <c>CanExecuteChanged</c> and invoked immediately after binding.
        /// Pass <see langword="null"/> to skip subscription.
        /// </param>
        public static void UpdateCommand<T>(ref IRelayCommand<T> command,
            IRelayCommand<T> value,
            in Action<IRelayCommand<T>> onCanExecuteChanged = null)
        {
            if (command == value) return;

            if (command is not null && onCanExecuteChanged is not null)
                command.CanExecuteChanged -= onCanExecuteChanged;

            command = value;

            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }

        /// <summary>
        /// Replaces <paramref name="command"/> with <paramref name="value"/>,
        /// transferring the <c>CanExecuteChanged</c> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <c>CanExecuteChanged</c> and invoked immediately after binding.
        /// Pass <see langword="null"/> to skip subscription.
        /// </param>
        public static void UpdateCommand<T1, T2>(
            ref IRelayCommand<T1, T2> command,
            IRelayCommand<T1, T2> value,
            in Action<IRelayCommand<T1, T2>> onCanExecuteChanged = null)
        {
            if (command == value) return;

            if (command is not null && onCanExecuteChanged is not null)
                command.CanExecuteChanged -= onCanExecuteChanged;

            command = value;

            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }

        /// <summary>
        /// Replaces <paramref name="command"/> with <paramref name="value"/>,
        /// transferring the <c>CanExecuteChanged</c> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <typeparam name="T3">The type of the third command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <c>CanExecuteChanged</c> and invoked immediately after binding.
        /// Pass <see langword="null"/> to skip subscription.
        /// </param>
        public static void UpdateCommand<T1, T2, T3>(
            ref IRelayCommand<T1, T2, T3> command,
            IRelayCommand<T1, T2, T3> value,
            in Action<IRelayCommand<T1, T2, T3>> onCanExecuteChanged = null)
        {
            if (command == value) return;

            if (command is not null && onCanExecuteChanged is not null)
                command.CanExecuteChanged -= onCanExecuteChanged;

            command = value;

            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }

        /// <summary>
        /// Replaces <paramref name="command"/> with <paramref name="value"/>,
        /// transferring the <c>CanExecuteChanged</c> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <typeparam name="T3">The type of the third command parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <c>CanExecuteChanged</c> and invoked immediately after binding.
        /// Pass <see langword="null"/> to skip subscription.
        /// </param>
        public static void UpdateCommand<T1, T2, T3, T4>(
            ref IRelayCommand<T1, T2, T3, T4> command,
            IRelayCommand<T1, T2, T3, T4> value,
            in Action<IRelayCommand<T1, T2, T3, T4>> onCanExecuteChanged = null)
        {
            if (command == value) return;

            if (command is not null && onCanExecuteChanged is not null)
                command.CanExecuteChanged -= onCanExecuteChanged;

            command = value;

            if (command is not null && onCanExecuteChanged is not null)
            {
                command.CanExecuteChanged += onCanExecuteChanged;
                onCanExecuteChanged.Invoke(command);
            }
        }
    }
}
