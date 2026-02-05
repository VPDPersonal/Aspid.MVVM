#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System;
using System.Linq;
using UnityEditor;
using Aspid.Internal;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class MonoViewVisualElement : MonoViewVisualElement<MonoView, MonoViewEditor>
    {
        public MonoViewVisualElement(MonoViewEditor editor) :
            base(editor) { }
    }
    
    public abstract class MonoViewVisualElement<TView, TEditor> : ViewVisualElement<TView, TEditor>
        where TView : MonoView
        where TEditor : MonoViewEditor<TView, TEditor>
    {
        private const string GeneralBindersId = "general-binders";
        
        private UnassignedBindersVisualElement<TView, TEditor>? _unassignedBindersVisualElement;
        
        protected override IEnumerable<string> PropertiesExcluding
        {
            get
            {
                foreach (var property in base.PropertiesExcluding)
                    yield return property;
                
                yield return Editor.BindersList.Property.propertyPath;
                yield return Editor.DesignViewModel.propertyPath;
            }
        }
        
        protected override string IconPath => Editor.UnassignedBinders.Any()
            ? EditorConstants.AspidIconYellow
            : base.IconPath;
        
        public MonoViewVisualElement(TEditor editor)
            : base(editor) { }

        #region Update
        public void UpdateGeneralBinders()
        {
            var generalBinders = this.Q<VisualElement>(GeneralBindersId);
            if (generalBinders is null) return;
            
            var generalBindersParent = generalBinders.parent;
            generalBindersParent.Remove(this.Q<VisualElement>(GeneralBindersId));
            generalBindersParent.AddChild(BuildGeneralBinders());

            generalBindersParent.SetDisplay(this.Q<VisualElement>(GeneralBindersId).childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);
        }
        
        protected override void OnUpdate() =>
            _unassignedBindersVisualElement!.Update();
        #endregion
        
        protected override VisualElement? OnBuiltHeader()
        {
            var type = Editor.TargetAsView.GetType();
            
            if (type == typeof(MonoView)
                || type.IsDefined(typeof(ShowDesignViewModelAttribute), false))
            {
                return new VisualElement()
                    .AddChild(BuildDesignViewModel())
                    .AddChild(BuiltGeneralBinders());
            }

            return null;
        }
        
        protected override VisualElement OnBuiltBaseInspector() =>
            BuildUnassignedBinders();
        
        private VisualElement BuildDesignViewModel()
        {
            var container = new AspidContainer(AspidContainer.StyleType.Dark);
            container.AddChild(new AspidTitle("Design ViewModel"));
            
            var propertyField = new PropertyField(Editor.DesignViewModel, label: string.Empty);
            
            return container.AddChild(propertyField);
        }
        
        private VisualElement BuiltGeneralBinders()
        {
            var container = new AspidContainer();
            
            return container
                .AddChild(BuildGeneralBinders());
        }

        private VisualElement BuildGeneralBinders()
        {
            var container = new VisualElement().SetName(GeneralBindersId);
            
            for (var i = 0; i < Editor.BindersList.ArraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);

                var property = new AspidPropertyField(element.MonoBindersProperty, label: ObjectNames.NicifyVariableName(element.Id))
                    .SetMargin(top: 3);
                
                container.AddChild(property);
            }

            return container;
        }

        private UnassignedBindersVisualElement<TView, TEditor> BuildUnassignedBinders()
        {
            _unassignedBindersVisualElement = new UnassignedBindersVisualElement<TView, TEditor>(Editor);
            return _unassignedBindersVisualElement;
        }
        
        protected override string GetScriptName()
        {
            var view = Editor.TargetAsView;
            if (!view) return string.Empty;
            
            if (!string.IsNullOrWhiteSpace(Editor.DesignViewModel.stringValue))
            {
                var viewModelType = Type.GetType(Editor.DesignViewModel.stringValue);
                if (viewModelType is not null) return $"{view.GetScriptNameWithIndex()} ({viewModelType.Name})";
            }

            return view.GetScriptNameWithIndex();
        }
    }
}
#endif