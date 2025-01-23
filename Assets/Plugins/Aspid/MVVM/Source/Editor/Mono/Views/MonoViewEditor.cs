using UnityEditor;
using UnityEngine;
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

        protected StyleEnum<DisplayStyle> OtherBindersDisplay
        {
            get => Root.Q<VisualElement>("OtherBinders").style.display;
            set => Root.Q<VisualElement>("OtherBinders").style.display = value;
        }
        
        protected override void OnEnable()
        {
            ViewUtility.ValidateView(View);
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
                .AddChild(
                    new IMGUIContainer(DrawOtherBinders)
                    .SetName("OtherBindersContainer"))
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

            UpdateOtherBindersDisplay();
            Root.Q<VisualElement>("Header").Q<Image>().SetImageFromResource(IconPath);
        }

        private IReadOnlyList<IMonoBinderValidable> GetOtherBinders()
        {
            var otherBinders = new List<IMonoBinderValidable>();

            foreach (var binder in View.GetComponentsInChildren<IMonoBinderValidable>(true))
            {
                var view = binder.View;

                if (view is null && !string.IsNullOrEmpty(binder.Id))
                    binder.Id = null;
                
                if (!string.IsNullOrEmpty(binder.Id) && view is not null && !view.TryGetMonoBinderValidableFieldById(binder.Id, out _)) 
                    binder.Id = null;
                
                if (string.IsNullOrEmpty(binder.Id)) 
                    otherBinders.Add(binder);
            }

            return otherBinders;
        }
        
        protected override int OnDrewBaseInspector()
        {
            if (OtherBindersDisplay == DisplayStyle.None)
                UpdateOtherBindersDisplay();

            return 0;
        }

        protected bool UpdateOtherBindersDisplay()
        {
            var binders = GetOtherBinders();
            
            var isShow = binders.Count > 0;
            OtherBindersDisplay = isShow ? DisplayStyle.Flex : DisplayStyle.None;
            
            return isShow;
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