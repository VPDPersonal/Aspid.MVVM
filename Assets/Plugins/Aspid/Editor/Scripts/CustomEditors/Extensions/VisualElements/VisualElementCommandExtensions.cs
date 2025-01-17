using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.CustomEditors.Extensions.VisualElements
{
    public static class VisualElementCommandExtensions
    {
        public static void AddOpenScriptCommand<T>(this T element, Object obj)
            where T : VisualElement
        {
            var script = obj switch
            {
                MonoBehaviour mono => MonoScript.FromMonoBehaviour(mono),
                ScriptableObject scriptable => MonoScript.FromScriptableObject(scriptable),
                _ => null
            };

            if (!script) return;
            
            var lastClickTime = 0f;
            const float doubleClickTime = 0.3f;
                
            element.RegisterCallback<MouseUpEvent>(ev =>
            {
                var currentTime = (float)EditorApplication.timeSinceStartup;

                if (currentTime - lastClickTime < doubleClickTime)
                    AssetDatabase.OpenAsset(script);
                    
                lastClickTime = currentTime;
            });
        }
    }
}