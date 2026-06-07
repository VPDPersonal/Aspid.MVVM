using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors
{
    public static partial class VisualElementExtensions
    {
        /// <summary>
        /// Registers a double-click handler on <paramref name="element"/> that opens the script associated with <paramref name="obj"/> in the IDE.
        /// Supports <see cref="MonoBehaviour"/> and <see cref="ScriptableObject"/> instances. Has no effect if no script can be resolved.
        /// </summary>
        /// <param name="element">The element to register the double-click command on.</param>
        /// <param name="obj">The Unity object whose script should be opened on double-click.</param>
        /// <returns><paramref name="element"/> for method chaining.</returns>
        public static T AddOpenScriptCommand<T>(this T element, Object obj)
            where T : VisualElement
        {
            var script = obj switch
            {
                MonoBehaviour mono => MonoScript.FromMonoBehaviour(mono),
                ScriptableObject scriptable => MonoScript.FromScriptableObject(scriptable),
                _ => null
            };

            if (!script) return element;

            var doubleClick = new DoubleClickTracker();
            element.RegisterCallback<MouseUpEvent>(_ =>
            {
                if (doubleClick.Detect()) AssetDatabase.OpenAsset(script);
            });

            return element;
        }
    }
}
