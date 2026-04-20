#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System;
using System.Linq;
using UnityEditor;
using Aspid.Internal;
using Aspid.MVVM.Validation;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.Editors;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

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
                
                yield return Editor.DesignViewModel.propertyPath;
                yield return Editor.BindersList.Property.propertyPath;
                yield return Editor.DesignViewModelAssemblyQualifiedNameProperty.propertyPath;
            }
        }
        
        protected override StatusStyle Status => Editor.UnassignedBinders.Any()
            ? StatusStyle.Warning
            : base.Status;
        
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
            _unassignedBindersVisualElement?.Invalidate();
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
            var container = new AspidBox(ThemeStyle.Dark)
                .SetMargin(top: 5, left: -10f);
            container.AddChild(new AspidLabel(text: "Design ViewModel").SetMarginBottom(5));
            
            var attribute = Editor.ShowDesignViewModelAttribute;
            var propertyField = new PropertyField(Editor.DesignViewModel, label: string.Empty);

            if (attribute is not null)
            {
                propertyField.RegisterCallback<GeometryChangedEvent>(_ =>
                    propertyField.Q<Button>()?.SetEnabled(!attribute.StrictType));
            }
            
            return container.AddChild(propertyField);
        }
        
        private VisualElement BuiltGeneralBinders()
        {
            var container = new AspidBox()
                .SetMargin(top: 5, left: -10f);

            return container
                .AddChild(BuildGeneralBinders());
        }

        private VisualElement BuildGeneralBinders()
        {
            var container = new VisualElement().SetName(GeneralBindersId);

            for (var i = 0; i < Editor.BindersList.ArraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);
                var capturedAssemblyQualifiedName = element.AssemblyQualifiedName;

                var property = new MonoBinderPropertyField(element.MonoBindersProperty, label: ObjectNames.NicifyVariableName(element.Id), element.Id, capturedAssemblyQualifiedName)
                    .SetMargin(top: 3);
                
                container.Add(property);
            }

            return container;
        }

        private UnassignedBindersVisualElement<TView, TEditor> BuildUnassignedBinders()
        {
            _unassignedBindersVisualElement = new UnassignedBindersVisualElement<TView, TEditor>(Editor, OnUnassignedBinderClicked, CanAutoAssign, OnAutoAssign);
            return _unassignedBindersVisualElement;
        }

        private bool CanAutoAssign(IMonoBinderValidable binder)
        {
            var result = false;
            
            this.Query<MonoBinderPropertyField>().ForEach(field =>
            {
                if (!result && field.IsCompatibleBinderWithField(binder) is CompatibleBinderWithField.TypeAndId)
                    result = true;
            });
            
            return result;
        }

        private void OnAutoAssign(IMonoBinderValidable binder)
        {
            MonoBinderPropertyField? targetField = null;
            
            this.Query<MonoBinderPropertyField>().ForEach(field =>
            {
                if (targetField is null && field.IsCompatibleBinderWithField(binder) == CompatibleBinderWithField.TypeAndId)
                    targetField = field;
            });

            if (targetField is null) return;

            var property = targetField.Property;
            if (property.isArray)
            {
                var startIndex = property.arraySize;
                property.arraySize += 1;
                property.GetArrayElementAtIndex(startIndex).objectReferenceValue = (UnityEngine.Object)binder;
            }
            else
            {
                property.objectReferenceValue = (UnityEngine.Object)binder;
            }
            
            property.ApplyModifiedProperties();
        }

        private void OnUnassignedBinderClicked(IMonoBinderValidable binder)
        {
            this.Query<MonoBinderPropertyField>().ForEach(field =>
            {
                var result = field.IsCompatibleBinderWithField(binder);
                if (result is CompatibleBinderWithField.None) return;
                
                field.AnimateHighlight(result is CompatibleBinderWithField.Type 
                    ? EditorConstants.WarningColor
                    : EditorConstants.SuccessColor);
            });
        }

        protected override string GetScriptName()
        {
            var view = Editor.TargetAsView;
            return view ? view.GetScriptNameWithIndex() : string.Empty;
        }

        protected override string GetScriptSubtext()
        {
            if (string.IsNullOrWhiteSpace(Editor.DesignViewModel.stringValue)) return string.Empty;

            var viewModelType = Type.GetType(Editor.DesignViewModel.stringValue);
            return viewModelType?.Name ?? string.Empty;
        }
    }
}
#endif