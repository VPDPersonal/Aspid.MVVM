using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Manipulators
{
    public static class ManipulatorExtensions
    {
        /// <summary>
        /// Adds an <see cref="IManipulator"/> to the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="manipulator">The manipulator to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddManipulatorSelf<T>(this T element, IManipulator manipulator)
            where T : VisualElement
        {
            element.AddManipulator(manipulator);
            return element;
        }

        /// <summary>
        /// Removes an <see cref="IManipulator"/> from the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="manipulator">The manipulator to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveManipulatorSelf<T>(this T element, IManipulator manipulator)
            where T : VisualElement
        {
            element.RemoveManipulator(manipulator);
            return element;
        }

        /// <summary>
        /// Adds a new <see cref="Clickable"/> manipulator that invokes the specified handler.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="handler">The action to invoke when the element is clicked.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClickable<T>(this T element, Action handler)
            where T : VisualElement
        {
            return element.AddManipulatorSelf(new Clickable(handler));
        }

        /// <summary>
        /// Adds a new <see cref="Clickable"/> manipulator that invokes the specified handler and outputs the created manipulator.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="handler">The action to invoke when the element is clicked.</param>
        /// <param name="manipulator">The created <see cref="Clickable"/> manipulator.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClickable<T>(this T element, Action handler, out Clickable manipulator)
            where T : VisualElement
        {
            manipulator = new Clickable(handler);
            return element.AddManipulatorSelf(manipulator);
        }

        /// <summary>
        /// Adds a new <see cref="Clickable"/> manipulator that invokes the specified handler with the triggering <see cref="EventBase"/>.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="handler">The action to invoke when the element is clicked, receiving the triggering event.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClickable<T>(this T element, Action<EventBase> handler)
            where T : VisualElement
        {
            return element.AddManipulatorSelf(new Clickable(handler));
        }

        /// <summary>
        /// Adds a new <see cref="Clickable"/> manipulator that invokes the specified handler with the triggering <see cref="EventBase"/> and outputs the created manipulator.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="handler">The action to invoke when the element is clicked, receiving the triggering event.</param>
        /// <param name="manipulator">The created <see cref="Clickable"/> manipulator.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClickable<T>(this T element, Action<EventBase> handler, out Clickable manipulator)
            where T : VisualElement
        {
            manipulator = new Clickable(handler);
            return element.AddManipulatorSelf(manipulator);
        }

        /// <summary>
        /// Adds a new repeating <see cref="Clickable"/> manipulator that invokes the specified handler after an initial delay and then at a fixed interval while pressed.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="handler">The action to invoke on each click tick.</param>
        /// <param name="delay">The delay, in milliseconds, before the first repeated invocation.</param>
        /// <param name="interval">The interval, in milliseconds, between subsequent invocations while pressed.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClickable<T>(this T element, Action handler, long delay, long interval)
            where T : VisualElement
        {
            return element.AddManipulatorSelf(new Clickable(handler, delay, interval));
        }

        /// <summary>
        /// Adds a new repeating <see cref="Clickable"/> manipulator that invokes the specified handler after an initial delay and then at a fixed interval while pressed, and outputs the created manipulator.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="handler">The action to invoke on each click tick.</param>
        /// <param name="delay">The delay, in milliseconds, before the first repeated invocation.</param>
        /// <param name="interval">The interval, in milliseconds, between subsequent invocations while pressed.</param>
        /// <param name="manipulator">The created <see cref="Clickable"/> manipulator.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClickable<T>(this T element, Action handler, long delay, long interval, out Clickable manipulator)
            where T : VisualElement
        {
            manipulator = new Clickable(handler, delay, interval);
            return element.AddManipulatorSelf(manipulator);
        }

        /// <summary>
        /// Adds a new <see cref="KeyboardNavigationManipulator"/> that invokes the specified action for keyboard navigation operations.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="action">The action to invoke with the <see cref="KeyboardNavigationOperation"/> and triggering <see cref="EventBase"/>.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddKeyboardNavigationManipulator<T>(this T element, Action<KeyboardNavigationOperation, EventBase> action)
            where T : VisualElement
        {
            return element.AddManipulatorSelf(new KeyboardNavigationManipulator(action));
        }

        /// <summary>
        /// Adds a new <see cref="KeyboardNavigationManipulator"/> that invokes the specified action for keyboard navigation operations, and outputs the created manipulator.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="action">The action to invoke with the <see cref="KeyboardNavigationOperation"/> and triggering <see cref="EventBase"/>.</param>
        /// <param name="manipulator">The created <see cref="KeyboardNavigationManipulator"/>.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddKeyboardNavigationManipulator<T>(this T element, Action<KeyboardNavigationOperation, EventBase> action, out KeyboardNavigationManipulator manipulator)
            where T : VisualElement
        {
            manipulator = new KeyboardNavigationManipulator(action);
            return element.AddManipulatorSelf(manipulator);
        }

        /// <summary>
        /// Adds a new <see cref="ContextualMenuManipulator"/> that uses the specified menu builder to populate the contextual menu.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="menuBuilder">The action invoked to populate the menu when it is shown.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddContextualMenuManipulator<T>(this T element, Action<ContextualMenuPopulateEvent> menuBuilder)
            where T : VisualElement
        {
            return element.AddManipulatorSelf(new ContextualMenuManipulator(menuBuilder));
        }

        /// <summary>
        /// Adds a new <see cref="ContextualMenuManipulator"/> that uses the specified menu builder to populate the contextual menu, and outputs the created manipulator.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="menuBuilder">The action invoked to populate the menu when it is shown.</param>
        /// <param name="manipulator">The created <see cref="ContextualMenuManipulator"/>.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddContextualMenuManipulator<T>(this T element, Action<ContextualMenuPopulateEvent> menuBuilder, out ContextualMenuManipulator manipulator)
            where T : VisualElement
        {
            manipulator = new ContextualMenuManipulator(menuBuilder);
            return element.AddManipulatorSelf(manipulator);
        }
    }
}
