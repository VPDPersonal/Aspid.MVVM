#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine.Localization;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Subscription helpers for <see cref="LocalizedString"/> used by the localization binders.
    /// </summary>
    internal static class TextLocalizationExtensions
    {
        /// <summary>
        /// Applies <paramref name="formatArguments"/> and subscribes <paramref name="updateString"/> to
        /// <see cref="LocalizedString.StringChanged"/>.
        /// </summary>
        /// <param name="stringReference">The localized string to subscribe to.</param>
        /// <param name="formatArguments">The format arguments; an empty list leaves the current ones untouched.</param>
        /// <param name="updateString">The handler called whenever the localized string changes.</param>
        internal static void Subscribe(
            this LocalizedString stringReference,
            List<Object> formatArguments,
            LocalizedString.ChangeHandler updateString)
        {
            // ReSharper disable once CoVariantArrayConversion
            if (formatArguments.Count > 0) stringReference.Arguments = formatArguments.ToArray();
            stringReference.StringChanged += updateString;
        }

        /// <summary>
        /// Unsubscribes <paramref name="updateString"/> from <see cref="LocalizedString.StringChanged"/>.
        /// </summary>
        /// <param name="stringReference">The localized string to unsubscribe from.</param>
        /// <param name="updateString">The handler to detach.</param>
        internal static void Unsubscribe(
            this LocalizedString stringReference,
            LocalizedString.ChangeHandler updateString) =>
            stringReference.StringChanged -= updateString;
    }
}
#endif
