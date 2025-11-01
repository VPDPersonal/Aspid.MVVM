using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class AspidInspectorHeader : VisualElement
    {
        private const string StyleSheetPath = "Editor/Styles/aspid-mvvm-inspector-header";
        
        public readonly Image Icon;
        public readonly Label Label;

        public AspidInspectorHeader(Object obj, string iconPath)
            : this(obj.GetScriptName(), obj, iconPath) { }
        
        public AspidInspectorHeader(string label, Object obj, string iconPath)
        {
            var container = new AspidContainer(AspidContainer.StyleType.Dark)
                .SetMargin(top: 2);
            container.styleSheets.Add(Resources.Load<StyleSheet>(StyleSheetPath));
            
            Icon = new Image()
                .AddOpenScriptCommand(obj)
                .SetImageFromResource(iconPath);

            Label = new Label(label);
            
            Add(container
                .AddChild(Icon)
                .AddChild(Label));
        }
    }
}