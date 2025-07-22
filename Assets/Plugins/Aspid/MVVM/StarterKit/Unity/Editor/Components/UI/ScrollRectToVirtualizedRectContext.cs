using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    internal class ScrollRectToVirtualizedRectContext
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [UnityEditor.MenuItem("CONTEXT/ScrollRect/Convert to VirtualizedList", priority = 100001)]
        private static void AddContextMenu(UnityEditor.MenuCommand command)
        {
            var gameObject = ((Component)command.context).gameObject;
            var scrollRect = gameObject.GetComponent<ScrollRect>();

            if (scrollRect is VirtualizedList) return;
            
            var content = scrollRect.content;
            var horizontal = scrollRect.horizontal;
            var vertical = scrollRect.vertical;
            var movementType = scrollRect.movementType;
            var elasticity = scrollRect.elasticity;
            var inertia = scrollRect.inertia;
            var decelerationRate = scrollRect.decelerationRate;
            var scrollSensitivity = scrollRect.scrollSensitivity;
            var viewport = scrollRect.viewport;
            var horizontalScrollbar = scrollRect.horizontalScrollbar;
            var horizontalScrollbarVisibility = scrollRect.horizontalScrollbarVisibility;
            var horizontalScrollbarSpacing = scrollRect.horizontalScrollbarSpacing;
            var verticalScrollbar = scrollRect.verticalScrollbar;
            var verticalScrollbarVisibility = scrollRect.verticalScrollbarVisibility;
            var verticalScrollbarSpacing = scrollRect.verticalScrollbarSpacing;
            var onValueChanged = scrollRect.onValueChanged;
            
            Object.DestroyImmediate(scrollRect);
            var virtualizedList = gameObject.AddComponent<VirtualizedList>();
            
            virtualizedList.content = content;
            virtualizedList.horizontal = horizontal;
            virtualizedList.vertical = vertical;
            virtualizedList.movementType = movementType;
            virtualizedList.elasticity = elasticity;
            virtualizedList.inertia = inertia;
            virtualizedList.decelerationRate = decelerationRate;
            virtualizedList.scrollSensitivity = scrollSensitivity;
            virtualizedList.viewport = viewport;
            virtualizedList.horizontalScrollbar = horizontalScrollbar;
            virtualizedList.horizontalScrollbarVisibility = horizontalScrollbarVisibility;
            virtualizedList.horizontalScrollbarSpacing = horizontalScrollbarSpacing;
            virtualizedList.verticalScrollbar = verticalScrollbar;
            virtualizedList.verticalScrollbarVisibility = verticalScrollbarVisibility;
            virtualizedList.verticalScrollbarSpacing = verticalScrollbarSpacing;
            virtualizedList.onValueChanged =onValueChanged;
        }
    }
}