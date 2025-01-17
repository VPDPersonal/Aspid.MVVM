using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.CustomEditors;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Aspid.MVVM.Mono
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public class MonoViewEditor : ViewEditor<MonoView>
    {
        protected override ErrorType MessageType => 
            Root?.Q<VisualElement>("OtherBinders").style.display != DisplayStyle.None
                ? ErrorType.Warning 
                : ErrorType.None;
        
        protected override void OnEnable()
        {
            ViewUtility.FindAllMonoBinderValidableInChildren(View);
            base.OnEnable();
        }

        #region Build Methods
        protected override VisualElement Build()
        {
            return new VisualElement()
                .AddChild(BuildHeader())
                .AddChild(BuildBaseInspector())
                .AddChild(BuildOtherBinder())
                .AddChild(BuildViewModel());
        }

        protected VisualElement BuildOtherBinder()
        {
            const string unassignedBindersWarning = "It is recommended not to leave unassigned Binders";
            
            var helpBox = Elements.CreateHelpBox(unassignedBindersWarning, HelpBoxMessageType.Warning)
                .SetHelpBoxFontSize(14);
            
            return Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "Other Binders")
                .AddChild(helpBox)
                .AddChild(new IMGUIContainer(DrawOtherBinders))
                .SetMargin(top: 10)
                .SetName("OtherBinders");;
        }
        #endregion

        private void DrawOtherBinders()
        {
            var binders = GetOtherBinders();

            if (binders.Count > 0)
            {
                EditorGUILayout.Space();
                
                foreach (var binder in binders)
                    EditorGUILayout.ObjectField((Component)binder, binder.GetType(), false);
            }
            
            Root.Q<VisualElement>("OtherBinders").style.display = binders.Count > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            Root.Q<VisualElement>("Header").Q<Image>().SetImageFromResource(IconPath);
        }

        private IReadOnlyList<IMonoBinderValidable> GetOtherBinders()
        {
            var otherBinders = new List<IMonoBinderValidable>();

            foreach (var binder in  View.GetComponentsInChildren<IMonoBinderValidable>(true))
            {
                var view = binder.View;

                if (view is null && !string.IsNullOrEmpty(binder.Id))
                    binder.Id= null;

                if (string.IsNullOrEmpty(binder.Id))
                {
                    otherBinders.Add(binder);
                }
                else if (view is not null)
                {
                    var fields = view.GetType().GetFieldInfosIncludingBaseClasses(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var isExist = fields.Select(field => ViewUtility.GetIdName(field.Name)).Any(idName => idName == binder.Id);

                    if (!isExist)
                    {
                        binder.Reset();
                    }
                }
            }

            return otherBinders;
        }

        protected override string GetScriptName()
        {
	        if (!View) return null;
	        
	        var type = View.GetType();
	        var views = View.GetComponents(type);
	        
	        switch (views.Length)
	        {
		        case 0: return null;
		        case 1: return views[0].GetScriptName();
		        default:
			        {
				        var index = 0;
	        
				        foreach (var component in views)
				        {
					        if (component.GetType() != type) continue;
		        
					        index++;
					        if (component == View) return $"{View.GetScriptName()} ({index})";
				        }
				        
				        return null;
			        }
	        }
        }
    }
}