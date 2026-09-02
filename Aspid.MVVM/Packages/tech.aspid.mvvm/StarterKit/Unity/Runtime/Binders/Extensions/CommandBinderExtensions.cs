using System;
using UnityEngine.UI;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Static helpers for atomically replacing a bound command reference while managing <see cref="IRelayCommand.CanExecuteChanged"/> subscriptions,
    /// and for reflecting a command's <see cref="IRelayCommand.CanExecute()"/> result on a <see cref="Selectable"/>.
    /// </summary>
    public static class CommandBinderExtensions
    {
        /// <summary>
        /// Reflects <paramref name="isInteractable"/> on <paramref name="target"/> according to <paramref name="mode"/>.
        /// </summary>
        /// <remarks>
        /// A missing reference is logged rather than thrown: this method runs from
        /// <see cref="IRelayCommand.CanExecuteChanged"/>, and throwing would cut that notification short for every
        /// other subscriber.
        /// </remarks>
        /// <param name="target">The <see cref="Selectable"/> the command binder operates on.</param>
        /// <param name="mode">Determines what <paramref name="isInteractable"/> is applied to.</param>
        /// <param name="isInteractable">The command's current <see cref="IRelayCommand.CanExecute()"/> result.</param>
        /// <param name="customView">
        /// The view used by <see cref="InteractableMode.Custom"/>. Ignored by every other mode.
        /// </param>
        /// <param name="owner">The binder applying the value; used to name the source in any diagnostic.</param>
        public static void SetInteractable(
            this Selectable target,
            InteractableMode mode,
            bool isInteractable,
            ICanExecuteView customView,
            object owner)
        {
            switch (mode)
            {
                case InteractableMode.None:
                    break;

                case InteractableMode.Visible:
                    if (HasTarget(target, mode, owner)) target.gameObject.SetActive(isInteractable);
                    break;

                case InteractableMode.Interactable:
                    if (HasTarget(target, mode, owner)) target.interactable = isInteractable;
                    break;

                case InteractableMode.Custom:
                    if (customView is null)
                    {
                        BinderLogger.LogError(
                            binderType: owner.GetType(),
                            problem: $"the mode is {nameof(InteractableMode)}.{nameof(InteractableMode.Custom)} but no {nameof(ICanExecuteView)} is assigned",
                            consequence: "The command's CanExecute state is not reflected anywhere.",
                            context: owner as Object);

                        break;
                    }

                    customView.SetCanExecute(isInteractable);
                    break;
            }
        }

        private static bool HasTarget(Selectable target, InteractableMode mode, object owner)
        {
            if (target) return true;

            BinderLogger.LogError(
                binderType: owner.GetType(),
                problem: $"the mode is {nameof(InteractableMode)}.{mode} but its target {nameof(Selectable)} is missing or destroyed",
                consequence: "The command's CanExecute state is not reflected anywhere.",
                context: owner as Object);

            return false;
        }

        /// <summary>
        /// Replaces <paramref name="command"/> with <paramref name="value"/>,
        /// transferring the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <see cref="IRelayCommand.CanExecuteChanged"/> and invoked immediately after binding.
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
        /// transferring the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <see cref="IRelayCommand.CanExecuteChanged"/> and invoked immediately after binding.
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
        /// transferring the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <see cref="IRelayCommand.CanExecuteChanged"/> and invoked immediately after binding.
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
        /// transferring the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <typeparam name="T3">The type of the third command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <see cref="IRelayCommand.CanExecuteChanged"/> and invoked immediately after binding.
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
        /// transferring the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and immediately invoking <paramref name="onCanExecuteChanged"/>.
        /// Does nothing if <paramref name="command"/> already references <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <typeparam name="T3">The type of the third command parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth command parameter.</typeparam>
        /// <param name="command">Reference to the current command field to replace.</param>
        /// <param name="value">The new command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">
        /// Callback subscribed to the new command's <see cref="IRelayCommand.CanExecuteChanged"/> and invoked immediately after binding.
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
