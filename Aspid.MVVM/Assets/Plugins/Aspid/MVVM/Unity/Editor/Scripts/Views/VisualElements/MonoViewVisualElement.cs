#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System;
using System.Linq;
using UnityEngine;
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
        private readonly List<(IVisualElementScheduledItem scheduledItem, VisualElement element)> _activeHighlightAnimations = new();
        
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
            container.AddChild(new AspidTitle(text: "Design ViewModel"));
            
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
            _unassignedBindersVisualElement = new UnassignedBindersVisualElement<TView, TEditor>(Editor, OnUnassignedBinderClicked);
            return _unassignedBindersVisualElement;
        }

        #region Unassigned Binders
        private void OnUnassignedBinderClicked(IMonoBinderValidable binder)
        {
            var generalBinders = this.Q<VisualElement>(name: GeneralBindersId);
            if (generalBinders is null) return;

            StopHighlightAnimations();

            for (var i = 0; i < generalBinders.childCount && i < Editor.BindersList.ArraySize; i++)
            {
                var assemblyQualifiedName = Editor.BindersList.GetArrayElementAtIndex(i).AssemblyQualifiedName;
                if (!IsCompatible(binder, assemblyQualifiedName)) continue;
                
                var element = generalBinders[i];
                _activeHighlightAnimations.Add((AnimateHighlight(element), element));
            }
        }

        private void StopHighlightAnimations()
        {
            foreach (var (scheduledItem, element) in _activeHighlightAnimations)
            {
                scheduledItem.Pause();
                element[0].style.backgroundColor = new StyleColor(StyleKeyword.Null);
            }
            _activeHighlightAnimations.Clear();
        }

        private static IVisualElementScheduledItem AnimateHighlight(VisualElement element)
        {
            const int totalSteps = 50;
            var step = 0;
            var initialColor = element[0].resolvedStyle.backgroundColor;

            IVisualElementScheduledItem? scheduledItem = null;
            scheduledItem = element.schedule.Execute(() =>
            {
                step++;
                if (step >= totalSteps)
                {
                    element[0].style.backgroundColor = new StyleColor(StyleKeyword.Null);
                    scheduledItem?.Pause();
                    return;
                }

                var time = 1f - (float)step / totalSteps;
                element[0].style.backgroundColor = Color.Lerp(initialColor, new Color(0.2f, 0.8f, 0.2f, 0.2f), time);
            }).Every(15);

            return scheduledItem!;
        }
        
        private static bool IsCompatible(IMonoBinderValidable binder, string? assemblyQualifiedName)
        {
            var binderType = ((Component)binder).GetType();

            if (typeof(IAnyBinder).IsAssignableFrom(binderType)) return true;
            if (string.IsNullOrEmpty(assemblyQualifiedName)) return false;

            var propertyType = Type.GetType(assemblyQualifiedName);
            if (propertyType is null) return false;

            return binderType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBinder<>))
                .Select(i => i.GetGenericArguments()[0])
                .Any(binderTypeArg =>
                    binderTypeArg.IsAssignableFrom(propertyType) ||
                    propertyType.IsAssignableFrom(binderTypeArg));
        }
        #endregion
        
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