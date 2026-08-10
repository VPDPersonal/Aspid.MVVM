#if (UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION) && ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine.Localization;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="LocalizedString"/> used by localization binders.
    /// </summary>
    internal static class TextLocalizationExtensions
    {
        /// <summary>
        /// Configures format arguments and subscribes to string change notifications.
        /// </summary>
        /// <param name="stringReference">The localized string to subscribe to.</param>
        /// <param name="formatArguments">The objects substituted into the localized string; an empty list leaves its arguments untouched.</param>
        /// <param name="updateString">The handler called whenever the localized string changes.</param>
        internal static void Subscribe(this LocalizedString stringReference, List<Object> formatArguments, LocalizedString.ChangeHandler updateString)
        {
            if (formatArguments.Count > 0)
            {
                // ReSharper disable once CoVariantArrayConversion
                stringReference.Arguments = formatArguments.ToArray();
            }
            
            stringReference.StringChanged += updateString;
        }
        
        /// <summary>
        /// Unsubscribes from string change notifications.
        /// </summary>
        /// <param name="stringReference">The localized string to unsubscribe from.</param>
        /// <param name="updateString">The handler to detach.</param>
        internal static void Unsubscribe(this LocalizedString stringReference, LocalizedString.ChangeHandler updateString) =>
            stringReference.StringChanged -= updateString;
    }
}
#endif