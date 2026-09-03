using System;
using UnityEngine.UI;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Helpers for command binders: swapping the bound command while keeping the
    /// <see cref="IRelayCommand.CanExecuteChanged"/> subscription, and reflecting <c>CanExecute</c> on a <see cref="Selectable"/>.
    /// </summary>
    public static class CommandBinderExtensions
    {
        /// <summary>
        /// Reflects <paramref name="isInteractable"/> on <paramref name="target"/> according to <paramref name="mode"/>.
        /// </summary>
        /// <remarks>
        /// A missing reference is logged, not thrown: this runs from <see cref="IRelayCommand.CanExecuteChanged"/>,
        /// and an exception would cut the notification short for other subscribers.
        /// </remarks>
        /// <param name="target">The <see cref="Selectable"/> the command binder operates on.</param>
        /// <param name="mode">What <paramref name="isInteractable"/> is applied to.</param>
        /// <param name="isInteractable">The command's current <see cref="IRelayCommand.CanExecute()"/> result.</param>
        /// <param name="customView">The handler used by <see cref="InteractableMode.Custom"/>. Ignored by other modes.</param>
        /// <param name="owner">The binder applying the value; names the source in diagnostics.</param>
        public static void SetInteractable(
            this Selectable target,
            InteractableMode mode,
            bool isInteractable,
            ICanExecuteHandler customView,
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
                            problem: $"the mode is {nameof(InteractableMode)}.{nameof(InteractableMode.Custom)} but no {nameof(ICanExecuteHandler)} is assigned",
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
        /// Replaces <paramref name="command"/> with <paramref name="value"/>, moving the <see cref="IRelayCommand.CanExecuteChanged"/>
        /// subscription and invoking <paramref name="onCanExecuteChanged"/> once for the new command. No-op when both are the same instance.
        /// </summary>
        /// <param name="command">The field holding the current command.</param>
        /// <param name="value">The command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">The handler to subscribe, or <see langword="null"/> to skip subscription.</param>
        public static void UpdateCommand(
            ref IRelayCommand command,
            IRelayCommand value,
            in Action<IRelayCommand> onCanExecuteChanged = null)
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
        /// Replaces <paramref name="command"/> with <paramref name="value"/>, moving the <see cref="IRelayCommand.CanExecuteChanged"/>
        /// subscription and invoking <paramref name="onCanExecuteChanged"/> once for the new command. No-op when both are the same instance.
        /// </summary>
        /// <typeparam name="T">The type of the command parameter.</typeparam>
        /// <param name="command">The field holding the current command.</param>
        /// <param name="value">The command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">The handler to subscribe, or <see langword="null"/> to skip subscription.</param>
        public static void UpdateCommand<T>(
            ref IRelayCommand<T> command,
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
        /// Replaces <paramref name="command"/> with <paramref name="value"/>, moving the <see cref="IRelayCommand.CanExecuteChanged"/>
        /// subscription and invoking <paramref name="onCanExecuteChanged"/> once for the new command. No-op when both are the same instance.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <param name="command">The field holding the current command.</param>
        /// <param name="value">The command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">The handler to subscribe, or <see langword="null"/> to skip subscription.</param>
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
        /// Replaces <paramref name="command"/> with <paramref name="value"/>, moving the <see cref="IRelayCommand.CanExecuteChanged"/>
        /// subscription and invoking <paramref name="onCanExecuteChanged"/> once for the new command. No-op when both are the same instance.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <typeparam name="T3">The type of the third command parameter.</typeparam>
        /// <param name="command">The field holding the current command.</param>
        /// <param name="value">The command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">The handler to subscribe, or <see langword="null"/> to skip subscription.</param>
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
        /// Replaces <paramref name="command"/> with <paramref name="value"/>, moving the <see cref="IRelayCommand.CanExecuteChanged"/>
        /// subscription and invoking <paramref name="onCanExecuteChanged"/> once for the new command. No-op when both are the same instance.
        /// </summary>
        /// <typeparam name="T1">The type of the first command parameter.</typeparam>
        /// <typeparam name="T2">The type of the second command parameter.</typeparam>
        /// <typeparam name="T3">The type of the third command parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth command parameter.</typeparam>
        /// <param name="command">The field holding the current command.</param>
        /// <param name="value">The command to bind, or <see langword="null"/> to unbind.</param>
        /// <param name="onCanExecuteChanged">The handler to subscribe, or <see langword="null"/> to skip subscription.</param>
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
