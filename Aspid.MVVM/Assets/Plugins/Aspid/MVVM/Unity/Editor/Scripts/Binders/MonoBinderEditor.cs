#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Refactor
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : Editor
    {
        public MonoBinder TargetAsMonoBinder => target as MonoBinder;

        public bool HasBinderId => !string.IsNullOrWhiteSpace(IdProperty?.stringValue);
        
        #region Serialized Properties
        public SerializedProperty IdProperty { get; private set; }
        
        public SerializedProperty ViewProperty { get; private set; }
        
        public SerializedProperty ModeProperty { get; private set; }
        
        public SerializedProperty LogsProperty { get; private set; }
        
        public SerializedProperty IsDebugProperty { get; private set; }
        #endregion
        
        protected MonoBinderVisualElement Root { get; private set; }

        private string LastId;
        private MonoView LastView;
        
        #region Enable
        private void OnEnable()
        {
            OnEnabling();
            {
                FindProperties();
                Validate();
                
                RefreshViewEditorIfNeeded();
                EditorApplication.update += Update;
            }
            OnEnabled();
        }
        
        protected virtual void OnEnabling() { }
        
        protected virtual void OnEnabled() { }
        #endregion

        #region Disable
        private void OnDisable()
        {
            OnDisabling();
            {
                EditorApplication.update -= Update;

                if (TargetAsMonoBinder && IdProperty is not null && ViewProperty is not null)
                {
                    var view = ViewProperty.objectReferenceValue;

                    if (view && !string.IsNullOrWhiteSpace(IdProperty.stringValue))
                    {
                        // TODO Aspid.MVVM – Delete MonoView
                        if (view is not MonoView monoView) throw new NullReferenceException(nameof(monoView));
                        ViewAndMonoBinderSyncValidator.ValidateView(monoView);
                    }
                }
            }
            OnDisabled();
        }
        
        protected virtual void OnDisabling() { }
        
        protected virtual void OnDisabled() { }
        #endregion
        
        protected virtual void Update() => Root?.Update();

        protected virtual void FindProperties()
        {
            IdProperty = serializedObject.FindProperty("__id");
            ViewProperty = serializedObject.FindProperty("__view");
            ModeProperty = serializedObject.FindProperty("_mode");
             
            LogsProperty = serializedObject.FindProperty("_log");
            IsDebugProperty = serializedObject.FindProperty("_isDebug");
        }

        #region CreateInspectorGUI
        public sealed override VisualElement CreateInspectorGUI()
        {
            var monoBinderVisualElement = BuildVisualElement();
            monoBinderVisualElement.Initialize();

            Root = monoBinderVisualElement;
            OnCreatedInspectorGUI(monoBinderVisualElement);
            
            return monoBinderVisualElement;
        }
        
        protected virtual MonoBinderVisualElement BuildVisualElement() => new(this);

        protected virtual void OnCreatedInspectorGUI(MonoBinderVisualElement root)
        {
            var idDropdown = Root.IdDropdown;
            var viewDropdown = Root.ViewDropdown;
            
            root.RegisterCallback<SerializedPropertyChangeEvent>(e =>
            {
                if (e.changedProperty.propertyPath != IdProperty.propertyPath 
                    && e.changedProperty.propertyPath != ViewProperty.propertyPath) return;
                
                var dropdownDataId = DropdownData.CreateIdDropdownData(this);
                var dropdownDataView = DropdownData.CreateViewDropdownData(this);
                
                root.IdDropdown.choices = dropdownDataId.Choices;
                root.ViewDropdown.choices = dropdownDataView.Choices;

                var id = IdProperty.stringValue;
                var view = ViewProperty.objectReferenceValue;

                if (id != LastId || view != LastView)
                {
                    if (LastView && !string.IsNullOrWhiteSpace(LastId))
                        ViewAndMonoBinderSyncValidator.RemoveBinderIfExistInView(TargetAsMonoBinder, LastView, LastId);
                
                    if (view && !string.IsNullOrWhiteSpace(id))
                        ViewAndMonoBinderSyncValidator.SetBinderIfNotExistInView(TargetAsMonoBinder);
                }
                
                root.IdDropdown.SetValueWithoutNotify(dropdownDataId.Choices[dropdownDataId.Index]);
                root.ViewDropdown.SetValueWithoutNotify(dropdownDataView.Choices[dropdownDataView.Index]);

                LastId = IdProperty.stringValue;
                LastView = ViewProperty.objectReferenceValue as MonoView;
                
                root.Update();
            });
            
            idDropdown.RegisterValueChangedCallback(value =>
            {
                SaveId(value.newValue);
                root.Update();
            });
            
            viewDropdown.RegisterValueChangedCallback(value =>
            {
                SaveView(value.newValue);
                    
                IdProperty.stringValue = string.Empty;
                var data = DropdownData.CreateIdDropdownData(this);
                idDropdown.choices = data.Choices;
                idDropdown.index = data.Index;
        
                SaveId(idDropdown.value);
                root.Update();
            });
        }
        #endregion

        private void Validate()
        {
            serializedObject.Update();
            {
                ValidateView();
                ValidateId();
            }
            serializedObject.ApplyModifiedProperties();
            return;

            void ValidateView()
            {
                if (!ViewProperty.objectReferenceValue) return;
		        
                for (var parent = ((Component)target).transform; parent is not null; parent = parent.parent)
                {
                    if (parent.GetComponents<IView>().Any(view => ViewProperty.objectReferenceValue == view as Object)) return;
                }

                ViewProperty.objectReferenceValue = null;
            }

            void ValidateId()
            {
                if (string.IsNullOrWhiteSpace(IdProperty.stringValue)) return;

                // TODO Aspid.MVVM – Fix
                var componentView = ViewProperty.objectReferenceValue;
                if (componentView && componentView is IView view && view.TryGetRequireBinderFieldsById(IdProperty.stringValue, out var field))
                {
                    if (field.FieldType.IsArray)
                    {
                        foreach (var item in (IEnumerable)field.GetValue(field.FieldContainerObj))
                        {
                            if ((MonoBinder)item == TargetAsMonoBinder)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if ((MonoBinder)field.GetValue(field.FieldContainerObj) == TargetAsMonoBinder) return;
                    }
                }
                
                IdProperty.stringValue = null;
            }
        }
        
        #region Save Methods
        private void SaveId(string id)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                IdProperty.stringValue = id == "No Id" ? string.Empty : id;
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void SaveView(string viewName)
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                ViewProperty.objectReferenceValue = null;
                var views = BinderEditorUtilities.GetViews(TargetAsMonoBinder)
                    .Where(view => view.name == viewName);
                
                foreach (var view in views)
                {
                    ViewProperty.objectReferenceValue = view.view as Object;
                    break;
                }
            }
            serializedObject.ApplyModifiedProperties();

            RefreshViewEditorIfNeeded();
        }
        #endregion
        
        private void RefreshViewEditorIfNeeded()
        {
            if (ViewProperty?.objectReferenceValue is not MonoView monoView) return;
            
            // Create a temporary editor for MonoView to trigger its OnEnable,
            // which will update BindersList based on DesignViewModel
            var viewEditor = CreateEditor(monoView);
            if (viewEditor) DestroyImmediate(viewEditor);
        }
    }
}
#endif