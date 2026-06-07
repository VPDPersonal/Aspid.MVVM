#if !UNITY_2023_1_OR_NEWER
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class CallbackEventHandlerExtensions
    {
        /// <summary>
        /// Registers a callback for an event type that automatically unregisters itself after the first invocation.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to listen for.</typeparam>
        /// <param name="element">The element to register the callback on.</param>
        /// <param name="callback">The callback to invoke once.</param>
        /// <param name="useTrickleDown">Specifies whether to use trickle-down (capture) phase.</param>
        public static void RegisterCallbackOnce<TEventType>(this CallbackEventHandler element,
            EventCallback<TEventType> callback,
            TrickleDown useTrickleDown = TrickleDown.NoTrickleDown)
            where TEventType : EventBase<TEventType>, new()
        {
            element.RegisterCallback<TEventType>(Once, useTrickleDown);

            void Once(TEventType evt)
            {
                callback?.Invoke(evt);
                element.UnregisterCallback<TEventType>(Once, useTrickleDown);
            }
        }

        /// <summary>
        /// Registers a callback with user arguments for an event type that automatically unregisters itself after the first invocation.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to listen for.</typeparam>
        /// <typeparam name="TUserArgsType">The type of the user-defined arguments passed to the callback.</typeparam>
        /// <param name="element">The element to register the callback on.</param>
        /// <param name="userArgs">The user-defined arguments passed to the callback.</param>
        /// <param name="callback">The callback to invoke once.</param>
        /// <param name="useTrickleDown">Specifies whether to use trickle-down (capture) phase.</param>
        public static void RegisterCallbackOnce<TEventType, TUserArgsType>(this CallbackEventHandler element,
            TUserArgsType userArgs,
            EventCallback<TEventType, TUserArgsType> callback,
            TrickleDown useTrickleDown = TrickleDown.NoTrickleDown)
            where TEventType : EventBase<TEventType>, new()
        {
            element.RegisterCallback<TEventType, TUserArgsType>(Once, userArgs, useTrickleDown);

            void Once(TEventType evt, TUserArgsType args)
            {
                callback?.Invoke(evt, args);
                element.UnregisterCallback<TEventType, TUserArgsType>(Once, useTrickleDown);
            }
        }
    }
}
#endif
