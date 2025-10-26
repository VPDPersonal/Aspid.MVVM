using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools.Editors
{
    public static partial class VisualElementExtensions
    {
        private const float DoubleClickTime = 0.3f;
        
        public static T AddOpenScriptCommand<T>(this T element, Object obj)
            where T : VisualElement
        {
            var script = obj switch
            {
                MonoBehaviour mono => MonoScript.FromMonoBehaviour(mono),
                ScriptableObject scriptable => MonoScript.FromScriptableObject(scriptable),
                _ => null
            };

            if (script)
            {
                var lastClickTime = 0f;
                
                element.RegisterCallback<MouseUpEvent>(_ =>
                {
                    var currentTime = (float)EditorApplication.timeSinceStartup;

                    if (currentTime - lastClickTime < DoubleClickTime)
                        AssetDatabase.OpenAsset(script);
                    
                    lastClickTime = currentTime;
                });
            }

            return element;
        }
    }
}