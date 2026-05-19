#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
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
    internal sealed class MonoViewVisualElement : MonoViewVisualElement<MonoView, MonoViewEditor>
    {
        public MonoViewVisualElement(MonoViewEditor editor) :
            base(editor) { }
    }
    
    internal abstract class MonoViewVisualElement<TView, TEditor> : ViewVisualElement<TView, TEditor>
        where TView : MonoView
        where TEditor : MonoViewEditor<TView, TEditor>
    {
        private const string GeneralBindersId = "general-binders";
        private const string HeaderFoldoutViewDataKeyPrefix = "AspidMonoView.Header";
        
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
        
        protected override StatusStyle.Type Status => Editor.UnassignedBinders.Any()
            ? StatusStyle.Type.Warning
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
            var container = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Dark))
                .SetMargin(top: 5);
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
                .SetMargin(top: 5);

            return container
                .AddChild(BuildGeneralBinders());
        }

        private VisualElement BuildGeneralBinders()
        {
            var rootContainer = new VisualElement()
                .SetName(GeneralBindersId);

            var currentGroupCount = 0;
            string? currentGroup = null;
            IntegerField? currentCounter = null;
            var target = rootContainer;
            var metaById = Editor.MetaById;
            var viewTypeName = Editor.TargetAsView ? Editor.TargetAsView.GetType().FullName : "Unknown";

            for (var i = 0; i < Editor.BindersList.ArraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);
                var capturedAssemblyQualifiedName = element.AssemblyQualifiedName;

                metaById.TryGetValue(element.Id, out var meta);

                if (meta?.IsCommand is true)
                {
                    if (currentGroup is not null)
                    {
                        FinalizeFoldoutCount();
                        target = rootContainer;
                        currentGroup = null;
                        currentCounter = null;
                        currentGroupCount = 0;
                    }
                }
                else
                {
                    var header = string.IsNullOrWhiteSpace(meta?.Header) ? null : meta!.Header;
                    if (header is not null && header != currentGroup)
                    {
                        FinalizeFoldoutCount();

                        var (foldout, counter) = BuildHeaderFoldout(header, viewTypeName);
                        rootContainer.Add(foldout);
                        target = foldout.contentContainer;
                        currentGroup = header;
                        currentCounter = counter;
                        currentGroupCount = 0;
                    }
                }

                var property = new MonoBinderPropertyField(
                        element.MonoBindersProperty,
                        label: ObjectNames.NicifyVariableName(element.Id), 
                        binderId: element.Id,
                        assemblyQualifiedName: capturedAssemblyQualifiedName)
                    .SetMargin(top: 3);

                target.Add(property);

                if (currentCounter is not null)
                    currentGroupCount++;
            }

            FinalizeFoldoutCount();
            return rootContainer;

            void FinalizeFoldoutCount() =>
                currentCounter?.SetValueWithoutNotify(currentGroupCount);
        }

        private static (Foldout foldout, IntegerField counter) BuildHeaderFoldout(string header, string viewTypeName)
        {
            var foldout = new Foldout()
            .SetValue(true)
            .SetText(header)
            .SetPadding(left: 0)
            .SetMargin(top: 3, right: 0)
            .SetViewDataKey($"{HeaderFoldoutViewDataKeyPrefix}.{viewTypeName}.{header}");
            
            foldout.contentContainer
                .SetMargin(left: 0)
                .SetPadding(left: 10)
                .AddChild(new AspidDividingLine()
                    .SetPosition(Position.Absolute)
                    .SetDistance(top: 3, bottom: 0, left: 3)
                    .SetDirection(AspidDividingLineDirectionStyle.Type.Vertical));

            var counter = new IntegerField()
                .SetMargin(0)
                .SetWidth(50)
                .SetMinWidth(50)
                .SetMaxWidth(50)
                .SetEnabledSelf(false)
                .SetPickingMode(PickingMode.Ignore);
            
            counter.Q<TextElement>().SetMarginLeft(0).SetPaddingLeft(2);
            counter.RegisterCallback<AttachToPanelEvent>(_ =>
            {
                IgnorePickingRecursive(counter);
            });

            foldout.Q<Toggle>()
                .SetMargin(0)
                .SetBorderRadius(5)
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row)
                .SetPadding(top: 2, right: 5, bottom: 2, left: 3)
                .SetBackgroundColor(new Color(0x38 / 255f, 0x38 / 255f, 0x38 / 255f))
                .AddChild(counter);

            return (foldout, counter);
        }

        private static void IgnorePickingRecursive(VisualElement element)
        {
            element.pickingMode = PickingMode.Ignore;
            foreach (var child in element.Children())
                IgnorePickingRecursive(child);
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
                    ? new Color(r: 0.61f, g: 0.44f, b: 0.11f)
                    : new Color(r: 0.04f, g: 0.27f, b: 0.17f));
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