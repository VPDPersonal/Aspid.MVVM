using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static class ListViewExtensions
    {
        public static T SetBindItem<T>(this T element, Action<VisualElement, int> bindItem)
            where T : ListView
        {
            element.bindItem = bindItem;
            return element;
        }

        #region Make
        public static T SetMakeItem<T>(this T element, Func<VisualElement> makeItem)
            where T : ListView
        {
            element.makeItem = makeItem;
            return element;
        }
        
        public static T SetMakeFooter<T>(this T element, Func<VisualElement> makeFooter)
            where T : ListView
        {
            element.makeFooter = makeFooter;
            return element;
        }
        
        public static T SetMakeHeader<T>(this T element, Func<VisualElement> makeHeader)
            where T : ListView
        {
            element.makeHeader = makeHeader;
            return element;
        }
        
        public static T SetMakeNoneElement<T>(this T element, Func<VisualElement> makeNoneElement)
            where T : ListView
        {
            element.makeNoneElement = makeNoneElement;
            return element;
        }
        #endregion
    }
}