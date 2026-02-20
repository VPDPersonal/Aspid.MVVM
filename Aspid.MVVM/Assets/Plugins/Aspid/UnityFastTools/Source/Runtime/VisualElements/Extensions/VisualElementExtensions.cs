using UnityEngine.UIElements;
using System.Collections.Generic;

// TODO Aspid.UnityFastTools
// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static partial class VisualElementExtensions
    {
        public static T SetName<T>(this T element, string name)
            where T : VisualElement
        {
            element.name = name;
            return element;
        }
        
        public static T SetVisible<T>(this T element, bool visible)
            where T : VisualElement
        {
            element.visible = visible;
            return element;
        }

        public static T SetTooltip<T>(this T element, string tooltip)
            where T : VisualElement
        {
            element.tooltip = tooltip;
            return element;
        }
        
        public static T AddChild<T>(this T element, VisualElement child)
            where T : VisualElement
        {
            element.Add(child);
            return element;
        }

        public static T AddChildIfNotNull<T>(this T element, VisualElement child)
            where T : VisualElement
        {
            if (child is not null) element.Add(child);
            return element;
        }
        
        public static T AddChildren<T>(this T element, params VisualElement[] children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Add(child);
            
            return element;
        }
        
        public static T AddChildren<T>(this T element, IEnumerable<VisualElement> children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Add(child);
            
            return element;
        }

#if !UNITY_2023_1_OR_NEWER
        public static void RegisterCallbackOnce<TEventType>(this VisualElement element, 
            EventCallback<TEventType> callback,
            TrickleDown useTrickleDown = TrickleDown.NoTrickleDown)
            where TEventType : EventBase<TEventType>, new()
        {
            element.RegisterCallback<TEventType>(Once, useTrickleDown);
            return;

            void Once(TEventType evt)
            {
                callback?.Invoke(evt);
                element.UnregisterCallback<TEventType>(Once, useTrickleDown);
            }
        }
        
        public static void RegisterCallbackOnce<TEventType, TUserArgsType>(this VisualElement element, 
            TUserArgsType userArgs,
            EventCallback<TEventType, TUserArgsType> callback,
            TrickleDown useTrickleDown = TrickleDown.NoTrickleDown)
            where TEventType : EventBase<TEventType>, new()
        {
            element.RegisterCallback<TEventType, TUserArgsType>(Once, userArgs, useTrickleDown);
            return;

            void Once(TEventType evt, TUserArgsType args)
            {
                callback?.Invoke(evt, args);
                element.UnregisterCallback<TEventType, TUserArgsType>(Once, useTrickleDown);
            }
        }
#endif
    }
}